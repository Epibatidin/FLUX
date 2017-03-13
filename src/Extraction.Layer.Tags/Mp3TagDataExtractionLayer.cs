using Extraction.Interfaces.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extraction.Interfaces;

namespace Extraction.Layer.Tags
{
    public class Mp3TagDataExtractionLayer : IDataExtractionLayer
    {
        public Mp3TagDataExtractionLayer(IMp3TagVersionResolver tagVersionResolver)
        {

        }


        public void Execute(ExtractionContext extractionContext, UpdateObject updateObject)
        {
            foreach(var virtualFile in extractionContext.SourceValues)
            {
                var stream = extractionContext.StreamReader.OpenStreamForReadAccess(virtualFile);
            }
            
        }
    }
}
