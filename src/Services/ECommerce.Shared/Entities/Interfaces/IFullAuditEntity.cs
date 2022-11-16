namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IFullAuditEntity : IAuditEntity
    {
        Guid UpdatedBy { get; }
        DateTime UpdatedOn { get; }

        void UpdateModifierInfo(Guid updatedBy, DateTime updatedOn);
    }
}