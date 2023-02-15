<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBLNRegister_Widget" %>
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
<script src="widgets/IBLNRegister/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBLNRegister/JS/lang/en.js" type="text/javascript"></script>
<script src="widgets/IBLNRegister/JS/mask.js" type="text/javascript"></script>
<script src="widgets/IBLNRegister/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBLNRegister/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBLNRegister/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBLNRegister/CSS/steel/steel.css" rel="stylesheet" type="text/css" />


<!--Transfer In Bank-->
<asp:Panel ID="pnTIB" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	Đăng ký thông tin vay vốn
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin đăng ký vay vốn
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label77" runat="server" Text="Mã khách hàng *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtCustCode" runat="server"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label78" runat="server" Text="Tên khách hàng *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtCustName" runat="server" Width="237px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label79" runat="server" Text="Hợp đồng vay *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtContractNo" runat="server" Width="237px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label58" runat="server" 
                                        Text="Thành phần kinh tế *"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDDAccount0" runat="server">
                                        <asp:ListItem>Kinh tế cá thể</asp:ListItem>
                                        <asp:ListItem>Hợp tác xã</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" Text="Loại hình *"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDDAccount" runat="server">
                                        <asp:ListItem>Vay tín dụng</asp:ListItem>
                                        <asp:ListItem>Vay vốn làm ăn</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label63" runat="server" Text="Kỳ hạn vay *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtTerm" runat="server" Width="90px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label66" runat="server" Text="Lãi suất *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtRate" runat="server" Width="90px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label80" runat="server" Text="Hạn mức vay *"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtLimit" onkeyup="ntt('ctl00_ctl15_txtLimit','ctl00_ctl15_lblText',event);" runat="server"></asp:TextBox>
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
                        Đăng ký thông tin vay vốn                 	
                        
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin đăng ký vay vốn
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label19" runat="server" Text="Mã khách hàng "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label81" runat="server" Font-Bold="True" Text="04000910340"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label20" runat="server" Text="Tên khách hàng "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label82" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label22" runat="server" Text="Hợp đồng vay "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label83" runat="server" Text="09HK/JK4555"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label23" runat="server" 
                                        Text="Thành phần kinh tế "></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label84" runat="server" Text="Kinh tế cá thể"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label30" runat="server" Text="Loại hình "></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label85" runat="server" Text="Vay tín dụng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label31" runat="server" Text="Kỳ hạn vay "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label86" runat="server" Text="3 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label32" runat="server" Text="Lãi suất "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label87" runat="server" Text="3%"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label33" runat="server" Text="Hạn mức vay "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label88" runat="server" Text="20.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label34" runat="server" Text="Diễn giải "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label89" runat="server" Text="Vay vốn làm ăn"></asp:Label>
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
                    	Đăng ký thông tin vay vốn
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
                        Đăng ký thông tin vay vốn                 	
                        
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin đăng ký vay vốn
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="Mã khách hàng "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="04000910340"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" Text="Tên khách hàng "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label6" runat="server" Text="Trần Anh Tuấn"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label7" runat="server" Text="Hợp đồng vay "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label8" runat="server" Text="09HK/JK4555"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label9" runat="server" 
                                        Text="Thành phần kinh tế "></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label10" runat="server" Text="Kinh tế cá thể"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label11" runat="server" Text="Loại hình "></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label12" runat="server" Text="Vay tín dụng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label13" runat="server" Text="Kỳ hạn vay "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label14" runat="server" Text="3 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label15" runat="server" Text="Lãi suất "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label16" runat="server" Text="3%"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label17" runat="server" Text="Hạn mức vay "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label18" runat="server" Text="20.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label21" runat="server" Text="Diễn giải "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label24" runat="server" Text="Vay vốn làm ăn"></asp:Label>
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
      
           if(validateEmpty('<%=txtCustCode.ClientID %>','Bạn cần nhập mã khách hàng'))
                {
                    if(validateMoney('<%=txtCustName.ClientID %>','Bạn cần nhập tên khách hàng'))
                    {
                        if(validateMoney('<%=txtContractNo.ClientID %>','Bạn cần nhập số hợp đồng'))
                        {
                            if(validateMoney('<%=txtTerm.ClientID %>','Bạn cần nhập kỳ hạn vay'))
                            {
                                if(validateMoney('<%=txtRate.ClientID %>','Bạn cần nhập lãi suất'))
                                {
                                    if(validateMoney('<%=txtLimit.ClientID %>','Bạn cần nhập hạn mức vay'))
                                    {
                                        if(validateMoney('<%=txtDesc.ClientID %>','Bạn cần nhập mô tả'))
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
                                        document.getElementById('<%=txtLimit.ClientID %>').focus();
                                        return false;
                                    } 
                                }
                                else
                                {
                                    document.getElementById('<%=txtRate.ClientID %>').focus();
                                    return false;
                                } 
                            }
                            else
                            {
                                document.getElementById('<%=txtTerm.ClientID %>').focus();
                                return false;
                            } 
                        }
                        else
                        {
                            document.getElementById('<%=txtContractNo.ClientID %>').focus();
                            return false;
                        } 
                    }
                    else
                    {
                        document.getElementById('<%=txtCustName.ClientID %>').focus();
                        return false;
                    }  
                }
                else
                {
                    document.getElementById('<%=txtCustCode.ClientID %>').focus();                        
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