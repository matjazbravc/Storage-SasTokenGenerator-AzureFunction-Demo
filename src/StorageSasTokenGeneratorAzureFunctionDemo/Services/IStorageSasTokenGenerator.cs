using System.Net.Http;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services
{
    /// <summary>
    ///     This provides interfaces to the <see cref="StorageSasTokenGenerator" /> class.
    /// </summary>
    public interface IStorageSasTokenGenerator
    {
        HttpResponseMessage GetStorageAccessResponse(HttpRequestMessage req);
    }
}
