<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFastBanking_Controls_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/SEMSFastBanking/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css"> 
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css"> 

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
     #divATMHeader
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
<link href="widgets/SEMSFastBanking/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divATMHeader">
    <asp:Image ID="imgLoGo" ImageUrl="../../../widgets/SEMSFastBanking/Images/fastbank.png" runat="server" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" />  
    <asp:Label ID="lblATMHeader" runat="server"></asp:Label>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div id="divError">
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
</div>

<div class="divAddInfoPro">

<asp:Panel ID="pnAdd" runat="server">
<div class="divHeaderStyle">
       <%=Resources.labels.thongtinshop %>
    </div>
    <table class="style1" cellpadding="3">
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label><asp:Label ID="Label7" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtContractNo" runat="server" AutoPostBack="True" OnTextChanged="OnEnterContractNo"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, shopname %>"></asp:Label><asp:Label ID="Label3" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtShopName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, email %>"></asp:Label><asp:Label ID="Label4" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, dienthoai %>"></asp:Label><asp:Label ID="Label12" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblshopcode" runat="server" Text="<%$ Resources:labels, shopcode %>"></asp:Label><asp:Label ID="Label13" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtShopCode" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, shopaccountno %>"></asp:Label><asp:Label ID="Label14" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtShopAcctNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>            
            <td>
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, shopsuspendaccountno %>"></asp:Label><asp:Label ID="Label16" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSuspendAcctNo" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, shopincomaccountno %>"></asp:Label><asp:Label ID="Label17" runat="server" Text=" *"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIncomAcctNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <%--<tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, tinhthanhpho %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" onselectedindexchanged="ddlCity_SelectedIndexChanged" Width="130px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, quanhuyen %>"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" Width="130px">
                </asp:DropDownList>
            </td>
        </tr>--%>
    </table>  	      
     

</asp:Panel>

   </div>   
   
  
 


</ContentTemplate>

</asp:UpdatePanel>
<div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btsave" runat="server"  Text="<%$ Resources:labels, save %>" 
                         onclick="btsave_Click" OnClientClick="return validate();" />
&nbsp;
                    <asp:Button ID="btback" runat="server"  Text="<%$ Resources:labels, back %>" 
                        onclick="btback_Click" />
                 &nbsp;
                    </div>

<script language="javascript">
    function validate()
    {
        if (validateEmpty('<%=txtContractNo.ClientID %>', '<%=Resources.labels.bancannhapcontractno %>'))
        {
            if (validateEmpty('<%=txtShopName.ClientID %>', '<%=Resources.labels.bancannhapshopname %>'))
                {
                    if (validateEmpty('<%=txtShopName.ClientID %>', '<%=Resources.labels.bancannhapshopname %>'))
                        {
                        if (checkEmail('<%=txtEmail.ClientID %>', '<%=Resources.labels.bancannhapemail %>'))
                            {
                            if (validateEmpty('<%=txtPhoneNo.ClientID %>', '<%=Resources.labels.sodienthoaikhongdungdinhdangso %>'))
                                {
                                if (validateEmpty('<%=txtShopAcctNo.ClientID %>', '<%=Resources.labels.bancannhaptaikhoancuashop %>'))
                                {
                                    if (validateEmpty('<%=txtSuspendAcctNo.ClientID %>', '<%=Resources.labels.bancannhaptaikhoansuspend %>'))
                                    {
                                        if (validateEmpty('<%=txtIncomAcctNo.ClientID %>', '<%=Resources.labels.bancannhaptaikhoanincom %>'))
                                        {
                                            var minlength = <%= int.Parse(ConfigurationManager.AppSettings["minlengthloginname"].ToString())%>
                                            var maxlength = 20
                                            if (document.getElementById('<%=txtShopCode.ClientID %>').value.length >= minlength && document.getElementById('<%=txtShopCode.ClientID %>').value.length <= maxlength)
                                            {
                                                return true;
                                            } 
                                            else 
                                            {
                                                document.getElementById('<%=txtShopCode.ClientID %>').focus();
                                                alert(maxlength);
                                                alert('<%=string.Format(Resources.labels.usernamemustbetween, ConfigurationManager.AppSettings["minlengthloginname"].ToString(), ConfigurationManager.AppSettings["maxlengthloginname"].ToString()) %>');
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            document.getElementById('<%=txtIncomAcctNo.ClientID %>').focus();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        document.getElementById('<%=txtSuspendAcctNo.ClientID %>').focus();
                                        return false;
                                    }
                                }
                                else
                                {
                                    document.getElementById('<%=txtShopAcctNo.ClientID %>').focus();
                                    return false;
                                }
                            }
                            else
                            {
                                document.getElementById('<%=txtPhoneNo.ClientID %>').focus();
                                return false;
                            }
                        }
                        else
                        {
                            document.getElementById('<%=txtEmail.ClientID %>').focus();
                            return false;
                        }
                    }
                    else
                    {
                        document.getElementById('<%=txtShopName.ClientID %>').focus();
                        return false;
                    }
            }
            else
            {
                document.getElementById('<%=txtContractNo.ClientID %>').focus();
                return false;
            }
        }
    }
    
    
</script>