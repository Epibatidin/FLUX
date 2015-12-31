using System.Collections.Generic;
using Extraction.DomainObjects.StringManipulation;

namespace Extraction.Interfaces
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
