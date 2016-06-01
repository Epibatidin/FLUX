using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extraction.DomainObjects.StringManipulation
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


        //[System.Diagnostics.DebuggerStepThrough]
        //-----------------------------------------------------------------------------------------------------------------------
        public static List<string> Split(string input, bool splitByDot = false)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            List<string> result = new List<string>();
            if (input == null) return result;
            string[] parts;

            if (splitByDot)
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
        public static string Join(IEnumerable<string> input,char joinWith)
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
                        builder.Append(joinWith);
                    }
                }
                builder.Length--;
            }

            return builder.ToString();
        }
    }
}
