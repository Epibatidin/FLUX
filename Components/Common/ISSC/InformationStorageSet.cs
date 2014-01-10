using System.Collections.Generic;
using System;

namespace Common.ISSC
{
    [System.Diagnostics.DebuggerStepThrough]
    public class InformationStorageSet 
    {
        protected Dictionary<int, object> ValuesDict;

        public InformationStorageSet()
        {
            ValuesDict = new Dictionary<int, object>();
        }

        public IEnumerable<KeyValuePair<int,object>> IterateValues()
        {
            return ValuesDict;
            //var iter = ValuesDict.GetEnumerator();
            //while(iter.MoveNext())
            //    yield return iter.Current;
        }

        public T getByKey<T>(int Key)
        {
            if (ValuesDict.ContainsKey(Key))
                return (T)ValuesDict[Key];
            else
                return default(T);
        }

        public object getByKey(int Key)
        {
            if (ValuesDict.ContainsKey(Key))
                return ValuesDict[Key];
            else
                return null;
        }

        public void setByKey<T>(int key, T value)
        {
            if (ValuesDict.ContainsKey(key))
                ValuesDict[key] = value;
            else
                ValuesDict.Add(key, value);
        }

        public void setByKey(int key, object value)
        {
            if (ValuesDict.ContainsKey(key))
                ValuesDict[key] = value;
            else
                ValuesDict.Add(key, value);
        }

        public IEnumerable<int> GetKeys()
        {
            return ValuesDict.Keys;
        }

        public bool Update<T>(int key, Func<T, T> Func) where T : class
        {
            if (Func == null) return false;

            if (ValuesDict.ContainsKey(key))
            {
                var value = ValuesDict[key] as T;
                value = Func(value);
                ValuesDict[key] = Func(value);
                return true;
                //if (value == null)
                //{
                //    ValuesDict.Remove(key);
                //    return false;
                //}
                //else
                //{
                //    ValuesDict[key] = Func(value);
                //    return true;
                //}                
            }
            return false;

        }
    }
}
