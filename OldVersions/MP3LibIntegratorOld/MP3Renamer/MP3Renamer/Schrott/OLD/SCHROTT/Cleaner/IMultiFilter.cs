using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP3Renamer.Models.DataContainer;

namespace MP3Renamer.Models.Extraction.Cleaner
{
    interface IMultiFilter
    {
        List<IStringPartManager> Filter(List<IStringPartManager> ToFilter);
    }
}
