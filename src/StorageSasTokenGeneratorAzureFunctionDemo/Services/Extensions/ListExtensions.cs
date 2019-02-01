using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
	public static class ListExtensions
	{
		public static T GetRandomElement<T>(this IEnumerable<T> list)
		{
			return list.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
		}

		public static List<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount)
		{
			return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
		}

		public static List<DateTime> SortAscending(this List<DateTime> list)
		{
			list.Sort((a, b) => a.CompareTo(b));
			return list;
		}

		public static List<DateTime> SortDescending(this List<DateTime> list)
		{
			list.Sort((a, b) => b.CompareTo(a));
			return list;
		}

		/// <summary>
		/// Splits a <see cref="List{T}"/> into multiple chunks.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">The list to be chunked.</param>
		/// <param name="chunkSize">The size of each chunk.</param>
		/// <returns>A list of chunks.</returns>
		public static List<List<T>> SplitIntoChunks<T>(this List<T> list, int chunkSize)
		{
			if (chunkSize <= 0)
			{
				throw new ArgumentException("chunkSize must be greater than 0.");
			}

			var retVal = new List<List<T>>();
			var index = 0;
			while (index < list.Count)
			{
				var count = list.Count - index > chunkSize ? chunkSize : list.Count - index;
				retVal.Add(list.GetRange(index, count));
				index += chunkSize;
			}
			return retVal;
		}
	}
}
