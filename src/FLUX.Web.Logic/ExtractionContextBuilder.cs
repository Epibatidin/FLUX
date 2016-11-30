using System;
using DataAccess.Interfaces;
using Extraction.Interfaces;
using FLUX.Interfaces;

namespace FLUX.Web.Logic
{
    public class ExtractionContextBuilder : IExtractionContextBuilder
    {
        private IVirtualFileConfigurationReader _configurationReader;
        private IVirtualFilePeristentHelper _persistentHelper;

        public ExtractionContextBuilder(IVirtualFilePeristentHelper persistentHelper,
            IVirtualFileConfigurationReader configurationReader)
        {
            _persistentHelper = persistentHelper;
            _configurationReader = configurationReader;
        }
        
        public ExtractionContext Build()
        {
            return BuildInternal(true);
        }

        public ExtractionContext BuildForPersistence()
        {
            return BuildInternal(false);
        }
        
        private ExtractionContext BuildInternal(bool loadFromDiskIfRequired)
        {
            var providerName = _persistentHelper.LoadProviderName();
            var activeGrp = _persistentHelper.LoadActiveGrp();

            if (providerName == null || activeGrp == null) return null;

            var extractionContext = new ExtractionContext();

            var context = _configurationReader.BuildContext(providerName);

            var virtualFileFactory = _configurationReader.FindActiveFactory(activeGrp);
            extractionContext.StreamReader = virtualFileFactory.GetReader(context);

            extractionContext.SourceValues = _persistentHelper.LoadSource(virtualFileFactory.GetVirtualFileArrayType());

            if (!loadFromDiskIfRequired) return extractionContext;
            if (extractionContext.SourceValues == null)
            {
                extractionContext.SourceValues = virtualFileFactory.RetrieveVirtualFiles(context);
                _persistentHelper.SaveSource(extractionContext.SourceValues);
            }

            return extractionContext;
        }

    }
}
