using System.Web;

namespace Lieve.Crawler.Application.Interfaces;

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
            Console.WriteLine(e);
            return null;
        }
    }
}