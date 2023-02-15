<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBSyncOTPToken_Widget" %>


<style type="text/css">
    .style1
    {
        width: 100%; 
        background-color:#EAEDD8;       
    }
    .tibtd
    {
    	width: 30%; 
    }
        .tibtd1
    {
    	width: 40%; 
    }
            .tibtd2
    {
    	width: 20%; 
    }
    .tibtdh
    {
    	width: 100%; 
    	background-color:#EAFAFF;color:#003366;
    	font-weight:bold;
    }
    </style>
<script src="widgets/IBSyncOTPToken/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBSyncOTPToken/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBSyncOTPToken/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBSyncOTPToken/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBSyncOTPToken/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBSyncOTPToken/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBSyncOTPToken/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<!--IBCKVTKTKKKH-->
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
<span>Đồng bộ OTP Token</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
 <!--end-->
 <!--confirm-->
 <!--end--> 
 <!--token-->
 <asp:Panel ID="pnOTP" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Chi Tiết đồng bộ
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5" border=0>
                            <tr>
                                <td colspan="3" class="tibtdh">
                                    Thông tin đồng bộ
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Lable" runat="server" Text="Token ID * "  />
                                </td>
                                <td class="tibtd1">
                                    <asp:TextBox ID="txtTokenID" runat="server"></asp:TextBox>&nbsp;
                                </td>  
                                <td rowspan=2 class="tibtd2">
                                    <img alt="" src="widgets/IBSyncOTPToken/Images/otp.gif" style="width: 100px; height: 60px" />
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="Mật khẩu OTP 1 * "  />
                                </td>
                                <td >
                                    <asp:TextBox ID="txtpass1" runat="server"></asp:TextBox>&nbsp;
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label2" runat="server" Text="Mật khẩu OTP 2 *"  />
                                </td>
                                <td >
                                    <asp:TextBox ID="txtpass2" runat="server"></asp:TextBox>&nbsp;
                                </td>                                
                            </tr>
                                                        
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                     <asp:Button ID="Button5" runat="server" OnClientClick="return validate();" 
                         Text="Thực hiện" />
                    <asp:Button ID="Button4" runat="server" 
                         Text="Quay lại" PostBackUrl="javascript:history.go(-1)" />
                 &nbsp;
                    </div>
                  
    </div>
 </asp:Panel>
 <!--end-->
<!--sao ke-->

<!--end-->
<script type="text/javascript">
    function validate()
    {
        if(validateEmpty('<%=txtTokenID.ClientID %>','Nhập TokenID ở mặt sau của Token.'))
        {
            if(validateMoney('<%=txtpass1.ClientID %>','Bạn cần nhập mã token lần 1'))
            {
                if(validateEmpty('<%=txtpass2.ClientID %>','Bạn cần nhập mã token lần 2'))
                {
                }
                else
                {
                    document.getElementById('<%=txtpass2.ClientID %>').focus();
                    return false;
                }
            }
            else
            {
                document.getElementById('<%=txtpass1.ClientID %>').focus();
                return false;
            }
        } 
        else
        {
            document.getElementById('<%=txtTokenID.ClientID %>').focus();
            return false;
        }     
        
    }
  
</script>