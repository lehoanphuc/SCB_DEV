<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTKTHSearch_Delete_Widget" %>
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
    	padding:0px 0px 0px 0px;
    	
    	
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
    .style2
    {
        height: 30px;
    }
               .al
    {
	    font-weight:bold;
	    padding-left:5px;
	    padding-top:10px;
	    padding-bottom:10px;
    }
</style>
<script type="text/javascript" src="widgets/SEMSUser/js/common.js"> </script> 
<script type="text/javascript" src="widgets/SEMSUser/js/subModal.js"> </script> 
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script> 

<link href="widgets/SEMSUser/css/style.css" rel="stylesheet" type="text/css"> 
<!-- Add this to have a specific theme--> 
<link href="widgets/SEMSUser/css/subModal.css" rel="stylesheet" type="text/css"> 


<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<div class="al">
<span>Xoá tài khoản chuyển đến </span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<br />

<%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
<ProgressTemplate>
<img alt="" src="Images/WidgetImage/ajaxloader.gif" style="width: 16px; height: 16px;" /> <%=Resources.labels.loading %>
</ProgressTemplate>
</asp:UpdateProgress>--%>
<asp:Panel runat="server" ID="pnresult">
<div class="block1">
<div class="divGetInfoCust">
    <div class="handle">
      Kết quả giao dịch
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td>
                <div id="divError">
                <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                </div>
            </td>
           
        </tr>
        </table>
</div>
</div>
    
</asp:Panel>
<!-- Thong tin khach hang-->
<!--end-->
<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">
<div class="block1">
<div class="divGetInfoCust">
    <div class="handle">
      Thông tin xác nhận
    </div>
    <table class="style1" cellspacing="0" cellpadding="5">
        <tr>
            <td align="center" class="style2">
                <asp:Label ID="lblConfirm" runat="server" 
                    Text="Bạn chắc chắn muốn xóa tài khoản không?"></asp:Label>
            </td>
           
        </tr>
        </table>
</div>
</div>
    
</asp:Panel>
<div style="text-align:center; padding-top:10px;">
    <asp:Button ID="btsaveandcont" runat="server" onclick="btsaveandcont_Click" 
        Text="Xóa" Width="71px" />
&nbsp;
    <asp:Button ID="btback" runat="server" 
        Text="Quay lại" onclick="btback_Click" />
    &nbsp; &nbsp;
 </div>
    
<!--end-->
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">
   
</script>
