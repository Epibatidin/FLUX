using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileStructureDataExtraction.Cleaner.CursesRepair
{
    internal class ShitRepair : ICursesRepair
    {
        public bool Fixed { get; private set; }

        public string TryFix(string posShit)
        {                // es fängt mit s an 
            if (posShit[0] == 's')
            {
                if (posShit.Length >= 4)
                {
                    if (posShit.Substring(1, 3).IndexOf('*') > -1)
                    {
                        posShit = "shit" + posShit.Remove(0, 4);
                        Fixed = true;
                    }
                }
            }
            return posShit;
        }
    }
}
