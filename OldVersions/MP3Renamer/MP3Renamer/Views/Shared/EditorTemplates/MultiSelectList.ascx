<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SelectListItem>>" %>

<%
    string IsChecked = "";
    string name = (string)ViewData["Name"];
    foreach(SelectListItem item in Model)
    {
        if (item.Selected)        
            IsChecked = "checked=\"checked\"";        
        else
            IsChecked = "";
%>
<label><input type="checkbox" <%=IsChecked %> name="<%:name%>" value="<%:item.Value%>" />&nbsp;<%:item.Text%></label><br />
<%--<%=Html.CheckBox(name,item.Selected) %><br />--%>
<%} %>
<%--<input type="hidden" name="<%:name%>" value =""/>--%>
