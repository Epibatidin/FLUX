﻿using DynamicLoading;
using Extraction.Interfaces;
using Extraction.Interfaces.Layer;
using Extraction.Layer.File.Config;
using Extraction.Layer.File.FullTreeOperators;
using Extraction.Layer.File.FullTreeOperators.SingleElementOperations;
using Extraction.Layer.File.FullTreeOperators.MultiElementOperations;
using Extraction.Layer.File.FullTreeOperators.SingleElementOperations.CursesRepair;
using Extraction.Layer.File.Interfaces;

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
            services.AddSingleton<IFullTreeOperator, SimpleTreeOperator>();
            services.AddSingleton<IFullTreeOperator, DropAllRedundantInformationTreeOperator>();
            services.AddSingleton<IFullTreeOperator, YearExtractionTreeOperator>();
            services.AddSingleton<IFullTreeOperator, TrackExtractingTreeOperator>();
            services.AddSingleton<IFullTreeOperator, DropRedundantExtractedInformationTreeOperator>();
                        
            services.AddSingleton<IPartedStringOperation, InternetStuffPartedStringOperation>();            
            services.AddSingleton<IPartedStringOperation, DropNonWordPhraseOperation>();
            services.AddSingleton<IPartedStringOperation, RemoveBlackListValuesOperation>();

            services.AddSingleton<IDropInformationInAllElementsOnThisLvlOperation, DropInformationInAllElementsOnThisLvlOperation>();
            services.AddSingleton<IYearExtractor, YearExtractor>();
            services.AddSingleton<ITrackExtractor, TrackExtractor>();
            
            if (config.RepairCurses)
            {
                services.AddSingleton<IPartedStringOperation, CurseRepairOperation>();
                services.AddSingleton<ICurseRepairComponent, ShitRepair>();
                services.AddSingleton<ICurseRepairComponent, FuckRepair>();
            }
        }
    }
}

