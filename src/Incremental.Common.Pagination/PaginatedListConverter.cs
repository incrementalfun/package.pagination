using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Incremental.Common.Pagination;

internal class PaginatedListConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        return typeToConvert.GetGenericTypeDefinition() == typeof(PaginatedList<>);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var itemsType = typeToConvert.GetGenericArguments()[0];

        return (JsonConverter?)Activator.CreateInstance(typeof(PaginatedListTypedConverter<>).MakeGenericType(itemsType));
    }

    private class PaginatedListTypedConverter<TItem> : JsonConverter<PaginatedList<TItem>>
    {
        private static readonly string MetadataPropertyName = "metadata";
        private static readonly string ItemsPropertyName = "items";

        public override PaginatedList<TItem>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            XPaginationMetadata? metadata = default;
            IEnumerable<TItem>? items = default;
            
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject && metadata is not null && items is not null)
                {
                    return new PaginatedList<TItem>(items, metadata.TotalCount, metadata.CurrentPage, metadata.PageSize);
                }
                
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }
                
                var propertyName = reader.GetString();

                if (string.Equals(propertyName, MetadataPropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    metadata = JsonSerializer.Deserialize<XPaginationMetadata>(ref reader, options);
                }
                else if (string.Equals(propertyName, ItemsPropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    items = JsonSerializer.Deserialize<IEnumerable<TItem>>(ref reader, options);
                }
            }
            
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, PaginatedList<TItem> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(MetadataPropertyName);

            JsonSerializer.Serialize(writer, value.ExtractMetadata(), options);
            
            writer.WritePropertyName(ItemsPropertyName);

            JsonSerializer.Serialize(writer, value as IEnumerable<TItem>);
            
            writer.WriteEndObject();
        }
    }
}