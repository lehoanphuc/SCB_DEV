<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSDeposit_ViewDetails_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%; 
        background-color:#EAEDD8;       
    }
    .tibtd
    {
    	
    }
    .tibtdh
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    }
    .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:10px;
	    padding-bottom:10px;
    }
        .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
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
        .style3
    {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }
</style>
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div class="al">
<img alt="" src="widgets/SEMSTransactionsApprove/Images/icon_transactions.jpg" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
<span><%=Resources.labels.chitietgiaodich %></span><br />

</div>
 <div id="divError" style="text-align:center; color:Red;">
<asp:Label ID="lblError" runat="server"></asp:Label>
</div>
 <div class="block1">            	 
            	            
                    
                        <table class="style1" cellspacing="0" cellpadding="5">
                             <tr>
                                <td class="thtdf" style="width:20%;">
                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
                                </td>
                                <td class="style3" style="width:20%;">
                                    <asp:Label ID="lblTransID" runat="server"></asp:Label>
                                &nbsp;</td>   
                                <td class="thtdbold" style="width:20%;">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, ngaygiogiaodich %>"></asp:Label>
                                </td>
                                <td class="thtd" style="width:20%;">
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                &nbsp;</td>                                 
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, debitaccount %>"></asp:Label>
                                </td>
                                <td class="style3" >
                                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                &nbsp;</td>  
                                 <td class="thtdbold">
                                    <asp:Label ID="Label50" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label>
                                &nbsp;</td>                                 
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                                </td>
                                <td class="style3" >
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                                &nbsp;</td>  
                               <td class="thtdbold">
                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                    
                                &nbsp;</td>                                
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label51" runat="server" Text="<%$ Resources:labels, nganhang %>"></asp:Label>
                                </td>
                                <td class="style3" >
                                    <asp:Label ID="lblBank" runat="server"></asp:Label>
&nbsp;</td>  
                               <td class="thtdbold">
                    <asp:Label ID="Label52" runat="server" Text="<%$ Resources:labels, socmnd %>"></asp:Label>
                                </td>
                                <td class="thtd">
                    <asp:Label ID="lblLicense" runat="server"></asp:Label>
                &nbsp;</td>                                
                            </tr>
                            <tr>
                                <td class="thtdf">
                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, ngaycap %>"></asp:Label>
                                </td>
                                <td class="style3" >
                    <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                &nbsp;</td>  
                               <td class="thtdbold">
                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, noicap %>"></asp:Label>
                                </td>
                                <td class="thtd">
                    <asp:Label ID="lblIssuePlace" runat="server"></asp:Label>
                &nbsp;</td>                                
                            </tr>
                            <tr>
                                <td class="thtdf">
                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                                </td>
                                <td class="style3" >
                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                &nbsp;</td>  
                               <td class="thtdbold">
                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                                </td>
                                <td class="thtd">
                    <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                &nbsp;</td>                                
                            </tr>
                            <tr>
                                <td class="thtdf">
                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, diachinguoinhantien %>"></asp:Label>
                                </td>
                                <td class="style3" >
                    <asp:Label ID="lblReceiverAdd" runat="server"></asp:Label>
                &nbsp;</td>  
                               <td class="thtdbold">
                                    &nbsp;</td>
                                <td class="thtd">
                                    &nbsp;&nbsp;</td>                                
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label48" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                &nbsp;</td>
                                <td class="thtdbold">
                                    
                                    <asp:Label ID="Label49" runat="server" Text="Kết quả" Visible="False"></asp:Label>
                                    
                                </td>
                                <td class="thtd">
                                   
                                    <asp:Label ID="lblResult" runat="server" Visible="False"></asp:Label>
                                   
                                &nbsp;</td>
                            </tr>         
                        </table>
                                   
                 <!--Button next-->
                 
                  
    </div>
<br />

      
<div style="text-align:center; margin-top:10px;">
&nbsp;
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" 
                         Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1)" />
                 </div>
<!--end-->
<script type="text/javascript">
</script>
