using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Tests
{
    public class SomeItem
    {
        public SomeItem(params string[] values)
        {
            Values = values.ToList();
        }

        public IList<string> Values { get; set; }

        public string GetKey(int depth)
        {
            if (Values == null) return null;

            if (Values.Count > depth)
                return Values[depth];

            return null;
        }

    }
}