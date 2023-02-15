<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSSwiftCreate_ViewDetail_Widget" %>
<%@ Register src="../../../Controls/LetterSearch/LetterSearch.ascx" tagname="LetterSearch" tagprefix="uc1" %>

<link href="widgets/SEMSContractList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script src="widgets/SEMSContractList/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/SEMSContractList/JS/tab-view.js" type="text/javascript"></script>

<style>
.tblVDC
{
	width:100%;
	border:solid 1px #E3E3E3;
	background-color:#F8F8F8;
	margin-top:20px;
}
#tblVDC_Q
{
	width:100%;
	border:solid 1px #E3E3E3;
	background-color:#F8F8F8;
}
.tdVDC
{
	background-color:#EAFAFF;color:#003366;
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
    .divResult
    {
    	overflow:auto;
    	margin:20px 5px 5px 5px;
    	padding:0px 0px 0px 0px;
    	height:250px;
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
    	width:99%;	
    	font-weight:bold;
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
    .btnGeneral
    {}
</style>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSTransactionsApprove/Images/icon_transactions.jpg" style="width: 40px; height: 32px; margin-bottom:10px;" align="middle" /> 
    THÔNG TIN CHI TIẾT ĐIỆN

</div>
<div id="divError" style="text-align:center;">
<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
		<table id="tblVDC" cellspacing="1" cellpadding="5" class="tblVDC">
		    <tr>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label1" runat="server" Text="Mã giao dịch"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblTransID" runat="server"></asp:Label>
                </td>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label2" runat="server" Text="Ngày giao dịch"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblDate" runat="server" ></asp:Label>
                </td>
		    </tr>
		    </table>
		
<div style="margin-top:15px; font-weight:bold;">THÔNG TIN NGƯỜI GỬI</div><hr />
<table id="Table1" cellspacing="1" cellpadding="5" class="tblVDC">
		    <tr>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                </td>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label9" runat="server" Text="Tài khoản"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblSenderAccount" runat="server" ></asp:Label>
                </td>
		    </tr>
		    </table>
<div style="margin-top:15px;font-weight:bold;">THÔNG TIN NGƯỜI NHẬN</div><hr />
<table id="Table2" cellspacing="1" cellpadding="5" class="tblVDC">
		    <tr>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblRecieverName" runat="server"></asp:Label>
                </td>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label21" runat="server" Text="Tài khoản"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblRecieverAccount" runat="server" ></asp:Label>
                </td>
		    </tr>
		    <tr>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label42" runat="server" Text="<%$ Resources:labels, sochungminh %>"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblCMND" runat="server"></asp:Label>
                </td>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label44" runat="server" Text="Ngày cấp"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblCMNDDate" runat="server"></asp:Label>
                </td>
		    </tr>
		    <tr>
		        <td class="tdVDC">
                    <asp:Label ID="Label46" runat="server" Text="Nơi cấp"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblCMNDPlace" runat="server"></asp:Label>
                </td>
		        <td class="tdVDC">
                    &nbsp;</td>
		        <td>
                    &nbsp;</td>
		    </tr>
		    <tr>
		        <td class="tdVDC">
                    <asp:Label ID="Label23" runat="server" Text="Ngân hàng"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblBank" runat="server"></asp:Label>
                </td>
		        <td class="tdVDC">
                    <asp:Label ID="Label47" runat="server" Text="Tỉnh/Thành phố"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblCity" runat="server"></asp:Label>
                </td>
		    </tr>
		    </table>  
<div style="margin-top:15px;font-weight:bold;">CHI TIẾT CHUYỂN KHOẢN</div><hr />
<table id="Table3" cellspacing="1" cellpadding="5" class="tblVDC">
		    <tr>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label31" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                </td>
		        <td style="width:25%;"> 
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                &nbsp;</td>
		        <td class="tdVDC" style="width:25%;">
                    <asp:Label ID="Label35" runat="server" Text="Phí"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="lblFee" runat="server"></asp:Label>
                </td>
		    </tr>
		    <tr>
		        <td class="tdVDC">
                    <asp:Label ID="Label33" runat="server" Text="<%$ Resources:labels, diengiai %>"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblDesc" runat="server" ></asp:Label>
                </td>
		        <td class="tdVDC">
                    <asp:Label ID="Label48" runat="server" Text="<%$ Resources:labels, tiente %>"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                </td>
		    </tr>
		    </table>
  <div style="margin-top:15px;font-weight:bold;">THÔNG TIN ĐƯỜNG ĐI</div><hr />
<table id="Table4" cellspacing="1" cellpadding="5" class="tblVDC">
		    <tr>
		        <td style="width:25%;">
                    <asp:Label ID="Label49" runat="server" Text="Đường đi điện"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:DropDownList ID="ddlSwift" runat="server">
                        <asp:ListItem>CITAD</asp:ListItem>
                        <asp:ListItem Value="VCB">VCB Money</asp:ListItem>
                    </asp:DropDownList>
                </td>
		        <td style="width:25%;">
                    &nbsp;</td>
		        <td style="width:25%;">
                    &nbsp;</td>
		    </tr>
		    </table>
	<div style="margin-top:15px;font-weight:bold;">THÔNG TIN XÁC NHẬN</div><hr />
	<table id="Table5" cellspacing="1" cellpadding="5" class="tblVDC">
		    <tr>
		        <td style="width:25%;">
                    <asp:Label ID="Label40" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:DropDownList ID="ddlAuthenType" runat="server">
                    </asp:DropDownList>
                </td>
		        <td style="width:25%;">
                    <asp:Label ID="Label41" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
                </td>
		        <td style="width:25%;">
                    <asp:TextBox ID="txtAuthenCode" runat="server"></asp:TextBox>
                </td>
		    </tr>
		    </table>
		    
		<div style="margin-top:30px; text-align:center;">
	&nbsp;	
	<asp:Button ID="btApprove" runat="server" CssClass="btnGeneral"  Text="Tạo điện" 
            Width="80px" onclick="btApprove_Click" />	
	&nbsp;	
	<asp:Button ID="btnExit" runat="server" CssClass="btnGeneral"  Text="<%$ Resources:labels, back %>" onclick="btnExit_Click" 
                 />	
	</div>


	

 
<script type="text/javascript">

function SelectCbx(obj)
    {   
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked)
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='')
                {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }
            
        }else
        {
            for(i=0;i < count;i++)
            {
                if(elements[i].type == 'checkbox'&& elements[i].id!='')
                {
                    elements[i].checked = false;                     
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }
    
    function HighLightCBX(obj,obj1)
    {   
        //var obj2=document.getElementById(obj1);
        if(obj1.checked)
        {
            document.getElementById(obj).className="hightlight";
        }        
        else
        {
             document.getElementById(obj).className="nohightlight";
        }
    }
</script>