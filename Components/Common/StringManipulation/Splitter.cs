using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.StringManipulation
{
    public static class Splitter
    {
        private readonly static char[] SplitByWithoutDot = new char[] { ',', ' ', '-', '_' };
        private readonly static char[] SplitByWithDot = new char[] { '.', ',', ' ', '-', '_' };



        //02 - Impulso Biomec?nico (English Version)  mp3
        //Song => 02 biomec?nico (english version) | 
        //2005 Amduscia - Impulso Biomecanico


        //04 - Impulso Biomec?nico (Hell Model Version) 

        // Sonderzeichen = ausländische alphabete (mexiko, frankreich, ... )


        [System.Diagnostics.DebuggerStepThrough]
        //-----------------------------------------------------------------------------------------------------------------------
        public static List<string> Split(string input, bool SplitByDot = false)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            List<string> result = new List<string>();
            if (input == null) return result;
            string[] parts;

            if (SplitByDot)
                parts = input.Split(SplitByWithDot);
            else
                parts = input.Split(SplitByWithoutDot);

            foreach (string part in parts)
            {
                if (!System.String.IsNullOrWhiteSpace(part))
                {
                    result.Add(part.ToLower());
                }
            }
            return result;
        }


        //-----------------------------------------------------------------------------------------------------------------------
        public static string Join(IEnumerable<string> input,char JoinWith)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            StringBuilder builder = new StringBuilder();

            if (input != null && input.Count() > 0)
            {
                foreach (var item in input)
                {
                    if (!String.IsNullOrWhiteSpace(item))
                    {
                        builder.Append(item);
                        builder.Append(JoinWith);
                    }
                }
                builder.Remove(builder.Length - 1, 1);
            }

            return builder.ToString();
        }
    }
}
