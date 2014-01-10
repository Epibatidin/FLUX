using System.Linq;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Algos;
using MP3Renamer.Filter.Interfaces;

namespace MP3Renamer.Filter.Cleaner.SubrootWorkunit
{
    public class SubrootEqualityCleaner :  ISubrootWorkunit , IExecuteable
    {
        public ISubRoot Workunit { get; set; }
        

        public void Execute()
        {
            // hier den extraction algo anstoßen 
            EqualityCleaner algo = new EqualityCleaner();

            var list = Workunit.Leafs.Select(c => c.StringManager).ToList();

            list = algo.Filter(list);

            for (int i = 0; i < list.Count; i++)
            {
                Workunit.Leafs[i].StringManager = list[i];
            }
        }
    }
}