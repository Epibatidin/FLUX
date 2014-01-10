using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.FileIO;
using MP3Renamer.Filter.Algos;
using MP3Renamer.Filter.Interfaces;

namespace MP3Renamer.Filter.Cleaner.SubrootWorkunit
{
    public class SubrootOneStepUpCleaner :  ISubrootWorkunit , IExecuteable
    {
        public ISubRoot Workunit { get; set; }

        public void Execute()
        {
            // root data 
            OneStepUpCleaner algo = new OneStepUpCleaner();
            algo.AddOneStepUp(FileManager.Get.Current.Name);
            algo.AddOneStepUp(Workunit.Name);

            for (int i = 0; i < Workunit.Leafs.Count; i++)
            {
                Workunit.Leafs[i].StringManager = algo.Filter(Workunit.Leafs[i].StringManager);
            }
        }
    }
}
