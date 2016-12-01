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
            var postbackItems = new List<PostbackSong>();
            var extractionContext = _contextBuilder.BuildForPersistence();


            await TryUpdateModelAsync(postbackItems);



            
            //var sourceAndResult = from post in postbackItems
            //                      join vf in vfs on post.Id equals vf.ID
            //                      select new { post, vf};

            //foreach (var item in sourceAndResult)
            //{

            //}
            

            return View();
        }
    }
}
