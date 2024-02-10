using Lieve.Crawler.Application.Implementations.Alibaba.Airports;
using Lieve.Crawler.Application.Interfaces;
using Lieve.Crawler.Console.Extensions;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Start getting data...");

var serviceProvider = ConfigureApp();

var alibabaCrawlerService = serviceProvider.GetService<ICrawlerService<Request, Response>>();
await alibabaCrawlerService!.FetchAsync(new CancellationToken());

Console.WriteLine("Fetching data finished.");
Console.ReadLine();

return;

static ServiceProvider ConfigureApp()
{
    var services = new ServiceCollection();
    services.AddAlibabaHttpClient();
    services.AddLieveMongoDbClient();
    return services.BuildServiceProvider();
}