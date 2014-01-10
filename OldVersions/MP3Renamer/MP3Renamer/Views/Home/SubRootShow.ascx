<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<MP3Renamer.DataContainer.EntityInterfaces.ISubRoot>" %>

<table>
 <tr>
    <td>
        <%:Model.Year %>
    </td>
    <td>
        <%:Model.Name %>
    </td> 
    <td>
        <%foreach (var leaf in Model.Leafs)
          { %>
            <%Html.RenderPartial("LeafShow", leaf); %>

        <%} %>
    </td>

 </tr>
</table>
