using ECommerce.Shared.Entities.Base;

namespace ECommerce.Products.Domain.Entities
{
    public partial class ProductCategories : AuditAndDeleteEntityBase
    {
        public ProductCategories()
        {
        }

        public ProductCategories(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }

        public ProductCategories(Product product, Guid categoryId)
        {
            Product = product;
            CategoryId = categoryId;
        }
    }
}