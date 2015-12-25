using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace Facade.MVC
{
    public interface IModelBinderFacade
    {
        bool TryUpdateModel<TFormModel>(TFormModel form, Controller controller) where TFormModel : class;
    }

    public class ModelBinderFacade : IModelBinderFacade
    {
        public bool TryUpdateModel<TFormModel>(TFormModel form, Controller controller) where TFormModel : class
        {
            var result = ModelBindingHelper.TryUpdateModelAsync<TFormModel>(form, "", controller.ActionContext.HttpContext, controller.ModelState, 
                controller.MetadataProvider, controller.BindingContext.ModelBinder, controller.BindingContext.ValueProvider
                  , controller.BindingContext.InputFormatters, controller.ObjectValidator, controller.BindingContext.ValidatorProvider);

            result.ConfigureAwait(false);
            return result.Result;
        }
    }
}
