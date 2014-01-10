using System.Text;
using System.Text.RegularExpressions;
using Common.StringManipulation;


namespace FileStructureDataExtraction.Extraction
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


        // iteriere durch alle stufen durch und such dir die dinger die nach internet
        // addressen aussehen also klumpen des formats 
        // www.blub.de => googlen wie urls korrekt mit regex gefunden werdne => punkte usw reichen nicht 
        // relvant sind endungen wie : 'org,de,net,com' 
        // suche diese endungen - mit punkt - iteriere dann rückwärts bis du den punkt findest 
        private PartedString execute(PartedString parts)        
        {
            var part = parts.ToString();
            var matches = Reg.Matches(part);
            if (matches.Count > 0)
            {
                int removed = 0;
                StringBuilder b = new StringBuilder(part);
                foreach (Match item in matches)
                {
                    b.Remove(item.Index - removed, item.Length);
                    removed += item.Length;
                }
                parts = new PartedString(b.ToString());
            }
            return parts;
        }

        public PartedString Filter(PartedString part)
        {
            part = execute(part);
            part.ReSplit(true);

            return part;
        }

    }
} 