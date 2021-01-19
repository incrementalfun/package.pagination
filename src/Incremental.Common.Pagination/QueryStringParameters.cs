namespace Incremental.Common.Pagination
{
    /// <summary>
    /// Query string parameters for easy interaction with <see cref="PaginatedList{T}"/> related endpoints.
    /// </summary>
    public abstract class QueryStringParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        /// <summary>
        /// Page number requested.
        /// </summary>
        public int PageNumber { get; set; } = 1;
        
        
        /// <summary>
        /// Number of items per page requested.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize || value <= 0
                ? MaxPageSize 
                : value;
        }
    }
}