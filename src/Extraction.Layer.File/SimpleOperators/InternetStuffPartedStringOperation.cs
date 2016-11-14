﻿using System.Text.RegularExpressions;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;

namespace Extraction.Layer.File.SimpleOperators
{
    ///<summary>    
    /// Entfernt Urls
    ///</summary>
    public class InternetStuffPartedStringOperation : IPartedStringOperation
    {
        //private static readonly string isInternetstuff = @"^(www.)?([a-z]*[.])(com|de|org|to|net)$";
        private static readonly string isInternetstuffNonExcluding = @"(www.)?([a-z0-9]*[.])(com|de|org|to|net)";

        private Regex Reg;

        public InternetStuffPartedStringOperation()
        {
            Reg = new Regex(isInternetstuffNonExcluding,
                RegexOptions.Compiled
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnoreCase);
        }

        // just look for macthing string 
        // and remove this part 
        private void execute(PartedString parted)
        {
            for (int i = 0; i < parted.Count; i++)
            {
                var match = Reg.Match(parted[i]);
                if (match.Success)
                {
                    parted.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Operate(PartedString part)
        {
            execute(part);
            part.ReSplit(true);            
        }
    }
}