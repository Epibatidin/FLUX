using System.Collections.Generic;
using MP3Renamer.FileIO;
using MP3Renamer.Models.Extraction;

namespace MP3Renamer.Models
{
    public class ExecutionManager
    {
        private CleaningCoordinator Cleaner;
        private ExtractionCoordinator Extracter;

        public ExecutionManager()
        {
            Cleaner = new CleaningCoordinator();
            Extracter = new ExtractionCoordinator();
        }

        private IEnumerable<AbstractCoordinator> Coordinators()
        {
            yield return Cleaner;
            yield return Extracter;
        }


        public void ExecuteAllFilter()
        {
            do
            {
                if (FileManager.Get.Current != null)
                {
                    foreach (var helper in Coordinators())
                    {
                        helper.Execute(FileManager.Get.Current);
                    }
                }
            }
            while (FileManager.Get.Next());
        }


    }
}