<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBNTHSearch_Delete_Widgets" %>



<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<div class="al">
    <span><%=Resources.labels.xoanguoithuhuong %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<!-- Thong tin khach hang-->
<!--end-->
<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">
    <figure>
        <legend class="handle"><%=Resources.labels.thongtinxacnhan %></legend>
        <div class="content display-label" style="text-align: center">
            <asp:Label runat="server" ID="lblConfirm" CssClass="bold"><%= Resources.labels.banchacchanmuonhuynguoithuhuongkhong %></asp:Label>
            <div style="text-align: center; padding-top: 10px;">
                <asp:Button ID="Button2" CssClass="btn btn-warning" runat="server" Text="<%$ Resources:labels, quaylai %>" PostBackUrl="javascript:history.go(-1)" />
                <asp:Button ID="btsaveandcont" CssClass="btn btn-danger" runat="server" Text="<%$ Resources:labels, huy %>" Width="71px"
                    OnClick="btsaveandcont_Click" />
            </div>
        </div>
    </figure>

</asp:Panel>

<asp:Panel runat="server" ID="pnResult">
    <figure>
        <legend class="handle"><%=Resources.labels.ketquagiaodich %></legend>
        <div class="content display-label" style="text-align: center">
            <asp:Label runat="server" ID="lblError" ForeColor="Red" CssClass="bold" Text="<%$ Resources:labels, huynguoithuhuongthanhcong %>"></asp:Label>
        </div>
        <div style="text-align: center; padding-top: 10px;">
            <asp:Button ID="BtnThoat" CssClass="btn btn-warning" runat="server" Text="<%$ Resources:labels, thoat %>" Width="71px" OnClick="BtnThoat_Click" />
        </div>
    </figure>

</asp:Panel>


