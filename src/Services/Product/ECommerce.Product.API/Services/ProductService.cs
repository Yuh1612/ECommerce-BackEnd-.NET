using Castle.Core.Internal;
using Dasync.Collections;
using Ecommerce.Utilities.Image.Service;
using ECommerce.Products.API.Services.Base;
using ECommerce.Products.API.ViewModels.Products.Requests;
using ECommerce.Products.API.ViewModels.Products.Responses;
using ECommerce.Products.Domain;
using ECommerce.Products.Domain.Entities;
using ECommerce.Products.Domain.Products.Interfaces;
using ECommerce.Products.Infrastructure.DbContexts;
using ECommerce.Shared.Exceptions;
using ECommerce.Shared.Extensions;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Products.API.Services
{
    public class ProductService : BaseProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductCategoriesRepository _productCategoriesRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IUnitOfWork<ProductContext> unitOfWork
            , IServiceProvider serviceProvider
            , IProductRepository productRepository
            , IWebHostEnvironment webHostEnvironment
            , IProductCategoriesRepository productCategoriesRepository
            , IShopRepository shopRepository
            , IBrandRepository brandRepository
            , ICategoryRepository categoryRepository)
            : base(unitOfWork, serviceProvider)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _productCategoriesRepository = productCategoriesRepository;
            _shopRepository = shopRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }

        private string GetImagePath(string code)
        {
            return Path.Combine(_webHostEnvironment.ContentRootPath, "Resources", code);
        }

        public async Task<ProductInfoResponse> CreateProduct(CreateProductRequest request)
        {
            if (!await _shopRepository.AnyAsync(request.ShopId))
                throw new BadRequestException(MessagesResource.NotFoundShop);

            if (request.BrandId.HasValue && !await _brandRepository.AnyAsync(request.BrandId.Value))
                throw new BadRequestException(MessagesResource.NotFoundBrand);

            var categoryCount = await _categoryRepository.GetQuery()
                .Select(c => c.Id)
                .Where(_ => request.CategoryIds.Contains(_))
                .CountAsync();

            if (categoryCount != request.CategoryIds.Count)
                throw new BadRequestException(MessagesResource.NotFoundCategory);

            var product = new Product(request.ShopId
                , request.Name
                , request.Description
                , request.Price
                , request.Quantity
                , request.Weight
                , request.Height
                , request.Length
                , request.BrandId);

            product.AddCategories(request.CategoryIds);

            string path = GetImagePath(product.Code.ToString());

            if (!request.Images.IsNullOrEmpty())
            {
                if (!await FileService.UploadFiles(request.Images!, path))
                    throw new BadRequestException(MessagesResource.UploadImageFailed);
            }

            await _productRepository.InsertAsync(product);
            await UnitOfWork.SaveChangesAsync();

            return new ProductInfoResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = FileService.GetFileUrls(path, "images")?.FirstOrDefault()
            };
        }

        public async Task<PagingResult<ProductInfoResponse>> GetProducts(GetProductsRequest request)
        {
            var products = await _productRepository
                .GetQuery(request.GetFilter())
                .Select(request.GetSelection())
                .ToPagedListAsync(request.PageNo, request.PageSize);

            await products.Data.ParallelForEachAsync(product =>
            {
                product.ImageUrl = FileService.GetFileUrls(GetImagePath(product.Code.ToString()), "images")?.FirstOrDefault();
                return Task.CompletedTask;
            }, 5);

            return products;
        }

        public async Task<ProductInfoDetailResponse> GetProductDetail(Guid productId)
        {
            if (!await _productRepository.AnyAsync(productId))
                throw new BadRequestException(MessagesResource.NotFoundProduct);

            var product = await _productRepository
                .GetQuery(_ => _.Id == productId)
                .Select(new GetProductDetailRequest().GetSelection())
                .FirstAsync();

            product.ImageUrl = FileService.GetFileUrls(GetImagePath(product.Code.ToString()), "images");

            return product;
        }
    }
}