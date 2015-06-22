using Extension.Configuration;

namespace DataAccess.Base.Config
{
    public interface IVirtualFileAccessorSectionGroupProvider
    {
        VirtualFileAccessorSectionGroup Configuration { get; set; }
    }

    public class VirtualFileAccessorSectionGroupProvider :
        ConfigurationHolder<VirtualFileAccessorSectionGroup>, IVirtualFileAccessorSectionGroupProvider
    {
    }
}
