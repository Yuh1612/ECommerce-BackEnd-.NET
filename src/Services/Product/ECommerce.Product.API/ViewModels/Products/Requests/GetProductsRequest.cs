using Castle.Core.Internal;
using ECommerce.Products.API.ViewModels.Products.Responses;
using ECommerce.Products.Domain.Entities;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.ViewModels;
using System.Linq.Expressions;

namespace ECommerce.Products.API.ViewModels.Products.Requests
{
    public class GetProductsRequest : PagingRequest, ISelection<Product, ProductInfoResponse>, IFilter<Product>
    {
        public Guid? shopId { get; set; }
        public string? KeyWord { get; set; }
        public Guid? CategoryId { get; set; }

        public Expression<Func<Product, bool>> GetFilter()
        {
            return _ => (KeyWord.IsNullOrEmpty() || _.Name.Contains(KeyWord!))
                && (!CategoryId.HasValue || _.ProductCategories.Any(_ => _.CategoryId == CategoryId)
                && (!shopId.HasValue || _.ShopId == shopId));
        }

        public Expression<Func<Product, ProductInfoResponse>> GetSelection()
        {
            return _ => new ProductInfoResponse
            {
                Id = _.Id,
                Code = _.Code,
                Name = _.Name,
                Price = _.Price
            };
        }
    }
}