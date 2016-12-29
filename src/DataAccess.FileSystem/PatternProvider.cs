using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public IList<string> FormattedLevelValue(IExtractionValueFacade facade)
        {
            return null;

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

                    if (element.Item2 != null)
                        allMacthesAreNull = false;

                    return element.Item2;
                });
                if (!allMacthesAreNull)
                    result.Add(replacedValues);                    
            }
            return result;
        } 



        //public string FormattedLevelValue(IExtractionValueFacade facade, int lvl)
        //{
        //    // lalala 
        //    // parts anhand der pattern einsetzen und zurück geben 

        //    var format = Pattern[lvl];

        //    foreach (var value in facade.ToValues())
        //    {
        //        format.Replace("{" + value.Item1 + "}", value.Item2);
        //    }
        //    return format;
        //}
    }
}
