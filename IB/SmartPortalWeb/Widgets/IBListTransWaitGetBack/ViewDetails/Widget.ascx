<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBListTransWaitGetBack_ViewDetails_Widget" %>
<style type="text/css">
    .style1
    {
        width: 100%; 
        background-color:#EAEDD8;       
    }
    .tibtd
    {
    	
    }
    .tibtdh
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    }
        .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:10px;
	    padding-bottom:10px;
    }
            .thtdf
    {
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    	border-top:solid 1px #b9bfc1;
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
        .style3
    {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }
</style>
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div class="al">
<span>Giao dịch dự thu</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Chi tiết giao dịch dự thu
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                                                         <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label8" runat="server" Text="Mã giao dịch "></asp:Label>
                                &nbsp;</td>
                                <td class="style3">
                                    <asp:Label ID="Label17" runat="server" Text="A3231"></asp:Label>
                                &nbsp;</td>   
                                <td class="thtdbold">
                                    <asp:Label ID="Label18" runat="server" Text="Loại giao dịch"></asp:Label>
                                &nbsp;</td>
                                <td class="thtd">
                                    <asp:Label ID="Label19" runat="server" Text="Chuyển khoản liên ngân hàng"></asp:Label>
                                &nbsp;</td>                                 
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label11" runat="server" Text="Ngày "></asp:Label>
                                &nbsp;</td>
                                <td class="style3" >
                                    <asp:Label ID="Label20" runat="server" Text="03/06/2010 09:30 AM"></asp:Label>
                                &nbsp;</td>  
                                 <td class="thtdbold">
                                    <asp:Label ID="Label21" runat="server" Text="Ghi có"></asp:Label>
                                &nbsp;</td>
                                <td class="thtd">
                                    <asp:Label ID="Label22" runat="server" Text="3.000.000"></asp:Label>
                                    &nbsp;<asp:Label ID="Label1" runat="server" Text="VNĐ"></asp:Label>
                                &nbsp;</td>                                 
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label40" runat="server" Text="Ghi nợ "></asp:Label>
                                &nbsp;</td>
                                <td class="style3" >
                                    <asp:Label ID="Label41" runat="server" Text="3.000.000"></asp:Label>
                                    &nbsp;<asp:Label ID="Label24" runat="server" Text="VNĐ"></asp:Label>
                                &nbsp;</td>  
                               <td class="thtdbold">
                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, mota%>"></asp:Label>
                                &nbsp;</td>
                                <td class="thtd">
                                    <asp:Label ID="Label23" runat="server" Text="Chuyển khoản"></asp:Label>
                                    
                                &nbsp;</td>                                
                            </tr>
                            <tr>
                                <td class="thtdf">
                                    <asp:Label ID="Label48" runat="server" Text="Trạng thái "></asp:Label>
                                &nbsp;</td>
                                <td class="style3">
                                    <asp:Label ID="Label49" runat="server" Text="Chờ duyệt"></asp:Label>
                                &nbsp;</td>
                                <td class="thtdbold">
                                    
                                    &nbsp;</td>
                                <td class="thtd">
                                   
                                    &nbsp;</td>
                            </tr> 
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button3" runat="server" 
                         Text="Quay lại" PostBackUrl="javascript:history.go(-1)" />
                 </div>
                  
    </div>

<!--end-->
<script type="text/javascript">
</script>
