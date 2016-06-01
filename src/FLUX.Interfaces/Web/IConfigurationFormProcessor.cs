using System;
using FLUX.DomainObjects;    
using Facade.MVC;
using Microsoft.AspNetCore.Http;

namespace FLUX.Interfaces.Web
{
    public interface IConfigurationFormProcessor : IFormProcessor<ConfigurationFormModel>
    {
    }

    public interface IFormProcessor<TFormModel>
    {
        TFormModel Build();

        void Update(TFormModel formModel, HttpRequest request, Func<IModelBinderFacade, ModelBinderContext> modelBindingContextBuilder);

        void Process(ConfigurationFormModel formModel);
    }
}
