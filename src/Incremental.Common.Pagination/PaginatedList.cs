using System;
using System.Collections.Generic;

namespace Incremental.Common.Pagination
{
    /// <summary>
    /// Generic paginated list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; protected set; }
        
        /// <summary>
        /// Total number of pages.
        /// </summary>
        public int TotalPages { get; protected set; }
        
        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int PageSize { get; protected set; }
        
        /// <summary>
        /// Total number of items.
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// Has a previous page.
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;
        
        /// <summary>
        /// Has a next page.
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PaginatedList(IEnumerable<T> items, int count, int pageNumber = 1, int pageSize = 50)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);

            AddRange(items);
        }
    }
}