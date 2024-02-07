using Lieve.Crawler.Application.Interfaces;
using Newtonsoft.Json;

namespace Lieve.Crawler.Application.Implementations.Alibaba.Airports;

public sealed class Response : IResponseModel
{
    [JsonProperty("result")]
    public required Result Results { get; set; }
    public required bool Success { get; set; }
    
    public class Result
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public required List<Item> Items { get; set; }
    }

    public class Item
    {
        public required City City { get; set; }
        public required int Code { get; set; }
        public required string IataCode { get; set; }
        public required string Name { get; set; }
        [JsonProperty("displayName")]
        public required List<DisplayName> DisplayNames { get; set; }
        public required string DomainCode { get; set; }
        public required bool IsPopular { get; set; }
    }

    public class City
    {
        public int Code { get; set; }
        public required string Name { get; set; }
        [JsonProperty("displayName")]
        public required List<DisplayName> DisplayNames { get; set; }
        public required string DomainCode { get; set; }
        public required Country Country { get; set; }
    }

    public class Country
    {
        public required int Code { get; set; }
        public required string Name { get; set; }
        [JsonProperty("displayName")]
        public required List<DisplayName> DisplayNames { get; set; }
        public required string DomainCode { get; set; }
    }

    public class DisplayName
    {
        public required string Language { get; set; }
        public required string Value { get; set; }
    }
}