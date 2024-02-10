using System.Web;
using Lieve.Crawler.Application.Helpers;
using Lieve.Crawler.Application.Interfaces;

namespace Lieve.Crawler.Application.Implementations.Alibaba;

public class AlibabaHttpClient(HttpClient httpClient) : IAlibabaHttpClient
{
    public async Task<string?> GetAsync(string endpoint, string query = "", CancellationToken cancellationToken = default)
    {
        try
        {
            var encodedQuery = HttpUtility.UrlEncode(query);
            var response = await httpClient.GetAsync($"{endpoint}{encodedQuery}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return jsonContent;
        }
        catch (Exception e)
        {
            ConsoleHelper.CustomConsoleWrite(e.InnerException?.Message ?? e.Message, ConsoleColor.Red);

            throw;
        }
    }
}