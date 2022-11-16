using ECommerce.Products.API.Services.Base;
using ECommerce.Products.API.ViewModels.Categories.Requests;
using ECommerce.Products.API.ViewModels.Categories.Responses;
using ECommerce.Products.Domain.Products.Interfaces;
using ECommerce.Products.Infrastructure.DbContexts;
using ECommerce.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Products.API.Services
{
    public class CategoryService : BaseProductService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(IUnitOfWork<ProductContext> unitOfWork
            , IServiceProvider serviceProvider
            , ICategoryRepository categoryRepo)
            : base(unitOfWork, serviceProvider)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<List<CategoryInfoResponse>> GetCategories()
        {
            return await _categoryRepo.GetQuery()
                .Select(new GetCategoriesRequest().GetSelection())
                .ToListAsync();
        }
    }
}