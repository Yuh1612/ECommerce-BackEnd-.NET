using ECommerce.Products.Domain.Products.Interfaces;
using ECommerce.Shared.Entities.Base;

namespace ECommerce.Products.Domain.Entities
{
    public partial class Product : FullAuditAndDeleteEntity
    {
        public Product()
        {
        }

        public Product(Guid shopId
            , string name
            , string? description
            , decimal price
            , int quantity
            , double weight
            , double height
            , double length
            , Guid? brandId)
        {
            Code = Guid.NewGuid();
            ShopId = shopId;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            Weight = weight;
            Height = height;
            Length = length;
            BrandId = brandId;
        }

        public void AddCategory(Guid categoryId)
        {
            if (!ProductCategories.Any(_ => _.CategoryId == categoryId))
            {
                ProductCategories.Add(new ProductCategories(this, categoryId));
            }
        }

        public void AddCategories(List<Guid> categoryIds)
        {
            foreach (var categoryId in categoryIds)
            {
                AddCategory(categoryId);
            }
        }
    }
}