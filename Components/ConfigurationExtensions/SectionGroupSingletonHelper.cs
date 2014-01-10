using System;
using System.Configuration;

namespace ConfigurationExtensions
{
    public class SectionGroupSingletonHelper<T> where T : ConfigurationSectionGroup
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

        public T Get(Func<Configuration> locater)
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
