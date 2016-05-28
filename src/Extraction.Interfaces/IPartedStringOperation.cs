using System.Collections.Generic;
using Extraction.DomainObjects.StringManipulation;

namespace Extraction.Interfaces
{
    public interface IPartedStringOperation
    {
        PartedString Operate(PartedString source);
    }

    public interface IMultiCleaner
    {
        List<PartedString> Filter(List<PartedString> source);
    }
}
