using System.Collections.Generic;
using System.Configuration;
using ConfigurationExtensions.Interfaces;

namespace ConfigurationExtensions
{
    public class GenericElementCollection<T> : ConfigurationElementCollection, IEnumerable<T> 
        where T : ConfigurationElement, IKeyedElement, new()
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)element).Key;
        }
      
        public new IEnumerator<T> GetEnumerator()
        {
            return new CollectionIterator<T>(this);
        }


        public T Item(int i)
        {
            return (T)BaseGet(i);
        }

        public T Item(string key)
        {
            return (T)BaseGet(key);
        }
    }
}
