
namespace Common
{
    public enum CIIS: byte
    {
        Unknown = 0, 
        Artist = 1,
        Year = 2,
        Album = 3, 
        Track = 4,
        Title = 5, 
        Genre = 6, 
        CD = 7 
    }

    public enum CleanIIS : byte
    {
        Artist = 0, 
        Album = 1,
        CD = 2,
        Song = 3
    }

    public enum FIIS : byte
    {
        FileInfo = 1,
        MetaData = 2, 
        DirectoryPart = 3,
        FileName = 4,
        Extension = 5
    }




    public enum DataStatus : byte
    {
        None = 0,
        New = 1, 
        Updated = 2 , 
        Unchanged = 4, 
        Deleted = 8 
    }




}
