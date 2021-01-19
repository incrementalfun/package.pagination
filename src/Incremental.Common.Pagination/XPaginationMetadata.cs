namespace Incremental.Common.Pagination
{
    /// <summary>
    /// Pagination metadata.
    /// </summary>
    public class XPaginationMetadata
    {
        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; init; }
        
        /// <summary>
        /// Total number of pages.
        /// </summary>
        public int TotalPages { get; set; }
        
        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Total number of items.
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// Has a previous page.
        /// </summary>
        public bool HasPrevious { get; set; }
        
        /// <summary>
        /// Has a next page.
        /// </summary>
        public bool HasNext { get; set; }
    }
}