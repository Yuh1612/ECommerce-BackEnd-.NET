using ECommerce.Shared.Constants;
using ECommerce.Shared.Interfaces;

namespace ECommerce.Shared.ViewModels
{
    public class PagingRequest : IPagingRequest
    {
        public int PageNo { get; set; } = DefaultPaging.DEFAULT_PAGE_NO;
        public int PageSize { get; set; } = DefaultPaging.DEFAULT_PAGE_SIZE;
    }
}