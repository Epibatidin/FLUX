using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Facade.MVC
{
    public class ModelBinderContext
    {
        public ActionContext ActionContext { get; set; }
        public IModelMetadataProvider MetadataProvider { get; set; }
        public IModelBinderFactory ModelBinderFactory { get; set; }
        public IValueProvider ValueProvider { get; set; }
        public IObjectModelValidator Validator { get; set; }
    }
}
