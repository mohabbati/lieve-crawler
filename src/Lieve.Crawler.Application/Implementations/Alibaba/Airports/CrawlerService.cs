using System.Web;
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

    public async Task<Response?> FetchAsync(Request request, CancellationToken cancellationToken)
    {
        var requestUrl = HttpUtility.HtmlEncode($"api/v1/basic-info/airports/international?filter=");

        var response = await alibabaHttpClient.GetAsync(
            requestUrl,
            $"q={{ct:'{request.AirportIataCode}'}}",
            cancellationToken);

        var airports = JsonConvert.DeserializeObject<Response>(response ?? string.Empty);

        return airports;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        var airports = await FetchAsync(new Request() { AirportIataCode = "ber" }, cancellationToken);
        
        if (airports is null) return;
        
        foreach (var item in airports.Results.Items)
        {
            var foundItems = await _airports.FindAsync(x => x.IataCode == item.IataCode,
                cancellationToken: cancellationToken);
            
            if (await foundItems.AnyAsync(cancellationToken) is false)
                await _airports.InsertOneAsync(item, new InsertOneOptions(), cancellationToken);
        }
    }
    
    private async Task<List<Response.Airport>> GetAirportByIataCodeAsync(string iataCode, CancellationToken cancellationToken)
    {
        var filter = Builders<Response.Airport>.Filter.Eq(x => x.IataCode, iataCode);
        return await _airports.Find(filter).ToListAsync(cancellationToken);
    }
}