<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserApproveLimit_Delete_Widget" %>

<br />

<div class="al">
    <asp:Label ID="Label2" runat="server"
        Text="<%$ Resources:labels, thietlaphanmucduyetgiaodich %>"></asp:Label>
    <br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
    <br />
</div>
<!-- Thong tin khach hang-->
<!--end-->
<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">
    <div class="divGetInfoCust">
        <figure>
            <legend class="handle"><%=Resources.labels.thongtinxacnhan %></legend>
        </figure>
        <div class="alg">
            <asp:Label ID="Label1" runat="server"
                Text="<%$ Resources:labels, bancochacchanmuonhuykhong %>"></asp:Label>
        </div>
    </div>

    <div style="text-align: center; padding-top: 10px;">
        <asp:Button ID="btback" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" PostBackUrl="javascript:history.go(-1)" />

        <asp:Button ID="btsaveandcont" runat="server" CssClass="btn btn-danger" Text="<%$ Resources:labels, huy %>" Width="71px"
            OnClick="btsaveandcont_Click" />&nbsp;&nbsp;
    </div>
</asp:Panel>

<style>
    .alg {
        text-align: center;
        font-weight: bold;
    }
</style>
