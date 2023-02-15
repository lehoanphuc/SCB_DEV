<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCard_Add_Widget" %>
<%@ Register Src="~/Widgets/SEMSCard/Controls/Widget.ascx" TagPrefix="uc1" TagName="Widget" %>
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCustomerList/Images/messenger.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.themnguoidungmoi %>
</div>
<uc1:Widget runat="server" ID="Widget" />
