<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTransactionsApprove_ViewDetailCloseFD_Widget" %>
<%@ Register src="../../../Controls/LetterSearch/LetterSearch.ascx" tagname="LetterSearch" tagprefix="uc1" %>


<script src="widgets/SEMSContractList/JS/ajax.js" type="text/javascript"></script>

<script src="widgets/SEMSContractList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>

<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
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
    	margin-bottom:10px;
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
    <%=Resources.labels.thongtingiaodich %>

</div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div id="divError" style="text-align:center; color:Red;">
<asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<asp:Panel ID="pnChiTiet" runat="server">
<div>
<B><%=Resources.labels.chitietgiaodich %></B> <hr />
</div>
		<table class="tblVDC" cellspacing="1" cellpadding="5">
		   
		    <tr>
                <td class="tdVDC" style="width:20%;">
                    <asp:Label ID="Label1" runat="server" 
                        Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
                    &nbsp;</td>
                <td style="width:20%;">
                    <asp:Label ID="lblTransID" runat="server"></asp:Label>
                    &nbsp;</td>
                <td class="tdVDC" style="width:20%;">
                    <asp:Label ID="Label2" runat="server" 
                        Text="<%$ Resources:labels, ngaygiogiaodich %>"></asp:Label>
                    &nbsp;</td>
                <td style="width:20%;">
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                    &nbsp;</td>
            </tr>
             <tr>
		        <td colspan="4">
                    <asp:Label ID="Label63" runat="server" 
                        Text="<%$ Resources:labels, thongtintaikhoantietkiem %>" Font-Bold="True"></asp:Label><hr />
                </td>
		    </tr>
		    <tr>
		        <td class="tdVDC" style="width:20%;">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, taikhoantietkiemcokyhan %>"></asp:Label>
                &nbsp;</td>
		        <td style="width:20%;">
                    <asp:Label ID="lblSenderAccount" runat="server" Font-Bold="True"></asp:Label>
                &nbsp;</td>
		        <td class="tdVDC" style="width:20%;">
                    <asp:Label ID="Label52" runat="server" 
                        Text="<%$ Resources:labels, currentbalance %>"></asp:Label>
                </td>
		        <td style="width:20%;">
                    <asp:Label ID="lblCB_FD" runat="server" Text="1.000.000"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYIDB" runat="server"></asp:Label>
                </td>
		    </tr>
		    <tr>
		        <td class="tdVDC">
                                    <asp:Label ID="Label48" runat="server" 
                                        Text="<%$ Resources:labels, dateopened %>"></asp:Label>
                                </td>
		        <td>
                                    <asp:Label ID="lblDO_FD" runat="server" Text="11/01/2009"></asp:Label>
                                </td>
		        <td class="tdVDC">
                    <asp:Label ID="Label67" runat="server" 
                        Text="<%$ Resources:labels, interestrate %>"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblIR_FD" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblpercentyear" runat="server" 
                        Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                                </td>
		    </tr>
		    <tr>
		        <td class="tdVDC">
                                    <asp:Label ID="Label59" runat="server" 
                                        Text="<%$ Resources:labels, ExpireDate %>"></asp:Label>
                                </td>
		        <td>
                                    <asp:Label ID="lblLT_FD" runat="server" Text="11/01/2009"></asp:Label>
                                </td>
		        <td class="tdVDC">
                                    <asp:Label ID="Label71" runat="server" Text="<%$ Resources:labels, tentaikhoan %>"></asp:Label>
                </td>
		        <td>
                                <asp:Label ID="lblACCTNAME" runat="server"></asp:Label>
                </td>
		    </tr>
		    <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label72" runat="server" Text="<%$ Resources:labels, chinhanh %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBranchFD" runat="server"></asp:Label>
                    &nbsp;</td>
                <td class="tdVDC">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
		    <tr>
		        <td colspan="4">
                                    <asp:Label ID="Label64" runat="server" 
                                        Text="<%$ Resources:labels, thongtintaikhoanthanhtoan %>" Font-Bold="True"></asp:Label><hr />
                </td>
		    </tr>
		    <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label9" runat="server" 
                        Text="<%$ Resources:labels, taikhoanthanhtoan %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblReceiverAccount" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label70" runat="server" Text="<%$ Resources:labels, branch %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBranch_DD" runat="server"></asp:Label>
                </td>
            </tr>
		    <tr>
                <td colspan="4">
                    <asp:Label ID="Label65" runat="server" 
                        Text="<%$ Resources:labels, thongtingiaodich %>" Font-Bold="True"></asp:Label><hr />
                </td>
            </tr>
		    <tr>
		        <td class="tdVDC">
                    <asp:Label ID="Label66" runat="server" Text="<%$ Resources:labels, sotien %>"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                    &nbsp;<asp:Label ID="lblCCYID" runat="server"></asp:Label>
                </td>
		        <td class="tdVDC">
                                    
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                </td>
		        <td>
                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                </td>
		    </tr>
		    <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label68" runat="server" 
                        Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblUserCreate" runat="server"></asp:Label>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label54" runat="server" 
                        Text="<%$ Resources:labels, nguoiduyet %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblAppSts" runat="server"></asp:Label>
                    &nbsp;</td>
            </tr>
		    <tr>
                                <td class="tdVDC">
                                    <asp:Label ID="Label69" runat="server" 
                                        Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </td>
                                <td class="tdVDC">
                                    
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, ketqua %>" Visible="False"></asp:Label>
                &nbsp;</td>
                                <td>
                                   
                    <asp:Label ID="lblResult" runat="server" Visible="False"></asp:Label>
                &nbsp;</td>
                            </tr>              
		    </table>
		
	 
	     
       
 
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
		
	 <br />
