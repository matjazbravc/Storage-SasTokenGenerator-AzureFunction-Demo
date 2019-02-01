using System.IO;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
    public static class StreamExtensions
    {
        public static string GetString(this Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
