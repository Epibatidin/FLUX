using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class Dictionary
    {
        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey Key) where TValue : new() 
        {
            if (dict.ContainsKey(Key))
                return dict[Key];
            else
            {
                var result = new TValue();
                dict.Add(Key, result);
                return result;
            }
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey Key, Func<TValue> Constructor)
        {
            if (dict.ContainsKey(Key))
                return dict[Key];
            else
            {
                var result = Constructor();
                dict.Add(Key, result);
                return result;
            }
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey Key, Func<TKey, TValue> Constructor)
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