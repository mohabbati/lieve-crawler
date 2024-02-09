using Lieve.Crawler.Application.Implementations.Alibaba.Airports;
using Lieve.Crawler.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Lieve.Crawler.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAlibabaHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IAlibabaHttpClient, AlibabaHttpClient>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://ws.alibaba.ir/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddScoped(typeof(ICrawlerService<Request, Response>), typeof(CrawlerService));

        return services;
    }

    public static IServiceCollection AddLieveMongoDbClient(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(s =>
            new MongoClient("mongodb://localhost:27017"));
        
        services.AddSingleton<IMongoDatabase>(s =>
        {
            var client = s.GetRequiredService<IMongoClient>();
            return client.GetDatabase("YourDatabaseName");
        });

        return services;
    }
}