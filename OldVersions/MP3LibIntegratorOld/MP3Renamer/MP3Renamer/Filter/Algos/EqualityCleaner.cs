using System.Collections.Generic;
using MP3Renamer.DataContainer;
using System.Linq;

namespace MP3Renamer.Filter.Algos
{
    public class EqualityCleaner
    {
        private readonly static HashSet<string> WhiteList = generateWhiteList();

        //-----------------------------------------------------------------------------------------------------------------------
        private static HashSet<string> generateWhiteList()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            HashSet<string> whitelist = new HashSet<string>();

            whitelist.Add("the");
            whitelist.Add("a");
            whitelist.Add("you");
            whitelist.Add("die");
            whitelist.Add("an");

            return whitelist;
        }

        private HashSet<string> StringsToDelete = null;

        private Dictionary<string, int> Repeatings = null;

        //-----------------------------------------------------------------------------------------------------------------------
        public EqualityCleaner()
        //-----------------------------------------------------------------------------------------------------------------------
        {
        }


        #region Equalities
        //-----------------------------------------------------------------------------------------------------------------------
        public List<IStringPartManager> Filter(List<IStringPartManager> StringList)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            if (StringList.Count == 1) return StringList;

            StringsToDelete = new HashSet<string>();
            Repeatings = new Dictionary<string, int>();

            foreach (var phrase in StringList)
            {
                foreach (string word in phrase.RawDataParts)
                {
                    if (!WhiteList.Contains(word))
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
                phrase.RawDataParts = SeekAndDestroy(phrase.RawDataParts);
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
        private List<string> SeekAndDestroy(List<string> parts)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            for (int i = 0; i < parts.Count(); i++)
            {
                if (StringsToDelete.Contains(parts[i]))
                {
                    parts.RemoveAt(i);
                    i--;
                }
            }
            return parts;
        }
        #endregion
    }
}