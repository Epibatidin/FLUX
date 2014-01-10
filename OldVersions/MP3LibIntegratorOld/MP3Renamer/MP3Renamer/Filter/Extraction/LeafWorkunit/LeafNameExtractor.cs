using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Interfaces;
using MP3Renamer.Helper;

namespace MP3Renamer.Filter.Extraction.LeafWorkunit
{
    public class LeafNameExtractor : ILeafWorkunit , IExecuteable
    {
        public ILeaf Workunit {get;set;}
     
        public void Execute()
        {
            if (EnumerableHelper.IsNotNullOrEmpty(Workunit.StringManager.RawDataParts))
                Workunit.Name = Workunit.StringManager.Join(' ');
        }
    }
}