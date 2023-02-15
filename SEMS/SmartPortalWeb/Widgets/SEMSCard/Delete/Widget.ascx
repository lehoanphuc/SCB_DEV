<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCard_Detele_Widget" %>

<link href="widgets/SEMSCard/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCard/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSCard/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/SEMSCard/JS/tab-view.js" type="text/javascript"></script>

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

<script type="text/javascript" src="widgets/SEMSCard/js/common.js"> </script> 
<script type="text/javascript" src="widgets/SEMSCard/js/subModal.js"> </script> 
<script type="text/javascript" src="widgets/SEMSCard/js/commonjs.js"> </script> 

<link href="widgets/SEMSCard/css/style.css" rel="stylesheet" type="text/css"> 
<!-- Add this to have a specific theme--> 
<link href="widgets/SEMSCard/css/subModal.css" rel="stylesheet" type="text/css"> 

<style>
.th
{
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
}
</style>
<div class="th">
<span><%=Resources.labels.deletecardinformation %></span><br />
<img style="margin-top:5px;" src="widgets/IBTransactionHistory/Images/underline.gif" />
</div>


<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>

<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">

<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.thongtinchitiet %>
                    </div>                    
                    <div class="content">
    <div class="divHeaderStyle">
       <%=Resources.labels.deletecard %>
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td align="center" class="style2">
                <asp:Label ID="lblConfirm" runat="server" 
                    Text="<%$ Resources:labels, areyousuredeletethiscard %>"></asp:Label>
            </td>
           
        </tr>
        </table>

</div>
</div>

    
</asp:Panel>
<asp:Panel runat="server">
<div style="text-align:center; padding-top:10px;">
    <asp:Button ID="btsaveandcont" runat="server" onclick="btsaveandcont_Click" 
        Text="<%$ Resources:labels, xoa %>" Width="71px" />
&nbsp;
    <asp:Button ID="btback" runat="server" onclick="btback_Click" Text="<%$ Resources:labels, quaylai %>" />
    &nbsp; &nbsp;
    </asp:Panel>
<!--end-->
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">
   
</script>

    
    
   