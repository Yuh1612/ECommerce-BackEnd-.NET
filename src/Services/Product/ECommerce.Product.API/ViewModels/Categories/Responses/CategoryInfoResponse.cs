namespace ECommerce.Products.API.ViewModels.Categories.Responses
{
    public class CategoryInfoResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}