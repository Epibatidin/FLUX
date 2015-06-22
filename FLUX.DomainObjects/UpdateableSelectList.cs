using System;
using System.Collections.Generic;
using System.Linq;

namespace FLUX.DomainObjects
{
    public class UpdateableSelectList<TValue, TList>
    {
        public IList<TList> Items { get; set; }

        public TValue Value { get; set; }

        public Func<TValue, TList, bool> IsSelectedEvaluator { get; set; }

        public TList GetSelectedItem()
        {
            return Items.First(r => IsSelectedEvaluator(Value, r));
        }

        public bool IsSelected(TList item)
        {
            return IsSelectedEvaluator(Value, item);
        }
    }
}