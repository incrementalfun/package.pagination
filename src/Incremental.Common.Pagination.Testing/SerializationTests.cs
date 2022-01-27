using System.Text.Json;
using NUnit.Framework;

namespace Incremental.Common.Pagination.Testing;

public class SerializationTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Can_Be_Serialized_And_Deserialized()
    {
        var entry = new PaginatedList<int>(new[] { 5, 6, 7, 8, 9 }, 10, 2, 5);

        var serialized = JsonSerializer.Serialize(entry);

        var deserialized = JsonSerializer.Deserialize<PaginatedList<int>>(serialized);
        
        Assert.AreEqual(entry, deserialized);
    }

    [Test]
    public void Can_Handle_Inner_Paginated_Lists()
    {
        var entry = new PaginatedList<PaginatedList<int>>(new PaginatedList<int>[]
        {
            new(new[] { 0, 1, 2, 3, 4 }, 10, 1, 5),
            new(new[] { 5, 6, 7, 8, 9 }, 10, 2, 5)
        }, 10, 1, 2);
        
        var serialized = JsonSerializer.Serialize(entry);

        var deserialized = JsonSerializer.Deserialize<PaginatedList<PaginatedList<int>>>(serialized);
        
        Assert.AreEqual(entry, deserialized);
    }
}