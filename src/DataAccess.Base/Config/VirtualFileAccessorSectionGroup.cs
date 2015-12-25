namespace DataAccess.Base.Config
{
    public class VirtualFileAccessorSectionGroup 
    {
        public GeneralSection General { get; set; }
        
        public DebugSection Debug { get; set; }
                
        public SourceItem[] Sources { get; set; }
    }
}
