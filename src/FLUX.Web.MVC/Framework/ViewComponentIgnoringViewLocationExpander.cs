using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace FLUX.Web.MVC.Framework
{
    public class ViewComponentIgnoringViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (!context.IsMainPage)
                return viewLocations;

            return viewLocations.Concat(new[] { "/Views/Components/{0}.cshtml", "/Views/{0}.cshtml"});
        }
    }
}
