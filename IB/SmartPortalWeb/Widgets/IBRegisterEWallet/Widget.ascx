<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBRegisterEWallet_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<style type="text/css">
    .style1 {
        width: 100%;
        background-color: #EAEDD8;
    }

    .tiwhite {
        background-color: #EAEDD8;
        /*font-weight:bold;*/
    }

    .tibtd {
    }

    .tibtdh {
        background-color: #009CD4;
        /*background-color: #EAFAFF;*/
        font-weight: bold;
    }

    .style2 {
        height: 36px;
    }

    .th {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
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
<!--Transfer In Bank-->

<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div class="th">
    <span><%=Resources.labels.registerewallet %></span><br />
    <img style="margin-top: 5px;" src="widgets/IBTransactionHistory/Images/underline.gif" />
</div>
<div style="text-align: center; color: Red;">
    <asp:Label runat="server" ID="lblTextError"></asp:Label>
</div>

<asp:Panel ID="pnTIB" runat="server">
    <div class="block1">
        <div class="handle">
            <%=Resources.labels.chitietgiaodich %>
        </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <table class="style1" cellspacing="0" cellpadding="5">

                        <tr>

                            <td colspan="2" class="tibtdh">
                                <asp:Label runat="server" Text='<%$ Resources:labels, registerinfomation %>'></asp:Label>
                            </td>

                            <td class="tibtdh">
                                <asp:Label ID="Label26" runat="server" Text="<%$ Resources:labels, lasttransactiondate %>"></asp:Label>
                            </td>
                            <td class="tibtdh">
                                <asp:Label ID="Label28" runat="server" Text="<%$ Resources:labels, availablebalance %>"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                                &nbsp;*
                            </td>
                            <td class="tibtd">
                                <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tiwhite">
                                <asp:Label ID="lblLastTranDate" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td class="tiwhite">
                                <asp:Label ID="lblAvailableBal" runat="server"></asp:Label>
                                &nbsp;
                            <asp:Label ID="lblAvailableBalCCYID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, ewallettype %>'></asp:Label>&nbsp;*
                            </td>
                            <td class="tibtd">
                                <asp:DropDownList ID="ddlWalletType" runat="server"></asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, sodienthoai %>'></asp:Label>&nbsp;*
                            </td>
                            <td class="tibtd">
                                <asp:TextBox ID="txtPhoneNumber" runat="server" Width="182px"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnTIBNext" runat="server" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>'
                OnClick="btnTIBNext_Click" />
        </div>
    </div>
    <%--    <script type="text/javascript">//<![CDATA[

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
                        <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, thongtintaikhoan %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, tentaikhoan %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, sotaikhoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                        <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label18" runat="server" Text='<%$ Resources:labels, availablebalance %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtdh" colspan="2">
                        <asp:Label ID="Label21" runat="server" Text='<%$ Resources:labels, ewalletinformation %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label36" runat="server" Text="<%$ Resources:labels, ewallettype %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblWalletType" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, sodienthoai %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPhonenumber" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />
            &nbsp;
            <asp:Button ID="btnBackTransfer" runat="server" OnClick="btnBackTransfer_Click" Text='<%$ Resources:labels, quaylai %>' />
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
                        <asp:Label ID="Label61" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" Height="22px" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOTP_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="btnSendOTP" runat="server" OnClick="btnSendOTP_Click" Text="Send" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label62" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">&nbsp;
                    </td>
                    <td>
                        <img alt="" src="widgets/IBTransferInBank1/Images/otp.gif" style="width: 100px; height: 60px" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnAction" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text='<%$ Resources:labels, thuchien %>' />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text='<%$ Resources:labels, quaylai %>' />
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
                        <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, thongtintaikhoan %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, tentaikhoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, sotaikhoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblendSenderAccount" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtdh" colspan="2">
                        <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, ewalletinformation %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label43" runat="server" Text='<%$ Resources:labels, ewallettype %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndWalletType" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, sodienthoai %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndPhoneNumber" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtdh" colspan="2">
                        <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, thongtingiaodich %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, sogiaodich %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label32" runat="server" Text='<%$ Resources:labels, thoidiem %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, mathamchieu %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndRefCode" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview()"
                Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()"
                Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" />
            &nbsp;
            <asp:Button ID="btnNew" runat="server" Text='<%$ Resources:labels, lammoi %>' OnClick="Button6_Click" />
        </div>
    </div>
</asp:Panel>
<!--end-->
<br />

<script type="text/javascript">
    function poponload() {
        testwindow = window.open("widgets/IBRegisterEWallet/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function poponloadview() {
        testwindow = window.open("widgets/IBRegisterEWallet/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);


        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }

        return str;
    }


    function ValidateLimit(obj, maxchar) {
        if (this.id) obj = this;
        replaceSQLChar(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }

</script>

