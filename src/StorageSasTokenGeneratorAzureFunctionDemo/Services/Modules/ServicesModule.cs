using Autofac;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using StorageSasTokenGeneratorAzureFunctionDemo.Services.Logging;
using StorageSasTokenGeneratorAzureFunctionDemo.Services.Logging.Serilog.Sinks;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var loggingStorageTableName = CloudConfigurationManager.GetSetting("Logging.Storage.TableName");
            var storageConnectingString = CloudConfigurationManager.GetSetting("StorageAccount.ConnectionString");
            var storageAccount = CloudStorageAccount.Parse(storageConnectingString);
            builder.Register(c => new SerilogToAzureTableStorage(nameof(ServicesModule), storageAccount, loggingStorageTableName)).As<ILog>();
            builder.RegisterType<StorageSasTokenGenerator>().As<IStorageSasTokenGenerator>();
        }
    }
}
