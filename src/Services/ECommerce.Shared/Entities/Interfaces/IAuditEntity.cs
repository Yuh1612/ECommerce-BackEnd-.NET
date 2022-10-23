namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IAuditEntity : IEntityBase
    {
        Guid CreatedBy { get; }
        DateTime CreatedOn { get; }

        void UpdateAudit(Guid createdBy, DateTime createdOn);
    }
}