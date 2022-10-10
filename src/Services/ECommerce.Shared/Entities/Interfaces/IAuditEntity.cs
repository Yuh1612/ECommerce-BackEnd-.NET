namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IAuditEntity : IEntityBase
    {
        int CreatedBy { get; }
        DateTime CreatedOn { get; }

        void UpdateAudit(int createdBy, DateTime createdOn);
    }
}