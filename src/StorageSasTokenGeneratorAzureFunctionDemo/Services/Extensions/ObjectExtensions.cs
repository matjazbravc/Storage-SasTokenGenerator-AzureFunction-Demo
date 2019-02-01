using System;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object instance) where T : class
        {
            if (instance == null)
            {
                return default(T);
            }
            if (!(instance is T casted))
            {
                throw new ArgumentException($"Cannot cast '{instance.GetType().FullName}' to '{typeof(T).FullName}'.");
            }
            return casted;
        }
    }
}