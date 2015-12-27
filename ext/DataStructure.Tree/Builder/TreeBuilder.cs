using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Builder
{
    public class TreeBuilder<TValueType, TIDType> where TValueType : IUnique<TIDType>, new()
    {
        public List<Func<TIDType, int, int, TValueType>> Factories { get; set; }

        public TreeBuilder(IEnumerable<Func<TIDType, int, int, TValueType>> _factories)
        {
            Factories = new List<Func<TIDType, int, int, TValueType>>();
            Factories.AddRange(_factories);
        }

        private ItemBuilder<TValueType, TIDType> _builder;

        public ItemBuilder<TValueType, TIDType> Root(Action<ItemBuilder<TValueType, TIDType>> builderAction)
        {
            _builder = new ItemBuilder<TValueType, TIDType>(0, Factories);
            builderAction(_builder);
            return _builder;
        }
    }
}
