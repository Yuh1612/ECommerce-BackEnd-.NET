using ECommerce.Shared.Entities.Interfaces;

namespace ECommerce.Shared.Entities.Base
{
    public class FullAuditAndDeleteEntity : FullAuditEntity, IDeleteEntity
    {
        public bool IsDeleted { get; protected set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void UnDelete()
        {
            IsDeleted = false;
        }
    }
}