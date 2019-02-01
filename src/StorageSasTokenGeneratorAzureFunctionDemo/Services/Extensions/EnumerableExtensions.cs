using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
	/// <summary>
	///     Extension methods for IEnumerable
	/// </summary>
	public static class EnumerableExtensions
	{
	    [DebuggerStepThrough]
        public static bool IsAny<T>(this IEnumerable<T> enumerable)
        {
            return enumerable?.Any() == true;
        }

	    [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable?.Any() != true;
        }

	    [DebuggerStepThrough]
        public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            if (list1 == null || list2 == null)
            {
                return false;
            }
            var deletedItems = list1.Except(list2).Any();
            var newItems = list2.Except(list1).Any();
            return !newItems && !deletedItems;
        }

	    [DebuggerStepThrough]
        public static Dictionary<int, T> ToIndexedDictionary<T>(this IEnumerable<T> self)
        {
			return self.ToIndexedDictionary(t => t);
		}

	    [DebuggerStepThrough]
		public static Dictionary<int, T> ToIndexedDictionary<TS, T>(this IEnumerable<TS> self, Func<TS, T> valueSelector)
		{
			var index = -1;
			return self.ToDictionary(t => ++index, valueSelector);
		}

        /// <summary>
        /// Selects a random element from an Enumerable with only one pass (O(N) complexity); 
        /// It contains optimizations for arguments that implement ICollection{T} by using the 
        /// Count property and the ElementAt LINQ method. The ElementAt LINQ method itself contains 
        /// optimizations for <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="self">Sequence parameter</param>
        [DebuggerStepThrough]
        public static T SelectRandom<T>(this IEnumerable<T> self)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return self.SelectRandom(random);
        }

        /// <summary>
        /// Selects a random element from an Enumerable with only one pass (O(N) complexity); 
        /// It contains optimizations for arguments that implement ICollection{T} by using the 
        /// Count property and the ElementAt LINQ method. The ElementAt LINQ method itself contains 
        /// optimizations for <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="self">Sequence parameter</param>
        /// <param name="random">Random parameter</param>
        public static T SelectRandom<T>(this IEnumerable<T> self, Random random)
        {
            // Optimization for ICollection<T>
            if (self is ICollection<T> collection)
            {
                return collection.ElementAt(random.Next(collection.Count));
            }
            var count = 1;
            var selected = default(T);
            foreach (var element in self)
            {
                if (random.Next(count++) == 0)
                {
                    // Select the current element with 1/count probability
                    selected = element;
                }
            }
            return selected;
        }

        /// <summary>
        /// Randomizes a <paramref name="self"/>.
        /// </summary>
        /// <param name="self">Sequence parameter</param>
        [DebuggerStepThrough]
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> self)
        {
            return Randomize(self, new Random(Guid.NewGuid().GetHashCode()));
        }

        /// <summary>
        /// Randomizes a <paramref name="self"/>.
        /// </summary>
        /// <param name="self">Sequence parameter</param>
        /// <param name="random">Random parameter</param>
        [DebuggerStepThrough]
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> self, Random random)
        {
            var buffer = self.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = random.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}