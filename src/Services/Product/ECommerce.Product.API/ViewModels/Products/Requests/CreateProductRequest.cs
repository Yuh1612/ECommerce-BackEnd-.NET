using Ecommerce.Utilities.Image.Attributes;
using Ecommerce.Utilities.Image.Constants;
using ECommerce.Shared.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerce.Products.API.ViewModels.Products.Requests
{
    public class CreateProductRequest
    {
        [Required]
        public Guid ShopId { get; set; } = Guid.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public int Width { get; set; }

        public Guid? BrandId { get; set; }

        public List<ProductOptionsInfo>? ProductOptions { get; set; }

        [RequiredList(1)]
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();

        [AllowedFile(FileType.Image)]
        public List<IFormFile>? Images { get; set; }
    }

    public class ProductOptionsInfo
    {
        public Guid Id { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}