<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCKVTKTKKKH_Widget" %>


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
    </style>
<script src="widgets/IBCKVTKTKKKH/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBCKVTKTKKKH/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBCKVTKTKKKH/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBCKVTKTKKKH/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBCKVTKTKKKH/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBCKVTKTKKKH/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBCKVTKTKKKH/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

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
<span>Chuyển khoản vào tài khoản tiết kiệm không kỳ hạn</span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:Panel ID="pnTransfer" runat="server">
<div class="block1">    
<div class="handle">                    	
                    	Chi tiết giao dịch
                    </div>        	                         
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                        <tr>
                                <td colspan="2" class="tibtdh">Thông Tin Tài Khoản</td>
                                    
                               
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label46" runat="server" Text="Tài Khoản Chuyển Đi *"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDListAccPay" runat="server" Width="230">
                                    <asp:ListItem>00009102001-Tài khoản thanh toán</asp:ListItem>
                                    <asp:ListItem>00009102002-Tài khoản thanh toán</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="Số Dư"></asp:Label>
                                <td>
                                    <asp:Label ID="Label48" runat="server" Font-Bold="True" Text="3,000,000 <%$ Resources:labels, lak %>"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label2" runat="server" Text="Tài Khoản Tiết Kiệm KKH *"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDListAccTKKH" runat="server" Width="230" >
                                    <asp:ListItem>00009102003-Tài khoản TKKKH</asp:ListItem>
                                    <asp:ListItem>00009102005-Tài khoản TKKKH</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" Text="Loại Hình"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label49" runat="server" Font-Bold="False" Text="Tiết Kiệm" 
                                        Font-Underline="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" Text="Tiền Tệ"></asp:Label>
                                <td>
                                    <asp:Label ID="Label50" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label5" runat="server" Text="Số Dư "></asp:Label>
                                <td>
                                    <asp:Label ID="Label51" runat="server" Text="5,000,000 VNĐ" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông Tin Chuyển Khoản
                                </td>
                                    </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label52" runat="server" Text="Số Tiền *"></asp:Label>
                                </td>
                                 <td>
                                    <asp:TextBox ID="txtAmount" runat="server" onkeyup="ntt('ctl00_ctl12_txtAmount','ctl00_ctl12_lblmoney',event);" ></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="lblmoney" runat="server" Font-Size="7pt" Font-Bold="True" 
                                        Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label53" runat="server" Text="Mô Tả *"></asp:Label>
                                <td>
                                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="230"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                 <asp:Button ID="Button2" runat="server" 
                         Text="Quay Lại" />
                 &nbsp;
                    <asp:Button ID="BtConfirm" runat="server" OnClientClick="return validate();"  
                         Text="Tiếp Tục" onclick="BtConfirm_Click" />
                 &nbsp;
                 </div>
                  
    </div>
   </asp:Panel>
    <asp:Panel ID="pnConfirm" runat="server">
<div class="block1">  
                    <div class="handle">                    	
                    	Thông Tin Xác Nhận
                    </div>          	                         
                    <div class="content">
                        <table class="style1" cellpadding="5" cellspacing="0">
                        <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông Tin Tài Khoản
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Tài Khoản Chuyển Đi"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbAccNo" runat="server" Text="00009102001-Tài khoản thanh toán"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Số Dư"></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="3,000,000 VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label22" runat="server" Text="Người Chuyển"></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="lbDestName" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Tài Khoản Tiết Kiệm KKH"></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="lbDestNo" runat="server" Text="00009102005-Tài khoản TKKKH"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text="Loại Hình"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Text="Tiết Kiệm" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Text="Tiền Tệ"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Text="Số Dư "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" Text="5,000,000 VNĐ" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin chuyển khoản
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Text="Số Tiền "></asp:Label>
                                <td>
                                    <asp:Label ID="lbAfter" runat="server" Text="3,000,000 VNĐ" Font-Bold =true ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Text="Mô Tả "></asp:Label>
                                <td>
                                    <asp:Label ID="lbDesc" runat="server" Text="Nạp Tiền Tiết Kiệm"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    
                 <asp:Button ID="BtCancel" runat="server" 
                         Text="Quay Lại" onclick="BtCancel_Click" />
                 &nbsp;
                 <asp:Button ID="BtTransfer" runat="server" 
                         Text="Tiếp Tục" onclick="BtTransfer_Click" />
                 &nbsp;
                 </div>
                  
    </div>
