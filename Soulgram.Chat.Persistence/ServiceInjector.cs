using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Chat.Persistence.Ports;
using Soulgram.Chat.Persistence.Repositories;
using Soulgram.Mongo.Repository;
using Soulgram.Mongo.Repository.Interfaces;
using Soulgram.Mongo.Repository.Models;

namespace Soulgram.Chat.Persistence;

public static class ServiceInjector
{
    public static void AddMongoDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DbSettings>(configuration.GetSection("DbSettings"));
        var dbSettings = configuration
            .GetSection("DbSettings")
            .Get<DbSettings>();

        new MongoMapper().MapModels();

        services.AddSingleton<IMongoClient, MongoClient>(sp
            => new MongoClient(dbSettings?.ConnectionString));

        services.AddScoped<IMongoConnection, MongoConnection>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IChatRepository, ChatRepository>();

        services.AddMemoryCache();
    }
}