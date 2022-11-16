using Ecommerce.Utilities.EFCore;
using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Shared.Entities.Interfaces;
using ECommerce.Shared.Exceptions;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Infrastructure
{
    public class UnitOfWork<T> : EFCoreUnitOfWork<T>, IUnitOfWork<T> where T : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserInfo _userInfo;

        public UnitOfWork(IDbFactory<T> dbFactory, IServiceProvider serviceProvider, IUserInfo userInfo)
            : base(dbFactory)
        {
            _serviceProvider = serviceProvider;
            _userInfo = userInfo;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = DbContext.ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        OnEntryAdded(entry);
                        break;

                    case EntityState.Modified:
                        OnEntryUpdated(entry);
                        break;

                    case EntityState.Deleted:
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                        break;
                }
            }
            var domainEvents = GetDomainEvents();
            var integrationEvents = GetIntegrationEvents();

            var saved = await base.SaveChangesAsync(cancellationToken);

            if (domainEvents?.Any() == true)
                await PushDomainEventsAsync(domainEvents);

            if (integrationEvents?.Any() == true)
                await PushIntegrationEventsAsync(integrationEvents);

            return saved;
        }

        private Task PushIntegrationEventsAsync(IEnumerable<INotification> integrationEvents)
        {
            throw new NotImplementedException();
        }

        private async Task PushDomainEventsAsync(IEnumerable<INotification> domainEvents)
        {
            var mediator = _serviceProvider.GetRequiredService<IMediator>();
            foreach (var @event in domainEvents)
            {
                await mediator.Publish(@event);
            }
        }

        private IEnumerable<INotification> GetIntegrationEvents()
        {
            var entries = DbContext.ChangeTracker.Entries();
            var events = entries
                .Where(_ =>
                    typeof(IEntityBase).IsAssignableFrom(_.Entity.GetType())
                    && (_.Entity as IEntityBase)?.GetIntegrationEvents()?.Any() == true
                )
                .SelectMany(_ =>
                {
                    var entity = _.Entity as IEntityBase ?? throw new DomainException(MessagesResource.UndefinedEntity);
                    var events = entity.GetIntegrationEvents().ToList();
                    entity.ClearIntegrationEvents();
                    return events;
                })
                .ToList();

            return events;
        }

        private IEnumerable<INotification> GetDomainEvents()
        {
            var entries = DbContext.ChangeTracker.Entries();
            var events = entries
                .Where(_ =>
                    typeof(IEntityBase).IsAssignableFrom(_.Entity.GetType())
                    && (_.Entity as IEntityBase)?.GetDomainEvents()?.Any() == true
                )
                .SelectMany(_ =>
                {
                    var entity = _.Entity as IEntityBase ?? throw new DomainException(MessagesResource.UndefinedEntity);
                    var events = entity.GetDomainEvents().ToList();
                    entity.ClearDomainEvents();
                    return events;
                })
                .ToList();

            return events;
        }

        private void OnEntryUpdated(EntityEntry entry)
        {
            var entity = entry.Entity;
            var entityType = entity.GetType();
            if (typeof(IFullAuditEntity).IsAssignableFrom(entityType))
            {
                Guid updatedBy = _userInfo?.Id ?? default;
                var auditEntity = entity as IFullAuditEntity ?? throw new DomainException(MessagesResource.UndefinedEntity);
                auditEntity.UpdateModifierInfo(updatedBy, DateTime.Now);
            }
        }

        private void OnEntryAdded(EntityEntry entry)
        {
            var entity = entry.Entity;
            var entityType = entity.GetType();

            if (typeof(IAuditEntity).IsAssignableFrom(entityType))
            {
                Guid createdBy = _userInfo?.Id ?? default;
                var auditEntity = entity as IAuditEntity ?? throw new DomainException(MessagesResource.UndefinedEntity);
                if (auditEntity.CreatedBy == default)
                    auditEntity.UpdateAudit(createdBy, auditEntity.CreatedOn);

                if (auditEntity.CreatedOn == default)
                    auditEntity.UpdateAudit(auditEntity.CreatedBy, DateTime.Now);
            }
        }
    }
}