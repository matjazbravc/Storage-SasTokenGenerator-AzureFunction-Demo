using System.ComponentModel;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Enums
{
    public enum EncodingType
    {
        [Description("gzip")]
        Gzip,
        [Description("deflate")]
        Deflate
    }
}
