using Redis.OM.Modeling;

namespace GraphQLBlog;

[Document(StorageType = StorageType.Json)]
public class Book
{
    [Indexed]
    [RedisIdField]
    public string? Id { get; set; }
    
    [Searchable]
    public string? Title { get; set; }

    [Searchable]
    public string? SubTitle { get; set; }

    [Searchable]
    public string? Description { get; set; }

    [Indexed]
    public string? Language { get; set; }

    [Indexed]
    public long? PageCount { get; set; }

    [Indexed]
    public string? Thumbnail { get; set; }

    [Indexed]
    public double? Price { get; set; }

    [Indexed]
    public string? Currency { get; set; }

    public string? InfoLink { get; set; }

    [Indexed]
    public string[]? Authors { get; set; }
}
