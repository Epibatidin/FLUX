<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Web.Mvc.SelectListItem>" %>

<%string name = (string)ViewData["Name"];
string checkedAttr = Model.Selected ? "checked=\"checked\"" : String.Empty;%>

<input type="checkbox" name="<%:name%>" value="<%:Model.Value%>" <%:checkedAttr%> />&nbsp;<%:Model.Text%>
<input type="hidden" name="<%:name%>" value="" />