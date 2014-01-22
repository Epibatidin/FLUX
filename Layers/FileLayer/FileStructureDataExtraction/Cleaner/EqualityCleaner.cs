using System.Collections.Generic;
using System.Linq;
using Common.StringManipulation;
using FileStructureDataExtraction.Config;
using FileStructureDataExtraction.Inferfaces;

namespace FileStructureDataExtraction.Cleaner
{
    /// <summary>
    /// WORKS AND IS BOUND SO WILL BE CALLED 
    /// </summary>
    public class EqualityCleaner : IMultiCleaner
    {
        private HashSet<string> _whiteList;

      
        private HashSet<string> StringsToDelete = null;

        private Dictionary<string, int> Repeatings = null;

        //-----------------------------------------------------------------------------------------------------------------------
        public EqualityCleaner(IWhiteListConfig _config)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            _whiteList = _config.WhiteList;
        }


        #region Equalities
        //-----------------------------------------------------------------------------------------------------------------------
        public List<PartedString> Filter(List<PartedString> StringList)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (StringList.Count == 1) return StringList;

            StringsToDelete = new HashSet<string>();
            Repeatings = new Dictionary<string, int>();

            foreach (var phrase in StringList)
            {
                foreach (string word in phrase)
                {
                    if (!_whiteList.Contains(word))
                    {
                        TellTheDict(word);
                    }
                }
            }

            foreach (var key in Repeatings.Keys)
            {
                if (Repeatings[key] >= StringList.Count())
                {
                    StringsToDelete.Add(key);
                }
            }
            foreach (var phrase in StringList)
            {
                SeekAndDestroy(phrase);
            }
            return StringList;
        }

        //-----------------------------------------------------------------------------------------------------------------------
        private void TellTheDict(string s)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (Repeatings.ContainsKey(s))
            {
                Repeatings[s]++;
            }
            else
            {
                Repeatings.Add(s, 1);
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------
        private void SeekAndDestroy(PartedString ps)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            for (int i = 0; i < ps.Count; i++)
            {
                if (StringsToDelete.Contains(ps[i]))
                {
                    ps.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion
    }
}