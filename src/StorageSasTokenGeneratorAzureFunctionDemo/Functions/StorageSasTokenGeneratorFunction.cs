using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using StorageSasTokenGeneratorAzureFunctionDemo.Services;
using StorageSasTokenGeneratorAzureFunctionDemo.Services.Ioc;
using StorageSasTokenGeneratorAzureFunctionDemo.Services.Logging;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Functions
{
    public class StorageSasTokenGeneratorFunction
    {
        [FunctionName(nameof(StorageSasTokenGeneratorFunction))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
            [Inject] ILog log,
            [Inject] IStorageSasTokenGenerator storageSasTokenGenerator)
        {
            try
            {
                // Check if it is "Wake-Up" request?
                if (req.Method == HttpMethod.Get)
                {
                    return req.CreateResponse(HttpStatusCode.OK); // Return OK status
                }

                log.Info($"Starting {nameof(StorageSasTokenGeneratorFunction)}");
                return storageSasTokenGenerator.GetStorageAccessResponse(req);
            }
            catch (Exception ex)
            {
                var exMessage = $"And error occured processing your request: {ex.Message}";
                log.Error(exMessage);
                return req.CreateResponse(HttpStatusCode.InternalServerError, exMessage);
            }
        }
    }
}
