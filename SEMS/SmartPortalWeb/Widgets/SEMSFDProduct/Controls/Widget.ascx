<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFDProduct_Controls_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
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

<link href="widgets/SEMSProvince/Images/branch.png" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divFDProductHeader">
    <asp:Image ID="imgLoGo" ImageUrl="../../../widgets/SEMSProvince/Images/branch.png" runat="server" style="width: 32px; height: 32px; margin-bottom:10px;" align="middle" />  
    <b><asp:Label ID="lblFDProductHeader" runat="server" ></asp:Label></b>
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
       <%=Resources.labels.thongtinquanhuyen %>
    </div>
    <table class="style1" cellpadding="3">
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Mã sản phẩm"></asp:Label>
                &nbsp;*</td>
            <td>
                <asp:TextBox ID="txtFDProductID" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Tên sản phẩm"></asp:Label>
                &nbsp;*</td>
            <td>
                <asp:TextBox ID="txtFDProductName" runat="server"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Kỳ hạn"></asp:Label>
                &nbsp;*</td>
            <td>
                <asp:TextBox ID="txtTerm" runat="server"></asp:TextBox>   
            </td>
             <td>
                <asp:Label ID="Label1" runat="server" Text="Lãi suất"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtInterestRate" runat="server"></asp:TextBox>   
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ghi chú"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>   
            </td>
             <td>
                <asp:Label ID="Label3" runat="server" Text="Diễn giải"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>   
            </td>
        </tr>
        
        <tr>
        <td class="style5" colspan="2">                       
            
                <asp:CheckBox Text="<%$ Resources:labels, tattoantruochan %>" runat="server" ID="cbIsClose" />
            </td>
    </table>  	      
     

</asp:Panel>

   </div>   
   
  
 


</ContentTemplate>

</asp:UpdatePanel>
<div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btsave" OnClientClick="return validateFD();" runat="server"  Text="<%$ Resources:labels, save %>" onclick="btsave_Click" />
&nbsp;
                    <asp:Button ID="btback" runat="server"  Text="<%$ Resources:labels, back %>" 
                        onclick="btback_Click"  />
                 &nbsp;
                    </div>
                    
                    
<script>
    function validateFD() {
        if (document.getElementById("<%=txtFDProductID.ClientID %>").value == '') {
            alert('<%=Resources.labels.masanphamtietkiemkhongrong %>');
            document.getElementById("<%=txtFDProductID.ClientID %>").focus();
            return false;
        }
        else {
            if (document.getElementById("<%=txtFDProductName.ClientID %>").value == '') {
                alert('<%=Resources.labels.tensanphamtietkiemkhongrong %>');
                document.getElementById("<%=txtFDProductName.ClientID %>").focus();
                return false;
            }
            else {
                if (document.getElementById("<%=txtTerm.ClientID %>").value == '') {
                    alert('<%=Resources.labels.kyhankhongrong %>');
                    document.getElementById("<%=txtTerm.ClientID %>").focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    }
</script>