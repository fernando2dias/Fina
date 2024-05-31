﻿using System.Text.Json.Serialization;

namespace Fina.Common.Responses
{
    public class PagedResponse<T> : Response<T>
    {
        [JsonConstructor]
        public PagedResponse(T? data,
                             int totalCount,
                             int currentPage = 1,
                             int pageSize = Configuration.DetaultPageSize)
                             : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(T? data,
                             int code = Configuration.DefaultStatusCode,
                             string? message = null)
                             : base(data, code, message)
        {
        }

        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = Configuration.DetaultPageSize;
        public int TotalCount { get; set; }



    }
}
