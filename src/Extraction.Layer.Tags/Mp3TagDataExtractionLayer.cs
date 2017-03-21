using Extraction.Interfaces.Layer;
using Extraction.Interfaces;
using Extraction.Layer.Tags.Interfaces;
using System.Collections.Generic;
using Extraction.Layer.Tags.DomainObjects;

namespace Extraction.Layer.Tags
{
    public class Mp3TagDataExtractionLayer : IDataExtractionLayer
    {
        IMp3TagVersionResolver _tagVersionResolver;
        ITagSongFactory _songFactory;

        public Mp3TagDataExtractionLayer(IMp3TagVersionResolver tagVersionResolver, ITagSongFactory songFactory)
        {
            _tagVersionResolver = tagVersionResolver;
            _songFactory = songFactory;
        }
        
        public void Execute(ExtractionContext extractionContext, UpdateObject updateObject)
        {
            var accessor = new TagTreeByKeyAccessor();

            updateObject.UpdateData(accessor);

            foreach (var virtualFile in extractionContext.SourceValues)
            {
                var stream = extractionContext.StreamReader.OpenStreamForReadAccess(virtualFile);

                var tagReader = _tagVersionResolver.ResolverTagReader(stream);

                var tagData = tagReader.ReadAllTagData(stream);

                var song = _songFactory.Build(tagData);
                song.Id = virtualFile.ID;

                accessor.Add(song);
            }            
        }
    }
}
