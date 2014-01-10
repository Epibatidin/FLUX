<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<MP3Renamer.DataContainer.EntityInterfaces.IRoot>" %>
    
    <tbody>
        <%foreach (var subroot in Model.SubRoots)
          { %>
           <%foreach (var leaf in subroot.Leafs)
             { %>
        <tr>
            <td>
                <%:Model.Name %>
            </td> 
            <td>
                <%:subroot.Year %>
            </td>
            <td>
                <%:subroot.Name %>
            </td> 
            <td>
                <%:leaf.Count %>
            </td>
            <td>
                <%:leaf.Number %>
            </td> 
            <td>
                <%:leaf.Name %>
            </td>
        </tr>
        <%} %>
        <%} %>
    </tbody>