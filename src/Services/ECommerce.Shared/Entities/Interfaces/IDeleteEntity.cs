namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IDeleteEntity : IEntityBase
    {
        bool IsDeleted { get; }

        void Delete();

        void UnDelete();
    }
}