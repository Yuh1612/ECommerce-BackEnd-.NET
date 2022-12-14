using ECommerce.Products.API.Services;
using ECommerce.Products.API.ViewModels.Brands.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Products.API.Controllers
{
    [Route("api/v1/sv2/[controller]s")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;

        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<List<BrandInfoResponse>> GetBrands()
        {
            return await _brandService.GetBrands();
        }
    }
}