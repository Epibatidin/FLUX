using System.Collections.Generic;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;

namespace Extraction.Layer.File.FullTreeOperators.SingleElementOperations
{
    public class DropNonWordPhraseOperation : IPartedStringOperation
    {
        private static readonly HashSet<char> LettersMakingAValidWord = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'ä', 'ö', 'ü', 'y', '.' };

        public void Operate(PartedString source)
        {
            for (int i = 0; i < source.Count; i++)
            {
                string part = source[i];
                if (1 >= part.Length && part.Length >= 5) continue;
                // kontrolliere ob konsonanten enthalten sind 

                // but what about dots ? like in acronyms ? 
                var containsVowel = false;

                for (int j = 0; j < part.Length; j++)
                {
                    var c = part[j];
                    if ((c > 47 && c < 58) || LettersMakingAValidWord.Contains(c))
                    {
                        containsVowel = true;
                        break;
                    }
                }

                if (containsVowel) continue;
                source.RemoveAt(i);
                i--;
            }
        }
    }
}