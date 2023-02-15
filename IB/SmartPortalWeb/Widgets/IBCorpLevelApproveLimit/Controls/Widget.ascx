<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserApproveLimit_Controls_Widget" %>

<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="widgets/SEMSProductLimit/JS/common.js" type="text/javascript"></script>

<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    #divSearch
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:5px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
    #divToolbar
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:20px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
     #divLetter
    {
    	background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:20px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
    #divResult
    {
    	
    	margin:20px 5px 5px 5px;
    	padding:5px 5px 5px 5px;
    }
    .gvHeader
    {
    	text-align:left;
    }
       .divHeaderStyle
   {
   	    background-color:#EAFAFF;color:#003366;   	    	   
   	    font-weight:bold;
   	    margin:0px;
   	    line-height:20px;
   	    padding:5px;
   }
    #divProductHeader
    {   
    	width:100%;	
    	font-weight:bold;
    	padding:5px 5px 5px 5px;
    }
    #divError
    {   
    	width:100%;	
    	font-weight:bold;
    	height:10px;
    	text-align:center;
    	padding:0px 5px 5px 5px;
    }
    .hightlight
    {
    	background-color:#EAFAFF;color:#003366;
    }
    .nohightlight
    {
    	background-color:White;
    }
       .divAddInfoPro
   {
   	    background-color:#F8F8F8;
    	border:solid 1px #B9BFC1;
    	margin:2px 5px 5px 5px;
    	padding:0px 0px 0px 0px;
   }
       .tblVDC
    {
    	 background-color:#F8F8F8;
    	width:100%;
    	border:solid 1px #B9BFC1;
    	margin-bottom:5px;
    }
        .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:10px;
	    padding-bottom:10px;
    }
</style>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css"> 
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css"> 
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<br />
<%--<div id="divProductHeader">
    <asp:Image ID="imgLoGo" runat="server" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>

</div>--%>
<div class="al">
<asp:Label ID="Label3" runat="server" 
                                        Text="<%$ Resources:labels, thietlaphanmucduyettheocapbac %>"></asp:Label><br />
<br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>

<div class="divAddInfoPro">

<asp:Panel ID="pnAdd" runat="server">
<div class="divHeaderStyle">
       <%=Resources.labels.thongtinhanmucduyetgiaodich %>
    </div>
    <table class="style1" cellpadding="3">
        <tr>
            <td Width="15%">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, capbac %>"></asp:Label>
            </td>
            <td Width="30%">
                <asp:DropDownList ID="ddlTeller" runat="server" Width="87%">
                </asp:DropDownList>
            </td>
            <td Width="15%">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, loaigiaodich %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTrans" runat="server" Width="80%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, hanmuc %>"></asp:Label>&nbsp;*
            </td>
            <td>
                <asp:TextBox ID="txtlimit" runat="server" Width="85%"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, tiente %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCCYID" runat="server" Width="80%">
                <asp:ListItem Value="LAK" Text="<%$ Resources:labels, LAK %>"></asp:ListItem>
                
                </asp:DropDownList>
            </td>
            
        </tr>
                <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDesc" runat="server" Width="85%"></asp:TextBox>
            </td>

        </tr>
    </table>  	      
</asp:Panel>
   </div>   
</ContentTemplate>

</asp:UpdatePanel>
<div style="text-align:center; margin-top:10px;">
<asp:Button ID="btsave" runat="server"  Text="<%$ Resources:labels, luu %>"
                          OnClientClick="return validate();" onclick="btsave_Click" Width="70px" />&nbsp;&nbsp;
                    <asp:Button ID="btback" runat="server"  Text="<%$ Resources:labels, quaylai %>" PostBackUrl="javascript:history.go(-1)" />
                 
                    
</div> 

<script language="javascript">
function validate()
    {
    
      if(validateEmpty('<%=txtlimit.ClientID %>','<%=Resources.labels.hanmucgiaodichkhongrong %>'))
         {
                
         }
      else
         {
           document.getElementById('<%=txtlimit.ClientID %>').focus();
           return false;
         }
        
       
        
    }
    
    
</script>