using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace FLUX.Web.MVC.HtmlHelperEx
{
    public static class InputListHelpers
    {

        #region CheckBoxList
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper,
            string Name, 
            IEnumerable<SelectListItem> Items,
            object htmlAttributes)
        {
            TagBuilder outerDiv = new TagBuilder("div");
            StringBuilder labels = new StringBuilder();

            foreach (var item in Items)
            {
                labels.Append(BuildInputElement("checkbox", Name, item));
            }
            outerDiv.InnerHtml = labels.ToString();
            return MvcHtmlString.Create(outerDiv.ToString(TagRenderMode.Normal));
        }

        //public static MvcHtmlString CheckBoxListFor<TModel>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, int>> expression,
        //    IEnumerable<SelectListItem> Items,
        //    object htmlAttributes)
        //{
        //    var value = ExpressionHelper.GetExpressionText(expression);
        //    //var value = ModelMetadata.FromLambdaExpression(expression);
        //    var name = value;
        //    return MvcHtmlString.Create("");
        //}
        #endregion

        #region RadioButton

        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper,
            string Name,
            IEnumerable<SelectListItem> Items,
            object htmlAttributes)
        {
            TagBuilder outerDiv = new TagBuilder("div");
            StringBuilder labels = new StringBuilder();

            foreach (var item in Items)
            {
                labels.Append(BuildInputElement("radio", Name, item));
            }
            outerDiv.InnerHtml = labels.ToString();
            return MvcHtmlString.Create(outerDiv.ToString(TagRenderMode.Normal));
        }


        #endregion


        private static StringBuilder BuildInputElement(string Type, string Name, SelectListItem item)
        {
            StringBuilder b = new StringBuilder();
            b.AppendFormat("<label><input type='{0}' name='{1}' value='{2}' ", Type, Name, item.Value);

            if (item.Selected)
            {
                b.AppendFormat("checked='checked'");
            }
            b.AppendFormat("/>&nbsp;{0}</label><br /> ", item.Text);

            return b;
        }

    }
}
