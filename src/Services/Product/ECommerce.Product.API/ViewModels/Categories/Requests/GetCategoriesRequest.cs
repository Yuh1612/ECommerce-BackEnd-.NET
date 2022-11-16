using ECommerce.Products.API.ViewModels.Categories.Responses;
using ECommerce.Products.Domain.Entities;
using ECommerce.Shared.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Products.API.ViewModels.Categories.Requests
{
    public class GetCategoriesRequest : ISelection<Category, CategoryInfoResponse>
    {
        public Expression<Func<Category, CategoryInfoResponse>> GetSelection()
        {
            return _ => new CategoryInfoResponse
            {
                Id = _.Id,
                Name = _.Name,
                Description = _.Description,
                ImageUrl = _.ImageUrl
            };
        }
    }
}