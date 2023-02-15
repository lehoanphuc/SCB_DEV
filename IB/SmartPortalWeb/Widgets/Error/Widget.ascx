<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Error_Widget" %>
<link href="widgets/error/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<div class="content" style="padding:10px;">
    <div class="row">
        <label class="bold"><%= Resources.labels.notifycode %> : </label>
        <asp:Label ID="lblErrorCode" runat="server" SkinID="lblImportant"></asp:Label>
    </div>
    <div class="row">
        <label class="bold"><%= Resources.labels.notifydesc %> : </label>
        <asp:Label ID="lblErrorDesc" runat="server" SkinID="lblImportant"></asp:Label>
    </div>
    <div class="row" style="text-align: center; margin-top: 20px;">
        <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
        <a id="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>
    </div>
</div>

