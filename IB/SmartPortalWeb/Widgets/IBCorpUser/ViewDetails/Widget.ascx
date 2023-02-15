
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUser_ViewDetails_Widget" %>
<%@ Register src="../Controls/Widget.ascx" tagname="Widget" tagprefix="uc1" %>
<div>
<style>
.th
{
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
}
</style>
<div class="th">
<span><%=Resources.labels.xemchitietthongtinnguoidung %></span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
    <uc1:Widget ID="ucViewDetails" runat="server" />

</div>
