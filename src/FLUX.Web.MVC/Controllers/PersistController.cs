using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FLUX.DomainObjects;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FLUX.Web.MVC.Controllers
{
    public class PersistController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var postbackItems = new List<PostbackSong>();

            await TryUpdateModelAsync(postbackItems);
            
            return View();
        }
    }
}
