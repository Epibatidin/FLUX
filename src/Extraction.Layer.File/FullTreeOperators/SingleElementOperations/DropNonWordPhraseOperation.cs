using System;
using System.Collections.Generic;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;

namespace Extraction.Layer.File.FullTreeOperators.SingleElementOperations
{    
    public class DropNonWordPhraseOperation : IPartedStringOperation
    {
        private class CharEqualityComparer : IEqualityComparer<char>
        {
            public bool Equals(char x, char y)
            {
                return Char.ToUpper(x) == Char.ToUpper(y);
            }

            public int GetHashCode(char obj)
            {
                return Char.ToUpper(obj).GetHashCode();
            }
        }


        private static readonly HashSet<char> LettersMakingAValidWord = 
            new HashSet<char>(new CharEqualityComparer()) { 'a', 'e', 'i', 'o', 'u', 'ä', 'ö', 'ü', 'y', '.' };

        public void Operate(PartedString source)
        {
            for (int i = 0; i < source.Count; i++)
            {
                string part = source[i];
                if (part.Length > 4) continue;
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