﻿using System.Text;
using System.Text.RegularExpressions;
using Common.StringManipulation;

namespace FileStructureDataExtraction.Cleaner
{
    /// <summary>
    /// WORKS AND IS BOUND SO WILL BE CALLED 
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough]
    public class InternetStuffCleaner
    {
        private static readonly string isInternetstuff = @"^(www.)?([a-z]*[.])(com|de|org|to|net)$";
        private static readonly string isInternetstuffNonExcluding = @"(www.)?([a-z0-9]*[.])(com|de|org|to|net)";

        private Regex Reg;
    
        public InternetStuffCleaner()
        {
            Reg = new Regex(isInternetstuffNonExcluding,
                RegexOptions.Compiled
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnoreCase);
        }

        // just look for macthing string 
        // hand remove this part 

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


        public PartedString Filter(PartedString part)
        {
            execute(part);
            part.ReSplit(true);

            return part;
        }

    }
} 