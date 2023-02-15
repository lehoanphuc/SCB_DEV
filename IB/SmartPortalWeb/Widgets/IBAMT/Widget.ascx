<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBAMT_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%;        
    }
    .style2
    {
    	margin:5px 0px 10px 0px;
    }
    .thtdbold
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-left:solid 1px #b9bfc1;
    	border-top:solid 1px #b9bfc1;
    }
    .thtd
    {
    	border-left:solid 1px #b9bfc1;
    	border-top:solid 1px #b9bfc1;
    }
    .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
    }
    .thtdff
    {  	
    	
    	border-top:solid 1px #b9bfc1;
    }
    .thtr
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    }
    .thtds
    {
    	padding:10px 5px 10px 5px;
    }
</style>
<link href="widgets/IBTransactionHistory1/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div>
    <table class="style2" cellpadding="3">
        <tr>
            <td><asp:Label ID="Label1"  Font-Bold="true" runat="server" Text='<%$ Resources:labels, account %>'></asp:Label></td>
            <td><asp:DropDownList ID="ddlAccount" runat="server">
                <asp:ListItem Value="00009102001">00009102001-Tài khoản tiền vay</asp:ListItem>
                <asp:ListItem Value="00009103001">00009103001-Tài khoản tiền vay</asp:ListItem>
                <asp:ListItem Value="00009104001">00009104001-Tài khoản tiền vay</asp:ListItem>
                <asp:ListItem Value="00009105001">00009105001-Tài khoản tiền vay</asp:ListItem>
            </asp:DropDownList></td>
            <td><asp:Button ID="Button1" runat="server" Text='<%$ Resources:labels, view %>' 
                    onclick="Button1_Click" /></td>
        </tr>
    </table>


<!--thong tin tai khoan vay-->
<asp:Panel ID="pnLN" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Thông tin tài khoản
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label60" runat="server" Text="Số tài khoản"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label61" runat="server" Text="00009102"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label62" runat="server" Text="Tên tài khoản"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label63" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label64" runat="server" 
                                        Text="Trạng thái"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label65" runat="server" Text="Active"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label66" runat="server" 
                                        Text="Loại hình"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label67" runat="server" Text="Vay tín chấp"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label68" runat="server" 
                                        Text="Ngày mở"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label69" runat="server" Text="11/01/2009"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label70" runat="server" 
                                        Text="Kỳ hạn"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label71" runat="server" Text="12/02/2010"></asp:Label>
                                </td>
                            </tr>
                            </table>
                    </div>                
                 
    </div>
</asp:Panel>
<!--end-->
    <br />
<!--chi tiet giao dich -->
    <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Các khoản ngoại bảng
                    </div>                    
                    <div class="content">
                        
                        <div>
                            <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
                        </div>
                    </div>                
                 
    </div>    
<!--end-->
</div>

