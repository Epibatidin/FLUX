using System.Collections.Generic;
using Extraction.DomainObjects.StringManipulation;

namespace Extraction.Interfaces
{
    public interface IPartedStringOperation
    {
        void Operate(PartedString source);
    }

    public interface IEnumerablePartedStringOperation
    {
        void Operate(List<PartedString> source);
    }
}
