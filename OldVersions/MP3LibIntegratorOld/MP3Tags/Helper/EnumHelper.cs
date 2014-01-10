using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper
{
    public static class EnumHelper
    {

        /// <summary>
        /// (EnumVal > min && EnumVal < max)
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool BetweenExclusive(byte EnumVal, byte min, byte max)
        {
            return (EnumVal > min && EnumVal < max);
        }

        /// <summary>
        /// (EnumVal >= min && EnumVal <= max)
        /// </summary>   
        [System.Diagnostics.DebuggerStepThrough]
        public static bool BetweenInclusive(byte EnumVal, byte min, byte max)
        {
            return (EnumVal >= min && EnumVal <= max);
        }

    }
}
 