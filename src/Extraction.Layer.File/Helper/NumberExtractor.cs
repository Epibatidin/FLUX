using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Extraction.Layer.File.Helper
{
    public static class NumberExtractor
    {
        //[System.Diagnostics.Debuggable(DebuggableAttribute.DebuggingModes.None)]
        public static List<Int16> ExtractNumbers(string searchIn)
        {
            string intCandidate = String.Empty;

            List<Int16> ints = new List<Int16>();

            Int16? dummy = null;
            // darf nicht mit 0 anfangen 
            foreach (char s in searchIn)
            {
                // eine ziffer zwischen 0 und 9
                if (s > 47 && s < 58)
                {
                    intCandidate += s;
                }
                else
                {
                    dummy = Parse(intCandidate);

                    if (dummy == null) continue;
                    else ints.Add(dummy.Value);

                    intCandidate = String.Empty;
                }
            }
            dummy = Parse(intCandidate);
            if (dummy != null)
            {
                ints.Add(dummy.Value);
            }
            return ints;
        }

        //[System.Diagnostics.DebuggerStepThrough]
        private static Int16? Parse(string intCandidate)
        {
            if (!String.IsNullOrEmpty(intCandidate))
            {
                Int16 dummy = 0;
                if (Int16.TryParse(intCandidate, out dummy))
                {
                    if (dummy > 0)
                        return dummy;
                }
            }
            return null;
        }
    }
}