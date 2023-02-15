<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCustInfo1_Widget" %>

<div class="al">
    <span><%=Resources.labels.userinfo %></span><br>
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png">
</div>

<div class="divcontent">
    <div class="content_table_4c_cl">
        <div class="row">
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, fullname %>"></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="lblAccountName" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, loainguoidung %>"></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="lblUserType" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, gender %>"></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="lblCreateDay" runat="server" Text="<%$ Resources:labels, ngaysinhngaythanhlap %>"></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="lblBirthday" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, email %>"></asp:Label>
            </div>
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-md-3 line30">
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
            </div>
            <div class="col-xs-6 col-md-9 line30">
                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</div>
