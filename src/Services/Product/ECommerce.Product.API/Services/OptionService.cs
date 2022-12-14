using ECommerce.Products.API.Services.Base;
using ECommerce.Products.API.ViewModels.Brands.Requests;
using ECommerce.Products.API.ViewModels.Brands.Responses;
using ECommerce.Products.API.ViewModels.Options.Requests;
using ECommerce.Products.API.ViewModels.Options.Responses;
using ECommerce.Products.Domain.Entities;
using ECommerce.Products.Domain.Products.Interfaces;
using ECommerce.Products.Infrastructure.DbContexts;
using ECommerce.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Products.API.Services
{
    public class OptionService : BaseProductService
    {
        private readonly IOptionRepository _optionRepository;

        public OptionService(IUnitOfWork<ProductContext> unitOfWork
            , IServiceProvider serviceProvider
            , IOptionRepository optionRepository)
            : base(unitOfWork, serviceProvider)
        {
            _optionRepository = optionRepository;
        }

        public async Task<List<OptionInfoResponse>> GetOptions()
        {
            return await _optionRepository.GetQuery()
                .Select(new GetOptionsRequest().GetSelection())
                .ToListAsync();
        }
    }
}