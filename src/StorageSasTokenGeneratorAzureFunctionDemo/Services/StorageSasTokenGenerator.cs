using System;
using System.Net;
using System.Net.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Serilog;
using StorageSasTokenGeneratorAzureFunctionDemo.Models;
using StorageSasTokenGeneratorAzureFunctionDemo.Services.Logging;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services
{
    // An HTTP trigger Azure Function that returns a SAS (Shared Access Signatures) token
    // for Azure Storage for the specified container/blob.
    public class StorageSasTokenGenerator : IStorageSasTokenGenerator
    {
        private readonly ILog _log;
        private readonly HttpRequestMessageHelper _httpRequestMessageHelper;

        public StorageSasTokenGenerator(ILog log, HttpRequestMessageHelper httpRequestMessageHelper)
        {
            _log = log;
            _log.Debug();
            _httpRequestMessageHelper = httpRequestMessageHelper;
        }
        
        public HttpResponseMessage GetStorageAccessResponse(HttpRequestMessage req)
        {
            var storageConnectingString = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
            var storageAccount = CloudStorageAccount.Parse(storageConnectingString);
            if (storageAccount == null)
            {
                return _httpRequestMessageHelper.CreateBadRequestResponse(req, new
                {
                    error = "StorageAccount is null"
                });
            }

            HttpResponseMessage responseMessage;
            try
            {
                if (!_httpRequestMessageHelper.IsValidContentType(req))
                {
                    return _httpRequestMessageHelper.CreateBadRequestResponse(req, new
                    {
                        error = "Request has invalid content type"
                    });
                }

                _log.Info("Started generating SAS token");

                if (_httpRequestMessageHelper.IsContentCompressed(req))
                {
                    req.Content = _httpRequestMessageHelper.DecompressHttpContent(req);
                }

                dynamic data = req.Content.ReadAsAsync<object>().Result;
                if (data.container == null)
                {
                    return _httpRequestMessageHelper.CreateBadRequestResponse(req, new
                    {
                        error = "Specify value for 'container'"
                    });
                }

                bool success = Enum.TryParse(data.permissions.ToString(), out SharedAccessBlobPermissions permissions);
                if (!success)
                {
                    return _httpRequestMessageHelper.CreateBadRequestResponse(req, new
                    {
                        error = "Invalid value for 'permissions'"
                    });
                }

                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(data.container.ToString());

                var sasToken = data.blobName != null ?
                    GetBlobSasToken(container, data.blobName.ToString(), permissions) :
                    GetContainerSasToken(container, permissions);

                // Response:
                // token - SAS token, including a leading "?"
                // uri - Resource URI with token appended as query string
                var result = new StorageAccess
                {
                    Token = sasToken,
                    Uri = data.blobName == null ? container.Uri + sasToken : container.Uri + "/" + data.blobName.ToString() + sasToken
                };

                responseMessage = _httpRequestMessageHelper.CreateOkResponse(req, result);

                _log.Info("Generating SAS token has ended");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                responseMessage = req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return responseMessage;
        }

        private static string GetBlobSasToken(CloudBlobContainer container, string blobName, SharedAccessBlobPermissions permissions, string policyName = null)
        {
            string sasBlobToken;

            // Get a reference to a blob within the container.
            // Note that the blob may not exist yet, but a SAS can still be created for it.
            var blob = container.GetBlockBlobReference(blobName);

            if (policyName == null)
            {
                var adHocSas = CreateAdHocSasPolicy(permissions);

                // Generate the shared access signature on the blob, setting the constraints directly on the signature.
                sasBlobToken = blob.GetSharedAccessSignature(adHocSas);
            }
            else
            {
                // Generate the shared access signature on the blob. In this case, all of the constraints for the
                // shared access signature are specified on the container's stored access policy.
                sasBlobToken = blob.GetSharedAccessSignature(null, policyName);
            }

            return sasBlobToken;
        }

        private static string GetContainerSasToken(CloudBlobContainer container, SharedAccessBlobPermissions permissions, string storedPolicyName = null)
        {
            string sasContainerToken;

            // If no stored policy is specified, create a new access policy and define its constraints.
            if (storedPolicyName == null)
            {
                var adHocSas = CreateAdHocSasPolicy(permissions);

                // Generate the shared access signature on the container, setting the constraints directly on the signature.
                sasContainerToken = container.GetSharedAccessSignature(adHocSas, null);
            }
            else
            {
                // Generate the shared access signature on the container. In this case, all of the constraints for the
                // shared access signature are specified on the stored access policy, which is provided by name.
                // It is also possible to specify some constraints on an ad-hoc SAS and others on the stored access policy.
                // However, a constraint must be specified on one or the other; it cannot be specified on both.
                sasContainerToken = container.GetSharedAccessSignature(null, storedPolicyName);
            }

            return sasContainerToken;
        }

        private static SharedAccessBlobPolicy CreateAdHocSasPolicy(SharedAccessBlobPermissions permissions)
        {
            // Create a new access policy and define its constraints.
            // Note that the SharedAccessBlobPolicy class is used both to define the parameters of an ad-hoc SAS, and 
            // to construct a shared access policy that is saved to the container's shared access policies. 
            return new SharedAccessBlobPolicy
            {
                // Set start time to five minutes before now to avoid clock skew.
                SharedAccessStartTime = DateTime.UtcNow.AddHours(-1),
                SharedAccessExpiryTime = DateTime.UtcNow.AddDays(1),
                Permissions = permissions
            };
        }
    }
}