<%--<script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      
      cal.manageFields("ctl00_ctl15_txtTS", "ctl00_ctl15_txtTS", "%d/%m/%Y");     
            
           
    //]]></script>--%>
</asp:Panel>
 <!--end-->
 <!--confirm-->
 <!--end--> 
 <!--token-->
 <asp:Panel ID="pnOTP" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Chi Tiết Xác Thực
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Xác thực giao dịch
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:RadioButton ID="radOTPBSMS" runat="server" Text="OTP qua SMS" 
                                        Checked="True" GroupName="OTP" onclick="enableSMSOTP();" />
                                </td>
                                <td >
                                    <asp:TextBox ID="txtOTPBSMS" runat="server" AutoCompleteType="None"></asp:TextBox>
&nbsp;<asp:Label ID="Label26" runat="server" Text="(Nhắn tin Siam Commercial Bank OTP gửi tới 8199)"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:RadioButton ID="radOTP" runat="server" Text="Nhập số OTP" 
                                        GroupName="OTP" onclick="enableOTP();" />
                                </td>
                                <td >
                                    <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    &nbsp;</td>
                                <td >
                                    <img alt="" src="widgets/IBCKVTKTKKKH/Images/otp.gif" style="width: 100px; height: 60px" /></td>                                
                            </tr>
                                                        
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button4" runat="server" 
                         Text="Quay lại" onclick="Button4_Click" />
                 &nbsp;
                    <asp:Button ID="Button5" runat="server" 
                         Text="Thực hiện" onclick="Button5_Click" />
                 </div>
                  
    </div>
 </asp:Panel>
 <!--end-->
<!--sao ke-->
<asp:Panel ID="pnResultTransaction" runat="server">
<div class="block1">  
                    <div class="handle">                    	
                    	Kết Quả Giao Dịch
                    </div>          	                         
                    <div class="content">
                        <table class="style1" cellpadding="5" cellspacing="0">
                        <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông Tin Tài Khoản
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Tài Khoản Chuyển Đi"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text="00009102001-Tài khoản thanh toán"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Text="Số Dư"></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Text="3,000,000 VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label23" runat="server" Text="Người Chuyển"></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="Label24" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label25" runat="server" Text="Tài Khoản Tiết Kiệm KKH"></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="Label27" runat="server" Text="00009102005-Tài khoản TKKKH"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label28" runat="server" Text="Loại Hình"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Font-Bold="False" Text="Tiết Kiệm" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label30" runat="server" Text="Tiền Tệ"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label31" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label32" runat="server" Text="Số Dư "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label33" runat="server" Text="5,000,000 VNĐ" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin chuyển khoản
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label34" runat="server" Text="Số Tiền "></asp:Label>
                                <td>
                                    <asp:Label ID="Label35" runat="server" Text="3,000,000 VNĐ" Font-Bold =true ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label36" runat="server" Text="Mô Tả "></asp:Label>
                                <td>
                                    <asp:Label ID="Label37" runat="server" Text="Nạp Tiền Tiết Kiệm"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>                
               
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button1" runat="server" 
                         Text="In kết quả" onclick="Button1_Click" />
                 &nbsp;
                    <asp:Button ID="Button6" runat="server" 
                         Text="Làm mới" onclick="Button6_Click" />
                 </div>
                  
    </div>
