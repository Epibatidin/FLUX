using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP3Renamer.Models.DataContainer.EntityInterfaces;

namespace MP3Renamer.Models.Extraction
{
    [Flags]
    public enum LeafCleanLevel : byte
    {
        Equalities = 0x01,
        Internet = 0x02,
        OneStepUp = 0x04,
        ShortWords = 0x08
    };

    [Flags]
    public enum LeafExtractLevel : byte
    {
        Track = 0x01,
        Name = 0x02
    }


    public interface ILeafExtractionHelper : IFilterManager
    {
        void WorkUnit(ISubRoot workunit);

        ISubRoot execute();
    }
}
