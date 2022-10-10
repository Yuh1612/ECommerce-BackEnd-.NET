using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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