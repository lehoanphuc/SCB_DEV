<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTKTH_Widget" %>
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
                     <asp:Button ID="Button4" runat="server" onclick="Button4_Click2" 
                         Text="Quay lại" />
                 <asp:Button ID="Button1" runat="server" OnClientClick="return validate();" 
                         Text="<%$ Resources:labels, next %>" onclick="Button1_Click"  />&nbsp;
                     
                    
                        <div class="clearfix"></div>
                 </div>
                  
    </div>
  
</asp:Panel>
 <!--end-->
 
 <!--confirm-->
  
 <asp:Panel ID="pnConfirm" runat="server">

 <div class="block1">            	 
            	            
                    <div class="handle">  
                        Đăng ký tài khoản nhận chuyển khoản</div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin tài khoản nhận chuyển khoản
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label86" runat="server" Text="Tên người chuyển khoản"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label87" runat="server" Text="Số tài khoản"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblAcctNo" runat="server" Text="00009102001"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label23" runat="server" 
                                        Text="Ghi chú"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button3" runat="server" 
                         Text="Quay lại" onclick="Button3_Click" />     
                  <asp:Button ID="Button2" runat="server" 
                         Text="Hoàn thành" onclick="Button2_Click" /> &nbsp;
                        <div class="clearfix"></div>           
                   
                 </div>
                  
    </div>
</asp:Panel>
 <!--end--> 
 <asp:Panel ID="pnReTranstion" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Kết quả giao dịch
                    </div>                    
                    <div class="content">
                      
                        <div style=" padding-top:10px; padding-bottom:10px; text-align:center;">
                      
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                            Text="Đăng ký tài khoản được phép chuyển tới thành công"></asp:Label>
                        </div>
                        
                      
                    </div>                
                 <!--Button next-->
                         <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btnNew" runat="server" 
                         Text="Tạo mới" onclick="btnNew_Click" />
                    &nbsp;
                    <asp:Button ID="btnExit" runat="server" 
                         Text="Thoát" onclick="btnExit_Click"/>
                         </div>
</div>
 </asp:Panel>
 <!--end--> 
 
<script type="text/javascript">
    function validate()
    {
      
           
                    if(validateEmpty('<%=txtNTHName.ClientID %>','Bạn cần nhập tên người chuyển khoản'))
                    {
                        if(validateEmpty('<%=txtNTHAccount.ClientID %>','Bạn cần nhập số tài khoản'))
                        {
                            
                        }
                        else
                        {
                            document.getElementById('<%=txtNTHAccount.ClientID %>').focus();
                            return false;
                        } 
                    }
                    else
                    {
                        document.getElementById('<%=txtNTHName.ClientID %>').focus();
                        return false;
                    } 
     }
</script>