using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tree;
using Interfaces;

namespace TestHelpers
{
    public class TreeBuilder<ValueType, IDType> where ValueType : IUnique<IDType> , new()
    {
        public List<Func<IDType, int, int, ValueType>> Factories { get; set; }

        public TreeBuilder(IEnumerable<Func<IDType, int, int, ValueType>> _factories)
        {
            Factories = new List<Func<IDType, int, int, ValueType>>();
            Factories.AddRange(_factories);
        }

        private ItemBuilder<ValueType, IDType> _builder;

        public ItemBuilder<ValueType, IDType> Root(Action<ItemBuilder<ValueType, IDType>> _builderAction)
        {
            _builder = new ItemBuilder<ValueType, IDType>(0, Factories);
            _builderAction(_builder);
            return _builder;
        }

        public TreeItem<ValueType> AsTree()
        {
            return null;
        }

    }
}
