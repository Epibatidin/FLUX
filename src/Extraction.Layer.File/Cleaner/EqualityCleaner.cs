//using System.Collections.Generic;
//using System.Linq;
//using Extraction.DomainObjects.StringManipulation;
//using Extraction.Interfaces;
//using Extraction.Layer.File.Config;

//namespace Extraction.Layer.File.Cleaner
//{
//    / <summary>
//    / WORKS AND IS BOUND SO WILL BE CALLED
//    / </summary>
//    public class EqualityCleaner : IMultiCleaner
//    {
//        private HashSet<string> _whiteList;


//        private HashSet<string> StringsToDelete = null;

//        private Dictionary<string, int> Repeatings = null;

//        -----------------------------------------------------------------------------------------------------------------------
//        public EqualityCleaner(IWhiteListConfig _config)
//        -----------------------------------------------------------------------------------------------------------------------
//        {
//            _whiteList = _config.WhiteList;
//        }


//        #region Equalities
//        -----------------------------------------------------------------------------------------------------------------------
//        public List<PartedString> Filter(List<PartedString> stringList)
//        -----------------------------------------------------------------------------------------------------------------------
//        {
//            if (stringList.Count == 1) return stringList;

//            StringsToDelete = new HashSet<string>();
//            Repeatings = new Dictionary<string, int>();

//            foreach (var phrase in stringList)
//            {
//                foreach (string word in phrase)
//                {
//                    if (!_whiteList.Contains(word))
//                    {
//                        TellTheDict(word);
//}
//                }
//            }

//            foreach (var key in Repeatings.Keys)
//            {
//                if (Repeatings[key] >= stringList.Count())
//                {
//                    StringsToDelete.Add(key);
//                }
//            }
//            foreach (var phrase in stringList)
//            {
//                SeekAndDestroy(phrase);
//            }
//            return stringList;
//        }

//        -----------------------------------------------------------------------------------------------------------------------
//        private void TellTheDict(string s)
//        -----------------------------------------------------------------------------------------------------------------------
//        {
//            if (Repeatings.ContainsKey(s))
//            {
//                Repeatings[s]++;
//            }
//            else
//            {
//                Repeatings.Add(s, 1);
//            }
//        }


//        -----------------------------------------------------------------------------------------------------------------------
//        private void SeekAndDestroy(PartedString ps)
//        -----------------------------------------------------------------------------------------------------------------------
//        {
//            for (int i = 0; i<ps.Count; i++)
//            {
//                if (StringsToDelete.Contains(ps[i]))
//                {
//                    ps.RemoveAt(i);
//                    i--;
//                }
//            }
//        }
//        #endregion
//    }
//}