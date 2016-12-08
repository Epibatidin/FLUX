namespace DataAccess.Base.Config
{
    public class VirtualFileAccessorSectionGroup 
    {
        public GeneralSection General { get; set; }
        
        public DebugSection Debug { get; set; }
                
        public SourceElement[] Sources { get; set; }
    }
}
