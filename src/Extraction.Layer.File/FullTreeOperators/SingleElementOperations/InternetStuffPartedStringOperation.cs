using System.Text.RegularExpressions;
using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;

namespace Extraction.Layer.File.FullTreeOperators.SingleElementOperations
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
                | RegexOptions.ExplicitCapture
                | RegexOptions.IgnoreCase);
        }

        // just look for macthing string 
        // and remove this part 
        private void execute(PartedString parted)
        {
            /* klammern aufnehmen ?!
            // was geschieht mit [x] - rX ? 

            * option : bei klammern und anderen noch zu wählenden sonderzeichen auch splitten 
            * dann normal suchen und die durch dieses spezielle splitten nach bearbeiten 
            * also - such nach inet zeug ohne auf wortanfang usw zu achten 
            * gefunden ? 
            * ja  
            * entferne den teil des strings der das internet ding enthielt 
            * ich würde ungern in string parts rumschneiden 
            * am liebsten wäre mir danach noch speziell zu splitten und zu kontrollieren ob dann noch inet zeug drin ist 
            * 
            * vorgehen : 
            * such in dem komplettem string 
            * 
            * nimm einfach die worterkennung am anfang raus 
            * dadurch werden auch inet addressen in strings gefunden 
            * dann muss ich aber auch auf dem komplettem string arbeiten und nicht nur auf einem stück             
            */

            var newValue = Reg.Replace(parted.StringValue, "");

            parted.Split(newValue);

            //for (int i = 0; i < parted.Count; i++)
            //{
            //    var match = Reg.Match(parted[i]);
            //    if (match.Success)
            //    {
            //        parted.RemoveAt(i);
            //        i--;
            //        continue;
            //    }
            //    // we need to protect acronyms 
            //    //parted[i] = parted[i].Replace(".", "<ar>");
            //}
        }

        public void Operate(PartedString part)
        {
            execute(part);           
        }
    }
}