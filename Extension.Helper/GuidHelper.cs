using System;
using System.Text;

namespace Extension.Helper
{
    public static class GuidHelper
    {
        public static Guid IntToGuid(Int32 i)
        {
            StringBuilder B = new StringBuilder("00000000-0000-0000-0000-000000000000", 36);
            var usas = i.ToString();
            for (int j = 0; j < usas.Length; j++)
            {
                B[B.Length - (usas.Length - j)] = usas[j];
            }
            return new Guid(B.ToString());
        }
    }
}
