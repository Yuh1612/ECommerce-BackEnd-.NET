namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IFullAuditEntity : IAuditEntity
    {
        int UpdatedBy { get; }
        DateTime UpdatedOn { get; }

        void UpdateModifierInfo(int updatedBy, DateTime updatedOn);
    }
}