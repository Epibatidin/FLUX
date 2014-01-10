using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP3Renamer.DataContainer;

namespace MP3Renamer.Filter.Algos
{
    public class BlacklistCleaner
    {
        private static HashSet<string> Blacklist = new HashSet<string>()
        {
            "ftyh", "promo" , "cdm" 
        };

        public IStringPartManager Filter(IStringPartManager ToFilter)
        {
            for (int i = 0; i < ToFilter.RawDataParts.Count; i++)
            {
                if (Blacklist.Contains(ToFilter.RawDataParts[i]))
                {
                    ToFilter.RawDataParts.RemoveAt(i);
                    i--;
                }


            }
            return ToFilter;
        }

        private string CheckShit(string posShit)
        {
            // es fängt mit s an 
            if (posShit[0] == 's')
            {
                if (posShit.Length >= 4)
                {
                    if (posShit.Substring(1, 3).Contains('*'))
                    {
                        posShit = "shit" + posShit.Remove(0, 4);
                    }
                }
            }
            return posShit;
        }


        private string CheckFuck(string posFuck)
        {
            // es fängt mit s an 
            if (posFuck[0] == 'f')
            {
                if (posFuck.Length >= 4)
                {
                    if (posFuck.Substring(1, 3).Contains('*'))
                    {
                        posFuck = "fuck" + posFuck.Remove(0, 4);
                    }
                }
            }
            return posFuck;
        }



    }
}