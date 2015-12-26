using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;

namespace Facade.MVC
{
    public class ModelBinderContext
    {
        public HttpContext HttpContext { get; set; }
        public ModelStateDictionary ModelState { get; set; }
        public IModelMetadataProvider MetadataProvider { get; set; }
        public IModelBinder ModelBinder { get; set; }
        public IValueProvider ValueProvider { get; set; }
        public IList<IInputFormatter> InputFormatters { get; set; }
        public IObjectModelValidator Validator { get; set; }
        public IModelValidatorProvider ValidatorProvider { get; set; }
    }
}
