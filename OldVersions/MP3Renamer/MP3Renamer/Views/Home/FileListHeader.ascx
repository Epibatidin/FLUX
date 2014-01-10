<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MP3Renamer.ViewModels.FileListViewModel>" %>
<%@ Import Namespace="MP3Renamer.FileIO" %>


<%using (Html.BeginForm("SetRoots", "Home", FormMethod.Get)){%>
<div class="SideBySide">    
<%=Html.EditorFor(c => FileManager.Get.RootAsMSL, "MultiSelectList", new { Name = "Roots" })%>
<input type="submit" value ="SetRoots" title ="SetRoots" name ="SetRoots" />
</div>
<%} %>

<%using (Html.BeginForm("Proceed", "Home", FormMethod.Post))
  { %>
<div class = "SideBySide">
    <table>
        <tr>
        <th colspan="3">
            SubRoot
        </th>        
        </tr>
        <tr>
            <td>Clean</td>
            <td style="width:40px"></td>
            <td>Extract</td>
        </tr>
       <%-- <tr>
            <td>
            <%=Html.EditorFor(c => Model.SubRootCleanLevelAsMSL, "MultiSelectList", new { Name = "SubRootClean" })%>
            </td>
            <td></td>
            <td>
            <%=Html.EditorFor(c => Model.SubRootExtractLevelAsMSL, "MultiSelectList", new { Name = "SubRootExtract" })%>
            </td>
        </tr>    --%>
    </table>
</div>


<div class = "SideBySide">
    <table>
        <tr>
        <th colspan="3">
            Leaf
        </th>        
        </tr>
        <tr>
            <td>Clean</td>
            <td style="width:40px"></td>
            <td>Extract</td>
        </tr>
       <%-- <tr>
            <td>
            <%=Html.EditorFor(c => Model.LeafCleanLevelAsMSL, "MultiSelectList", new { Name = "LeafClean" })%>
            </td>
            <td></td>
            <td>
            <%=Html.EditorFor(c => Model.LeafExtractLevelAsMSL, "MultiSelectList", new { Name = "LeafExtract" })%>
            </td>
        </tr>    --%>
    </table>
</div>
<div style="clear:left" ></div>
<p>
<label><%=Html.CheckBox("FullProcessing",Model.FullProcessing)%>&nbsp;FullProcessing</label>    
</p>

<p>
<%=Html.ActionLink("Refresh", "FileList", "Home")%>
</p>

<input type="submit" value="Proceed" title="Proceed" name ="Proceed" />
<%} %>


<%    
    SelectList list = 
        new SelectList(
            new Dictionary<int,string>
            {
                {1, "Copy"} , 
                {2, "Create Moq"}
            } , "Key" , "Value" , 1);
 %>



<%using(Html.BeginForm("WriteToFS","Home", FormMethod.Post)){ %>
    
   <%= Html.DropDownList("CopyMode",list) %><br />  

   <input type= "submit" value="Execute" title="WriteToFS" />
<%} %>
<p>Es wurden insgesamt <%:FileManager.Get.LeafCount%> Songs in <%=FileManager.Get.SubRootCount%> Alben von <%=FileManager.Get.RootCount%> K&uuml;nstler gefunden</p>
