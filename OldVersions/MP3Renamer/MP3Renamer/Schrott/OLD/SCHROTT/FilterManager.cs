using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MP3Renamer.Models.Extraction
{
    public class FilterManager : IFilterManager
    {
        protected byte FilterLevel = 0;

        //-----------------------------------------------------------------------------------------------------------------------
        protected bool FilterIsSet(byte levelToCheck)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            return (FilterLevel & levelToCheck) == levelToCheck;
        }


        //-----------------------------------------------------------------------------------------------------------------------
        public void setFilter(byte LevelToSet)
        //-----------------------------------------------------------------------------------------------------------------------
        {
            FilterLevel = LevelToSet;
        }

        //-----------------------------------------------------------------------------------------------------------------------
        public void activateAllFilters()
        //-----------------------------------------------------------------------------------------------------------------------
        {
            FilterLevel = Byte.MaxValue;
        }
    }
}