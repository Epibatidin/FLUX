using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;

namespace Facade.MVC
{
    public interface IModelBinderFacade
    {
        ModelBinderContext BuildContext(Controller controller);
        ModelBinderContext BuildContext(ViewComponent component, IActionContextAccessor actionBindingContextAccessor);

        bool TryUpdateModel<TFormModel>(TFormModel form, ModelBinderContext modelBinderContext) where TFormModel : class;
    }

    public class ModelBinderFacade : IModelBinderFacade
    {
        public ModelBinderContext BuildContext(Controller controller)
        {
            var context = new ModelBinderContext();
            context.ActionContext = controller.ControllerContext;
            context.MetadataProvider = controller.MetadataProvider;            
            context.ModelBinderFactory = controller.ModelBinderFactory;
            context.Validator = controller.ObjectValidator;            
            context.ValueProvider = CompositeValueProvider.CreateAsync(controller.ControllerContext).GetAwaiter().GetResult();
            return context;
        }

        public ModelBinderContext BuildContext(ViewComponent component, IActionContextAccessor actionBindingContextAccessor)
        {
            var resolver = component.HttpContext.RequestServices;
            var context = new ModelBinderContext();

            context.ActionContext = actionBindingContextAccessor.ActionContext;
            context.MetadataProvider = resolver.GetRequiredService<IModelMetadataProvider>();
            context.ModelBinderFactory = resolver.GetRequiredService<IModelBinderFactory>();
            context.Validator = resolver.GetRequiredService<IObjectModelValidator>();
            context.ValueProvider = CompositeValueProvider.CreateAsync(new ControllerContext(context.ActionContext)).GetAwaiter().GetResult();

            return context;
        }
        
        public bool TryUpdateModel<TFormModel>(TFormModel form, ModelBinderContext modelBinderContext) where TFormModel : class
        {
            var result = ModelBindingHelper.TryUpdateModelAsync(form, typeof(TFormModel),"",
                modelBinderContext.ActionContext, modelBinderContext.MetadataProvider,
                modelBinderContext.ModelBinderFactory, modelBinderContext.ValueProvider, modelBinderContext.Validator);

            return result.GetAwaiter().GetResult();
        }
    }
}
