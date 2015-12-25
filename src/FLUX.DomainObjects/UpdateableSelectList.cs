using System;
using System.Collections.Generic;
using System.Linq;

namespace FLUX.DomainObjects
{
    public class UpdateableSelectList<TValue, TListItem>
    {
        public IList<TListItem> Items { get; set; }

        public TValue Value { get; set; }

        public Func<TValue, TListItem, bool> IsSelectedEvaluator { get; set; }

        public TListItem GetSelectedItem()
        {
            return Items.First(r => IsSelectedEvaluator(Value, r));
        }

        public bool IsSelected(TListItem item)
        {
            return IsSelectedEvaluator(Value, item);
        }
    }
}