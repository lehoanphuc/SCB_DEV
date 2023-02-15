<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSDeposit_Delete_Widget" %>

<link href="widgets/SEMSUser/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSUser/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSUser/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/SEMSUser/JS/tab-view.js" type="text/javascript"></script>

<style type="text/css">
    .style1
    {
        width: 100%;
    }
   .divGetInfoCust
   {
   	    background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:2px 5px 5px 5px;
    	padding:0px 0px 0px 2px;
    	
    	
   }
   .divHeaderStyle
   {
   	    background-color:#EAFAFF;color:#003366;   	    	   
   	    font-weight:bold;
   	    margin:0px;
   	    line-height:20px;
   	    padding:5px;
   }
    .gvHeader
    {
    	text-align:left;
    }
    #divCustHeader
    {   
    	width:100%;	
    	font-weight:bold;
    	padding:0px 5px 0px 5px;
    }
    #divError
    {   
    	width:100%;
    	
    	height:10px;
    	text-align:center;
    	padding:5px 5px 5px 5px;
    }
    .hightlight
    {
    	background-color:#EAFAFF;color:#003366;
    }
    .nohightlight
    {
    	background-color:White;
    }
    .tblVDC
    {
    	 background-color:#F8F8F8;
    	width:100%;
    	border:solid 1px #B9BFC1;
    	margin-bottom:5px;
    }
    .style2
    {
        height: 30px;
    }
</style>

<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />

<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="widgets/SEMSUser/js/common.js"> </script> 
<script type="text/javascript" src="widgets/SEMSUser/js/subModal.js"> </script> 
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script> 

<link href="widgets/SEMSUser/css/style.css" rel="stylesheet" type="text/css"> 
<!-- Add this to have a specific theme--> 
<link href="widgets/SEMSUser/css/subModal.css" rel="stylesheet" type="text/css"> 


<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSTransactionsApprove/Images/icon_transactions.jpg" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <%=Resources.labels.hoantienchuyenkhoanngoaihethong %>

</div>
<div id="divError">
<%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>--%>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
<!-- Thong tin khach hang-->
<!--end-->
<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">
<div class="divGetInfoCust">
    <div class="divHeaderStyle">
       <%=Resources.labels.thongtinxacnhan %>
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td align="center" class="style2">
                <asp:Label ID="Label1" runat="server" 
                    Text="<%$ Resources:labels, banchacchanmuonhoantiencacgiaodichnay %>"></asp:Label>
            </td>
           
        </tr>
        </table>
</div>

    
</asp:Panel>
<div style="text-align:center; padding-top:10px;">
    <asp:Button ID="btsaveandcont" runat="server" Text="<%$ Resources:labels, hoantien %>" Width="71px" 
        onclick="btsaveandcont_Click"/>&nbsp;&nbsp;
    <asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, quaylai %>"
        onclick="btback_Click" 
         />
<!--end-->
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">
   
</script>

    
    
   