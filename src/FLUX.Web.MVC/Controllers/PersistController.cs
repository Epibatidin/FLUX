using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FLUX.DomainObjects;
using Facade.Session;
using FLUX.Interfaces;
using DataAccess.Base.Config;
using System.Linq;

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
            var r = songs;
            // overdue modelbinding for tree support 



            //var sourceAndResult = from post in postbackItems
            //                      join vf in vfs on post.Id equals vf.ID
            //                      select new { post, vf};

            //foreach (var item in sourceAndResult)
            //{

            //}


            return RedirectToAction("Index", "Home");
        }
    }
}
