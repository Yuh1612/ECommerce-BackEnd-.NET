using MediatR;

namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IEntityBase
    {
        void AddDomainEvent(INotification eventItem);

        void RemoveDomainEvent(INotification eventItem);

        void ClearDomainEvents();

        IReadOnlyCollection<INotification>? GetDomainEvents();

        void AddIntegrationEvent(INotification eventItem);

        void RemoveIntegrationEvent(INotification eventItem);

        void ClearIntegrationEvents();

        IReadOnlyCollection<INotification>? GetIntegrationEvents();
    }
}