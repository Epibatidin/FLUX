using System;
using System.Configuration;

namespace Extension.Configuration
{
    public interface ISectionGroupSingletonHelper<T> where T : ConfigurationSectionGroup
    {
        
    }

    public class SectionGroupSingletonHelper<T> : ISectionGroupSingletonHelper<T> where T : ConfigurationSectionGroup
    {
        private readonly string _sectionName;
        private T _configSection;

        public SectionGroupSingletonHelper(string sectionName)
        {
            _sectionName = sectionName;
        }

        public void Reset()
        {
            _configSection = null;
        }

        public T Get(Func<System.Configuration.Configuration> locater)
        {
            if (_configSection == null)
            {
                var config = locater();
                _configSection = config.GetSectionGroup(_sectionName) as T;
            }
            return _configSection;
        }

    }
}
