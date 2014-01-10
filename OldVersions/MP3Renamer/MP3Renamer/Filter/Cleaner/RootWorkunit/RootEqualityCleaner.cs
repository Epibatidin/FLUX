using System.Linq;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Algos;
using MP3Renamer.Filter.Interfaces;

namespace MP3Renamer.Filter.Cleaner.RootWorkunit
{
    public class RootEqualityCleaner :  IRootWorkunit , IExecuteable
    {
        public IRoot Workunit { get; set; }
        

        public void Execute()
        {
            // hier den extraction algo anstoßen 
            EqualityCleaner algo = new EqualityCleaner();

            var list = Workunit.SubRoots.Select(c => c.StringManager).ToList();

            list = algo.Filter(list);

            for (int i = 0; i < list.Count; i++)
            {
                Workunit.SubRoots[i].StringManager = list[i];
            }
        }
    }
}