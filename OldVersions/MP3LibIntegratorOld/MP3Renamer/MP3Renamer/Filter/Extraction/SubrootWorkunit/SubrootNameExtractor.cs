using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Interfaces;
using MP3Renamer.Helper;

namespace MP3Renamer.Filter.Extraction.SubrootWorkunit
{
    public class SubrootNameExtractor : ISubrootWorkunit , IExecuteable
    {
        public ISubRoot Workunit { get; set; }        

        public void Execute()
        {
            if(EnumerableHelper.IsNotNullOrEmpty(Workunit.StringManager.RawDataParts))
                Workunit.Name = Workunit.StringManager.Join(' ');
        }

    }
}