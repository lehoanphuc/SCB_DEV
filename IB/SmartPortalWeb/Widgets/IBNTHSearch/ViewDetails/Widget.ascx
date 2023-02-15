<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBNTHSearch_Details_Widget" %>

<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="JS/common.js"> </script>
<style>
    .divcontent div.row div:nth-child(odd) {
        text-align: left !important;
    }
</style>

<div class="divError">
    <asp:Label ID="lblTextError" runat="server" ForeColor="Red"></asp:Label>
</div>
<asp:Panel ID="pnConfirm" runat="server" CssClass="divcontent">
    <%--sender information--%>
    <div class="header-title">
        <label class="bold"><%= Resources.labels.thongtinnguoithuhuong %></label>
    </div>
    <div class="divcontent">
        <div class="content_table_4c_cl">
            <div class="row">
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, loaigiaodich %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lbgiaodich" runat="server" Font-Bold="False"></asp:Label>
                </div>
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label56" runat="server" Text="<%$ Resources:labels, tennguoithuhuong %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lbnguoithuhuong" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, taikhoan %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lbacctno" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label91" runat="server" Text="<%$ Resources:labels, tainganhang %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lblBank" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, socmnd %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lbcmnd" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, ngaycap %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lbngaycap" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, noicap %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lbnoicap" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label90" runat="server" Text="<%$ Resources:labels, diachinguoinhantien %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-3 line30 height30 ">
                    <asp:Label ID="lblConfirmAddress" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-4 col-sm-3 line30 height30 ">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                </div>
                <div class="col-xs-8 col-sm-9 line30 height30 ">
                    <asp:Label ID="lbdesc" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <!--Button next-->
    <div style="text-align: center; margin-top: 10px;">
        <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning"
            Text="<%$ Resources:labels, quaylai %>"  OnClick="btnBack_OnClick"/>
        &nbsp;
    </div>
</asp:Panel>



