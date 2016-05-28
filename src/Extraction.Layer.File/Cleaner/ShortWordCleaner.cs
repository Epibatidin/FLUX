
//using System.Collections.Generic;

//namespace Extraction.Layer.File.Cleaner
//{
//    public class ShortWordCleaner : IFilter
//    {

//        private static HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'ä', 'ö', 'ü', 'y' };


//        //-----------------------------------------------------------------------------------------------------------------------
//        public IStringPartManager Operate(IStringPartManager RawManager)
//        //-----------------------------------------------------------------------------------------------------------------------
//        {
//            var parts = RawManager.RawDataParts;
//            bool containsVowel = false;
//            char c;
//            for (int i = 0; i < parts.Count(); i++)
//            {
//                string part = parts[i];
//                if (1 < part.Length || part.Length < 5)
//                {
//                    // kontrolliere ob konsonanten enthalten sind 
//                    containsVowel = false;
//                    for (int j = 0; j < part.Length; j++)
//                    {
//                        c = part[j];
//                        if ((c > 47 && c < 58) || vowels.Contains(c))
//                        {
//                            containsVowel = true;
//                            break;
//                        }
//                    }
//                    if (!containsVowel)
//                    {
//                        parts.RemoveAt(i);
//                        i--;
//                    }
//                }
//            }
//            RawManager.RawDataParts = parts;

//            return RawManager;
//        }
//    }
//}