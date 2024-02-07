using System.Net.Http;
using Lieve.Crawler.Application.Implementations.Alibaba.Airports;
using Lieve.Crawler.Application.Interfaces;
using Lieve.Crawler.Console.Extensions;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Start getting data...");


// Register HttpClient in DI container (assuming console app)
var services = new ServiceCollection();
services.AddAlibabaHttpClient();
var serviceProvider = services.BuildServiceProvider();

// Use HttpClient to fetch data
var alibabaCrawlerService = serviceProvider.GetService<ICrawlerService<Request, Response>>();
var data = await alibabaCrawlerService.FetchAsync(new Request() { AirportIataCode = "ist" }, CancellationToken.None);

Console.WriteLine(data);
