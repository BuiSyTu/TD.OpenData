<%@ Page Language="C#" MasterPageFile="~sitecollection/_catalogs/masterpage/default/default.master" inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document"%>
<%@ Register Tagprefix="layout" Namespace="TD.Core.Layouts.Controls" Assembly="TD.Core.Layouts.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fdcb66d7090aabcd" %>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageHeaderTitle">
	Default page
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
<h1>-_-</h1>
<script type="text/x-handlebars-template" id="tableTemplate">
<table>
    <thead>
        {{#each array.[0]}}
            <th>{{@key}}</th>
        {{/each}}
    </thead>
    <tbody>
        {{#each array}}
        <tr>
            {{#each this}}
                <td>{{this}}</td>
            {{/each}}
        </tr>
        {{/each}}
    </tbody>
</table>

<div style="position: absolute;bottom: 5px;">
Read about this Fiddle at: <a href="http://jsdev.wikidot.com/howto:12" target="_blank">How To: Handlebars - Create Table element</a>
</div>
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageVendorStyles">
    <link type="text/css" rel="stylesheet" href="/_layouts/15/tdcore/v3/assets/vendors/custom/datatables/datatables.bundle.css" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageVendorScripts">
    <script type="text/javascript" src="/_layouts/15/tdcore/v3/assets/vendors/custom/datatables/datatables.bundle.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageScripts">
    <script type="text/javascript" src="Default.js"> </script>
</asp:Content>
