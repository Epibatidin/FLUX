using System;
using System.Text;

namespace MP3Renamer.Helper
{
    public class UpperCaseFormatProvider : IFormatProvider, ICustomFormatter
    {

        
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }


        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string temp = null;
            if (arg != null)
            {
                int i = 0;
                IFormattable formatable = arg as IFormattable;
                if (formatable != null)
                {
                    temp = formatable.ToString(format, formatProvider);
                }
                else
                {
                    temp = arg.ToString();
                }

                if (Int32.TryParse(temp, out i))
                    temp = ZeroStaredInts(i);
                else
                    temp = FirstLetterToUpper(temp);
            }            
            return temp;            
        }

        [System.Diagnostics.DebuggerStepThrough]
        private string FirstLetterToUpper(string input)
        {
            StringBuilder b = new StringBuilder(input);
            bool NextWord = true;

            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] == ' ')
                {
                    NextWord = true;
                    continue;
                }

                if (NextWord)
                {
                    b[i] = b[i].ToString().ToUpper()[0];
                    NextWord = false;
                }
            }
            return b.ToString();
        }

        private string ZeroStaredInts(int s)
        {
            if (s < 10 && s > -1)
            {
                return "0" + s;
            }
            return s.ToString();
        }

    }
}