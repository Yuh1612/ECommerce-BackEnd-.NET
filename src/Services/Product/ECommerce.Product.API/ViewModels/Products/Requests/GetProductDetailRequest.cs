using ECommerce.Products.API.ViewModels.Brands.Responses;
using ECommerce.Products.API.ViewModels.Categories.Responses;
using ECommerce.Products.API.ViewModels.Options.Responses;
using ECommerce.Products.API.ViewModels.Products.Responses;
using ECommerce.Products.Domain.Entities;
using ECommerce.Shared.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Products.API.ViewModels.Products.Requests
{
    public class GetProductDetailRequest : ISelection<Product, ProductInfoDetailResponse>
    {
        public Expression<Func<Product, ProductInfoDetailResponse>> GetSelection()
        {
            return _ => new ProductInfoDetailResponse
            {
                Id = _.Id,
                Name = _.Name,
                Description = _.Description,
                Code = _.Code,
                Discount = _.Discount,
                Height = _.Height,
                Weight = _.Weight,
                Price = _.Price,
                Slug = _.Slug,
                Length = _.Length,
                Quantity = _.Quantity,
                Brand = new BrandInfoResponse
                {
                    Id = _.Brand.Id,
                    Name = _.Brand.Name,
                    Description = _.Brand.Description
                },
                Shop = new ShopInfo
                {
                    Id = _.Shop.Id,
                    Name = _.Shop.Name
                },
                Categories = _.ProductCategories.Select(pc => pc.Category).Select(c => new CategoryInfoResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToList(),
                Options = _.ProductOptions.GroupBy(_ => _.OptionId).Select(_ => new ProductOptionInfoResponse
                {
                    Id = _.Key,
                    Option = _.First().Option.Name,
                    Name = _.Select(_ => _.Description).ToList()
                }).ToList(),
            };
        }
    }
}