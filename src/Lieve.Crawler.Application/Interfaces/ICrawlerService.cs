namespace Lieve.Crawler.Application.Interfaces;

public interface ICrawlerService<in TRequest, TResponse> 
    where TRequest : IRequestModel
    where TResponse : IResponseModel
{
    Task<TResponse?> FetchAsync(TRequest request, CancellationToken cancellationToken);
}