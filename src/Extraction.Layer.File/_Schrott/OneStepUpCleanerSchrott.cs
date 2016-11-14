//using System.Collections.Generic;
//using Common.StringManipulation;
//using System;

//namespace Cleaner_SCHROTT
//{
//    public class OneStepUpCleaner_OLD // : IPartedStringOperation 
//    {
//        private List<string> StepUp;
//        public void AddOneStepUp(string OneStepUp)
//        {
//            if (StepUp == null)
//                StepUp = new List<string>();

//            StepUp.Add(OneStepUp.ToLower());
//        }


//        public PartedString Filter2(PartedString ToFilter)
//        {
//            string rawdata = ToFilter.ToString();
//            int pos = 0;

//            foreach (string clean in StepUp)
//            {
//                pos = rawdata.IndexOf(clean);

//                if (pos >= 0)
//                {
//                    rawdata = rawdata.Remove(pos, clean.Length);
//                }
//            }
//            ToFilter.InitFromString(rawdata);
//            return ToFilter;
//        }

//        private void RemoveLongestMatch_old(PartedString result, PartedString b)
//        {
//            int length = 0;
//            int start = 0;
//            for (int i = 0; i < result.Count; i++)
//            {
//                for (int j = 0; j < b.Count; j++)
//                {
//                    if (i == result.Count)
//                    {
//                        // noch rausschneiden 
//                        result.RemoveRange(start, length);
//                        return;
//                    }
//                    else
//                    {
//                        if (result[i] == b[j])
//                        {
//                            if (length == 0) start = i;
//                            i++;
//                            length++;
//                            continue;
//                        }
//                        else
//                        {
//                            if (length > 0)
//                            {
//                                result.RemoveRange(start, length);
//                                return;
//                                i = 0;
//                                j = 0;
//                                length = 0;
//                            }
//                        }
//                    }
//                }
//                if (length > 0)
//                {
//                    length = 0;
//                }

//                length = 0;
//            }
//        }

//        private void RemoveLongestMatch_newandold(PartedString toBeShortend, PartedString patternContainer)
//        {
//            bool foundMismatchAfterMatch = false;

//            var pos = FindFirstMatch(toBeShortend, patternContainer);
//            if (pos != null)
//            {
//                // du hast den anfang 
//                // jetzt lauf zusammen solange weiter 
//                // bis entweder ende ist 
//                // oder ein mismatch 
//                int length = 0;
//                int i = pos.Item1;
//                int j = pos.Item2;
//                int start = i;
//                while (true)
//                {
//                    if (toBeShortend[i] == patternContainer[j])
//                    {
//                        i++;
//                        j++;
//                        length++;
//                        if (toBeShortend.Count == i) break;
//                        if (patternContainer.Count == j) break;
//                    }
//                    else
//                    {
//                        break;
//                    }
//                }
//                toBeShortend.RemoveRange(start, length);
//            }
//        }

//        private Tuple<int, int> FindFirstMatch(PartedString toBeShortend, PartedString patternContainer)
//        {
//            for (int i = 0; i < toBeShortend.Count; i++)
//            {
//                for (int j = 0; j < patternContainer.Count; j++)
//                {
//                    if (toBeShortend[i] == patternContainer[j])
//                    {
//                        // found 
//                        // jetzt gleichmääsig zusammen iterieren 
//                        return Tuple.Create(i, j);
//                    }

//                }
//            }
//            return null;
//        }
//    }
//}