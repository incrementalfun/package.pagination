using System;
using System.Collections.Generic;

namespace Incremental.Common.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; protected set; }
        public int TotalPages { get; protected set; }
        public int PageSize { get; protected set; }
        public int TotalCount { get; protected set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);

            AddRange(items);
        }
    }
}