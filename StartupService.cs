using System.Text.Json;
using Redis.OM;
using Redis.OM.Contracts;

namespace GraphQLBlog;

public class StartupService : IHostedService
{
    private readonly IRedisConnectionProvider _provider;

    public StartupService(IRedisConnectionProvider provider)
    {
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var books = _provider.RedisCollection<Book>();
        _provider.Connection.DropIndexAndAssociatedRecords(typeof(Book));
        await _provider.Connection.CreateIndexAsync(typeof(Book));

        var files = Directory.GetFiles("data");
        foreach (var file in files)
        {
            var tasks = new List<Task>();
            var str = await File.ReadAllTextAsync(file, cancellationToken);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var booksInFile = JsonSerializer.Deserialize<Book[]>(str, options);
            foreach (var book in booksInFile)
            {
                tasks.Add(books.InsertAsync(book));
            }

            await Task.WhenAll(tasks);
        }

    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}