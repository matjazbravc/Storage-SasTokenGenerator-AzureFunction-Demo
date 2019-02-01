using System;
using System.ComponentModel;
using Microsoft.Azure;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services
{
	public static class CloudConfigurationReader
	{
		/// <summary>
		/// Generic method for reading Cloud/App settings
		/// https://stacktoheap.com/blog/2013/01/20/using-typeconverters-to-get-appsettings-in-net/
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">Setting name/key</param>
		/// <param name="defaultValue"></param>
		/// <returns>Result T</returns>
		public static T Read<T>(string name, T defaultValue = default(T))
		{
			var result = defaultValue;
			var value = CloudConfigurationManager.GetSetting(name);
			if (string.IsNullOrWhiteSpace(value))
			{
				return result;
			}
			try
			{
				var converter = TypeDescriptor.GetConverter(typeof(T));
				result = (T)converter.ConvertFromInvariantString(value);
			}
			catch (Exception)
			{
				result = defaultValue;
			}
			return result;
		}
	}
}