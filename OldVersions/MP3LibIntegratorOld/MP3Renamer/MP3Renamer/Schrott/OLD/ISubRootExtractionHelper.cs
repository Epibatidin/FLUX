using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using MP3Renamer.Models.DataContainer.EntityInterfaces;

namespace MP3Renamer.Models.Extraction
{

    [Flags]
    public enum SubRootCleanLevel : byte
    {
        Equalities = 0x01,
        Internet = 0x02,
        OneStepUp = 0x04,
        ShortWords = 0x08
    };


    [Flags]
    public enum SubRootExtractLevel : byte
    {
        Year = 0x01,
        Name = 0x02
    };


    public interface ISubRootExtractionHelper : IFilterManager
    {
        void WorkUnit(IRoot workunit);

        IRoot execute();

    }
}
