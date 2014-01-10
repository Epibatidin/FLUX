using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileStructureDataExtraction.Cleaner.CursesRepair
{
    public interface ICursesRepair
    {
        bool Fixed { get; }
        string TryFix(string toFix);
    }
}
