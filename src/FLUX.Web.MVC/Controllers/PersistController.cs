using FLUX.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FLUX.DomainObjects;
using System.Threading.Tasks;
using DataAccess.XMLStub;
using DataAccess.FileSystem;
using FLUX.Interfaces.Web;

namespace FLUX.Web.MVC.Controllers
{
    public class PersistController : Controller
    {
        IVirtualFilePeristentHelper _persistentHelper;
        IExtractionContextBuilder _contextBuilder;
        IPostbackSongBuilder _songBuilder;

        public PersistController(IVirtualFilePeristentHelper persistentHelper, IExtractionContextBuilder contextBuilder, IPostbackSongBuilder songBuilder)
        {
            _persistentHelper = persistentHelper;
            _contextBuilder = contextBuilder;
            _songBuilder = songBuilder;
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
            var writer = new XmlSongWriter(new DataAccess.XMLStub.Config.XMLSourcesCollection());
            writer.Persist(extractionContext.SourceValues, songs);

            var blubber = new SongToFileSystemWriter();
            blubber.Write(extractionContext.StreamReader, extractionContext.SourceValues, songs);


            return RedirectToAction("Index", "Home");
        }
        
    }
}
