//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DataAccess.Interfaces;
//using Extraction.Interfaces;
//using Extraction.Interfaces.Layer;
//using Facade.MVC;
//using FLUX.Interfaces;
//using Microsoft.AspNet.Mvc;

//namespace FLUX.Web.MVC.ViewComponents
//{
//    public class ProcessingViewComponent : ViewComponent
//    {
//        private readonly IVirtualFilePeristentHelper _persistentHelper;
//        private readonly IVirtualFileConfigurationReader _configurationReader;
//        private readonly IDataExtractionLayer _extractionProcessor;

//        public ProcessingViewComponent(IVirtualFilePeristentHelper persistentHelper, 
//            IVirtualFileConfigurationReader configurationReader, IDataExtractionLayer extractionProcessor)
//        {
//            _persistentHelper = persistentHelper;
//            _configurationReader = configurationReader;
//            _extractionProcessor = extractionProcessor;
//        }

//        private IDictionary<int, IVirtualFile> LoadSources()
//        {
//            var providerName = _persistentHelper.LoadProviderName();
//            var activeGrp = _persistentHelper.LoadActiveGrp();

//            if (providerName == null || activeGrp == null) return null;

//            var reader = _configurationReader.RetrieveReader(activeGrp);

//            var sources = _persistentHelper.LoadSource(reader.GetVirtualFileArrayType());
//            if (sources != null) return sources;

//            sources = _configurationReader.GetVirtualFiles(providerName, activeGrp);
//            _persistentHelper.SaveSource(sources);

//            return sources;
//        }


//        public IViewComponentResult Invoke()
//        {
//            var vf = LoadSources();
//            if (vf != null)
//            {
//                _extractionProcessor.Execute(new ExtractionContext()
//                {
//                    SourceValues = vf
//                });
//            }
//            return new EmptyViewComponentResult();
//        }
//    }
//}
