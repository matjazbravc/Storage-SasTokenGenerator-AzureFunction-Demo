using System;
using Microsoft.Azure.WebJobs.Description;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Ioc
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class InjectAttribute : Attribute
    {
    }
}
