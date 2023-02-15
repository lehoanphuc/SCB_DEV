<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBFeedBack_Details_Widget" %>
<style type="text/css">
    .style11
    {
        width: 100%;
        background-color: #EAEDD8;
    }
    .tibtd
    {
    }
    .tibtdh
    {
        background-color: #009CD4;
        font-weight: bold;
    }
    .al
    {
	font-weight:bold;
	padding-left:5px;
	padding-top:10px;
	padding-bottom:10px;
    }
    .style12
    {
        width: 39%;
        height: 29px;
    }
    .style13
    {
        width: 71%;
        height: 29px;
    }
    #areaContent
    {
        width: 518px;
        height: 128px;
    }
    #areacomment
    {
        width: 518px;
        height: 128px;
    }
    .style14
    {
        background-color: #009CD4;
        font-weight: bold;
        height: 16px;
    }
</style>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script> 
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/mask.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div class="al">
<span><%=Resources.labels.trasoatgiaodich %></span><br />
<img style="margin-top:5px;" src="Images/WidgetImage/underline.png" />
</div>
<div style="text-align:center; color:Red;">
<asp:Label runat="server" ID="lblTextError"></asp:Label></div>
<!--thong tin tai khoan DD-->
<div>
<asp:Panel ID="pnConfirm" runat="server">
 <div class="block1">            	 
            	            
                    <div class="handle">                    	
                    	<%=Resources.labels.chitiettrasoatgiaodich%></div>                    
                    <div class="content">
                        <table class="style11" cellspacing="0" cellpadding="5">
                        <tr>
                                <td colspan="2" class="style14">
                                    <%=Resources.labels.thongtintrasoatgiaodich%>
                                </td>
                            </tr>
                            <asp:Panel ID="pnipctransid" runat="server">
                           <tr>
                                <td class="tibtd" style="Width:39%;">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, sogiaodich %>"></asp:Label>
                                </td>
                                <td style="Width:71%;">
                                    <asp:TextBox ID="txtsgd" runat="server" Text=""></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, tieude %>"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:TextBox ID="txttitle" SkinID="txt150" runat="server" Text=""></asp:TextBox>
                                </td>                                
                            </tr>
                            </asp:Panel>
                            <asp:Panel ID="pntitle" runat="server">
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, tieude %>"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:Label ID="lbltieude" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>                                
                            </tr>
                             </asp:Panel>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label56" runat="server" Text="<%$ Resources:labels, Content %>"></asp:Label>
                                </td>
                                <td >
                                    <textarea ID="areaContent" runat="server" rows="10" cols="50"></textarea>
                                </td>                                
                            </tr>
                            <asp:Panel ID="pncomment" runat="server">
                            <tr>
                                <td colspan="2" class="tibtdh">
                                    <%=Resources.labels.phanhoitunganhang%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, comment %>"></asp:Label>
                                </td>
                                <td >
                                    <textarea ID="areacomment" runat="server" rows="10" cols="50" readonly="readonly"></textarea>
                                </td>                                
                            </tr>
                            </asp:Panel>
                            
                        </table>
                    </div>                

                  
    </div>
</asp:Panel>
                     <asp:Panel ID="pnfeedback" runat="server">
                         <div class="block1">
                             <div class="handle">
                                 <asp:Label ID="Label4" runat="server" 7
                                     Text="<%$ Resources:labels, ketquagiaodich %>"></asp:Label>
                             </div>
                             <div class="content">
                                 <div style=" padding-top:10px; padding-bottom:10px; text-align:center;">
                                     <asp:Label ID="Label5" runat="server" ForeColor="Red" 
                                         Text="Tra soát giao dịch thành công"></asp:Label>
                                 </div>
                             </div>
                         </div>
                     </asp:Panel>
                                      <!--Button next-->
                 <div style="text-align:center; margin-top:10px;">

                    <asp:Button ID="btnsend" runat="server" OnClientClick="return validateFB();"
                         Text="<%$ Resources:labels, send %>"
                         onclick="btnsend_Click"/>
                         <asp:Button ID="btnPrint" runat="server"  Text="<%$ Resources:labels, inthongtin %> " 
                            onclientclick="javascript:return poponload()"  />&nbsp;
                                             &nbsp;
                    <asp:Button ID="btnBack" runat="server" 
                         Text="<%$ Resources:labels, quaylai %>"  onclick="btnBack_Click"/>
                 
                 </div>

</div>

<script>
	      function poponload()
    {
    testwindow= window.open ("widgets/IBFeedBack/print.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=700,height=650");
    testwindow.moveTo(0,0);
    return false;
    }
    function validateFB() {
        if (document.getElementById('<%=txtsgd.ClientID %>').value == '') {
            alert('Số giao dịch không rỗng');
            document.getElementById('<%=txtsgd.ClientID %>').focus();
            return false;
        }
        else {
            if (document.getElementById('<%=txttitle.ClientID %>').value == '') {
                alert('Tiêu đề không rỗng');
                document.getElementById('<%=txttitle.ClientID %>').focus();
                return false;
            }
            else {
                if (document.getElementById('<%=areaContent.ClientID %>').value == '') {
                    alert('Nội dung không rỗng');
                    document.getElementById('<%=areaContent.ClientID %>').focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    }
</script>