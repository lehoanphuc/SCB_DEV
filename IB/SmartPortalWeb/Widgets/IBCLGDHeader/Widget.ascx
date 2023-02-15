<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCLGDHeader_Widget" %>

<style>
.al
{
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
}
</style>
<div class="al">
<span><%=Resources.labels.chuyenkhoantronghethongtheolo %></span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<div class="al" style="text-align:right;padding-right:10px;">
    <a href="TemplateDownload/Batch Transfer.xls"><%= Resources.labels.downloadTemplate %></a> 
</div>