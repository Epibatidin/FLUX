
namespace Extension.Configuration
{
    public class ConfigurationHolder<TConfig> where TConfig : class
    {
        public TConfig Configuration { get; set; }
    }
}
