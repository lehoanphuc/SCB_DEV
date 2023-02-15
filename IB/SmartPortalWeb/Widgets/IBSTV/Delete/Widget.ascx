<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBSTV_Delete_Widget" %>


<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<div class="al">
    <span><%=Resources.labels.huylichchuyenkhoan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<!-- Thong tin khach hang-->
<!--end-->
<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">
    <figure>
        <legend class="handle"><%=Resources.labels.thongtinxacnhan %></legend>
        <div class="content display-label" style="text-align: center">

            <label class="bold"><%= Resources.labels.banchacchanmuonhuylichkhong %></label>


            <div style="text-align: center; padding-top: 10px;">
                &nbsp;
                <asp:Button ID="btback" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" PostBackUrl="javascript:history.go(-1)" />
                &nbsp;
    <asp:Button ID="btsaveandcont" runat="server" CssClass="btn btn-danger" Text="<%$ Resources:labels, huy %>" Width="71px"
        OnClick="btsaveandcont_Click" />

            </div>
        </div>
    </figure>

</asp:Panel>

<asp:Panel runat="server" ID="pnresult">
    <figure>
        <legend class="handle"><%=Resources.labels.ketquagiaodich %></legend>
        <div class="content display-label" style="text-align: center">

            <asp:Label runat="server" ID="lblError" ForeColor="Red" CssClass="bold"></asp:Label>
            <div style="text-align: center; padding-top: 10px;">
                <asp:Button ID="BtnThoat" CssClass="btn btn-warning" runat="server" Text="<%$ Resources:labels, thoat %>" Width="71px" OnClick="BtnThoat_Click" />
            </div>
        </div>
    </figure>    

</asp:Panel>


