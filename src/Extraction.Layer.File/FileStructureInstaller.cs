using DynamicLoading;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using Extraction.Layer.File.Cleaner;
using Extraction.Layer.File.Cleaner.CursesRepair;
using Extraction.Layer.File.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Extraction.Layer.File
{
    public class FileStructureInstaller : DynamicExtensionInstallerBase<FileLayerConfig>
    {
        public override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IDataExtractionLayer, FileStructureDataExtractionLayer>();
            services.AddSingleton<ITreeByKeyAccessorBuilder, TreeByKeyAccessorBuilder>();
        }

        public override void RegisterServices(IServiceCollection services, FileLayerConfig config)
        {
            // processing 
            services.AddSingleton<IPartedStringOperation, InternetStuffPartedStringOperation>();
            services.AddSingleton<IPartedStringOperation, RemoveBlackListValuesOperation>();

            if (config.RepairCurses)
            {
                services.AddSingleton<IPartedStringOperation, CurseRepairOperation>();

                services.AddSingleton<ICurseRepairComponent, ShitRepair>();
            }
        }
    }
}

