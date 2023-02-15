<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBInfoBill_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%;        
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
    .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
    }
    .thtdff
    {  	
    	
    	border-top:solid 1px #b9bfc1;
    }
    .thtr
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    }
    .thtds
    {
    	padding:10px 5px 10px 5px;
    }
    .style3
    {
        background-color: #009CD4;
        font-weight: bold;
        border-top: solid 1px #b9bfc1;
        width: 348px;
    }
    .style4
    {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
        width: 270px;
    }
</style>
<link href="widgets/IBInfoBill/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBInfoBill/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBInfoBill/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBInfoBill/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoBill/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoBill/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<style>
.al
{
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
}
</style>
<div class="al">
<span>Thông tin hối phiếu</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<div>
<asp:Panel ID="pnBill" runat="server">
    <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Thông tin hối phiếu
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label6" runat="server" Text="Mã tham chiếu"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountNumber" runat="server" Text="040-2-4100-10-00020"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label4" runat="server" Text="Số hối phiếu"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountName" runat="server" Text="123156468"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label109" runat="server" 
                                        Text="Trạng thái"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCurrency0" runat="server" Text="Chấp nhận thanh toán"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label9" runat="server" 
                                        Text="Ngày tạo"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCurrency" runat="server" Text="07/04/2010"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label12" runat="server" 
                                        Text="Trạng thái bộ chứng từ"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblDO" runat="server" Text="Hợp lệ"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label16" runat="server" 
                                        Text="Tiền tệ"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblLTD" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label10" runat="server" Text="Số ngày trả chậm"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCB" runat="server" Text="0 VNĐ"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label14" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAB" runat="server" Text="07/04/2010"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label11" runat="server" Text="Số tiền"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblODPDL" runat="server" Text="1,300,000 VNĐ"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label15" runat="server" 
                                        Text="Số tiền đã trả"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblIR" runat="server" Text="0.00 VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label18" runat="server" Text="Số tiền phải trả"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblHA" runat="server" Text="0.00 VNĐ"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label20" runat="server" 
                                        Text="Phí đã trả"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblACR" runat="server" Text="0.00 VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label17" runat="server" Text="Phí phải trả"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblODPI" runat="server" Text="0.00 VNĐ"></asp:Label>
                                </td>
                                 <td class="thtdbold">
                                    <asp:Label ID="Label1" runat="server" 
                                        Text="Ngày gửi"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label2" runat="server" Text="07/04/2010"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label3" runat="server" Text="Ngày chấp nhận thanh toán"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label5" runat="server" Text="07/04/2010"></asp:Label>
                                </td>
                                 <td class="thtdbold">
                                    <asp:Label ID="Label7" runat="server" 
                                        Text="Mã tham chiếu đối tác"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label8" runat="server" Text="123156468"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label13" runat="server" Text="Tỉ lệ chiết khấu"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label19" runat="server" Text="0"></asp:Label>
                                </td>
                                 <td class="thtdbold">
                                    <asp:Label ID="Label21" runat="server" 
                                        Text=""></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>                
                 
    </div>
        <br />
                  <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button1" runat="server" 
                         Text="Quay lại" />
                 </div>
</asp:Panel>

<!--end-->
</div>

