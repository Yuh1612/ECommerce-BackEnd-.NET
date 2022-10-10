using ECommerce.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Entities.Base
{
    public class AuditEntity : Entity, IAuditEntity
    {
        public virtual int CreatedBy { get; protected set; }
        public virtual DateTime CreatedOn { get; protected set; }

        public virtual void UpdateAudit(int createdBy, DateTime createdOn)
        {
            CreatedBy = createdBy;
            CreatedOn = createdOn;
        }
    }
}