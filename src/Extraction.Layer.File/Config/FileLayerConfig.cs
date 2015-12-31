
namespace Extraction.Layer.File.Config
{
    public class FileLayerConfig 
    {
        public BlackListConfig BlackList { get; set; }
        public WhiteListConfig WhiteList { get; set; }

        public bool RepairCurses { get; set; }
    }
}
