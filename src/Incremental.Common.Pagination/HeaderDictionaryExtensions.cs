using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Incremental.Common.Pagination
{
    /// <summary>
    /// Extensions for <see cref="IHeaderDictionary"/>.
    /// </summary>
    public static class HeaderDictionaryExtensions
    {
        /// <summary>
        /// Adds pagination metadata to headers in response.
        /// Default key for pagination metadata: `X-Pagination`
        /// </summary>
        /// <param name="header">The <see cref="IHeaderDictionary"/> that will be used.</param>
        /// <param name="data">The paginated list data to add to the header.</param>
        /// <typeparam name="T">The data.</typeparam>
        public static void AddPagination<T>(this IHeaderDictionary header, PaginatedList<T> data)
        {
            header.Add("X-Pagination", JsonSerializer.Serialize(data.ExtractMetadata()));
        }

        /// <summary>
        /// Extracts pagination information if available.
        /// </summary>
        /// <param name="headers">The <see cref="HttpResponseHeaders"/> where the pagination information may be.</param>
        /// <returns>Pagination information as <see cref="XPaginationMetadata"/></returns>
        public static XPaginationMetadata? GetPagination(this HttpResponseHeaders headers)
        {
            var pagination = headers.FirstOrDefault(pair => pair.Key == "X-Pagination").Value.FirstOrDefault();

            return pagination is not null ? JsonSerializer.Deserialize<XPaginationMetadata>(pagination) : default;
        }
    }
}