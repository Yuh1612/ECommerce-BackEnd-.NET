using ECommerce.Shared.Entities.Interfaces;

namespace ECommerce.Shared.Entities.Base
{
    public class AuditEntity : Entity, IAuditEntity
    {
        public virtual Guid CreatedBy { get; protected set; }
        public virtual DateTime CreatedOn { get; protected set; }

        public virtual void UpdateAudit(Guid createdBy, DateTime createdOn)
        {
            CreatedBy = createdBy;
            CreatedOn = createdOn;
        }
    }
}