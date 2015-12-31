using DynamicLoading;
using Extraction.Interfaces;
using Extraction.Layer.File.Cleaner;
using Extraction.Layer.File.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Extraction.Layer.File
{
    public class FileStructureInstaller : DynamicExtensionInstallerBase<FileLayerConfig>
    {
        public override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IDataExtractionLayer, FileStructureDataExtractionLayer>();
            services.AddSingleton<ICleaner, InternetStuffCleaner>();
        }
    }
}
