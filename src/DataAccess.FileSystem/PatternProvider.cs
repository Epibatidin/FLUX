using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataAccess.FileSystem
{
    public class PatternProvider : IPatternProvider
    {
        string[] _pattern;

        Regex _placeHolderMatcher = new Regex("{(\\w+)}", RegexOptions.Compiled | RegexOptions.Singleline); 

        public PatternProvider()
        {
            _pattern = new string[4];

            _pattern[0] = "{Artist}";
            _pattern[1] = "{Year} - {Album}";
            _pattern[2] = "{CD}";
            _pattern[3] = "{TrackNr} - {SongName} - {Artist}";
        }

        public IList<string> CreatePathParts(IExtractionValueFacade facade)
        {
            return FormatInternal(facade.ToValues(), _pattern);
        }

        internal List<string> FormatInternal(IList<Tuple<string, string>> values, string[] patterns)
        {
            var result = new List<string>();

            foreach (var pattern in patterns)
            {
                bool allMacthesAreNull = true;

                var replacedValues = _placeHolderMatcher.Replace(pattern, r =>
                {
                    var IamNotThatGoodWithRegex = r.Value.Substring(1, r.Value.Length - 2);
                    var element = values.FirstOrDefault(c => c.Item1 == IamNotThatGoodWithRegex);

                    if (element == null)
                    {
                        allMacthesAreNull = false;
                        return "";
                    }
                    if (element.Item2 == null)
                    {
                        return "";
                    }
                    else
                    {
                        allMacthesAreNull = false;
                    }

                    return ToTitleCase(element.Item2);
                });
                if (allMacthesAreNull) continue;
                
                result.Add(replacedValues);                    
            }
            return result;
        } 


        private string ToTitleCase(string source)
        {
            var chars = source.ToCharArray();
            bool newWord = true;

            for (int i = 0; i < chars.Length; i++)
            {
                if (newWord)
                {
                    chars[i] = Char.ToUpper(chars[i]);
                    newWord = false;
                }
                else
                    chars[i] = Char.ToLower(chars[i]);

                if (chars[i] == ' ') newWord = true;                
            }

            return new string(chars);
        }
    }
}
