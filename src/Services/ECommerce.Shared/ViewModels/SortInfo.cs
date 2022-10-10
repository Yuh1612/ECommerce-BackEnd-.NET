namespace ECommerce.Shared.ViewModels
{
    public class SortInfo
    {
        public string Property { get; set; }
        public bool IsDesc { get; set; }

        public SortInfo(string property, bool isDesc)
        {
            Property = property;
            IsDesc = isDesc;
        }
    }
}