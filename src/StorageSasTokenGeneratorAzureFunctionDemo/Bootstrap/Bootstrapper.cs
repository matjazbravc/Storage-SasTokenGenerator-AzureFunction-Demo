using Autofac;
using StorageSasTokenGeneratorAzureFunctionDemo.Services.Ioc;
using StorageSasTokenGeneratorAzureFunctionDemo.Services.Modules;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Bootstrap
{
    public class Bootstrapper : IBootstrapper
    {
        public Module[] CreateModules()
        {
            return new Module[]
            {
                new ServicesModule()
            };
        }
    }
}