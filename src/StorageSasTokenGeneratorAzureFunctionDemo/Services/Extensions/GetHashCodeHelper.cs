using System.Collections.Generic;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
	/// <summary>
	/// Provides extensions for producing hash codes to support overrides
	/// http://blog.somewhatabstract.com/2012/03/12/a-little-help-with-gethashcode/
	/// </summary>
	public static class GetHashCodeHelper
	{
		public static readonly int InitialHash = 17; // Prime number
		private const int MULTIPLIER = 37;

		/// <summary>
		/// Combines the given value's hash code with an existing hash code value.
		/// </summary>
		/// <typeparam name="T">The type of the value being hashed.</typeparam>
		/// <param name="code">The previous hash code.</param>
		/// <param name="value">The value to hash.</param>
		/// <returns>The new hash code.</returns>
		public static int HashWith<T>(this int code, T value)
		{
			return Hash(code, value);
		}

		/// <summary>
		/// Combines the hash codes for the elements in the given sequence with an
		/// existing hash code and returns the hash code.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="sequence">The sequence.</param>
		/// <returns>The new hash code.</returns>
		public static int HashWithContentsOf<T>(this int code, IEnumerable<T> sequence)
		{
			foreach (T element in sequence)
			{
				code = code.HashWith(element);
			}
			return code;
		}

		/// <summary>
		/// Combines the hash code for the given value to an existing hash code
		/// and returns the hash code.
		/// </summary>
		/// <param name="code">The previous hash value.</param>
		/// <param name="value">The value to hash.</param>
		/// <returns>The new hash code.</returns>
		private static int Hash<T>(int code, T value)
		{
			var hash = 0;
			if (value != null)
			{
				hash = value.GetHashCode();
			}
			unchecked
			{
				code = (code * MULTIPLIER) + hash;
			}
			return code;
		}
	}
}

