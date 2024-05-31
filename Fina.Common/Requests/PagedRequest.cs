namespace Fina.Common.Requests
{
    public abstract class PagedRequest : Request
    {
        public int PageSize { get; set; } = Configuration.DetaultPageSize;
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    }
}
