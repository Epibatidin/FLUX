<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MP3Renamer.Master" 
Inherits="System.Web.Mvc.ViewPage<MP3Renamer.ViewModels.FileListViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    FileList text
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>FileList</h2>

<%Html.RenderPartial("FileListHeader", Model); %>

<table border ="1" width ="100%" >
    <tr>
        <th width ="15%">
            Artistname 
        </th>
        <th width ="85%" ></th>
    </tr>

    <%foreach (var root in MP3Renamer.FileIO.FileManager.Get.Roots())
      { %>
       <tr>
          <td width ="15%">
            <%:root.Name %>      
          </td>
          <td width ="85%">
            <table border ="1" width ="100%">
                <%foreach (var subroot in root.SubRoots)
                  { %>
                <tr>
                    <td width ="6%">
                        <%:subroot.Year%>
                    </td>
                    <td width ="30">
                        Name :  <%:subroot.Name%><br /> 
                        OldValue : <%:subroot.StringManager.RawData %> <br />
                        ProcceedValue :
                        <%foreach (var word in subroot.StringManager.RawDataParts)
                          { %>
                            <%=word %> |
                        <%} %> <br />


                    </td>
                    <td width ="64%">
                        <table border="1" width ="100%">
                            <%foreach (var leaf in subroot.Leafs)
                              { %>
                            <tr>
                                <td width ="10%">
                                    <%:leaf.Count %>
                                </td>
                                <td width ="10%">
                                    <%:leaf.Number %>
                                </td>
                                <td width ="80%">
                                    Name : <%:leaf.Name %><br />
                                    OldValue : <%:leaf.StringManager.RawData %> <br />
                                    ProcceedValue :
                                    <%foreach (string word in leaf.StringManager.RawDataParts)
                                    { %>
                                      <%=word %> |
                                  <%} %>
                                </td>
                            </tr>
                            <%} %>
                        </table>
                    
                    </td> 
                </tr>
            <%} %>
            </table>         
          </td>
       </tr>  

    <%} %>
    </table>

</asp:Content>
