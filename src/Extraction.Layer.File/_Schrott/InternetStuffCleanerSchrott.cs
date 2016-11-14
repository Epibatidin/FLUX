//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Cleaner.Schrott
//{
//    class InternetStuffCleanerSchrott
//    {
//        #region InternetStuff
//        //-----------------------------------------------------------------------------------------------------------------------
//        private void OldFilter2(PartedString parts)
//        //-----------------------------------------------------------------------------------------------------------------------
//        {
//            // klammern aufnehmen ?! 
//            // was gehschiet mit [x] - rX ? 
//            /*
//             * option : bei klammern und anderen noch zu wählenden sonderzeichen auch splitten 
//             * dann normal suchen und die durch dieses spezielle splitten nach bearbeiten 
//             * also - such nach inet zeug ohne auf wortanfang usw zu achten 
//             * gefunden ? 
//             * ja  
//             * entferne den teil des strings der das internet ding enthielt 
//             * ich würde ungern in string parts rumschneiden 
//             * am liebsten wätre mir danach noch speziell zu splitten und zu kontrollieren ob dann noch inet zeug drin ist 
//             * 
//             * vorgehen ;: 
//             * such in dem komplettem string 
//             * 
//             * nimm einfach die worterkennung am anfangg raus 
//             * dadurch werden auch inet addressen in strings gefunden 
//             * dann muss ich aber auch auf dem komplettem string arbeiten und nicht nur auf einem stück 
             
//             * 
//             */

//            var part = parts.ToString();
//            var matches = System.Text.RegularExpressions.Regex.Matches(part, isInternetstuffNonExcluding);
//            if (matches.Count > 0)
//            {
//                int removed = 0;
//                StringBuilder b = new StringBuilder(part);
//                foreach (Match item in matches)
//                {
//                    b.Remove(item.Index - removed, item.Length);
//                    removed += item.Length;
//                }
//                parts = new PartedString(b.ToString());
//            }
//            //return parts;
//        }


//        private void OldExtract(PartedString parts)
//        {

//            for (int i = 0; i < parts.Count; i++)
//            {
//                if (Regex.IsMatch(parts[i], isInternetstuff, RegexOptions.IgnoreCase))
//                {
//                    parts.RemoveElementAt(i);
//                    i--;
//                }
//                //else
//                //{
//                //    parts[i] = parts[i].Replace(".", "");
//                //}
//            }
//            if (parts.Changed)
//            {
//                parts.ReSplit(true);
//            }
//        }

//        #endregion
//    }
//}
