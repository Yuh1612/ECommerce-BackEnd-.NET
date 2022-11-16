using ECommerce.Shared.Entities.Interfaces;

namespace ECommerce.Shared.Entities.Base
{
    public abstract class DeleteEntityBase : EntityBase, IDeleteEntity
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