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
        public async Task<ProductInfoResponse> CreateProductAsync([FromForm] CreateProductRequest request)
        {
            return await _productService.CreateProductAsync(request);
        }
        
        [HttpPut("{productId:Guid}/active")]
        public async Task ActiveProductAsync([FromRoute] Guid productId)
        {
            await _productService.ActiveProductAsync(productId);
        }

        [HttpPut("{productId:Guid}/deactive")]
        public async Task DeactiveProductAsync([FromRoute] Guid productId)
        {
            await _productService.DeactiveProductAsync(productId);
        }

        [HttpGet]
        public async Task<PagingResult<ProductInfoResponse>> GetProductsAsync([FromQuery] GetProductsRequest request)
        {
            return await _productService.GetProductsAsync(request);
        }

        [HttpGet("deactive")]
        public async Task<PagingResult<ProductInfoResponse>> GetDeactiveProductsAsync([FromQuery] PagingRequest request)
        {
            return await _productService.GetDeactiveProductsAsync(request);
        }

        [HttpGet("{productId:Guid}")]
        public async Task<ProductInfoDetailResponse> GetProductDetailAsync([FromRoute] Guid productId)
        {
            return await _productService.GetProductDetailAsync(productId);
        }


    }
}