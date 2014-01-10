using System;
using System.Collections.Generic;

namespace MP3Renamer.DataContainer.EntityInterfaces
{
    public interface ISubRoot : IStringParts
    {
        byte ID { get; set; }

        
        /// <summary>
        /// ErscheinungsJahr
        /// </summary>
        Int16 Year { get; set; }

        /// <summary>
        /// Albumname
        /// </summary>
        string Name { get; set; }
        

        /// <summary>
        /// CDCount die Gesamtzahl der CDs in diesem Album
        /// </summary>
        byte CDCount { get; set; }

        List<ILeaf> Leafs { get; set; }


    }
}
