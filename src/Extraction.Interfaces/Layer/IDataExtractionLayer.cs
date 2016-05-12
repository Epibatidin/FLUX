
namespace Extraction.Interfaces.Layer
{
    public interface IDataExtractionLayer
    {
        void Execute(ExtractionContext extractionContext, UpdateObject updateObject);
    }
}
