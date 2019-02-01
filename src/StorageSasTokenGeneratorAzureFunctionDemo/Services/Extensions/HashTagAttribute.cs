using System;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
	public class HashTagAttribute : Attribute
	{
		public HashTagAttribute(string value)
		{
			Value = value;
		}

		public string Value { get; }
	}
}
