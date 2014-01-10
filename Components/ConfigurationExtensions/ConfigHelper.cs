using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationExtensions
{
    public static class ConfigHelper
    {
        public static HashSet<string> CommaSeparatedStringToHashSet(string source)
        {
            var items = source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToLowerInvariant());

            HashSet<string> result = new HashSet<string>();

            foreach (var item in items)
            {
                if(!result.Contains(item))
                    result.Add(item);
            }
            return result;
        }
    }
}
