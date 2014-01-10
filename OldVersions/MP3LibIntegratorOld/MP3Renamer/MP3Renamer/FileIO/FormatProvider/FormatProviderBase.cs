using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using MP3Renamer.Helper;

namespace MP3Renamer.FileIO.FormatProvider
{
    public abstract class FormatProviderBase
    {
        protected Dictionary<byte, string> dict;
        private string Splitter = Environment.NewLine;
        protected string[] FormatStrings;
        protected string[] FileStrings;

        StringFormatExtension stringext;


        protected FormatProviderBase(string FullFilePath)
        {
            dict = new Dictionary<byte, string>();
            dict.Add(0, FullFilePath);
            stringext = new StringFormatExtension(new UpperCaseFormatProvider());           
        }
        

        private string JoinString(string[] formatStrings)
        {
            string result = "";

            foreach (var item in formatStrings)
            {
                result += Splitter + item;
            }
            return result;
        }


        private string[] SplitString(string source)
        {
            var splitted = source.Split(new[] { Splitter }, StringSplitOptions.RemoveEmptyEntries);

            return splitted;
        }


        public string[] ApplyDirFormat(Dictionary<string, object> data)
        {
            return ApplyFormat(FormatStrings, data, dict);            
        }

        public string[] ApplyFileFormat(Dictionary<string, object> data)
        {
            return ApplyFormat(FileStrings, data, dict); 
        }

        private string[] ApplyFormat(string[] _formatstrings,
            Dictionary<string, object> data, 
            Dictionary<byte, string> known)
        {
            var joined = JoinString(_formatstrings);

            var joinedvalues = (from knwonVal in known
                                join d in data on knwonVal.Value equals d.Key into left
                                from l in left.DefaultIfEmpty()
                                orderby knwonVal.Key
                                select l.Value).ToArray();

            var formatted = stringext.Format(joined, joinedvalues);

            return SplitString(formatted);
        }

    }
}