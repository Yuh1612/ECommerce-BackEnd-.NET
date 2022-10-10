using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ECommerce.Shared.Entities.Base
{
    public class Entity : Entity<Guid>
    {
    }

    public class Entity<TKey> : EntityBase
    {
        [Key]
        public virtual TKey Id { get; protected set; }
    }
}