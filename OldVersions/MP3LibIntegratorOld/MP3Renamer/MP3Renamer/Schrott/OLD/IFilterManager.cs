using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP3Renamer.Models.Extraction
{
    public interface IFilterManager
    {
        void setFilter(byte filter);

        void activateAllFilters();
    }
}
