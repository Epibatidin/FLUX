using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP3Renamer.Models.Extraction;
using MP3Renamer.Models.DataContainer.EntityInterfaces;
using MP3Renamer.Models.Helper;


namespace MP3Renamer.Models.Extraction
{
    public class LeafDataExtractor : FilterManager, ILeafExtractionHelper
    {
        ISubRoot SubRoot;
        
        public void WorkUnit(ISubRoot workunit)
        {
            SubRoot = workunit;
        }

        public ISubRoot execute()
        {
            if (SubRoot == null) return null;

            Track();
            
            Name();

            return SubRoot;
        }


        private void Track()
        {
            byte maxCDCount = 0;
            for (int current = 0; current < SubRoot.Leafs.Count; current++)
            {
                List<short> allNumbers = new List<short>();
                var parts = SubRoot.Leafs[current].StringManager.RawDataParts;

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

                SubRoot.Leafs[current].StringManager.RawDataParts = parts;

                Int16? track = allNumbers.OrderBy(c => c).FirstOrDefault();
                // ordne die zahlen der größe nach und hoffe das die kleinste zahl das track ist 
                byte assumedCdCount = 0;
                if (track.HasValue)
                {                    
                    if (track > 99)
                    {
                        var s = track.ToString();

                        Byte.TryParse(s[0] + "", out assumedCdCount);
                    }
                    SubRoot.Leafs[current].assumedCDCount(assumedCdCount);
                    SubRoot.Leafs[current].Number = (byte)track;
                }
                if (maxCDCount < assumedCdCount)
                    maxCDCount = assumedCdCount;
            }
            SubRoot.CDCount = maxCDCount;
        }

        private void Name()
        {
            for (int current = 0; current < SubRoot.Leafs.Count; current++)
            {
                SubRoot.Leafs[current].Name = SubRoot.Leafs[current].StringManager.Join(' ');
            }
        }
    }
}