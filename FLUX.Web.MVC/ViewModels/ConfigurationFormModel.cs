using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FLUX.Web.MVC.ViewModels
{
    public class ConfigurationFormModel
    {
        public IEnumerable<SelectListItem> AvailableProviders { get; set; }

    }
}