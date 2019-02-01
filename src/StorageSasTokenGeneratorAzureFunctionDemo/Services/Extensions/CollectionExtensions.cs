using System.Collections.Concurrent;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
	public static class CollectionExtensions
	{
		public static void Clear<T>(this ConcurrentBag<T> self)
		{
			while (self.Count > 0)
			{
			    self.TryTake(out _);
			}
		}
	}
}
