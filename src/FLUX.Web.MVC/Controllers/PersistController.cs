using DataAccess.XMLStub.Serialization;
using FLUX.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FLUX.DomainObjects;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using DataAccess.XMLStub;

namespace FLUX.Web.MVC.Controllers
{
    public class PersistController : Controller
    {
        IVirtualFilePeristentHelper _persistentHelper;
        IExtractionContextBuilder _contextBuilder;

        public PersistController(IVirtualFilePeristentHelper persistentHelper, IExtractionContextBuilder contextBuilder)
        {
            _persistentHelper = persistentHelper;
            _contextBuilder = contextBuilder;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var extractionContext = _contextBuilder.BuildForPersistence();

            var postbackTree = new PostbackTree();
                       
            await TryUpdateModelAsync(postbackTree);
                        
            var songs = postbackTree.Flatten();
            // overdue modelbinding for tree support 

            // search index.xml 
            // select name 
            var writer = new XmlSongWriter(new DataAccess.XMLStub.Config.XMLSourcesCollection());
            writer.Persist(extractionContext.SourceValues, songs);
            
            return RedirectToAction("Index", "Home");
        }
        
    }
}
