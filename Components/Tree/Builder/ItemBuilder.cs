using System;
using System.Collections.Generic;
using Interfaces;
using Tree;

namespace TestHelpers
{
    public class ItemBuilder<ValueType, IDType> where ValueType : IUnique<IDType> ,new()
    {
        protected int _level;
        List<Func<IDType, int, int, ValueType>> _factories;
        int _count = 0;

        ItemBuilder<ValueType, IDType> _builder;

        public ItemBuilder(int level, List<Func<IDType, int, int, ValueType>> factories)
        {
            _level = level;
            _factories = factories;
        }

        public ItemBuilder<ValueType, IDType> Item()
        {
            _count = 1;
            return this;
        }

        public ItemBuilder<ValueType, IDType> Item(Action<ItemBuilder<ValueType, IDType>> builderAction)
        {
            _count = 1;
            _builder = new ItemBuilder<ValueType,IDType>(_level + 1, _factories);
            builderAction(_builder);
            return this;
        }

        public ItemBuilder<ValueType, IDType> Items(int count)
        {
            _count = count;
            return this;
        }


        public ItemBuilder<ValueType, IDType> Items(int count, Action<ItemBuilder<ValueType, IDType>> builderAction)
        {
            _count = count;
            _builder = new ItemBuilder<ValueType, IDType>(_level + 1, _factories);
            builderAction(_builder);
            return this;
        }

        public TreeItem<ValueType> AsTree()
        {
            var root = new TreeItem<ValueType>();            
            var childs = AsTree(root, default(IDType));
            return childs.GetChildren()[0];
        }

        protected TreeItem<ValueType> AsTree(TreeItem<ValueType> root, IDType rootID)
        {
            if (_count == 0) return null;

            var childs = new List<TreeItem<ValueType>>();

            for (int i = 0; i < _count; i++)
            {
                var value = new TreeItem<ValueType>()
                {
                    Value = _factories[_level](rootID, _level, i + 1), 
                    Level = _level
                };
                if (_builder != null)
                {
                    var subTree = _builder.AsTree(root, value.Value.ID);
                    if (subTree != null)
                        value.SetChildren(subTree.GetChildren());
                }
                childs.Add(value);
            }
            root.SetChildren(childs);           
            return root;
        }

        public IEnumerable<ValueType> AsEnumerable()
        {

            return null;
        }

    }
}
