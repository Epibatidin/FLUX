using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP3Renamer.DataContainer.EntityInterfaces
{
    public interface ILeaf : IStringParts
    {
        byte ID { get; set; }

        string Name { get; set; }


        /// <summary>
        /// Number ist die Track Nummer
        /// </summary>
        byte Number { get; set; } // numer ist die track number 

        
        /// <summary>
        /// CDCount -- Die CD Auf der sich dieser Track befindet
        /// </summary>
        
        byte Count { get; } // unterschied number und count ?! 

        // irgendwo muss ich das speichern sonst find ich das ding NIEMALS wieder ^^
        string FullFilePath { get; }

        void assumedCDCount(byte assumedCount);
    }
}
