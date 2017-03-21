using System;
using System.Reflection;

namespace Extension.Test
{
    public class MyAutofixture
    {
        public int Seed { get; set; }

        public MyAutofixture()
        {
            Seed = 12;
        }

        public TType Build<TType>() where TType : class, new()
        {
            var properties = typeof(TType).GetProperties(); 
                
            var result = new TType();

            foreach (var property in properties)
            {
                var value = GenerateRandomValueByType(property.PropertyType);

                property.SetValue(result, value);
            }
            return result;
        }

        private object GenerateRandomValueByType(Type propertyType)
        {
            if (propertyType == typeof(string))
                return Guid.NewGuid().ToString();

            if (propertyType == typeof(int))
                return Seed++;

            if (propertyType == typeof(Guid))
                return Guid.NewGuid();

            if (propertyType.IsArray) return null;

            return null;
        }

    }
}
