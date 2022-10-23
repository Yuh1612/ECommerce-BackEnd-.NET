using ECommerce.Products.API.ViewModels.Brands.Responses;
using ECommerce.Products.Domain.Entities;
using ECommerce.Shared.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Products.API.ViewModels.Brands.Requests
{
    public class GetBrandsRequest : ISelection<Brand, BrandInfoResponse>
    {
        public Expression<Func<Brand, BrandInfoResponse>> GetSelection()
        {
            return _ => new BrandInfoResponse
            {
                Id = _.Id,
                Name = _.Name,
                Description = _.Description
            };
        }
    }
}