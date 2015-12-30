using System;
using System.Collections.Generic;

namespace Extension.IEnumerable
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (dict.ContainsKey(key))
                return dict[key];
            
            var result = new TValue();
            dict.Add(key, result);
            return result;
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey Key,
            Func<TValue> constructor)
        {
            if (dict.ContainsKey(Key))
                return dict[Key];
            else
            {
                var result = constructor();
                dict.Add(Key, result);
                return result;
            }
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey Key,
            Func<TKey, TValue> Constructor)
        {
            if (dict.ContainsKey(Key))
                return dict[Key];
            else
            {
                var result = Constructor(Key);
                dict.Add(Key, result);
                return result;
            }
        }
    }
}
