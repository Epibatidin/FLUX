using Extraction.Layer.File.Interfaces;

namespace Extraction.Layer.File.FullTreeOperators
{
    public class YearExtractionTreeOperator : IFullTreeOperator
    {
        private IYearExtractor _yearExtractor;

        public YearExtractionTreeOperator(IYearExtractor yearExtractor)
        {
            _yearExtractor = yearExtractor;
        }

        public void Operate(TreeByKeyAccessor treeAccessor)
        {
            foreach (var item in treeAccessor.Tree.Children)
            {
                _yearExtractor.Execute(item.Value);
            }
        }
    }
}
