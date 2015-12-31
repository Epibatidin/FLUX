using DynamicLoading;
using Extraction.Layer.File.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Extraction.Layer.File
{
    public class FileStructureInstaller : DynamicExtensionInstallerBase<FileLayerConfig>
    {
        public override void RegisterServices(IServiceCollection serviceCollection)
        {
            
        }
    }
}
