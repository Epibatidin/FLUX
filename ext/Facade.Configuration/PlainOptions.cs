using Microsoft.Extensions.Options;

namespace Facade.Configuration
{
    public class PlainOptions<TOptions> : IOptions<TOptions> where TOptions : class, new()
    {
        public PlainOptions(TOptions options)
        {
            Value = options;
        }

        public TOptions Value { get; private set; }
    }
}