<div class="al">
&nbsp;<B><%=Resources.labels.chitietduyetgiaodich %></B><hr /></div>
 <div style="text-align:center; margin-top:10px;">
                            <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
&nbsp;
                    &nbsp;
                    </div>
<br />
</asp:Panel>
<asp:Panel ID="pnToken" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.thongtinduyetgiaodich %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="5" cellpadding="5" width="100%">
                             <tr>
                                <td align="right" width="40%">
                                    <asp:Label ID="Label57" runat="server" Text="<%$ Resources:labels, laitamtinh %>"></asp:Label>
                                    :
                                 </td>
                                <td width="60%">
                                    <asp:Label ID="lblLaiTamTinh" runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;<asp:Label ID="lblCCYIDTT" runat="server"></asp:Label>
                                </td>
                             </tr>
                             <tr>
                                 <td align="right" width="40%">
                                     <asp:Label ID="Label60" runat="server" 
                                         Text="<%$ Resources:labels, accruedcreditinterest %>"></asp:Label>
                                 </td>
                                 <td width="60%">
                                     <asp:Label ID="lblACRI_FD" runat="server" Font-Bold="True"></asp:Label>
                                     &nbsp;<asp:Label ID="lblCCYIDACI" runat="server"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right" >
                                     <asp:Label ID="radNewRate" runat="server" 
                                         Text="<%$ Resources:labels, laiseduochuong %>" />
                                     :
                                 </td>
                                 <td>
                                     <asp:TextBox ID="txtNewRate" runat="server" Width="69px"></asp:TextBox>
                                     &nbsp;
                                     <asp:Button ID="btnTinhLai" runat="server" OnClientClick="return tinhlai();" 
                                         Text="<%$ Resources:labels, capnhatlaiduochuong %>" 
                                         onclick="btnTinhLai_Click" SkinID="btnGeneral" />
                                 </td>
                             </tr>
                                   
                             <tr>
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                     <asp:Label ID="lblText" runat="server" Font-Italic="True" Font-Size="7pt" 
                                         ForeColor="#0066FF"></asp:Label>
                                 </td>
                             </tr>
                                   
                             <tr>
                                 <td align="right">
                                     <asp:Label ID="Label56" runat="server" Text="<%$ Resources:labels, diengiai %>"></asp:Label>
                                     :
                                 </td>
                                 <td>
                                     <asp:TextBox ID="txtDesc" runat="server" SkinID="txtTwoColumn" 
                                         TextMode="MultiLine" Width="379px" Height="66px"></asp:TextBox>
                                 </td>
                             </tr>
                                   
                        </table>
                    </div>                
                 <!--Button next-->
                 
                  
    </div>
 </asp:Panel>
 
   
       
		<div style="margin-top:30px; text-align:center;">
		     <asp:Button ID="btnPrint" runat="server"  Text="<%$ Resources:labels, inthongtin %> " 
                   onclientclick="javascript:return poponload()" 
                 onclick="btnPrint_Click"  />&nbsp;
	        <asp:Button ID="btnPrevious" runat="server" onclick="btnPrevious_Click" 
                SkinID="skn200" Text="<%$ Resources:labels, giaodichtruoc %>" />
&nbsp;
	<asp:Button ID="btApprove" runat="server" CssClass="btnGeneral"  Text="<%$ Resources:labels, duyet %>" 
            Width="80px" onclick="btApprove_Click" />	
	        &nbsp;	
	<asp:Button ID="btReject" runat="server" CssClass="btnGeneral"  Text="<%$ Resources:labels, khongduyet %>" 
            Width="80px" onclick="btReject_Click1" OnClientClick="return reject();" />	
&nbsp;
	<asp:Button ID="btnExit" runat="server" CssClass="btnGeneral"  Text="<%$ Resources:labels, back %>" 
                onclick="btnExit_Click" />	
	        &nbsp; 
	        <asp:Button ID="btnNext" runat="server" SkinID="skn200" 
                Text="<%$ Resources:labels, giaodichketiep %>" onclick="btnNext_Click" />
            &nbsp;	
	</div>
	
	</ContentTemplate>
</asp:UpdatePanel>	   
	
	<script type="text/javascript">
	     function poponload()
    {
    testwindow= window.open ("widgets/SEMSTransactionsApprove/print.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=800,height=600");
    testwindow.moveTo(0,0);
    return false;
    }
function reject()
{
    if(document.getElementById('<%=txtDesc.ClientID %>').value=='')
    {
        window.alert('<%=Resources.labels.banvuilongnhaplydokhongduyetgiaodichnay %>');
        document.getElementById('<%=txtDesc.ClientID %>').focus();
        return false;
    }
}

function tinhlai() {
    if (document.getElementById('<%=txtNewRate.ClientID %>').value == '') {
        window.alert('<%=Resources.labels.banvuilongnhapsotienlai %>');
        document.getElementById('<%=txtNewRate.ClientID %>').focus();
        return false;
    }
    else {return true;
    } 
}
    
    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);


        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }

        return str;
    }
    function ntt(sNumber, idDisplay, event) {
        
        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
     }

</script>


	

 
