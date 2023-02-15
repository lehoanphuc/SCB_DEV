<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUser_Add" %>
<%@ Register Src="../Controls/Widget.ascx" TagPrefix="uc1" TagName="Widget" %>
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
        <span><%=Resources.labels.adduser %></span><br />
        <img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
    </div>
    <uc1:Widget runat="server" ID="Widget" />

</div>
