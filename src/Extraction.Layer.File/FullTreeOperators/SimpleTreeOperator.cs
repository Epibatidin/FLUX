using DataStructure.Tree.Iterate;
using Extraction.Interfaces;
using Extraction.Layer.File.Interfaces;
using System.Collections.Generic;

namespace Extraction.Layer.File.FullTreeOperators
{
    public class SimpleTreeOperator : IFullTreeOperator
    {
        IEnumerable<IPartedStringOperation> _simpleOperations;

        public SimpleTreeOperator(IEnumerable<IPartedStringOperation> simpleOperations )
        {
            _simpleOperations = simpleOperations;
        }

        public void Operate(TreeByKeyAccessor treeAccessor)
        {
            foreach (var item in TreeIterator.IterateDepthGetTreeItems(treeAccessor.Tree))
            {
                foreach (var operation in _simpleOperations)
                {
                    operation.Operate(item.Value.LevelValue);
                }
            }
        }
    }
}
