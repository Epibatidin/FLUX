using MP3Renamer.DataContainer.EntityInterfaces;

namespace MP3Renamer.DataContainer.EntityInterfaces
{
    public interface IDataContainer
    {
        string LeafAsString { get; }
        string RootAsString { get; }
        string SubRootAsString { get; }
    }
}
