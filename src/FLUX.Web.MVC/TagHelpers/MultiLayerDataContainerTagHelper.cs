using DataStructure.Tree;
using Extraction.Interfaces;
using FLUX.DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace FLUX.Web.MVC.TagHelpers
{
    public class MultiLayerDataTagHelper : TagHelper
    {
        public readonly static string[][] Keys;

        static MultiLayerDataTagHelper()
        {
            Keys = new string[4][];
            Keys[0] = new [] { nameof(ISong.Artist) };
            Keys[1] = new [] { nameof(ISong.Year), nameof(ISong.Album) };
            Keys[2] = new [] { nameof(ISong.CD) };
            Keys[3] = new [] { nameof(ISong.TrackNr), nameof(ISong.SongName) };
        }


        ICompositeViewEngine _viewEngine;

        public MultiLayerDataTagHelper(ICompositeViewEngine viewEngine )
        {
            _viewEngine = viewEngine;
        }

        [ViewContext] public ViewContext ViewContext { get; set; }
        
        [HtmlAttributeName("model")]
        public ITreeItem<MultiLayerDataContainer> Container { get; set; }
             
        [HtmlAttributeName("class")]
        public string Class { get; set; }
        
        public string NameFor { get; set; }

        [HtmlAttributeName("level")]
        public int ForLevel { get; set; }
        
        
        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;                      

            var model = new MultiLayerDataViewModel(Container.Value, Class ,ForLevel, Keys[ForLevel]);
            model.NamePattern = NameFor;
            var viewContext = new ViewContext(ViewContext, ViewContext.View,
                new ViewDataDictionary<MultiLayerDataViewModel>(ViewContext.ViewData, model), ViewContext.Writer);
         
            var view = _viewEngine.FindView(viewContext, "MultiLayerDataViewModel", false);
            
            await view.View.RenderAsync(viewContext);
        }
    }
}




//public async override void Process(TagHelperContext context, TagHelperOutput output)
//{
//    var sw = new StringWriter();

//    // Create a new viewData (viewbag). This will be used in a new ViewContext to define the model we want
//    ViewDataDictionary viewData = new ViewDataDictionary(ViewContext.ViewData, For.Model);

//    // Generate a viewContext with our viewData
//    var viewContext = new ViewContext(ViewContext, ViewContext.View, viewData, ViewContext.TempData, sw, new HtmlHelperOptions());

//    // Use the viewContext to run the given ViewName
//    output.Content.Append(new HtmlString(await viewContext.RenderPartialView(ViewName)));

//}


//public async static Task<string> RenderPartialView(this ViewContext context, string viewName, ICompositeViewEngine viewEngine = null, ViewEngineResult viewResult = null)
//{
//    viewEngine = viewEngine ?? context.HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>();

//    viewResult = viewResult ?? viewEngine.FindPartialView(context, viewName);

//    await viewResult.View.RenderAsync(context);

//    return context.Writer.ToString();

//}