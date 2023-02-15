<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBInfoLC_Widget" %>
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
    .quyen3
    {
    	width:35%;
    	border-left:solid 1px #b9bfc1;
    	border-bottom:solid 1px #b9bfc1;
    }
    .quyen1
    {
    	
    	border-left:solid 1px #b9bfc1;
    	border-right:solid 1px #b9bfc1;
    	border-bottom:solid 1px #b9bfc1;
    }
    .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
    }
    .quyen
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	width:35%;
    	border-left:solid 1px #b9bfc1;
    	border-top:solid 1px #b9bfc1;
    }
        .quyen2
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	width:35%;
    	border-left:solid 1px #b9bfc1;
    	border-right:solid 1px #b9bfc1;
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
    .searchtitle
    {

    	font-weight:bold;
    }
        .searchcontent
    {
    	width:25%;
    	border-top:solid 1px #b9bfc1;
    	border-right: solid 1px #b9bfc1;
    }
    .btsearch
    {
    	width:25%;
    	border-top:solid 1px #b9bfc1;
    }
    .style3
    {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }
</style>
<link href="widgets/IBInfoLC/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/IBInfoLC/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBInfoLC/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBInfoLC/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoLC/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBInfoLC/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div>
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
<span>Thông tin L/C</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<!--thong tin tai khoan DD-->
<asp:Panel ID="pnDD" runat="server">
    <div class="block1">            	        	           
                    <div class="handle">                    	
                    	Thông tin L/C
                    </div>  
                                 
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label6" runat="server" Text="Số tham chiếu"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblAccountNumber" runat="server" Text="040-2-7100-0003"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAccountName" runat="server" Text="Bình thường"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label109" runat="server" 
                                        Text="Loại hình"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblCurrency0" runat="server" Text="Out Wward"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label9" runat="server" 
                                        Text="Tiền tệ"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblCurrency" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label12" runat="server" 
                                        Text="Nhóm"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblDO" runat="server" Text="Đã phát hành"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label16" runat="server" 
                                        Text="Số hợp đồng"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblLTD" runat="server" Text="HĐ123"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label10" runat="server" Text="Người mở"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblCB" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, nguoithuhuong %>"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblAB" runat="server" Text="Nasi Group"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label11" runat="server" Text="Hạn giao hàng cuối"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblODPDL" runat="server" Text="15/04/2010"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label15" runat="server" 
                                        Text="Ngày giao dịch cuối"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblIR" runat="server" Text="15/03/2010"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label18" runat="server" Text="Số lần thế chấp"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblHA" runat="server" Text="2"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                    <asp:Label ID="Label20" runat="server" 
                                        Text="Giá trị thế chấp"></asp:Label>
                                </td>
                                <td class="thtd">
                                    <asp:Label ID="lblACR" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label17" runat="server" Text="Số tiền vay quỹ"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:Label ID="lblODPI" runat="server" Text="200 VNĐ"></asp:Label>
                                </td>
                                <td class="thtdbold">
                                   <asp:Label ID="Label1" runat="server" Text="Số tiền hối đoái đã nhận"></asp:Label>
                                    </td>
                                <td class="thtd">
                                    <asp:Label ID="Label2" runat="server" Text="3,000,000 VNĐ"></asp:Label>
                                    </td>
                            </tr>
                            
                        </table>
                        </div> 
                        <br />
                       
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="quyen">
                                    <asp:Label ID="Label3" runat="server" Text="Tổng tiền"></asp:Label>
                                </td>
                                <td class="quyen">
                                    <asp:Label ID="Label5" runat="server" Text="Tổng tiền thanh toán"></asp:Label>
                                </td>
                                <td class="quyen2">
                                    <asp:Label ID="Label7" runat="server" Text="Tổng tiền phải trả"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td class="quyen3">
                                    <asp:Label ID="Label13" runat="server" Text="2,000,000 VNĐ"></asp:Label>
                                </td>
                                <td class="quyen3">
                                    <asp:Label ID="Label19" runat="server" Text="1,700,000 VNĐ"></asp:Label>
                                </td>
                                 <td class="quyen1">
                                    <asp:Label ID="Label8" runat="server" Text="300,000 VNĐ"></asp:Label>
                                </td>
                            </tr>  
                            </table>
                            <br />
                            <table class="style1" cellspacing="0" cellpadding="5">               
                            <tr>
                                <td class="quyen">
                                    <asp:Label ID="Label21" runat="server" Text="Tổng tiền ký quỹ"></asp:Label>
                                </td>
                                <td class="quyen">
                                    <asp:Label ID="Label22" runat="server" Text="Số tiền ký quỹ đã thanh toán"></asp:Label>
                                </td>
                                <td class="quyen2">
                                    <asp:Label ID="Label23" runat="server" Text="Số tiền ký quỹ phải trả"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="quyen3">
                                    <asp:Label ID="Label24" runat="server" Text="200,000 VNĐ"></asp:Label>
                                </td>
                                <td class="quyen3">
                                    <asp:Label ID="Label25" runat="server" Text="0 VNĐ"></asp:Label>
                                </td>
                                 <td class="quyen1">
                                    <asp:Label ID="Label26" runat="server" Text="200,000 VNĐ"></asp:Label>
                                </td>
                            </tr> 
                            </table>
                            <br />
                            <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td class="quyen">
                                    <asp:Label ID="Label27" runat="server" Text="Tổng tiền bảo lãnh"></asp:Label>
                                </td>
                                <td class="quyen">
                                    <asp:Label ID="Label28" runat="server" Text="Số tiền bảo lãnh đã thanh toán"></asp:Label>
                                </td>
                                <td class="quyen2">
                                    <asp:Label ID="Label29" runat="server" Text="Số tiền bảo lãnh phải trả"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="quyen3">
                                    <asp:Label ID="Label30" runat="server" Text="1,800,000 VNĐ"></asp:Label>
                                </td>
                                <td class="quyen3">
                                    <asp:Label ID="Label31" runat="server" Text="1,700,000 VNĐ"></asp:Label>
                                </td>
                                 <td class="quyen1">
                                    <asp:Label ID="Label32" runat="server" Text="100,000 VNĐ"></asp:Label>
                                </td>
                            </tr>                  
                        </table>
                        <br />
                        <div class="handle">                    	
                    	Hối Phiếu L/C
                         </div>
                        <div class="content">
                    <asp:GridView ID="GridViewRelateBill" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="5" Width="100%">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:HyperLinkField DataTextField="Sothamchieu" HeaderText="Số tham chiếu" 
                                Text="040-2-7100-0003" NavigateUrl="~/Default.aspx?po=3&p=131">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="Sohoiphieu" HeaderText="Số hối phiếu">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ngaytao" HeaderText="Ngày tạo">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trangthai" HeaderText="<%$ Resources:labels, trangthai %>">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#009CD4" Font-Bold="True"  />
                    </asp:GridView>
                    
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

