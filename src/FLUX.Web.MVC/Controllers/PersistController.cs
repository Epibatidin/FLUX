using FLUX.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FLUX.DomainObjects;
using System.Threading.Tasks;
using FLUX.Interfaces.Web;
using DataAccess.Interfaces;

namespace FLUX.Web.MVC.Controllers
{
    public class PersistController : Controller
    {
        IVirtualFilePeristentHelper _persistentHelper;
        IExtractionContextBuilder _contextBuilder;
        IPostbackSongBuilder _songBuilder;
        ISongToFileSystemWriter _writer;

        public PersistController(IVirtualFilePeristentHelper persistentHelper,
            IExtractionContextBuilder contextBuilder, IPostbackSongBuilder songBuilder, ISongToFileSystemWriter writer)
        {
            _persistentHelper = persistentHelper;
            _contextBuilder = contextBuilder;
            _songBuilder = songBuilder;
            _writer = writer;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var extractionContext = _contextBuilder.BuildForPersistence();

            var postbackTree = new PostbackTree();
                       
            await TryUpdateModelAsync(postbackTree);

            var songs = _songBuilder.Flatten(postbackTree);
            // overdue modelbinding for tree support 

            // search index.xml 
            // select name 
            //var writer = new XmlSongWriter(new DataAccess.XMLStub.Config.XMLSourcesCollection());
            //writer.Persist(extractionContext.SourceValues, songs);

            _writer.Write(extractionContext.StreamReader, extractionContext.SourceValues, songs);


            return RedirectToAction("Index", "Home");
        }
        
    }
}
