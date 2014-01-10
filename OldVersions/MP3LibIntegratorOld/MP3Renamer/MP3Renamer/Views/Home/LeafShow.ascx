<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<MP3Renamer.DataContainer.EntityInterfaces.ILeaf>" %>

<p>
<%:Model.Number %>

<%:Model.Count %>

<%:Model.Name %>
</p>