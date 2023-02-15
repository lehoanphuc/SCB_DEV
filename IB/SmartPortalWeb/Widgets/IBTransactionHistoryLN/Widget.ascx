<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransactionHistoryLN_Widget" %>
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
                
            </asp:DropDownList></td>
            <td><asp:Button ID="Button1" runat="server" Text='<%$ Resources:labels, view %>' 
                    onclick="Button1_Click" /></td>
        </tr>
    </table>


<!--thong tin tai khoan vay-->
<asp:Panel ID="pnLN" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.thongtintaikhoan %> 
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, accountnumber %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountNumber_LN" runat="server" Text="00009102"></asp:Label>
                                    &nbsp;</td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label62" runat="server" Text="<%$ Resources:labels, accountname %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountName_LN" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label68" runat="server" 
                                        Text="<%$ Resources:labels, dateopened %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblDO_LN" runat="server" Text="11/01/2009"></asp:Label>
                                    &nbsp;</td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label70" runat="server" 
                                        Text="<%$ Resources:labels, ExpireDate %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblExpDate" runat="server" Text="12/02/2010"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            
                            
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label116" runat="server" 
                                        Text="<%$ Resources:labels, duno %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblBalance" runat="server" Text="1.000.000"></asp:Label>
                                    &nbsp;</td>
                               <td class="thtdbold">
                                    <asp:Label ID="Label150" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCCYID" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label44" runat="server" Text="<%$ Resources:labels, chinhanhphonggiaodich %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblBranch_DD" runat="server"></asp:Label>
                                </td>
                               <td class="thtdbold">
                                   
                                </td>
                                <td class="thtd">
                                    
                                </td>
                            </tr>                        
                            </table>
                    </div>                
                 
    </div>
    <%--<div style="padding-left:5px; margin-top:10px; text-align:center">
            <asp:Button ID="Button3" runat="server" Text="Lịch trả nợ" 
                onclick="Button3_Click" />
    </div>--%>
</asp:Panel>
<BR />
<asp:Panel runat="server" ID="pnTH">
    <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.tracuulichtrano %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="0">                            
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
                                        onclick="Button3_Click" />
                                </td>
                            </tr>
                                                    
                        </table>
                        
                    </div>                
                 
    </div>  
    
</asp:Panel>
<div style="height:250px; overflow:auto; margin-top:10px; width:99.3%; padding-left:3px;">
                            <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
                        </div>

<!--end-->
    <br />
<!--chi tiet giao dich -->
</div>


<script type="text/javascript">    //<![CDATA[

    var cal = Calendar.setup({
        onSelect: function(cal) { cal.hide() }
    });
    cal.manageFields("<%=txtFromDate.ClientID %>", "<%=txtFromDate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtToDate.ClientID %>", "<%=txtToDate.ClientID %>", "%d/%m/%Y");
    //]]></script>