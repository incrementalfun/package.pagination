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
        /// Adds pagination metadata to the headers dictionary.
        /// Default key for pagination metadata: `X-Pagination`
        /// </summary>
        /// <param name="header">The <see cref="IHeaderDictionary"/> that will be used.</param>
        /// <param name="data">The paginated list data to add to the header.</param>
        /// <param name="key">Key to use in header</param>
        /// <typeparam name="T">The data.</typeparam>
        public static void AddPagination<T>(this IHeaderDictionary header, PaginatedList<T> data, string key = "X-Pagination")
        {
            header.Add(key, JsonSerializer.Serialize(data.ExtractMetadata()));
        }

        /// <summary>
        /// Extracts pagination information if available.
        /// Default key for pagination metadata: `X-Pagination`
        /// </summary>
        /// <param name="headers">The <see cref="HttpResponseHeaders"/> where the pagination information may be.</param>
        /// <param name="key">Key to use in header</param>
        /// <returns>Pagination information as <see cref="XPaginationMetadata"/></returns>
        public static XPaginationMetadata? GetPagination(this HttpResponseHeaders headers, string key = "X-Pagination")
        {
            var pagination = headers.FirstOrDefault(pair => pair.Key == key).Value.FirstOrDefault();

            return pagination is not null ? JsonSerializer.Deserialize<XPaginationMetadata>(pagination) : default;
        }
    }
}