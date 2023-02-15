<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTKTH_Details_Widget" %>
<style type="text/css">
    .style11
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
    .style12
    {
        width: 370px;
    }
    .al
    {
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
    }
</style>
<script src="widgets/IBTKCKHNewAccount/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTKCKHNewAccount/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBTKCKHNewAccount/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBTKCKHNewAccount/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTKCKHNewAccount/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/steel/steel.css" rel="stylesheet" type="text/css" />


<!--Transfer In Bank-->
 
 <!--end-->
 <!--Nguoi thu huong cung ngan hang-->
<div class="al">
<span>Chi tiết thông tin tài khoản được phép chuyển đến</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:Panel ID="pnNTHCNH" runat="server">
<div class="block1">            	 
                    <div class="handle">                    	
      	                Tài khoản chuyển đến
                  </div>
                                     
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin tài khoản chuyển đến</td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label2" runat="server" Text="Tên người chuyển khoản *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtNTHName" runat="server" Width="237px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" Text="Số tài khoản *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtNTHAccount" runat="server" Width="237px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" 
                                        Text="Ghi chú"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:TextBox ID="txtDesc"                                         
                                        runat="server" Width="237px"></asp:TextBox>
                                    &nbsp;</td>                                
                            </tr>
                            </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
<%--                 <asp:Button ID="Button1" runat="server" OnClientClick="return validate();" 
                         Text="<%$ Resources:labels, next %>" onclick="Button1_Click"  />&nbsp;--%>
                     <asp:Button ID="Button4" runat="server"
                         Text="Quay lại" onclick="Button4_Click" />
                    
                 </div>
                  
    </div>
  
</asp:Panel><!--end-->