<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBSearchTransaction_Widget" %>
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
    	padding:5px 5px 5px 5px;
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
                <asp:ListItem Value="00009102001">00009102001-Tài khoản thanh toán</asp:ListItem>
                <asp:ListItem Value="00009103001">00009103001-Tài khoản tiền gửi KKH</asp:ListItem>
                <asp:ListItem Value="00009104001">00009104001-Tài khoản tiền gửi CKH</asp:ListItem>
                <asp:ListItem Value="00009105001">00009105001-Tài khoản tiền vay</asp:ListItem>
            </asp:DropDownList></td>
            <td><asp:Button ID="Button1" runat="server" Text='<%$ Resources:labels, view %>' 
                    onclick="Button1_Click" /></td>
        </tr>
    </table>
<!--thong tin tai khoan DD-->
<asp:Panel ID="pnDD" runat="server">
    <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Thông tin tài khoản
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, accountnumber %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountNumber" runat="server" Text="00009102"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, accountname %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountName" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label109" runat="server" 
                                        Text="<%$ Resources:labels, status %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCurrency0" runat="server" Text="Active"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label9" runat="server" 
                                        Text="<%$ Resources:labels, currency %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCurrency" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, currentbalance %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCB" runat="server" Text="1.000.000 VNĐ"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAB" runat="server" Text="1.000.000 VNĐ"></asp:Label>
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
                    	Tìm kiếm giao dịch
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="0"> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label5" Font-Bold="True" runat="server" 
                                        Text="Mã giao dịch"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    <asp:Label ID="Label7" Font-Bold="True" runat="server" 
                                        Text="Loại giao dịch"></asp:Label>
                                    
                                </td>
                                <td class="thtds">
                                    
                                    <asp:DropDownList ID="ddlTransactionType" runat="server">
                                        <asp:ListItem>Rút tiền</asp:ListItem>
                                        <asp:ListItem>Nạp tiền</asp:ListItem>
                                        <asp:ListItem>Chuyển khoản</asp:ListItem>
                                        <asp:ListItem>Thanh toán online</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td class="thtds">
                                    
                                    &nbsp;</td>
                            </tr> 
                            <tr >
                                <td class="thtds">
                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="<%$ Resources:labels, fromdate %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="<%$ Resources:labels, todate %>"></asp:Label>
                                </td>
                                <td class="thtds">
                                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                </td>
                                <td class="thtds">
                                    
                                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:labels, view %>" 
                                        onclick="Button2_Click" />
                                    
                                </td>
                            </tr>
                                                    
                        </table>
                        <div>
                            <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
                        </div>
                    </div>                
                 
    </div>    
<!--end-->
</div>

<script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      cal.manageFields("<%=txtFromDate.ClientID %>", "<%=txtFromDate.ClientID %>", "%d/%m/%Y");      
      cal.manageFields("<%=txtToDate.ClientID %>", "<%=txtToDate.ClientID %>", "%d/%m/%Y");      
    //]]></script>