
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace FLUX.Interfaces.Web
{
    public interface IConfigurationFormModelBuilder
    {
        ConfigurationViewModel BuildViewModel();
    }
}
