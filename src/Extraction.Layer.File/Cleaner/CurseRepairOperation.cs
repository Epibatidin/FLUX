using Extraction.DomainObjects.StringManipulation;
using Extraction.Interfaces;

namespace Extraction.Layer.File.Cleaner
{
    public class CurseRepairOperation : IPartedStringOperation
    {
        public PartedString Operate(PartedString source)
        {
            //    else
            //    {
            //    string dummy = "";
            //    foreach (var curse in Curses)
            //    {
            //        dummy = curse.TryFix(ToFilter[i]);
            //        if (curse.Fixed)
            //        {
            //            ToFilter[i] = dummy;
            //            break;
            //        }
            //    }
            //}


            return source;
        }
    }
}
