<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExportToFile.ascx.cs" Inherits="Widgets_SEMSCustomerListCorp_ExportToFile" %>


<div style="width:50%; margin:5px auto 5px auto;">

    <asp:RadioButtonList ID="radFile" runat="server">
        <asp:ListItem Selected="True" Value="E">Excel</asp:ListItem>
        <asp:ListItem Value="P">PDF</asp:ListItem>
        <asp:ListItem Value="W">Word</asp:ListItem>
        <asp:ListItem Value="C">CSV</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:labels, exporttofile %>" 
        onclick="btnExport_Click" />
</div>
<div>
    <asp:GridView ID="GridView1" runat="server" Visible="False">
    </asp:GridView>
</div>