using System.Collections.Generic;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetValue<T>(this IDictionary<string, object> dictionary, string key)
        {
            var result = default(T);
            if (dictionary.ContainsKey(key))
            {
                result = (T)dictionary[key];
            }
            return result;
        }

        public static T GetValue<T>(this IDictionary<int, object> dictionary, int key)
        {
            var result = default(T);
            if (dictionary.ContainsKey(key))
            {
                result = (T)dictionary[key];
            }
            return result;
        }

        public static void SetValue<T>(this IDictionary<string, T> dictionary, string key, T value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void SetValue<T>(this IDictionary<int, T> dictionary, int key, T value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static bool TryGetValueOrDefault<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            if (dictionary.TryGetValue(key, out var result) && result is T)
            {
                value = (T)result;
                return true;
            }
            value = default(T);
            return false;
        }
    }
}
