<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractReview_Widget" %>

<div>
        <div style="margin-top: 15px;">
        <asp:Literal ID="ltrContractReview" runat="server"></asp:Literal>
    </div>
    <br />    <br />
    <div style="text-align:center">
        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, banin %>" OnClientClick="javascript:return poponload()" />
        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, dongcuaso %>" OnClick="btnExit_Click" OnClientClick="Loading(); return Confirm('Are you sure want to delete these records?');" />
    </div>
        <br />
</div>

<script>
    function poponload() {
        testwindow = window.open("widgets/SEMSContractReview/print.aspx", "BanIn",
            "menubar=1,scrollbars=1,width=800,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
</script>
