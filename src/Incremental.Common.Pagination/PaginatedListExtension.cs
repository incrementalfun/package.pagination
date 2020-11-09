namespace Incremental.Common.Pagination
{
    public static class PaginatedListExtension
    {
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