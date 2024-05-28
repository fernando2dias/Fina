namespace Fina.Common.Requests
{
    public abstract class PagedRequest
    {
        public int PageSize { get; set; } = Configuration.DetaultPageSize;
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    }
}
