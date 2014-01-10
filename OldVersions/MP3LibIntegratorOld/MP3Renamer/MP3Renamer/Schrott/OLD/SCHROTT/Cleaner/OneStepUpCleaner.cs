using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MP3Renamer.Models.DataContainer;
using System.Text;

namespace MP3Renamer.Models.Extraction.Cleaner
{
    public class OneStepUpCleaner : IFilter
    {
        private List<string> StepUp;
        public void AddOneStepUp(string OneStepUp)
        {
            if (StepUp == null)
                StepUp = new List<string>();
            
            StepUp.Add(OneStepUp.ToLower());
        }


        public IStringPartManager Filter(IStringPartManager ToFilter)
        {
            string rawdata = ToFilter.Join(' ');
            int pos = 0;

            foreach (string clean in StepUp)
            {
                pos = rawdata.IndexOf(clean);

                if (pos >= 0)
                {
                    rawdata = rawdata.Remove(pos, clean.Length);
                }
            }
            ToFilter.RawDataParts = ToFilter.Split(rawdata);

            return ToFilter;
        }

    }
}