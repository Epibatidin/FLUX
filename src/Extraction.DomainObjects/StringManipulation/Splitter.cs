﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extraction.DomainObjects.StringManipulation
{
    public static class Splitter
    {
        public static IList<string> ComplexSplit(string source)
        { 
            StringBuilder builder = new StringBuilder(source);

            var bracesBlocks = SplitStringInBracesBlocks(builder);
            var withoutBraces = builder.ToString();

            var finalResult = new List<string>();

            foreach(var part in withoutBraces.Split(SplitByWithDot, StringSplitOptions.RemoveEmptyEntries))
            {
                if(part.StartsWith("$$$"))
                {
                    finalResult.Add((bracesBlocks[part[3] - 47]));
                    continue;
                }
                finalResult.Add(part);
            }
            return finalResult;
        }

        public static IList<string> SplitStringInBracesBlocks(StringBuilder builder)
        {
            IList<string> partsWithBraces = new List<string>();
            int counter = 0;
            CollectPartsInBraces(builder, partsWithBraces, '(', ')', ref counter);
            CollectPartsInBraces(builder, partsWithBraces, '{', '}', ref counter);
            CollectPartsInBraces(builder, partsWithBraces, '[', ']', ref counter);

            return partsWithBraces;
        }

        private static void CollectPartsInBraces(StringBuilder builder, IList<string> partsWithBraces, 
            char openingBrace, char closingBrace, ref int counter)
        {
            var toSearchIn = builder.ToString();
            int begin = 0, end = 0;
            do
            {
                if (begin >= toSearchIn.Length) return;

                begin = toSearchIn.IndexOf(openingBrace, begin);
                if (begin++ == -1) return;

                end = toSearchIn.IndexOf(closingBrace, begin);
                if (end == -1) return;

                partsWithBraces.Add(builder.ToString(begin - 1, end - begin + 2));

                builder.Remove(begin - 1, end - begin + 2);
                string braceContent = "$$$" + counter++;

                builder.Insert(begin -1,braceContent);
            }
            while (true);
        }


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
                if (!string.IsNullOrWhiteSpace(part))
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
