using Newtonsoft.Json;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Models
{
    public class StorageAccess
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
