using System.Configuration;

namespace DataAccess.Base.Config
{
    public class VirtualFileAccessorSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty("General", IsRequired = true)]
        public GeneralSection General
        {
            get
            {
                return (GeneralSection)Sections["General"];
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
