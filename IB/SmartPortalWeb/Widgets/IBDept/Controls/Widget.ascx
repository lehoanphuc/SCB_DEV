<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBDept_Controls_Widget" %>

<link href="widgets/IBDept/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBDept/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/IBDept/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/IBDept/JS/tab-view.js" type="text/javascript"></script>

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
</style>

<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />

<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="widgets/SEMSUser/js/common.js"> </script> 
<script type="text/javascript" src="widgets/SEMSUser/js/subModal.js"> </script> 
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script> 

<link href="widgets/IBDept/css/style.css" rel="stylesheet" type="text/css"> 
<!-- Add this to have a specific theme--> 
<link href="widgets/IBDept/css/subModal.css" rel="stylesheet" type="text/css"> 


<asp:Panel runat="server" ID="pnRole">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Thông tin chi tiết
                    </div>                    
                    <div class="content">
                        
                            <div class="divHeaderStyle">
                               Thông tin phòng ban
                            </div>
                            <table class="style1" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" Text="Tên phòng"></asp:Label>
                                        &nbsp;*</td>
                                    <td>
                                        <asp:TextBox ID="txtroomname" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Mô tả"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdesc" runat="server"></asp:TextBox>
                                    </td>
                                   
                                </tr>
                                </table>
                        
                   </div>
</div>
<div style="text-align:center; padding-top:10px; padding-bottom:10px" >
<asp:Button ID="btsaveandcont" runat="server" Text="<%$ Resources:labels, save %>" 
        OnClientClick="return validate1();" onclick="btsaveandcont_Click" 
        Width="69px"/>
        &nbsp;&nbsp;
    <asp:Button ID="btback" runat="server" Text="Quay lại" PostBackUrl="javascript:history.go(-1)" 
         />

</div>

</asp:Panel>

<script type="text/javascript">
   function validate1()
    {
   if(validateEmpty('<%=txtroomname.ClientID %>','Bạn cần nhập tên phòng'))
      {
                
      }
        else
         {
              document.getElementById('<%=txtroomname.ClientID %>').focus();
             return false;
         }
      }   
</script>

    
    
   