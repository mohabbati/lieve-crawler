using Lieve.Crawler.Application.Interfaces;

namespace Lieve.Crawler.Application.Implementations.Alibaba.Airports;

public sealed class Request : IRequestModel
{
    public string AirportIataCode { get; set; } = default!;
}