<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBAddRepaid_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
    .th
{
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
}
</style>
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager runat="server"></asp:ScriptManager>

<!--Transfer In Bank-->
<div class="th">
<span><%=Resources.labels.napthetratruocquocte %></span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<div style="text-align:center; color:Red;">
<asp:Label runat="server" ID="lblTextError"></asp:Label></div>
<asp:Panel ID="pnTIB" runat="server">
<div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<asp:Label ID="Label28" runat="server" Text='<%$ Resources:labels, chitietgiaodich %>'></asp:Label>
                    </div>                    
                    <div class="content">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                        
                        
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, thongtinnguoinaptien %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>&nbsp;*
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlSenderAccount_SelectedIndexChanged">
                                    </asp:DropDownList>
<%--                                &nbsp;<asp:RadioButton ID="radTS1" runat="server" Checked="True" GroupName="TIB" 
                                        onclick="resetTS();" Text="Chuyển ngay" Visible="False" />--%>
                                    <asp:HiddenField ID="txtChu" runat="server" />
                                </td>                                
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, thongtinthe %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, sothe %>'></asp:Label>&nbsp;*
                                </td>
                                <td style="margin-left: 40px" >
    
                                   <asp:TextBox ID="txtCardNo" runat="server"></asp:TextBox>
                                      
                                    
                                </td>                               
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, tenchuthe %>'></asp:Label>&nbsp;*
                                </td>
                                <td style="margin-left: 40px" >
    
                                   <asp:TextBox ID="txtCardHolder" runat="server"></asp:TextBox>
                                      
                                    
                                </td>                               
                            </tr>
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                                </td>
                                <td >
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" ></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" 
                                        Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>
                                            </td>                                        
                                        </tr>
                                    </table>
                                   
                                </td>                                
                            </tr>
<%--                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>&nbsp;*
                                </td>
                                <td >
                                    <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" 
                                        Width="300px"></asp:TextBox>
                                    &nbsp;<div style="width:220px; vertical-align:text-top;float:right;"><asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" 
                            ForeColor="#666666" Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                            </div></td>                                
                            </tr>--%>
                        </table>
                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="btnTIBNext" runat="server" OnClientClick="return validate();" 
                         Text='<%$ Resources:labels, tieptuc %>' onclick="btnTIBNext_Click"  />
                 </div>
                  
    </div>
    
<%--<script type="text/javascript">//<![CDATA[

      var cal = Calendar.setup({
          onSelect: function(cal) { cal.hide() }
      });
      
      cal.manageFields("<%=txtTS.ClientID %>", "<%=txtTS.ClientID %>", "%d/%m/%Y");     
            
           
    //]]></script>--%>
</asp:Panel>
 <!--end-->
 <!--confirm-->
 <asp:Panel ID="pnConfirm" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.chitietgiaodich %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, thongtinnguoinaptien %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                                    <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            
                            <tr>
                                <td colspan="2" class="tibtdh">
                                     <asp:Label ID="Label22" runat="server" 
                            Text='<%$ Resources:labels, thongtinthe %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, sothe %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCardNo" runat="server"></asp:Label>

                                </td>                               
                            </tr>
                                                        <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, tenchuthe %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCardHolder" runat="server"></asp:Label>

                                </td>                               
                            </tr>
                            
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label23" runat="server" 
                            Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                             <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </td>
                        </tr>
                            
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
                     <asp:Button ID="btnApply" runat="server" onclick="btnApply_Click" 
                         Text='<%$ Resources:labels, xacnhan %>' />
                 &nbsp;
                     <asp:Button ID="btnBackTransfer" runat="server" onclick="btnBackTransfer_Click" 
                         Text='<%$ Resources:labels, quaylai %>' />
&nbsp;
                 </div>
                  
    </div>
</asp:Panel>
 <!--end--> 
 <!--token-->
 <asp:Panel ID="pnOTP" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.chitietgiaodich %>
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
                                    <asp:Label ID="Label58" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                                </td>
                                <td >
                                    <asp:DropDownList ID="ddlLoaiXacThuc" runat="server">
                                    </asp:DropDownList>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label59" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
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
                     <asp:Button ID="btnAction" runat="server" onclick="btnAction_Click" 
                         Text="<%$ Resources:labels, thuchien %>" />
                 &nbsp;
                     <asp:Button ID="btnBack" runat="server" onclick="btnBack_Click" 
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
                    	<%=Resources.labels.ketquagiaodich %>
                    </div>                    
                    <div class="content">
                        <table class="style1" cellspacing="0" cellpadding="5">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, thongtinnguoinaptien %>'></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, hotennguoinaptien %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                             <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label50" runat="server" Text='<%$ Resources:labels, sodusaukhighino %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblBalanceCCYID" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                           
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, thongtinthe %>'></asp:Label></td>

                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, sothe %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCardNoEnd" runat="server"></asp:Label>
                                </td>                               
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, tenchuthe %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblCardHolderEnd" runat="server"></asp:Label>
                                </td>                               
                            </tr>
                           
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label34" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                                </td>                                
                            </tr>
                        </table>
                    </div>                
                 <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">
<%--                    <asp:Button ID="btnView" runat="server"
                        onclientclick="javascript:return poponloadview()" 
                     Text="<%$ Resources:labels, viewphieuin %>" />
                     &nbsp;
                     <asp:Button ID="btnPrint" runat="server" 
                         onclientclick="javascript:return poponload()" 
                         Text="<%$ Resources:labels, inketqua %>" />--%>
&nbsp;
                    <asp:Button ID="btnNew" runat="server" 
                         Text='<%$ Resources:labels, lammoi %>' onclick="btnNew_Click" />
                 </div>
                  
    </div>
</asp:Panel>
<asp:HiddenField ID="lblReceiverName" runat="server"></asp:HiddenField>
<asp:HiddenField ID="lblReceiverBranch" runat="server"></asp:HiddenField>
<asp:HiddenField ID="lblPhiAmount" runat="server"></asp:HiddenField>
<!--end-->
<script type="text/javascript">
    function poponload()
    {
    testwindow= window.open ("widgets/IBTransferInBank1/print.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=500,height=650");
    testwindow.moveTo(0,0);
    return false;
    }
    function poponloadview()
    {
    testwindow= window.open ("widgets/IBTransferInBank1/viewprint.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=500,height=650");
    testwindow.moveTo(0,0);
    return false;
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
        document.getElementById('<%=txtChu.ClientID %>').value=number2text(replaceAll(document.getElementById(sNumber).value,",",""),'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>');     
    }
    function validate()
    {
        if (validateEmpty('<%=txtCardNo.ClientID %>', '<%=Resources.labels.sothekhongrong %>')) {

            if(validateMoney('<%=txtAmount.ClientID %>','<%=Resources.labels.bancannhapsotien %>'))
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
            document.getElementById('<%=txtCardNo.ClientID %>').focus();
            return false;
        }     
        
    }
    
</script>
