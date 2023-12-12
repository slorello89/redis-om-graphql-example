using HotChocolate.Resolvers;
using HotChocolate.Data;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;

namespace GraphQLBlog;

[ExtendObjectType("Query")]
public class Query
{
    private readonly IRedisCollection<Book> _books;

    public Query(IRedisConnectionProvider provider)
    {
        _books = provider.RedisCollection<Book>();
    }

    [UsePaging(IncludeTotalCount = true, MaxPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Book> GetBook(IResolverContext context) => _books.Filter(context);
}