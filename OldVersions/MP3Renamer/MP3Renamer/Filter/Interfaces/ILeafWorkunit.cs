using MP3Renamer.DataContainer.EntityInterfaces;

namespace MP3Renamer.Filter.Interfaces
{
    public interface ILeafWorkunit
    {
        ILeaf Workunit { get; set; }
    }
}
