using Microsoft.Extensions.Internal;
using System.Collections.Generic;

namespace Extension.IEnumerable
{
    public class ObjectToDictionaryBuilder
    {
        public static IDictionary<string,string> ToDictionary<TType>(TType obj, params string[] exclude)
        {
            var helpers = PropertyHelper.GetVisibleProperties(typeof(TType));

            var result = new Dictionary<string, string>();

            foreach (var helper in helpers)
            {
                var value = helper.ValueGetter(obj);
                if (value == null) continue;

                var asString = value as string;
                if (asString == null)
                    asString = value.ToString();

                result.Add(helper.Name, asString);
            }
            return result;
        }

    }
}
