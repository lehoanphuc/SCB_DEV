<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTTTKCKH_Widget" %>

<style type="text/css">
    .style11
    {
        width: 100%; 
        background-color:#EAEDD8;       
    }
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
    .style12
    {
        background-color: #009CD4;
        font-weight: bold;
        height: 23px;
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

<div style="text-align:center; color:Red;">
<asp:Label runat="server" ID="lblTextError"></asp:Label></div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<!--Transfer In Bank-->
<asp:Panel ID="pnTIB" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.tattoantaikhoantietkiemcokyhan %>
                    </div>                    
                    <div class="content">
                        <asp:UpdatePanel runat="server"><ContentTemplate>
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                   <%=Resources.labels.thongtintaikhoan %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label58" runat="server" 
                                        Text="<%$ Resources:labels, taikhoantietkiemcokyhan %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlFDAccount" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlFDAccount_SelectedIndexChanged">
                                        
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label59" runat="server" 
                                        Text="<%$ Resources:labels, sodu %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblBalanceTK" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblCCYIDTK" runat="server" Text=""></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, ddaccountnostar %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlDDAccount" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlDDAccount_SelectedIndexChanged">
                                       
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label32" runat="server" 
                                        Text="<%$ Resources:labels, sodu %>"></asp:Label>                                    
                                </td>
                                <td >
                                    <asp:Label ID="lblBalanceDD" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblCCYIDDD" runat="server" Text=""></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, diengiai %>"></asp:Label> *
                                </td>
                                <td >
                                    <asp:TextBox ID="txtDesc" runat="server" Height="50px" Width="300px" 
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>                                
                            </tr>
                            </table>
                         
                         </ContentTemplate></asp:UpdatePanel>
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
                       <%=Resources.labels.tattoantaikhoantietkiemcokyhan %>
                        
                    </div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.thongtintaikhoan %>
                                </td>
                            </tr> 
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, taikhoantietkiemonline %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCFFDAccount" Font-Bold="True" runat="server"></asp:Label>
                                </td>                                
                            </tr>                           
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label69" runat="server" Text="<%$ Resources:labels, branch %>"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblCFBranch" runat="server" Text=""></asp:Label>
                                     <asp:Label ID="lblBranchID" runat="server" Visible="False"></asp:Label>
                                 </td>
                            </tr>
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label67" runat="server" Text="<%$ Resources:labels, loaitaikhoan %>"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblCFAccountName" runat="server"></asp:Label>
                                 </td>
                            </tr>
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label61" runat="server" Text="<%$ Resources:labels, sodutaikhoantietkiem %>"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblCFBalanceFD" runat="server"></asp:Label>
                                 </td>
                             </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label63" runat="server" Text="<%$ Resources:labels, ngaymo %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCFOpenDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label40" runat="server" Text="<%$ Resources:labels, ngaydenhan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCFMD" runat="server"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label42" runat="server" Text="<%$ Resources:labels, currency %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCFCCYID" runat="server"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label64" runat="server" Text="<%$ Resources:labels, interestrate %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCFInterestRate" runat="server"></asp:Label>
                                    <asp:Label ID="Label66" runat="server" Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, accruedcreditinterest %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCFACI" runat="server"></asp:Label>
                                </td>                                
                            </tr> 
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, taikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCFDDAcount" Font-Bold="True" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label65" runat="server" Text="<%$ Resources:labels, sodutaikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCFBalanceDD" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCFDesc" runat="server"></asp:Label>
                                </td>                                
                            </tr>                               
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                     <asp:Button ID="Button6" runat="server" onclick="Button2_Click" 
                         Text="<%$ Resources:labels, xacnhan %>" />
                 &nbsp;
                     <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                         Text="<%$ Resources:labels, quaylai %>" />
                     &nbsp;
                 </div>
                  
    </div>
</asp:Panel>
 <!--end--> 
 <!--token-->
 <asp:Panel ID="pnOTP" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.tattoantaikhoantietkiemcokyhan %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.xacthucgiaodich %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlLoaiXacThuc" runat="server">
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label20" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
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
                     <asp:Button ID="Button7" runat="server" onclick="Button5_Click" 
                         Text="<%$ Resources:labels, thuchien %>" />
                 &nbsp;
                     <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                         Text="<%$ Resources:labels, quaylai %>" />
                     &nbsp;
                 </div>
                  
    </div>
 </asp:Panel>
 <!--end-->
<!--sao ke-->
<asp:Panel ID="pnResultTransaction" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">  
                        <%=Resources.labels.tattoantaikhoantietkiemcokyhan %>
                        
                    </div>                    
                    <div class="content">
                         <table class="style11" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="style12">
                                    <%=Resources.labels.thongtintaikhoan %>
                                </td>
                            </tr> 
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, taikhoantietkiemonline %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblRFDAccount" Font-Bold="True" runat="server"></asp:Label>
                                </td>                                
                            </tr>                           
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label70" runat="server" Text="<%$ Resources:labels, branch %>"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblRBranch" runat="server"></asp:Label>
                                 </td>
                             </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label68" runat="server" 
                                        Text="<%$ Resources:labels, loaitaikhoan %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRSAccountName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label7" runat="server" 
                                        Text="<%$ Resources:labels, sodutaikhoantietkiem %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblRBalanceFD" runat="server"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label9" runat="server" 
                                        Text="<%$ Resources:labels, ngaymo %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblROpenDate" runat="server"></asp:Label>
                                </td>                                
                            </tr>                           
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label11" runat="server" 
                                        Text="<%$ Resources:labels, ngaydenhan %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRMD" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label13" runat="server" 
                                        Text="<%$ Resources:labels, currency %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblRCCYID" runat="server"></asp:Label>
                                </td>                                
                            </tr> 
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label15" runat="server" 
                                        Text="<%$ Resources:labels, interestrate %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblRInterestRate" runat="server"></asp:Label>
                                    <asp:Label ID="Label18" runat="server" 
                                        Text="<%$ Resources:labels, percentyear %>"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label21" runat="server" 
                                        Text="<%$ Resources:labels, accruedcreditinterest %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRACI" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label24" runat="server" 
                                        Text="<%$ Resources:labels, taikhoanthanhtoan %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblRDDAcount" runat="server" Font-Bold="True"></asp:Label>
                                </td>                                
                            </tr>                               
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label26" runat="server" 
                                         Text="<%$ Resources:labels, sodutaikhoanthanhtoan %>"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblRBalanceDD" runat="server"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td class="tibtd">
                                     <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblRD" runat="server"></asp:Label>
                                 </td>
                             </tr>
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button1" runat="server" 
                         Text="<%$ Resources:labels, close %>" onclick="Button3_Click" />
                 
                  
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
      
            if(validateEmpty('<%=txtDesc.ClientID %>','<%=Resources.labels.bancannhapmota %>'))
                {
                }
                else
                {
                    document.getElementById('<%=txtDesc.ClientID %>').focus();
                    return false;
                }            
         
        
    }
   
</script>