using Extraction.Interfaces.Layer;
using Extraction.Interfaces;
using Extraction.Layer.Tags.Interfaces;

namespace Extraction.Layer.Tags
{
    public class Mp3TagDataExtractionLayer : IDataExtractionLayer
    {
        IMp3TagVersionResolver _tagVersionResolver;

        public Mp3TagDataExtractionLayer(IMp3TagVersionResolver tagVersionResolver)
        {
            _tagVersionResolver = tagVersionResolver;
        }


        public void Execute(ExtractionContext extractionContext, UpdateObject updateObject)
        {
            updateObject.UpdateData(new TagTreeByKeyAccessor());
            
            foreach (var virtualFile in extractionContext.SourceValues)
            {
                var stream = extractionContext.StreamReader.OpenStreamForReadAccess(virtualFile);

                var tagReader = _tagVersionResolver.ResolverTagReader(stream);

                tagReader.ReadAllTagData(stream);
            }
            
        }
    }
}
