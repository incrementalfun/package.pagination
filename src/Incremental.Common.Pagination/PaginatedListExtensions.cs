namespace Incremental.Common.Pagination
{
    /// <summary>
    /// Extension methods for <see cref="PaginatedList{T}"/>.
    /// </summary>
    public static class PaginatedListExtensions
    {
        /// <summary>
        /// Extracts metadata for a paginated list.
        /// </summary>
        /// <param name="paginatedList"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static XPaginationMetadata ExtractMetadata<T>(this PaginatedList<T> paginatedList)
        {
            return new XPaginationMetadata
            {
                CurrentPage = paginatedList.CurrentPage,
                TotalPages = paginatedList.TotalPages,
                PageSize = paginatedList.PageSize,
                TotalCount = paginatedList.TotalCount,
                HasPrevious = paginatedList.HasPrevious,
                HasNext = paginatedList.HasNext
            };
        }
    }
}