namespace Extension.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// (EnumVal > min && EnumVal < max)
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool BetweenExclusive(byte enumVal, byte min, byte max)
        {
            return (enumVal > min && enumVal < max);
        }

        /// <summary>
        /// (EnumVal >= min && EnumVal <= max)
        /// </summary>   
        [System.Diagnostics.DebuggerStepThrough]
        public static bool BetweenInclusive(byte enumVal, byte min, byte max)
        {
            return (enumVal >= min && enumVal <= max);
        }
    }
}
 