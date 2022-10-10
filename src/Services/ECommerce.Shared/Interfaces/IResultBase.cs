namespace ECommerce.Shared.Interfaces
{
    public interface IResultBase
    {
        bool IsSuccess { get; set; }
        string? Message { get; set; }
    }

    public interface IResultBase<T> : IResultBase
    {
        T? Data { get; set; }
    }
}