using ECommerce.Shared.Entities.Interfaces;

namespace ECommerce.Shared.Entities.Base
{
    public class FullAuditEntity : AuditEntity, IFullAuditEntity
    {
        public virtual Guid UpdatedBy { get; protected set; }
        public virtual DateTime UpdatedOn { get; protected set; }

        public override void UpdateAudit(Guid createdBy, DateTime createdOn)
        {
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            UpdateModifierInfo(createdBy, createdOn);
        }

        public virtual void UpdateModifierInfo(Guid updatedBy, DateTime updatedOn)
        {
            UpdatedBy = updatedBy;
            UpdatedOn = updatedOn;
        }
    }
}