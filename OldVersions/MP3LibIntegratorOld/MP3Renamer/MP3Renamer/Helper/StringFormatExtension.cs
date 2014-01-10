using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MP3Renamer.Helper
{
    public class StringFormatExtension
    {
        private object[] values;
        private IFormatProvider FormatProvider;

        public StringFormatExtension()
            : this(CultureInfo.InvariantCulture)
        {
        }

        public StringFormatExtension(IFormatProvider provider)
        {
            FormatProvider = provider;
            if (!(provider is ICustomFormatter))
                FormatProvider = CultureInfo.InvariantCulture;
        }

        public string Format(string format, object value)
        {
            return Format(format, new object[] { value });
        }

        public string Format(string format, object[] values)
        {
            

            string regExpr = "\\{[0-9]+(:[a-zA-Z0-9#]+)?\\}";
            this.values = values;
            Regex reg = new Regex(regExpr, RegexOptions.None);
            MatchEvaluator evelator = new MatchEvaluator(ApplayFormat);
            string tempres = reg.Replace(format, evelator);
            return tempres;
        }
               
        private string ApplayFormat(Match m)
        {
            var match = m.Value;
            int endpos = match.IndexOf(':');
            if (endpos < 0)
                endpos = match.LastIndexOf('}');
            if (endpos < 0)
                return match;

            int pos = 0;
            if (!int.TryParse(match.Substring(1, endpos - 1), out pos))
                return match;

            object value = null;
            if (values.Length > pos)
                value = values[pos];

            // dann wurde der doppel punkt gefunden
            if (endpos < match.Length - 1)
                // also ICustomFormatter 
                return String.Format(FormatProvider,
                    match.Substring(endpos + 1, match.Length - endpos - 2),
                    value);
            else
                return String.Format(FormatProvider, "{0}", value);
        }
    }
}
