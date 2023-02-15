<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTATUTKCKH_Widget" %>
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
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>
<script src="widgets/IBTKCKHNewAccount/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTKCKHNewAccount/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTKCKHNewAccount/CSS/steel/steel.css" rel="stylesheet" type="text/css" />


<!--Transfer In Bank-->
<asp:Panel ID="pnTIB" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.taituctaikhoantietkiemcokyhan %>
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaikhoantietkiemcokyhan %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label58" runat="server" 
                                        Text="<%$ Resources:labels, taikhoantietkiemcokyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDDAccount0" runat="server">
                                        
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label59" runat="server" 
                                        Text="<%$ Resources:labels, sodu %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label60" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, kyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label63" runat="server" Text="2 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, ngaydenhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label64" runat="server" Text="01/02/2010"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label65" runat="server" Text="<%$ Resources:labels, laisuat %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label66" runat="server" Text="2% / năm"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label67" runat="server" Text="<%$ Resources:labels, lainhanduoc %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label68" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaikhoankhongkyhan %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label32" runat="server" 
                                        Text="<%$ Resources:labels, taikhoankhongkyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label69" runat="server" Text="<%$ Resources:labels, sodu %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label70" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaituc %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label33" runat="server" 
                                        Text="<%$ Resources:labels, sotien %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:TextBox ID="txtAmount" onkeyup="ntt('ctl00_ctl15_txtAmount','ctl00_ctl15_lblText',event);" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" 
                                        Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label34" runat="server" Text="<%$ Resources:labels, loaihinh %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem>Sử dụng tiết kiệm</asp:ListItem>
                                        <asp:ListItem>Tiết kiệm mua xe</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label72" runat="server" Text="<%$ Resources:labels, kyhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label74" runat="server" Text="3 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label73" runat="server" Text="<%$ Resources:labels, laisuat %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label75" runat="server" Text="1% / năm"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label76" runat="server" Text="<%$ Resources:labels, loaitien %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label77" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label78" runat="server" Text="<%$ Resources:labels, mota %>" Width="300px"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" 
                                        Width="300px"></asp:TextBox>
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
                        <%=Resources.labels.taituctaikhoantietkiemcokyhan %>
                        
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                  <%=Resources.labels.thongtintaikhoantietkiemcokyhan %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label2" runat="server" 
                                        Text="<%$ Resources:labels, taikhoantietkiemcokyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label79" runat="server" Text="TK00009102" Font-Bold="True"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" 
                                        Text="<%$ Resources:labels, sodu %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label17" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, kyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label20" runat="server" Text="2 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, ngaydenhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label22" runat="server" Text="01/02/2010"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:labels, laisuat %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label35" runat="server" Text="2% / năm"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, laiduocnhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label37" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaikhoankhongkyhan %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label38" runat="server" 
                                        Text="<%$ Resources:labels, taikhoankhongkyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label80" runat="server" Text="00009102" Font-Bold="True"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label39" runat="server" Text="<%$ Resources:labels, sodu %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label40" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaituc %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label41" runat="server" 
                                        Text="<%$ Resources:labels, sotien %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label81" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label42" runat="server" Text="<%$ Resources:labels, loaihinh %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label82" runat="server" Text="Tiết kiệm mua xe"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label43" runat="server" Text="<%$ Resources:labels, kyhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label44" runat="server" Text="3 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, laisuat %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label46" runat="server" Text="1% / năm"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label47" runat="server" Text="<%$ Resources:labels, loaitien %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label48" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label49" runat="server" Text="<%$ Resources:labels, mota %>" Width="300px"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label83" runat="server" Text="Tái tục tài khoản dùng mua xe"></asp:Label>
                                </td>                                
                            </tr>                               
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button3" runat="server" 
                         Text="<%$ Resources:labels, quaylai %>" onclick="Button3_Click" />
                 &nbsp;
                    <asp:Button ID="Button2" runat="server" 
                         Text="<%$ Resources:labels, xacnhan %>" onclick="Button2_Click" />
                 </div>
                  
    </div>
</asp:Panel>
 <!--end--> 
 <!--token-->
 <asp:Panel ID="pnOTP" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.taituctaikhoantietkiemcokyhan %>
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
                                    <asp:TextBox ID="txtOTPBSMS" runat="server"></asp:TextBox>
&nbsp;<asp:Label ID="Label26" runat="server" Text=""></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:RadioButton ID="radOTP" runat="server" Text="<%$ Resources:labels, otp %>" 
                                        GroupName="OTP" onclick="enableOTP();" />
                                </td>
                                <td >
                                    <asp:TextBox ID="txtOTP" runat="server"></asp:TextBox>
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
                         Text="<%$ Resources:labels, quaylai %>" onclick="Button4_Click" />
                 &nbsp;
                    <asp:Button ID="Button5" runat="server" 
                         Text="<%$ Resources:labels, thuchien %>" onclick="Button5_Click" />
                 </div>
                  
    </div>
 </asp:Panel>
 <!--end-->
<!--sao ke-->
<asp:Panel ID="pnResultTransaction" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">  
                        <%=Resources.labels.taituctaikhoantietkiemcokyhan %>
                        
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaikhoantietkiemcokyhan %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" 
                                        Text="<%$ Resources:labels, taikhoantietkiemcokyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label6" runat="server" Text="TK00009102" Font-Bold="True"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label7" runat="server" 
                                        Text="<%$ Resources:labels, sodu %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label8" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, kyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label10" runat="server" Text="2 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, ngaydenhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label12" runat="server" Text="01/02/2010"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, laisuat %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label14" runat="server" Text="2% / năm"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, laiduocnhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label16" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                   <%=Resources.labels.thongtintaikhoankhongkyhan %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label18" runat="server" 
                                        Text="<%$ Resources:labels, taikhoankhongkyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label24" runat="server" Text="00009102" Font-Bold="True"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label25" runat="server" Text="<%$ Resources:labels, sodu %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label27" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                   <%=Resources.labels.thongtintaituc %></td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label28" runat="server" 
                                        Text="<%$ Resources:labels, sotien %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="Label29" runat="server" Text="1.000.000 LAK"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label30" runat="server" Text="<%$ Resources:labels, loaihinh %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label31" runat="server" Text="Tiết kiệm mua xe"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label50" runat="server" Text="<%$ Resources:labels, kyhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label51" runat="server" Text="3 tháng"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label52" runat="server" Text="<%$ Resources:labels, laisuat %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label53" runat="server" Text="1% / năm"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label54" runat="server" Text="<%$ Resources:labels, loaitien %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label55" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label56" runat="server" Text="<%$ Resources:labels, mota %>" Width="300px"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label57" runat="server" Text="Tái tục tài khoản dùng mua xe"></asp:Label>
                                </td>                                
                            </tr>                                
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button1" runat="server" 
                         Text="<%$ Resources:labels, inketqua %>" onclick="Button3_Click" />
                 
                  
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
              
        document.getElementById(idDisplay).innerHTML="("+number2text(replaceAll(document.getElementById(sNumber).value,",",""),'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>')+")";
             
    }
    function validate()
    {
      
                if(validateEmpty('<%=txtDesc.ClientID %>',' <%=Resources.labels.bancannhapmota %>'))
                {
                    if(validateMoney('<%=txtAmount.ClientID %>',' <%=Resources.labels.bancannhapsotienhople %>'))
                    {
                    }
                    else
                    {
                        document.getElementById('<%=txtAmount.ClientID %>').focus();
                        return false;
                    } 
                }
                else
                {
                    document.getElementById('<%=txtDesc.ClientID %>').focus();
                    return false;
                }            
         
        
    }
   
</script>