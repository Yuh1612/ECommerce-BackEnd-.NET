namespace ECommerce.Products.API.ViewModels.Options.Responses
{
    public class ProductOptionInfoResponse
    {
        public Guid Id { get; set; }
        public string Option { get; set; } = string.Empty;
        public List<string> Name { get; set; } = new List<string>();
    }
}