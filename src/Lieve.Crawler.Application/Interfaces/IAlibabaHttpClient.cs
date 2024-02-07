using System.Net.Http.Json;

namespace Lieve.Crawler.Application.Interfaces;

public interface IAlibabaHttpClient
{
    Task<string?> GetAsync(string endpoint, CancellationToken cancellationToken);
}