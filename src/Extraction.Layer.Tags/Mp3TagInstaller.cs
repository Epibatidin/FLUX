using DynamicLoading;
using Extraction.Layer.Tags.Config;
using Microsoft.Extensions.DependencyInjection;
using Extraction.Interfaces.Layer;
using Extraction.Layer.Tags.TagReader;
using Extraction.Layer.Tags.Interfaces;

namespace Extraction.Layer.Tags
{
    public class Mp3TagInstaller : DynamicExtensionInstallerBase<TagConfig>
    {
        public override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IDataExtractionLayer, Mp3TagDataExtractionLayer>();

            services.AddSingleton<IMp3TagVersionResolver, Mp3TagVersionResolver>();

            services.AddSingleton<ITagSongFactory, TagSongFactory>();

            services.AddSingleton<IMp3TagReader, ID3V1TagReader>();
            services.AddSingleton<IMp3TagReader, ID3V2TagReader>();
            services.AddSingleton<IMp3TagReader, ID3V3TagReader>();
            services.AddSingleton<IMp3TagReader, ID3V4TagReader>();

        }
    }
}
