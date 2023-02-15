<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBAccountListFD_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%;
        margin-top:0px;
    }
    .trheaderFD
    {
    	background-color:#EAFAFF;color:#003366;
    	
    	font-weight:bold;
    }
</style>
<div>
    <%--<table class="style1" cellpadding="5" cellspacing="0">
        <tr class="trheaderFD">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Số tài khoản"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Mô tả"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Ngày giao dịch cuối"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Số dư"></asp:Label>
            </td>
        </tr>
        <tr>
           <td>
                <a href='#'>044009121</a></td>
           
            <td>
                Tài khoản thanh toán có kỳ hạn</td>
            <td>
                21/01/2010</td>
            <td>
                5.000.000 LAK</td>
                
        </tr>
        
    </table>--%>
    <asp:Literal ID="ltrFD" runat="server"></asp:Literal>            
</div>