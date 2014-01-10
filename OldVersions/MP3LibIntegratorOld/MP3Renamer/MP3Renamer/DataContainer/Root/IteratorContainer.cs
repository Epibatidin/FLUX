using System;
using System.Collections.Generic;
using System.Web;
using MP3Renamer.DataContainer.EntityInterfaces;
using System.Reflection;
using System.Linq;

namespace MP3Renamer.DataContainer.Root
{
    public class IteratorContainer
    {
        public Dictionary<string, object> Data { get; private set; }
        private Dictionary<Type, PropertyInfo[]> TypeCache;
        
        public bool Stop {get;set;}

        public IteratorContainer()
        {
            Data = new Dictionary<string, object>();
            TypeCache = new Dictionary<Type, PropertyInfo[]>();
        }

        [System.Diagnostics.DebuggerStepThrough]
        private PropertyInfo[] GetProperties<T>()
        {
            Type t = typeof(T);

            if (TypeCache.ContainsKey(t))
            {
                return TypeCache[t];
            }
            else
            {
                var props = t.GetProperties();
                TypeCache.Add(t,props);
                return props;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void SetData<T>(T value)
        {
            var props = GetProperties<T>();
            string typeName = typeof(T).Name;
            foreach (var prop in props)
            {
                string key = typeName + "." + prop.Name;

                object prop_Value = prop.GetValue(value,null);

                if(prop_Value != null)
                    RefreshValue(key, prop_Value);
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private void RefreshValue(string key ,object value)
        {
            if(Data.ContainsKey(key))
                Data[key] = value;
            else 
                Data.Add(key, value);
        }
                      

       
    }
}