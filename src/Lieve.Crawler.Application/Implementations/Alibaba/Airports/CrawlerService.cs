using System.Web;
using Lieve.Crawler.Application.Helpers;
using Lieve.Crawler.Application.Interfaces;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Lieve.Crawler.Application.Implementations.Alibaba.Airports;

public class CrawlerService(
    IAlibabaHttpClient alibabaHttpClient,
    IMongoDatabase database)
    : ICrawlerService<Request, Response>
{
    private readonly IMongoCollection<Response.Airport> _airports = database.GetCollection<Response.Airport>("airports");

    private async Task<Response?> FetchAsync(Request request, CancellationToken cancellationToken)
    {
        var requestUrl = HttpUtility.HtmlEncode($"api/v1/basic-info/airports/international?filter=");

        var response = await alibabaHttpClient.GetAsync(
            requestUrl,
            $"q={{ct:'{request.AirportIataCode}'}}",
            cancellationToken);

        var airports = JsonConvert.DeserializeObject<Response>(response ?? string.Empty);

        return airports;
    }

    public async Task FetchAsync(CancellationToken cancellationToken)
    {
        var random = new Random();

        var codes = Resources.Resources.GetAirportsIataCodes().OrderBy(x => x).ToList();
        
        foreach (var iataCode in codes)
        {
            try
            {
                var airports = await FetchAsync(new Request() { AirportIataCode = iataCode }, cancellationToken);
            
                if (airports is null) continue;
            
                foreach (var item in airports.Results.Items)
                {
                    var foundItems = await _airports.FindAsync(x => x.IataCode == item.IataCode,
                        cancellationToken: cancellationToken);

                    if (await foundItems.AnyAsync(cancellationToken) is true) continue;
                    
                    await _airports.InsertOneAsync(item, new InsertOneOptions(), cancellationToken);
                    ConsoleHelper.CustomConsoleWrite($"Inserted {item} successfully.", ConsoleColor.Green);
                }
            
                var delayInterval = random.Next(0, 1000 + 1);

                await Task.Delay(delayInterval, cancellationToken);
            }
            catch (Exception e)
            {
                ConsoleHelper.CustomConsoleWrite($"{iataCode} failed.", ConsoleColor.DarkMagenta);
            }
        }

    }
}