namespace ECommerce.Shared.Interfaces
{
    public interface IPagingRequest
    {
        int PageNo { get; set; }
        int PageSize { get; set; }
    }
}