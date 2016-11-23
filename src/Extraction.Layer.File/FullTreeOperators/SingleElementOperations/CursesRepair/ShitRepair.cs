
using Extraction.Layer.File.Interfaces;

namespace Extraction.Layer.File.SimpleOperators.CursesRepair
{
    public class ShitRepair : ICurseRepairComponent
    {
        public bool Fixed { get; private set; }

        public string TryFix(string posShit)
        {                // es fängt mit s an 
            if (posShit[0] == 's')
            {
                if (posShit.Length >= 4)
                {
                    if (posShit.Substring(1, 3).IndexOf('*') > -1 && posShit[3] == 't')
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
