using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFRenamer.FileAccess
{
    class Pattern
    {
        string interpret = "System Of A Down";
        string input = "";
        string year = "";
        string album = "";
   
        Dictionary<char, string> knownPattern = null;

        // ich brauch ne zuordnung von bekannten erweiterungen zu dem entsprechendem wort 
        // sprich Track = #
        // dictonarys sind goil darum her damit 
        // aber ich brauch nur eins genau eins
        // mehr static für alle

        
        public string getExtractedInformationByName(string infoName)
        {
            if(File.neededInformation.ContainsKey(infoName))
            {
                if (knownPattern.ContainsKey(File.neededInformation[infoName]))
                {
                    return knownPattern[File.neededInformation[infoName]];
                }
                else return "";
            }
            else return "";
        }

        //%# - Track
        //%t - Titel
        //%g - Garbage
        //%i - Interpret
        //%y - Year
        //%a - Album
        //%e - Extension
        //Blind Guardian - Guardian Of The Blind - 2
        string inputPattern = @"# %t";
        string outputPattern = @"[%#] - %t - %i";

        public Pattern(string year, string album)
        {
            this.year = year;
            this.album = album;
            setKnownPattern();
        }

        private void setKnownPattern()
        {
            knownPattern = new Dictionary<char, string>();

            foreach (char foreignKey in File.neededInformation.Values)
            {
                switch (foreignKey)
                {
                    case 'i':
                        knownPattern.Add(foreignKey, interpret);
                        break;
                    case 'a':
                        knownPattern.Add(foreignKey, album);
                        break;
                    case 'y':
                        knownPattern.Add(foreignKey, year);
                        break;
                    default:
                        knownPattern.Add(foreignKey, "");
                        break;
                }
            }
        }

        public string applyPatternOn(string inp)
        {
            this.input = inp + ";";
            //number-titel-notneeded
            //[number] - titel - interpret
            // ich muss als die information anhand des input pattern aus dem file namen rausholen
            //System.Console.WriteLine(input);
            // such zeichen zwischen den variablennamen
            string[] pattern = (inputPattern + ";").Split('%');

            foreach (string pat in pattern)
            {
                if (pat.Length > 0)
                {
                    SearchForPattern(pat);                     
                }
            }
            return insertDataInPattern();
        }

        private string insertDataInPattern()
        {       
            string dummy = outputPattern;

            foreach (char key in knownPattern.Keys)
            {
                dummy = dummy.Replace("%" + key, knownPattern[key]);
            }
            return dummy;
        }


        private void SearchForPattern(string pat)
        {
            char codechar = pat[0];
            string s = pat.Substring(1); // erste stelle wegschneiden weil ist noch vom pattern
            
            //System.Console.WriteLine("Codechar |" + codechar + "| Pattern |" + pat + "|");
            int pos = input.IndexOf(s); // position von " -" 

            if (pos < 0) return;

            if (knownPattern.ContainsKey(codechar))
            {
                string dummy = input.Substring(0, pos);
                dummy = makeUpper(dummy);               
                switch (codechar)
                {
                    case '#':
                        if (dummy.Length < 2) dummy = "0" + dummy;
                        break;                   
                    default: break;
                }
                //System.Console.WriteLine(codechar + " " + dummy);
                knownPattern[codechar] = dummy;
            }                  
            input = input.Substring(pos + s.Length);
        }

        private string makeUpper(string lowerCase)
        {
            string[] todo = lowerCase.Split(' ', '_');
            string done = "";

            foreach (string todopart in todo)
            {
                done += " "+ UppercaseFirstLetter(todopart);
            }
            return done.Trim();
        }

        private string UppercaseFirstLetter(string word)
        {

            if (string.IsNullOrEmpty(word))
            {
                return string.Empty;
            }
            char[] a = word.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);         
        }





      
    }
}
