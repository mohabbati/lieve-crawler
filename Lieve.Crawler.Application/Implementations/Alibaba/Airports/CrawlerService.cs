using Lieve.Crawler.Application.Interfaces;

namespace Lieve.Crawler.Application.Implementations.Alibaba.Airports;

public class CrawlerService : ICrawlerService<Request, Response>
{
    private readonly AlibabaHttpClient _httpClient;

    public CrawlerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public Task<Response> Get(Request request)
    {
        
        throw new NotImplementedException();
    }
}