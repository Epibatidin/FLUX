using System.Collections.Generic;
using Common.StringManipulation;

namespace FileStructureDataExtraction.Cleaner
{
    public interface ICleaner
    {
        PartedString Filter(PartedString source);
    }

    public interface IMultiCleaner
    {
        List<PartedString> Filter(List<PartedString> source);
    }
}
