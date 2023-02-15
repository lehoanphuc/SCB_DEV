<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Controls_ViewMode_Widget" %>
<div>
    <asp:RadioButton ID="radView" runat="server" GroupName="mode" 
        Text='<%$ Resources:labels, view %>' AutoPostBack="True" Checked="True" 
        oncheckedchanged="radView_CheckedChanged" />
    <asp:RadioButton ID="radEdit" runat="server" GroupName="mode" 
        Text='<%$ Resources:labels, edit %>' AutoPostBack="True" 
        oncheckedchanged="radEdit_CheckedChanged" />
</div>