using ECommerce.Products.Domain;
using ECommerce.Products.Domain.Entities;
using ECommerce.Products.Domain.Products.Interfaces;
using ECommerce.Products.RabbitMQ.IntegrationEvents;
using ECommerce.Shared.Interfaces;
using MediatR;

namespace ECommerce.Products.RabbitMQ.IntegrationEventHandlers
{
    public class RegisterShopIntegrationEventHandler : INotificationHandler<RegisterShopEvent>
    {
        private readonly IShopRepository _shopRepo;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterShopIntegrationEventHandler(IShopRepository shopRepo, IUnitOfWork unitOfWork)
        {
            _shopRepo = shopRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RegisterShopEvent notification, CancellationToken cancellationToken)
        {
            var isIdValid = Guid.TryParse(notification.ShopId, out var id);
            if (!isIdValid) throw new InvalidCastException(MessagesResource.InvalidField);

            var shop = new Shop(id, notification.Profile?.Name, notification.UserName);
            await _shopRepo.InsertAsync(shop);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}