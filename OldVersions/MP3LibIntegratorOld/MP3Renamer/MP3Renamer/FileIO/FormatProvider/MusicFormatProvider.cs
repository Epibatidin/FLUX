using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MP3Renamer.FileIO.FormatProvider
{
    public class MusicFormatProvider : FormatProviderBase
    {
        public MusicFormatProvider() : base("ILeaf.FullFilePath")
        {
            //1 Artist Name                    - Root.Name
            //2 Album erscheinungsjahr         - Subroot.Year
            //3 Album NAme                     - Subroot.Name
            //4 CD Nummer                      - Subroot.CDCount
            //5 Tracknummer                    - Leaf.Number
            //6 Trackname                      - Leaf.Name
            dict.Add(1, "IRoot.Name");
            dict.Add(2, "ISubRoot.Year");
            dict.Add(3, "ISubRoot.Name");
            dict.Add(4, "ISubRoot.CDCount");
            dict.Add(5, "ILeaf.Number");
            dict.Add(6, "ILeaf.Name");

            
            FormatStrings = new string[]
            {
                "{1}",  // root 
                "{2} - {3}", // subroot
                //"CD{3}\\", // extra cd 
                // "{3}", // extra cd
                
            };

            FileStrings = new string[]
            {
                "{0}", // OldFilePath
                "[{5}] - {6} - {1}" // files                
            };
        }
    }
}