using System.Web;
using Lieve.Crawler.Application.Interfaces;
using Newtonsoft.Json;

namespace Lieve.Crawler.Application.Implementations.Alibaba.Airports;

public class CrawlerService(IAlibabaHttpClient alibabaHttpClient) : ICrawlerService<Request, Response>
{
    public async Task<Response?> FetchAsync(Request request, CancellationToken cancellationToken)
    {
        var requestUrl = HttpUtility.HtmlEncode($"api/v1/basic-info/airports/international?filter=ist");

        var response = await alibabaHttpClient.GetAsync(requestUrl, cancellationToken);

        var airports = JsonConvert.DeserializeObject<Response>(response ?? string.Empty);

        return airports;
    }
}