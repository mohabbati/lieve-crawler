using Lieve.Crawler.Application.Interfaces;
using Lieve.Crawler.Console.Extensions;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Start getting data...");

var serviceProvider = ConfigureApp();

var vendorPersistent = serviceProvider.GetService<IVendorPersistent>();
await vendorPersistent!.PersistAsync(new CancellationToken());

//var alibabaCrawlerService = serviceProvider.GetService<ICrawlerService<Request, Response>>();
//await alibabaCrawlerService!.FetchAsync(new CancellationToken());

Console.WriteLine("Fetching data finished.");
Console.ReadLine();

return;

static ServiceProvider ConfigureApp()
{
    var services = new ServiceCollection();
    services.AddAlibabaHttpClient();
    services.AddLieveMongoDbClient();
    services.AddVendorPersistent();
    return services.BuildServiceProvider();
}