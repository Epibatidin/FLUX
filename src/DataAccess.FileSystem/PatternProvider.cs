using DataAccess.Interfaces;
using System;

namespace DataAccess.FileSystem
{
    public class PatternProvider : IPatternProvider
    {
        string[] Pattern;


        public PatternProvider()
        {
            Pattern = new string[4];

            Pattern[0] = "{Artist}";
            Pattern[1] = "{Year} - {Album}";
            Pattern[2] = "{CD}";
            Pattern[3] = "{TrackNr} - {SongName} - {Artist}";
        }


        public string FormattedLevelValue(IExtractionValueFacade facade, int lvl)
        {
            // lalala 
            // parts anhand der pattern einsetzen und zurück geben 

            var format = Pattern[lvl];

            foreach (var value in facade.ToValues())
            {
                format.Replace("{" + value.Item1 + "}", value.Item2);
            }
            return format;
        }
    }
}
