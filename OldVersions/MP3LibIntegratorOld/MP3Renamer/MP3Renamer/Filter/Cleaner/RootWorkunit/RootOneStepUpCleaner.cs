using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP3Renamer.Filter.Interfaces;
using MP3Renamer.DataContainer.EntityInterfaces;
using MP3Renamer.Filter.Algos;

namespace MP3Renamer.Filter.Cleaner.RootWorkunit
{
    public class RootOneStepUpCleaner :  IRootWorkunit , IExecuteable
    {
        public IRoot Workunit { get; set; }

        public void Execute()
        {
            // root data 
            OneStepUpCleaner algo = new OneStepUpCleaner();
            algo.AddOneStepUp(Workunit.Name);

            for (int i = 0; i < Workunit.SubRoots.Count; i++)
            {
                Workunit.SubRoots[i].StringManager = algo.Filter(Workunit.SubRoots[i].StringManager);
            }
        }
    }
}