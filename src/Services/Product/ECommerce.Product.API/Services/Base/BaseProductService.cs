using ECommerce.Products.Infrastructure.DbContexts;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Services;

namespace ECommerce.Products.API.Services.Base
{
    public class BaseProductService : SharedService
    {
        public BaseProductService(IUnitOfWork<ProductContext> unitOfWork, IServiceProvider serviceProvider)
            : base(unitOfWork, serviceProvider)
        {
        }
    }
}