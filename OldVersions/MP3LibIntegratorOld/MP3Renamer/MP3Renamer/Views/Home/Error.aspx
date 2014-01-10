<%@ Page Title="" Language="C#" MasterPageFile="~/Views/MP3Renamer.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<MP3Renamer.Helper.RuleViolation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Error
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Error</h2>

    <%foreach (var vio in Model)
      {%>
        <p><%:vio.Key %>   <%:vio.Value %> </p>
          
      <%} %>



</asp:Content>
