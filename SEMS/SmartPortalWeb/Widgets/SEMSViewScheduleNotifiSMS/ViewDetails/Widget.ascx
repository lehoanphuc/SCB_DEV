<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSViewScheduleNotifiSMS_ViewDetails_Widget" %>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script> 
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/mask.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<div class="al">
<span> <%=Resources.labels.chitietthongtindatlich %></span><br />
<img style="margin-top:5px;" src="widgets/IBTransactionHistory/Images/underline.gif" />
</div>
<!--thong tin tai khoan DD-->
<asp:Panel ID="pnDD" runat="server">
        <div class="block1">
        <div class="handle">
            <%=Resources.labels.chitietthongtindatlich %>
        </div>
        <div class="content">
            <table class="style1" cellspacing="0" cellpadding="5">
                <tr>
                    <td colspan="2" class="tibtdh">
                        <%=Resources.labels.thongtinlich %>
                    </td>
                </tr>
                <tr>
               
                <td colspan="4">
                            <table class="style1" cellpadding="4" cellspacing="0" width="100%">
                         <tr>
                            <td class="tibtd" width="30%" >
                                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, tenlich1 %>'></asp:Label>
                            </td>
                            <td width="70%" >
                                
                                <asp:Label ID="lbschedulename" runat="server" Text=""></asp:Label>
                                
                            </td> 
                        </tr>
                                <tr>
                                    <td class="tibtd" width="30%">
                                        <asp:Label ID="Label110" runat="server" Text='<%$ Resources:labels, loaigiaodich %>'></asp:Label>
                                    </td>
                                    <td width="70%">
                                        <asp:Label ID="lblScheduleType" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label111" runat="server" Text='<%$ Resources:labels, kieudatlich %>'></asp:Label>
                            </td>
                            <td>
                                
                                <asp:Label ID="lblTransferType" runat="server"></asp:Label>
                                
                            </td>
                            
                        </tr>
                        <tr>
                                    <td class="tibtd">
                                        <asp:Label ID="Label63" runat="server" Text='<%$ Resources:labels, tungay %>'></asp:Label>
                                    </td>
                                    <td >
                                        <asp:Label ID="lbfromdate" runat="server" Text="08/03/2010"></asp:Label>
                                    </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label64" runat="server" Text='<%$ Resources:labels, denngay %>'></asp:Label>
                            </td>
                            <td>
                                
                                <asp:Label ID="lbtodate" runat="server" Text="08/04/2010"></asp:Label>
                                
                            </td>
                        </tr>
                        <tr>
                                    <td class="tibtd">
                                        <asp:Label ID="Label36" runat="server" Text='<%$ Resources:labels, ngaythuchiengiaodichcualich %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbtime" runat="server" Text="08/03/2010"></asp:Label>
                                    </td>
                        </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label66" runat="server" Text='<%$ Resources:labels, thungaychuyen %>'></asp:Label>
                                    </td>
                                    <td class="tibtd">
                                        <asp:Label ID="lbdatetransfer" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            
                </td>
                
                </tr>
                <tr>
                    <td colspan="2" class="tibtdh">
                        <%=Resources.labels.thongtinsmsthongbao %></td>
                </tr>
                <tr>
                    <td class="tibtd" width="30%">
                        <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, noidung %>'></asp:Label>
                    </td>
                    <td Width="70%" >
                        <asp:Label ID="lblContent" runat="server" Text="noidung"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label18" runat="server" Text='<%$ Resources:labels, loaithongbao %>'></asp:Label>
                    </td>
                    <td Width="50%">
                        <asp:Label ID="lblOptionSend" runat="server" Text="tuychongui"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, danhsachdienthoaibosung %>'></asp:Label>
                    </td>
                    <td Width="50%">
                        <asp:Label ID="lblListPhone" runat="server" Text="999999999"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
                  <div style="text-align:center; margin-top:10px;">
                    <asp:Button ID="Button1" runat="server" 
                         Text='<%$ Resources:labels, quaylai %>' PostBackUrl="javascript:history.go(-1)" />
                 </div>
</asp:Panel>

