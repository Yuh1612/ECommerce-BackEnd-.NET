using ECommerce.Products.API.ViewModels.Brands.Responses;
using ECommerce.Products.API.ViewModels.Categories.Responses;
using ECommerce.Products.API.ViewModels.Options.Responses;
using System.Text.Json.Serialization;

namespace ECommerce.Products.API.ViewModels.Products.Responses
{
    public class ProductInfoDetailResponse
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid Code { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public string? Slug { get; set; }

        public double? Discount { get; set; }

        public ShopInfo Shop { get; set; } = new ShopInfo();

        public List<CategoryInfoResponse>? Categories { get; set; } = new List<CategoryInfoResponse>();

        public BrandInfoResponse? Brand { get; set; }

        public List<ProductOptionInfoResponse>? Options { get; set; }

        public List<string>? ImageUrl { get; set; }
    }
}