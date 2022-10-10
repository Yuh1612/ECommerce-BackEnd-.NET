using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IAuditEntity : IEntityBase
    {
        int CreatedBy { get; }
        DateTime CreatedOn { get; }

        void UpdateAudit(int createdBy, DateTime createdOn);
    }
}