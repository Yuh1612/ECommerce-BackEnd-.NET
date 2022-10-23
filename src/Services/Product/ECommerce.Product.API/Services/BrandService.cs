using ECommerce.Products.API.Services.Base;
using ECommerce.Products.API.ViewModels.Brands.Requests;
using ECommerce.Products.API.ViewModels.Brands.Responses;
using ECommerce.Products.Domain.Entities;
using ECommerce.Products.Domain.Products.Interfaces;
using ECommerce.Products.Infrastructure.DbContexts;
using ECommerce.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Products.API.Services
{
    public class BrandService : BaseProductService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IUnitOfWork<ProductContext> unitOfWork
            , IServiceProvider serviceProvider
            , IBrandRepository brandRepository)
            : base(unitOfWork, serviceProvider)
        {
            _brandRepository = brandRepository;
        }

        public async Task<List<BrandInfoResponse>> GetBrands()
        {
            return await _brandRepository.GetQuery()
                .Select(new GetBrandsRequest().GetSelection())
                .ToListAsync();
        }
    }
}