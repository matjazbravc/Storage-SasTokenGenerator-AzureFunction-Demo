using Autofac;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Ioc
{
    public interface IBootstrapper
    {
        Module[] CreateModules();
    }
}