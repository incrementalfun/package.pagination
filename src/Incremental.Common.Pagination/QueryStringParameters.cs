namespace Incremental.Common.Pagination
{
    public abstract class QueryStringParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize || value <= 0
                ? MaxPageSize 
                : value;
        }
    }
}