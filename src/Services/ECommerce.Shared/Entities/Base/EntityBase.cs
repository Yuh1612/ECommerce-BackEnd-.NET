using ECommerce.Shared.Entities.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.Shared.Entities.Base
{
    public abstract class EntityBase : IEntityBase
    {
        private List<INotification> _domainEvents = new();
        private List<INotification> _integrationEvents = new();

        [NotMapped]
        [JsonIgnore]
        public virtual IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyCollection<INotification> IntegrationEvents => _integrationEvents.AsReadOnly();

        public virtual void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public virtual void AddIntegrationEvent(INotification eventItem)
        {
            _integrationEvents ??= new List<INotification>();
            _integrationEvents.Add(eventItem);
        }

        public virtual void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public virtual void RemoveIntegrationEvent(INotification eventItem)
        {
            _integrationEvents.Remove(eventItem);
        }

        public virtual void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public virtual void ClearIntegrationEvents()
        {
            _integrationEvents.Clear();
        }

        public virtual IReadOnlyCollection<INotification> GetDomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public virtual IReadOnlyCollection<INotification> GetIntegrationEvents()
        {
            return _integrationEvents.AsReadOnly();
        }
    }
}