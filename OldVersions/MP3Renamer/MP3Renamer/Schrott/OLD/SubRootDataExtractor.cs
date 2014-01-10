using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using MP3Renamer.Models.DataContainer;
using MP3Renamer.Models.DataContainer.EntityInterfaces;
using MP3Renamer.Models.Helper;


namespace MP3Renamer.Models.Extraction
{ 
    public class SubRootDataExtractor : FilterManager, ISubRootExtractionHelper
    {
        public IRoot Root{get ; private set;}

        //-----------------------------------------------------------------------------------------------------------------------
        public SubRootDataExtractor() { }
        //-----------------------------------------------------------------------------------------------------------------------
        
        
        //-----------------------------------------------------------------------------------------------------------------------
        public void WorkUnit(IRoot workunit)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            this.Root = workunit;
        }


        //-----------------------------------------------------------------------------------------------------------------------
        public IRoot execute()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (Root == null) return null;

            Year();

            Name();
                                   
            return Root;
        }




        //-----------------------------------------------------------------------------------------------------------------------
        private void Year()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (FilterIsSet((byte)SubRootExtractLevel.Year))
            {
                Root.SubRoots = ExtractYear(Root.SubRoots);            
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------
        private List<ISubRoot> ExtractYear(List<ISubRoot> SubRoots)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            for (int current = 0; current < SubRoots.Count(); current++)
            {
                List<short> allNumbers = new List<short>();
                var parts = SubRoots[current].StringManager.RawDataParts;

                for (int i = 0; i < parts.Count(); i++)
                {
                    var numbers = NumberExtractor.ExtractNumbers(parts[i]);
                    if (numbers != null && numbers.Count() != 0)
                    {
                        allNumbers.AddRange(numbers);
                        parts.RemoveAt(i);
                        i--;
                    }
                }

                SubRoots[current].StringManager.RawDataParts = parts;

                Int16? year = allNumbers.OrderByDescending(c => c).FirstOrDefault();
                // ordne die zahlen der größe nach und hoffe das die größe zahl das jahr ist 

                if (year.HasValue)
                {
                    SubRoots[current].Year = year.Value;
                }
            }
            return SubRoots;
        }

        //-----------------------------------------------------------------------------------------------------------------------
        private void Name()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (FilterIsSet((byte)SubRootExtractLevel.Name))
            {
                Root.SubRoots = ExtractName(Root.SubRoots);
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private List<ISubRoot> ExtractName(List<ISubRoot> SubRoots)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            for (int current = 0; current < SubRoots.Count; current++)
            {
                var parts = SubRoots[current].StringManager.RawDataParts;

                if (parts != null && parts.Count != 0)
                {
                    SubRoots[current].Name = SubRoots[current].StringManager.Join(' ');
                }
                else
                {
                    SubRoots[current].Name = Root.Name;
                }
            }
            return SubRoots;
        }    
    }
} 