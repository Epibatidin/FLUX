using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ViewComponents;

namespace Facade.MVC
{
    public class EmptyViewComponentResult : IViewComponentResult
    {
        public void Execute(ViewComponentContext context)
        { 
        }

        public Task ExecuteAsync(ViewComponentContext context)
        {
            return Task.Run(() => { });
        }
    }
}
