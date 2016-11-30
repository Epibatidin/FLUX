using Extraction.Interfaces;

namespace FLUX.Interfaces
{
    public interface IExtractionContextBuilder
    {
        ExtractionContext Build();
        ExtractionContext BuildForPersistence();
    }
}
