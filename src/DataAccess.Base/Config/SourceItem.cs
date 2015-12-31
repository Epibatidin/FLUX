using DynamicLoading;

namespace DataAccess.Base.Config
{
    public class SourceItem : IDynamicLoadableExtensionConfiguration
    {
        public string SetionName { get; set; }

        public string Type { get; set; }
        public bool Active { get; set; } = true;
    }
}
