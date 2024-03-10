using MongoDB.Bson;

namespace Lieve.Crawler.Application.Implementations.Vendors.Models;

public sealed class VendorRoot
{
    public Vendor Vendor { get; set; }    
}

public class Vendor
{
    public ObjectId _id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public short Priority { get; set; }
    public string BaseUrl { get; set; }
    public string LogoUri { get; set; }
    public string FavIconUri { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ProvidedService> ProvidedServices { get; set; }
}

public class ProvidedService
{
    public ServiceType ServiceType { get; set; }
    public string UriTemplate { get; set; }
}

[Flags]
public enum ServiceType
{
    DomesticFlight = 1,
    InternationalFlight = 2,
    Flight = DomesticFlight | InternationalFlight
}