using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.Shared.Domain
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(IList<T> items, int count, int pageNumber, int pageSize)
        {
            PageCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }

        public int PageNumber { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        public int TotalItemCount { get; }
        public bool HasPreviousPage { get; }
        public bool HasNextPage { get; }
        public bool IsFirstPage { get; }
        public bool IsLastPage { get; }
        public IList<T> Items { get; }
    }
}
