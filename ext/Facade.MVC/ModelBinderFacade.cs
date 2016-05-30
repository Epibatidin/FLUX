using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.MVC
{
    public interface IModelBinderFacade
    {
        ModelBinderContext BuildContext(Controller controller);

        ModelBinderContext BuildContext(ViewComponent component,
            IActionBindingContextAccessor actionBindingContextAccessor);

        bool TryUpdateModel<TFormModel>(TFormModel form, ModelBinderContext modelBinderContext) where TFormModel : class;
    }

    public class ModelBinderFacade : IModelBinderFacade
    {
        public ModelBinderContext BuildContext(Controller controller)
        {
            var bindingContext = BuildContextFromActionContext(controller.BindingContext);

            bindingContext.HttpContext = controller.ActionContext.HttpContext;
            bindingContext.ModelState = controller.ModelState;
            bindingContext.MetadataProvider = controller.MetadataProvider;
            bindingContext.Validator = controller.ObjectValidator;
            return bindingContext;
        }

        public ModelBinderContext BuildContext(ViewComponent component, IActionBindingContextAccessor actionBindingContextAccessor)
        {
            var resolver = component.HttpContext.RequestServices;
            
            var bindingContext = BuildContextFromActionContext(actionBindingContextAccessor.ActionBindingContext);

            bindingContext.HttpContext = component.HttpContext;
            bindingContext.ModelState = component.ModelState;
            bindingContext.MetadataProvider = resolver.GetRequiredService<IModelMetadataProvider>();
            bindingContext.Validator = resolver.GetRequiredService<IObjectModelValidator>();

            return bindingContext;
        }

        private ModelBinderContext BuildContextFromActionContext(ActionBindingContext actionbinding)
        {
            var bindingContext = new ModelBinderContext();

            bindingContext.ModelBinder = actionbinding.ModelBinder;
            bindingContext.ValueProvider = actionbinding.ValueProvider;
            bindingContext.InputFormatters = actionbinding.InputFormatters;
            bindingContext.ValidatorProvider = actionbinding.ValidatorProvider;
            return bindingContext;
        }


        public bool TryUpdateModel<TFormModel>(TFormModel form, ModelBinderContext modelBinderContext) where TFormModel : class
        {
            var result = ModelBindingHelper.TryUpdateModelAsync<TFormModel>(form, "", modelBinderContext.HttpContext,
                modelBinderContext.ModelState,
                modelBinderContext.MetadataProvider, modelBinderContext.ModelBinder, modelBinderContext.ValueProvider
                  , modelBinderContext.InputFormatters, modelBinderContext.Validator, modelBinderContext.ValidatorProvider);
            
            return result.GetAwaiter().GetResult();
        }
    }
}
