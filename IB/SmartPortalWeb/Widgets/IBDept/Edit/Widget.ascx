<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBDept_Edit_Widget" %>
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
<span>Sửa đổi thông tin phòng ban </span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
    <uc1:Widget ID="ucEdit" runat="server" />

</div>
