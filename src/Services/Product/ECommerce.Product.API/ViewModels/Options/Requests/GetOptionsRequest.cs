using ECommerce.Products.API.ViewModels.Options.Responses;
using ECommerce.Products.Domain.Entities;
using ECommerce.Shared.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Products.API.ViewModels.Options.Requests
{
    public class GetOptionsRequest : ISelection<Option, OptionInfoResponse>
    {
        public Expression<Func<Option, OptionInfoResponse>> GetSelection()
        {
            return _ => new OptionInfoResponse
            {
                Id = _.Id,
                Name = _.Name
            };
        }
    }
}