
namespace Extraction.Layer.File.Cleaner.CursesRepair
{
    public interface ICurseRepairComponent
    {
        bool Fixed { get; }
        string TryFix(string toFix);
    }
}
