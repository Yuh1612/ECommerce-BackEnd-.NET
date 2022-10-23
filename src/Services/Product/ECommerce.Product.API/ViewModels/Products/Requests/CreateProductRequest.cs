using Ecommerce.Utilities.Image.Attributes;
using Ecommerce.Utilities.Image.Constants;
using ECommerce.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

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
        public float Weight { get; set; }

        [Required]
        public float Height { get; set; }

        [Required]
        public float Length { get; set; }

        public Guid? BrandId { get; set; }

        [RequiredList(1)]
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();

        [AllowedFile(FileType.Image)]
        public List<IFormFile>? Images { get; set; }
    }
}