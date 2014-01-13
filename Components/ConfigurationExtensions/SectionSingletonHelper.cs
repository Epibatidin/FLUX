using System;
using System.Configuration;

namespace ConfigurationExtensions
{
    public class SectionSingletonHelper<T> where T : ConfigurationSection
    {
        private readonly string _sectionName;
        private T _configSection;

        public SectionSingletonHelper(string sectionName)
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
                _configSection = config.GetSection(_sectionName) as T;
            }
            return _configSection;
        }

    }
}
