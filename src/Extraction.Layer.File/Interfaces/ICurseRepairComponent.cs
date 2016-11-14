
namespace Extraction.Layer.File.Interfaces
{
    public interface ICurseRepairComponent
    {
        bool Fixed { get; }
        string TryFix(string toFix);
    }
}
