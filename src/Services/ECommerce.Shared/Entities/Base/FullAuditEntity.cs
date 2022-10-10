using ECommerce.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Entities.Base
{
    public class FullAuditEntity : AuditEntity, IFullAuditEntity
    {
        public virtual int UpdatedBy { get; protected set; }
        public virtual DateTime UpdatedOn { get; protected set; }

        public override void UpdateAudit(int createdBy, DateTime createdOn)
        {
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            UpdateModifierInfo(createdBy, createdOn);
        }

        public virtual void UpdateModifierInfo(int updatedBy, DateTime updatedOn)
        {
            UpdatedBy = updatedBy;
            UpdatedOn = updatedOn;
        }
    }
}