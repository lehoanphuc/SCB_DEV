<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBankList_Controls_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>

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
    	padding:0px 0px 0px 2px;
   }
       .tblVDC
    {
    	 background-color:#F8F8F8;
    	width:100%;
    	border:solid 1px #B9BFC1;
    	margin-bottom:5px;
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
<div id="divProductHeader">
    <asp:Image ID="imgLoGo" runat="server" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" /> 
    <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>

</div>
<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSProduct/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
</ProgressTemplate>
</asp:UpdateProgress>
</div>

<div class="divAddInfoPro">
<asp:Panel ID="pnResult" runat="server">
<div class="divHeaderStyle">
<%=Resources.labels.ketquathuchien %>
</div>
<div style="text-align:center;font-weight:bold">
<br />
<asp:Label runat="server" ID="lbResult" ForeColor="Red"></asp:Label>
<br />
<br />
</div>

</asp:Panel>
<asp:Panel ID="pnAdd" runat="server">
<div class="divHeaderStyle">
       <%=Resources.labels.thongtinnganhang %>
    </div>
    <table class="style1" cellpadding="3">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, machinhanh%>"></asp:Label> *
            </td>
            <td>
                <asp:TextBox ID="txtbankcode" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, chinhanh%>"></asp:Label> *
            </td>
            <td>
                <asp:TextBox ID="txtbankname" runat="server"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, nganhang%>"></asp:Label> *
            </td>
            <td>
                         <asp:DropDownList ID="ddlbank" runat="server"
                                        SkinID="extDDL2">
                        </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, tinhthanh%>"></asp:Label> *
            </td>
            <td>
                        <asp:DropDownList ID="ddlProvince" runat="server" SkinID="extDDL4">
                        </asp:DropDownList>
            </td>

        </tr>
         <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, chuyendien%>"></asp:Label> *
            </td>
            <td>
                       <asp:DropDownList ID="ddlremit" runat="server" SkinID="extDDL4">
                        </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, mota%>"></asp:Label> *
            </td>
            <td>
                <asp:TextBox ID="txtdesc" runat="server"></asp:TextBox>
            </td>

        </tr>

    </table>  	      
</asp:Panel>
   </div>   
</ContentTemplate>

</asp:UpdatePanel>
<div style="text-align:center; margin-top:10px;">
            <asp:Button ID="btsave" runat="server"  Text="<%$ Resources:labels, save %>" 
                        OnClientClick="return validate();" onclick="btsave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btback" runat="server"  
                Text="<%$ Resources:labels, back %>"
                onclick="btback_Click" />
                 
                    
</div> 

<script language="javascript">
    function validate() {
        if (validateEmpty('<%=txtbankcode.ClientID %>', '<%=Resources.labels.bancannhapmachinhanh %>')) {
                    if (validateEmpty('<%=txtbankname.ClientID %>', '<%=Resources.labels.bancannhaptenchinhanh %>')) {  
                    
                    }
                    else {
                        document.getElementById('<%=txtbankname.ClientID %>').focus();
                        return false;
                    }
        }
        else {
            document.getElementById('<%=txtbankcode.ClientID %>').focus();
            return false;
        }
    }
    
    
</script>