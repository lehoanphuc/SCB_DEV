<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBDrawDown_Widget" %>
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
</style>
<script src="widgets/IBTKCKHNewAccount/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTKCKHNewAccount/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBTKCKHNewAccount/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBTKCKHNewAccount/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTKCKHNewAccount/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/steel/steel.css" rel="stylesheet" type="text/css" />


<!--Transfer In Bank-->
<asp:Panel ID="pnTIB" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Giải ngân vào tài khoản thanh toán
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin tài khoản
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label58" runat="server" 
                                        Text="Tài khoản vay *"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDDAccount0" runat="server">
                                        <asp:ListItem>00009102001 - Tài khoản vay</asp:ListItem>
                                        <asp:ListItem>00009102002 - Tài khoản vay</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label15" runat="server" 
                                        Text="Dư nợ hiện tại"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label16" runat="server" 
                                        Text="2.000.000 LAK" Font-Bold="true"></asp:Label>   
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, ddaccountnostar %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDDAccount" runat="server">
                                        <asp:ListItem>00009102001 - Tài khoản thanh toán</asp:ListItem>
                                        <asp:ListItem>00009102002 - Tài khoản TK</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label17" runat="server" 
                                        Text="Số dư"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label18" runat="server" 
                                        Text="2.000.000 LAK" Font-Bold="true"></asp:Label>   
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label63" runat="server" Text="Số tiền *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtAmount" onkeyup="ntt('ctl00_ctl15_txtAmount','ctl00_ctl15_lblText',event);" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" 
                                        Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label5" runat="server" Text="Diễn giải *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtDesc" runat="server" Height="50px" Width="300px"></asp:TextBox>
                                </td>                                
                            </tr>
                            </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btnTIBNext" runat="server" OnClientClick="return validate();" 
                         Text="<%$ Resources:labels, next %>" onclick="btnTIBNext_Click"  />
                 </div>
                  
    </div>
    

</asp:Panel>
 <!--end-->
 <!--confirm-->
 <asp:Panel ID="pnConfirm" runat="server">

 <div class="block1">            	 
            	            
                    <div class="handle">  
                        Giải ngân vào tài khoản thanh toán                  	
                        
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin tài khoản
                                </td>
                            </tr> 
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label19" runat="server" Text="Tài khoản vay"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label20" Font-Bold="true" runat="server" Text="TK009012"></asp:Label>
                                </td>                                
                            </tr>                           
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label61" runat="server" Text="Tên tài khoản"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="Label62" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                 </td>
                             </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label54" runat="server" Text="Tài khoản thanh toán"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label55" Font-Bold="true" runat="server" Text="00009102"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label42" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label43" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label22" runat="server" Text="Số tiền giải ngân"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label23" runat="server" Text="2.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>                               
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label64" runat="server" Text="Diễn giải"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label65" runat="server" Text="Giải ngân vào TKTT"></asp:Label>
                                </td>                                
                            </tr>                               
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button3" runat="server" 
                         Text="Quay lại" onclick="Button3_Click" />
                 &nbsp;
                    <asp:Button ID="Button2" runat="server" 
                         Text="Xác nhận" onclick="Button2_Click" />
                 </div>
                  
    </div>
</asp:Panel>
 <!--end--> 
 <!--token-->
 <asp:Panel ID="pnOTP" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Giải ngân vào tài khoản thanh toán
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.authencationtransaction %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:RadioButton ID="radOTPBSMS" runat="server" Text="<%$ Resources:labels, otpbysms %>" 
                                        Checked="True" GroupName="OTP" onclick="enableSMSOTP();" />
                                </td>
                                <td >
                                    <asp:TextBox ID="txtOTPBSMS" runat="server" AutoCompleteType="None"></asp:TextBox>
&nbsp;<asp:Label ID="Label26" runat="server" Text="(Nhắn tin Siam Commercial Bank OTP gửi tới 8199)"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:RadioButton ID="radOTP" runat="server" Text="<%$ Resources:labels, otp %>" 
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
                                    <img alt="" src="widgets/IBTransferInBank1/Images/otp.gif" style="width: 100px; height: 60px" /></td>                                
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
                        Giải ngân vào tài khoản thanh toán
                        
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin tài khoản
                                </td>
                            </tr> 
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="Tài khoản vay"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="TK009012"></asp:Label>
                                </td>                                
                            </tr>                           
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label3" runat="server" Text="Tên tài khoản"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="Label6" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                 </td>
                             </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label7" runat="server" Text="Tài khoản thanh toán"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label8" Font-Bold="true" runat="server" Text="00009102"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label11" runat="server" Text="Số tiền giải ngân"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label12" runat="server" Text="2.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>                               
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label13" runat="server" Text="Diễn giải"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label14" runat="server" Text="Giải ngân vào TKTT"></asp:Label>
                                </td>                                
                            </tr>                            
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button1" runat="server" 
                         Text="In kết quả" onclick="Button3_Click" />
                 
                  
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
        document.getElementById("ctl00_ctl15_txtOTP").disabled=false;
        document.getElementById("ctl00_ctl15_txtOTPBSMS").value="";
        document.getElementById("ctl00_ctl15_txtOTPBSMS").disabled=true;
    }
    function enableSMSOTP()
    {
        document.getElementById("ctl00_ctl15_txtOTP").disabled=true;
        document.getElementById("ctl00_ctl15_txtOTPBSMS").disabled=false;
        document.getElementById("ctl00_ctl15_txtOTP").value="";
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
      
            if(validateEmpty('<%=txtDesc.ClientID %>','Bạn cần nhập mô tả'))
                {
                    if(validateMoney('<%=txtAmount.ClientID %>','Bạn cần nhập số tiền hợp lệ'))
                    {
                    }
                    else
                {
                    return false;
                }  
                }
                else
                {
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