namespace Lieve.Crawler.Application.Interfaces;

public interface IVendorPersistent
{
    Task PersistAsync(CancellationToken cancellationToken);
}
