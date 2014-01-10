using System.Collections.Generic;
using System.Text.RegularExpressions;
using MP3Renamer.Models.DataContainer;

namespace MP3Renamer.Models.Extraction.Cleaner
{
    public class InternetStuffCleaner : IFilter
    {
        private static string isInternetstuff = @"^(www.)?([a-z]*[.])(com|de|org|to|net)$";

        #region InternetStuff
        //-----------------------------------------------------------------------------------------------------------------------
        public IStringPartManager Filter(IStringPartManager RawManager)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            var parts = RawManager.RawDataParts;

            for (int i = 0; i < parts.Count; i++)
            {
                if (Regex.IsMatch(parts[i], isInternetstuff, RegexOptions.IgnoreCase))
                {
                    parts.RemoveAt(i);
                    i--;
                }
            }
            RawManager.RawDataParts = parts;
            RawManager.SplitByDot = true;
            return RawManager;
        }
        #endregion


    }
}