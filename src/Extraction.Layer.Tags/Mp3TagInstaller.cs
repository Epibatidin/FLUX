using DynamicLoading;
using Extraction.Layer.Tags.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Extraction.Interfaces.Layer;

namespace Extraction.Layer.Tags
{
    public class Mp3TagInstaller : DynamicExtensionInstallerBase<TagConfig>
    {
        public override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IDataExtractionLayer, Mp3TagDataExtractionLayer>();
        }
    }
}
