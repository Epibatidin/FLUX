using System.Collections.Generic;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;
using Extraction.Layer.File.Config;
using FileStructureDataExtraction.Cleaner.CursesRepair;

namespace Extraction.Layer.File.Cleaner
{
    public class BlacklistCleaner : ICleaner
    {
        // TODO       
        IBlackListConfig _config;
        List<ICursesRepair> Curses;

        public BlacklistCleaner(IBlackListConfig config)
        {
            init(config);
        }

        private void init(IBlackListConfig config)
        {
            _config = config;
            Curses = new List<ICursesRepair>();
            if (_config.RepairCurses)
            {
                Curses.Add(new ShitRepair());
                Curses.Add(new FuckRepair());
            }
        }

        public PartedString Filter(PartedString ToFilter)
        {
            for (int i = 0; i < ToFilter.Count; i++)
            {
                if (_config.BlackList.Contains(ToFilter[i]))
                {
                    ToFilter.RemoveAt(i);
                    i--;
                }
                else
                {
                    string dummy = "";
                    foreach (var curse in Curses)
                    {
                        dummy = curse.TryFix(ToFilter[i]);
                        if (curse.Fixed)
                        {
                            ToFilter[i] = dummy;
                            break;
                        }
                    }
                }
            }
            return ToFilter;
        }
    }
}