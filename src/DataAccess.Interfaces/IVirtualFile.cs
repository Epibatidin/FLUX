namespace DataAccess.Interfaces
{
    public interface IVirtualFile
    {
        int ID { get; }


        string Extension { get; set; }
        string Name {get;}

        string[] PathParts { get; }
    }
}
