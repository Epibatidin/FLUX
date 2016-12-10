using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extraction.DomainObjects.StringManipulation
{
    public static class Splitter
    {
        public static List<string> ComplexSplit(string source)
        { 
            StringBuilder builder = new StringBuilder(source.ToLowerInvariant());

            var bracesBlocks = SplitStringInBracesBlocks(builder);
            var withoutBraces = builder.ToString();

            var finalResult = new List<string>();

            foreach(var part in withoutBraces.Split(SplitByEverything, StringSplitOptions.RemoveEmptyEntries))
            {
                if(part.StartsWith("?"))
                {
                    finalResult.Add((bracesBlocks[part[1] - 48]));
                    continue;
                }
                if(part.Length == 1)
                {
                    finalResult.Add(part);
                    continue;
                }

                SplitByDotAndAdd(part, finalResult);                
            }
            return finalResult;
        }

        private static void SplitByDotAndAdd(string source, IList<string> result)
        {
            StringBuilder acrony = new StringBuilder();
            int acronymPos = -1;
            foreach (var splitted in source.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (splitted.Length == 1)
                {
                    if(!Char.IsLetter(splitted[0]))
                    {
                        result.Add(splitted);
                        continue;
                    }

                    if (acronymPos == -1)
                    {
                        acronymPos = result.Count;
                        result.Add(null);
                    }
                    acrony.Append(splitted).Append('.');
                }
                else
                    result.Add(splitted);
            };
            if (acrony.Length == 0) return;

            result[acronymPos] = acrony.ToString();
        }

        public static IList<string> SplitStringInBracesBlocks(StringBuilder builder)
        {
            IList<string> partsWithBraces = new List<string>();
            int counter = 48; 
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
                begin = toSearchIn.IndexOf(openingBrace, begin);
                if (begin == -1) return;

                end = toSearchIn.IndexOf(closingBrace, begin);
                if (end == -1) return;
                
                int length = end - begin + 1;
                if (length == 2)
                {
                    builder[begin++] = ' ';
                    builder[begin] = ' ';                    
                    continue;
                }

                partsWithBraces.Add(builder.ToString(begin, length));                
                ReplaceTheBraceInBuilder(builder, begin, length, ref counter);
                begin++;
            }
            while (true);
        }

        public static void ReplaceTheBraceInBuilder(StringBuilder builder,
            int start, int length, ref int counter)
        {
            for (int i = 0; i < length -2; i++)
            {
                builder[i + start] = ' ';
            }
            builder[start + length - 2] = '?';
            builder[start + length - 1] = Convert.ToChar(counter++);
        }


        private readonly static char[] SplitByEverything = 
            new char[] {'(', ')','{', '}','[',']', ',', ' ', '-', '_' };
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