</asp:Panel>
<!--end-->
<script type="text/javascript">
    function resetTS()
    {
        document.getElementById("ctl00_ctl15_txtTS").value="";
        document.getElementById("ctl00_ctl15_txtTS").disabled=true;
    }
    function enableTS()
    {        
        document.getElementById("ctl00_ctl15_txtTS").disabled=false;
    }
    function enableOTP()
    {
        document.getElementById("<%=txtOTP.ClientID %>").disabled=false;
        document.getElementById("<%=txtOTPBSMS.ClientID %>").value="";
        document.getElementById("<%=txtOTPBSMS.ClientID %>").disabled=true;
    }
    function enableSMSOTP()
    {
        document.getElementById("<%=txtOTP.ClientID %>").disabled=true;
        document.getElementById("<%=txtOTPBSMS.ClientID %>").disabled=false;
        document.getElementById("<%=txtOTP.ClientID %>").value="";
    }
    function replaceAll( str, from, to ) {
        var idx = str.indexOf( from );


        while ( idx > -1 ) {
            str = str.replace( from, to ); 
            idx = str.indexOf( from );
        }

        return str;
    }

    function ntt(sNumber,idDisplay,event)
    {  
        
        executeComma(sNumber,event);  
        
        if(document.getElementById(sNumber).value=="")
        {       
            document.getElementById(idDisplay).innerHTML="";
            return;
        }  
              
        document.getElementById(idDisplay).innerHTML="("+number2text(replaceAll(document.getElementById(sNumber).value,",",""))+")";
             
    }
    function validate()
    {
        if(validateEmpty('<%=DDListAccTKKH.ClientID %>','Tài khoản đích không rỗng.'))
        {
            if(validateMoney('<%=txtAmount.ClientID %>','Bạn cần nhập số tiền'))
            {
                if(validateEmpty('<%=txtDesc.ClientID %>','Bạn cần nhập mô tả'))
                {
                }
                else
                {
                    document.getElementById('<%=txtDesc.ClientID %>').focus();
                    return false;
                }
            }
            else
            {
                document.getElementById('<%=txtAmount.ClientID %>').focus();
                return false;
            }
        } 
        else
        {
            document.getElementById('<%=DDListAccTKKH.ClientID %>').focus();
            return false;
        }     
        
    }
    function number2text(sNumber)
    {    
     var sResult=new String();
     var sTemp=new String(sNumber);
     var arrNumberText=new Array("không","một","hai","ba","bốn","năm","sáu","bẩy","tám","chín");

     for(var i=0;i<sTemp.length;i++)
     {
     var nNum=parseInt(sTemp.charAt(i));
     var sLevel=new String("");
     switch(sTemp.length-i)
     {
     case 16:sLevel="vạn";break;
     case 15:sLevel="trăm";break;
     case 14:sLevel="mươi";break;
     case 13:sLevel="nghìn";break;
     case 12:sLevel="trăm";break;
     case 11:sLevel="mươi";break;
     case 10:sLevel="tỉ";break;
     case 9:sLevel="trăm";break;
     case 8:sLevel="mươi";break;
     case 7:sLevel="triệu";break;
     case 6:sLevel="trăm";break;
     case 5:sLevel="mươi";break;
     case 4:sLevel="nghìn";break;
     case 3:sLevel="trăm";break;
     case 2:sLevel="mươi";break;
     case 1:sLevel="đồng";break;
     }
     sResult+=arrNumberText[nNum]+" "+sLevel+" ";
     }
     //
     //return sResult.charAt(0).toUpperCase() + sResult.slice(1);
     sResult=regReplace(sResult,"không trăm tỉ","lẻ");
     sResult=regReplace(sResult,"không trăm triệu","lẻ");
     sResult=regReplace(sResult,"không trăm nghìn","lẻ");
     sResult=regReplace(sResult,"không trăm đồng","đồng");
     sResult=regReplace(sResult,"không mươi không ","");
     sResult=regReplace(sResult,"mươi không","mươi");
     sResult=regReplace(sResult,"lẻ không trăm ","");
     sResult=regReplace(sResult,"mươi một","mươi mốt");
     sResult=regReplace(sResult,"một mươi","mười");
     sResult=regReplace(sResult,"mười không","mười");
     sResult=regReplace(sResult,"không mươi","lẻ");
     sResult=regReplace(sResult,"mốt tỉ","một tỉ");
     sResult=regReplace(sResult,"mốt nghìn","một nghìn");
     sResult=regReplace(sResult,"mốt đồng","một đồng");
     sResult=regReplace(sResult,"triệu nghìn","triệu lẻ");
     sResult=regReplace(sResult,"tỉ triệu lẻ","tỉ lẻ");
     sResult=regReplace(sResult,"không triệu","triệu");
     sResult=regReplace(sResult,"mươi một","mươi mốt");
     sResult=regReplace(sResult,"mươi năm","mươi lăm");
     sResult=regReplace(sResult,"mười mốt","mười một");
     //
     sResult=regReplace(sResult,"không trăm tỉ","lẻ");
     sResult=regReplace(sResult,"không trăm triệu","lẻ");
     sResult=regReplace(sResult,"không trăm nghìn","lẻ");
     sResult=regReplace(sResult,"không trăm đồng","đồng");
     sResult=regReplace(sResult,"không mươi không ","");

     sResult=regReplace(sResult,"lẻ lẻ","lẻ");
     sResult=regReplace(sResult,"lẻ đồng","đồng");
     return sResult.charAt(0).toUpperCase() + sResult.slice(1);
    }
    function regReplace(sInput,sReg,sNew)
    {
     var re = new RegExp(sReg, "g");
     return sInput.replace(re, sNew);
    } 
</script>