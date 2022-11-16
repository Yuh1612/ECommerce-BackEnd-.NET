using ECommerce.Shared.Entities.Base;

namespace ECommerce.Products.Domain.Entities
{
    public partial class Shop : Entity
    {
        public Shop()
        {
        }

        public Shop(Guid id, string? name, string? userName)
        {
            Id = id;
            UserName = userName;
            Name = name;
        }
    }
}