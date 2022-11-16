using ECommerce.Products.API.Services;
using ECommerce.Products.API.ViewModels.Products.Requests;
using ECommerce.Products.API.ViewModels.Products.Responses;
using ECommerce.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Products.API.Controllers
{
    [Route("api/v1/sv2/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ProductInfoResponse> CreateProduct([FromForm] CreateProductRequest request)
        {
            return await _productService.CreateProduct(request);
        }
        
        [HttpPut("{productId:Guid}/active")]
        public async Task ActiveProduct([FromRoute] Guid productId)
        {
            await _productService.ActiveProduct(productId);
        }

        [HttpGet]
        public async Task<PagingResult<ProductInfoResponse>> GetProducts([FromQuery] GetProductsRequest request)
        {
            return await _productService.GetProducts(request);
        }

        [HttpGet("{productId:Guid}")]
        public async Task<ProductInfoDetailResponse> GetProductDetail([FromRoute] Guid productId)
        {
            return await _productService.GetProductDetail(productId);
        }


    }
}