using ECommerce.Products.API.Services;
using ECommerce.Products.API.ViewModels.Categories.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Products.API.Controllers
{
    [Route("api/v1/sv2/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<CategoryInfoResponse>> GetCategories()
        {
            return await _categoryService.GetCategories();
        }
    }
}