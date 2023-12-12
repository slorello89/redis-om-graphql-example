using GraphQLBlog;
using HotChocolate.Data.Filters.Expressions;
using Redis.OM;
using Redis.OM.Contracts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IRedisConnectionProvider>(new RedisConnectionProvider("redis://localhost:6379"));

builder.Services.AddHostedService<StartupService>();

builder.Services.AddGraphQLServer()
    .AddProjections()
    .AddFiltering()
    .AddQueryType(d => d.Name("Query"))
    .AddType<Query>();
        
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.Run();
