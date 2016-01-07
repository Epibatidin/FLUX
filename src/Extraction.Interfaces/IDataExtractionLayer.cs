
namespace Extraction.Interfaces
{
    public interface IDataExtractionLayer
    {
        //void Configure(XmlNode config);
        //void InitData(Dictionary<int, IVirtualFile> dirtyData);
        //void SetUpdater(UpdateObject uo);
        void Execute(ExtractionContext store);
    }
}
