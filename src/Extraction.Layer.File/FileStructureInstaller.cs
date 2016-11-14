using DynamicLoading;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using Extraction.Layer.File.Cleaner;
using Extraction.Layer.File.Cleaner.CursesRepair;
using Extraction.Layer.File.Config;
using Extraction.Layer.File.FullTreeOperators.InnerOperators;
using Extraction.Layer.File.Operations.Cleaning;
using Extraction.Layer.File.Operations.FullTreeOperators;
using Extraction.Layer.File.Operations.Interfaces;
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
            services.AddSingleton<IPartedStringOperation, DropNonWordPhraseOperation>();

            services.AddSingleton<IFullTreeOperator, DropAllRedundantInformationTreeOperator>();            
            services.AddSingleton<IFullTreeOperator, TrackExtractingTreeOperator>();

            services.AddSingleton<IDropInformationInAllElementsOnThisLvlOperation, DropInformationInAllElementsOnThisLvlOperation>();
            services.AddSingleton<ITrackExtractor, TrackExtractor>();


            if (config.RepairCurses)
            {
                services.AddSingleton<IPartedStringOperation, CurseRepairOperation>();
                services.AddSingleton<ICurseRepairComponent, ShitRepair>();
            }
        }
    }
}

