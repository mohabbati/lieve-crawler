using Lieve.Crawler.Application.Helpers;
using Lieve.Crawler.Application.Implementations.Vendors.Models;
using Lieve.Crawler.Application.Interfaces;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Lieve.Crawler.Application.Implementations.Vendors;

public class VendorPersistent(IMongoDatabase mongoDatabase) : IVendorPersistent
{
    private readonly IMongoCollection<Vendor> _vendors = mongoDatabase.GetCollection<Vendor>("vendors");

    public async Task PersistAsync(CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Implementations/Vendors/Resources/Vendors.json");
        var json = File.ReadAllText(filePath);

        var deserialized = JsonConvert.DeserializeObject<List<VendorRoot>>(json);
        var vendors = deserialized!.Select(x => x.Vendor).ToList();

        if (vendors is null)
        {
            ConsoleHelper.CustomConsoleWrite($"No vendor found to persist.", ConsoleColor.Yellow);
            return;
        }

        ConsoleHelper.CustomConsoleWrite($"{vendors.Count} found to persist.", ConsoleColor.Green);

        foreach (var item in vendors)
        {
            var filter = Builders<Vendor>.Filter.Eq("Name", item.Name);
            var result = _vendors.Find(filter).FirstOrDefault();

            if (result is not null)
            {
                var update = Builders<Vendor>.Update
                    .Set("DisplayName", item.DisplayName)
                    .Set("Priority", item.Priority)
                    .Set("BaseUrl", item.BaseUrl)
                    .Set("LogoUri", item.LogoUri)
                    .Set("FavIconUri", item.FavIconUri)
                    .Set("IsActive", item.IsActive)
                    .Set("ProvidedServices", item.ProvidedServices);

                await _vendors.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
                ConsoleHelper.CustomConsoleWrite($"Updated {item.Name} successfully.", ConsoleColor.Green);
            }
            else
            {
                await _vendors.InsertOneAsync(item, new InsertOneOptions(), cancellationToken);
                ConsoleHelper.CustomConsoleWrite($"Inserted {item.Name} successfully.", ConsoleColor.Green);
            }
        }
    }
}
