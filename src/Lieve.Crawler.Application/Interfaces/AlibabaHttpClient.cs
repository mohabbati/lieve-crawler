using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Lieve.Crawler.Application.Interfaces;

public class AlibabaHttpClient(HttpClient httpClient) : IAlibabaHttpClient
{
    public async Task<string?> GetAsync(string endpoint, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.GetAsync(endpoint, cancellationToken);
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);

            return jsonContent;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}