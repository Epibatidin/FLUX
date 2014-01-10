using System.Configuration;
using ConfigurationExtensions;
using ConfigurationExtensions.Interfaces;

namespace VirtualFileProvider.Config
{
    public class VirtualFileAccessorSectionGroup : ConfigurationSectionGroup
    {
        private static readonly SectionGroupSingletonHelper<VirtualFileAccessorSectionGroup> Helper = 
            new SectionGroupSingletonHelper<VirtualFileAccessorSectionGroup>("VirtualFileAccessor"); 

        public static void Reset()
        {
            Helper.Reset();
        }

        public static VirtualFileAccessorSectionGroup Get(IConfigurationLocator locator)
        {
            return Helper.Get(locator.Locate);
        }


        [ConfigurationProperty("General", IsRequired = true)]
        public GeneralSection General
        {
            get
            {
                return (GeneralSection)base.Sections["General"];
            }
        }

        [ConfigurationProperty("Debug", IsRequired = false)]
        public DebugSection Debug
        {
            get
            {
                return (DebugSection)base.Sections["Debug"];
            }
        }


        [ConfigurationProperty("Sources", IsRequired = true)]
        public SourcesSection Sources
        {
            get
            {
                return (SourcesSection)base.Sections["Sources"];
            }
        }
    }
}
