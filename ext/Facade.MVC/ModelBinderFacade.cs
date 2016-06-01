using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.MVC
{
    public interface IModelBinderFacade
    {
        ModelBinderContext BuildContext(Controller controller);

        ModelBinderContext BuildContext(ViewComponent component,
            IActionContextAccessor actionBindingContextAccessor);

        bool TryUpdateModel<TFormModel>(TFormModel form, ModelBinderContext modelBinderContext) where TFormModel : class;
    }

    public class ModelBinderFacade : IModelBinderFacade
    {
        private readonly IControllerPropertyActivator _controllerActivator;

        public ModelBinderFacade(IControllerPropertyActivator controllerActivator)
        {
            _controllerActivator = controllerActivator;
        }

        public ModelBinderContext BuildContext(Controller controller)
        {
            var context = new ModelBinderContext();
            context.ActionContext = controller.ControllerContext;
            context.MetadataProvider = controller.MetadataProvider;
            context.InputFormatters = controller.ControllerContext.InputFormatters;
            context.ModelBinderFactory = controller.ModelBinderFactory;
            context.Validator = controller.ObjectValidator;
            context.ValidatorProvider = new CompositeModelValidatorProvider(controller.ControllerContext.ValidatorProviders);
            context.ValueProvider = new CompositeValueProvider(controller.ControllerContext.ValueProviders);
            return context;
        }

        public ModelBinderContext BuildContext(ViewComponent component, IActionContextAccessor actionBindingContextAccessor)
        {
            var resolver = component.HttpContext.RequestServices;
            var context = new ModelBinderContext();

            context.ActionContext = actionBindingContextAccessor.ActionContext;
            context.InputFormatters = new List<IInputFormatter>();

            context.MetadataProvider = resolver.GetRequiredService<IModelMetadataProvider>();
            context.ModelBinderFactory = resolver.GetRequiredService<IModelBinderFactory>();
            context.Validator = resolver.GetRequiredService<IObjectModelValidator>();

            var controllerContext = new ControllerContext();
            _controllerActivator.Activate(controllerContext, new object());
            
            return context;
        }

        private ModelBinderContext BuildContextFromActionContext(ActionContext actionbinding)
        {
            var bindingContext = new ModelBinderContext();
            

            //bindingContext.ModelBinder = actionbinding.ModelBinder;
            //bindingContext.ValueProvider = actionbinding.ValueProvider;
            //bindingContext.InputFormatters = actionbinding.InputFormatters;
            //bindingContext.ValidatorProvider = actionbinding.ValidatorProvider;
            return bindingContext;
        }


        public bool TryUpdateModel<TFormModel>(TFormModel form, ModelBinderContext modelBinderContext) where TFormModel : class
        {
            var result = ModelBindingHelper.TryUpdateModelAsync(form, "",
                modelBinderContext.ActionContext, modelBinderContext.MetadataProvider, 
                modelBinderContext.ModelBinderFactory, modelBinderContext.ValueProvider, 
                modelBinderContext.InputFormatters, modelBinderContext.Validator, modelBinderContext.ValidatorProvider);
            
            return result.GetAwaiter().GetResult();
        }
    }
}
