using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

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
