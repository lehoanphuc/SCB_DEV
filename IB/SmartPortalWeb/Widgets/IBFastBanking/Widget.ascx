<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransferFastBanking_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<script src="widgets/IBFastBanking/JS/jscal2.js" type="text/javascript"></script>

<script src="widgets/IBFastBanking/JS/lang/en.js" type="text/javascript"></script>

<script src="JS/mask.js" type="text/javascript"></script>

<script src="JS/docso.js" type="text/javascript"></script>

<script src="widgets/IBFastBanking/JS/common.js" type="text/javascript"></script>
<link type="text/css" rel="stylesheet" href="widgets/IBFastBanking/CSS/FastBanking.css">
<link href="widgets/IBFastBanking/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBFastBanking/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBFastBanking/CSS/steel/steel.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager runat="server">
</asp:ScriptManager>
<!--Transfer In Bank-->

<div id="fastbanking_header"></div>

<asp:Image ID="fastbanking_navigation1" runat="server" ImageUrl="~/Widgets/IBFastBanking/Images/navigator1.png" Width="100%" Height="29px" />
<asp:Image ID="fastbanking_navigation2" runat="server" ImageUrl="~/Widgets/IBFastBanking/Images/navigator2.png" Width="100%" Height="29px" />
<asp:Image ID="fastbanking_navigation3" runat="server" ImageUrl="~/Widgets/IBFastBanking/Images/navigator3.png" Width="100%" Height="29px" />
<asp:Image ID="fastbanking_navigation4" runat="server" ImageUrl="~/Widgets/IBFastBanking/Images/navigator4.png" Width="100%" Height="29px" />

<asp:Panel ID="pnTIB" runat="server">
    <div class="block1" style="width:100%;">
<%--        <div id="fastbanking_navigation"></div>--%>
        <div <%--class="handle"--%> class="page_title">
            <asp:Label ID="Label28" runat="server" Text='<%$ Resources:labels, chitietgiaodich %>'></asp:Label>
        </div>
        <div class="fastbanking_content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table class="tbl" cellspacing="0" cellpadding="5">
                        <tr>
                            <td class="red_tr bold">
                                <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                            </td>
                            <td class="red_tr">
                                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>&nbsp;*
                            </td>
                            <td class="red_tr">
                                <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;<asp:RadioButton ID="radTS1" runat="server" Checked="True" GroupName="TIB"
                                    onclick="resetTS();" Text="Chuyển ngay" Visible="False" />
                                <asp:HiddenField ID="txtChu" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="red_tr bold">
                                <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                            </td>
                            <td class="red_tr">
                                <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, hotennguoinhantien %>'></asp:Label>&nbsp;*
                            </td>
                            <td style="margin-left: 40px" class="red_tr">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtReceiverName" runat="server" Enabled="False"></asp:TextBox>
                                                    <asp:TextBox ID="txtReceiverAccount" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="radTS" runat="server" GroupName="TIB" onclick="enableTS();"
                                                Text="<%$ Resources:labels, chuyenvaongayddmmyyyy %>" Visible="False" />
                                            <asp:TextBox ID="txtTS" runat="server" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class=" bold">
                                <asp:Label ID="Label17" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                            </td>
                            <td >
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True" Font-Italic="True"
                                                ForeColor="#0066FF" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>&nbsp;*
                                <asp:Label ID="Label44" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'
                                    Visible="false">*</asp:Label>&nbsp;
                                <asp:RadioButtonList ID="rdNguoiChiuPhi" runat="server" Visible="false" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Text="<%$ Resources:labels, nguoigui %>"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:labels, nguoinhan %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" Width="300px" Enabled="False"></asp:TextBox>
                                &nbsp;<div style="width: 220px; vertical-align: text-top; float: right;">
                                    <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                        Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button CssClass="continue_btn" ID="btnTIBNext" runat="server" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>'
                OnClick="btnTIBNext_Click" />
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
<div style="text-align: center; color: Red;">
    <asp:Label runat="server" ID="lblTextError"></asp:Label></div>
<!--confirm-->
<asp:Panel ID="pnConfirm" runat="server">
    <div class="block1" style="width:100%;">
        <%--<div id="fastbanking_navigation2"></div>--%>
        <div class="page_title">
            <%=Resources.labels.chitietgiaodich %>
        </div>
        <div class="fastbanking_content">
            <table class="tbl" cellspacing="0" cellpadding="5">
                <tr>
                    <td class="bold">
                        <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                        <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="red_tr"></td>
                    <td class="red_tr">
                        <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                    </td>
                    <td class="red_tr">
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="red_tr bold">
                        <asp:Label ID="Label22" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                    </td>
                    <td class="red_tr">
                        <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                    </td>
                    <td class="red_tr">
                        <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="bold">
                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblReceiverAccount" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblReceiverBranch" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold">
                        <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label48" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPhi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label45" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, noidungthanhtoan %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button CssClass="continue_btn" ID="btnApply" runat="server" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />
            &nbsp;
            <asp:Button  CssClass="continue_btn" ID="btnBackTransfer" runat="server" OnClick="btnBackTransfer_Click" Text='<%$ Resources:labels, quaylai %>' />
            &nbsp;
        </div>
    </div>
</asp:Panel>
<!--end-->
<!--token-->
<asp:Panel ID="pnOTP" runat="server">
    <div class="block1" style="width:100%;">
        <%--<div id="fastbanking_navigation3"></div>--%>
        <div class="page_title">
            <%=Resources.labels.chitietgiaodich %>
        </div>
        <div class="fastbanking_content">
            <table class="tbl" cellspacing="0" cellpadding="5">
                <tr>
                    <td class="bold" width="23%">
                        <%=Resources.labels.xacthucgiaodich %>
                    </td>
                    <td class="tibtd">
                        <asp:Label ID="Label58" runat="server" Text="<%$ Resources:labels, loaixacthuc %>" ></asp:Label>
                    </td>
                    <td style="width:20px;">
                        <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOTP_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width:300px;">
                        <asp:Button CssClass="continue_btn1" ID="btnSendOTP" runat="server" OnClick="btnSendOTP_Click" Text="Send" Visible="False"/>
                        </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label59" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                        <input type="text" style="display:none;"/>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <img alt="" src="widgets/IBFastBanking/Images/otp.gif" style="width: 100px; height: 60px" />
                    </td>
                    <td>
                        </td>
                </tr>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnAction" CssClass="continue_btn" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>"/>
            &nbsp;
            <asp:Button ID="btnBack" CssClass="continue_btn" runat="server" OnClick="btnBack_Click" Text="<%$ Resources:labels, quaylai %>" />
            &nbsp;
        </div>
    </div>
</asp:Panel>
<!--end-->
<!--sao ke-->
<asp:Panel ID="pnResultTransaction" runat="server">
    <div class="block1" style="width:100%;">
        <%--<div id="fastbanking_navigation4"></div>--%>
        <div class="page_title">
            <%=Resources.labels.ketquagiaodich %>
        </div>
        <div class="fastbanking_content">
            <table class="tbl" cellspacing="0" cellpadding="5">
                <tr>
                    <td class="bold">
                        <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="red_tr"></td>
                    <td class="red_tr">
                        <asp:Label ID="Label50" runat="server" Text='<%$ Resources:labels, sodusaukhighino %>'></asp:Label>
                    </td>
                    <td class="red_tr">
                        <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblBalanceCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold">
                        <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label29" runat="server" Text='<%$ Resources:labels, hotennguoinhantien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndReceiverName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="red_tr"></td>
                    <td class="red_tr">
                        <asp:Label ID="Label27" runat="server" Text='<%$ Resources:labels, taikhoanbaoco %>' Visible="False"></asp:Label>
                    </td>
                    <td class="red_tr">
                        <asp:Label ID="lblEndReceiverAccount" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bold">
                        <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, sogiaodich %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndTransactionNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label32" runat="server" Text='<%$ Resources:labels, thoidiem %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndDateTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label51" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td >
                        <asp:Label ID="Label54" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label34" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblReturnUrl" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <%--                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label36" runat="server" Text="Số dư "></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="Label37" runat="server" Text="50.000.000 VNĐ"></asp:Label>
                                </td>                                
                            </tr>--%>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            &nbsp;
            <asp:Button CssClass="continue_btn" ID="btnContinue" runat="server" OnClientClick="javascript:return poponload()"
                Text="<%$ Resources:labels, tieptuc %>" OnClick="btnContinue_Click" Visible="False" />
            &nbsp;
            </div>
    </div>
</asp:Panel>

<!--end-->

<div id="fastbaking_footer"></div>
<script type="text/javascript">
    function poponload() {
        testwindow = window.open("widgets/IBFastBanking/print.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function poponloadview() {
        testwindow = window.open("widgets/IBFastBanking/viewprint.aspx?cul=" + '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function resetTS() {
        document.getElementById("<%=txtTS.ClientID %>").value = "";
        document.getElementById("<%=txtTS.ClientID %>").disabled = true;
    }
    function enableTS() {
        document.getElementById("<%=txtTS.ClientID %>").disabled = false;
    }

    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);


        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }

        return str;
    }

    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
        document.getElementById('<%=txtChu.ClientID %>').value = number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>');
    }
    function validate() {
          if (validateMoney('<%=txtAmount.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
                if (validateEmpty('<%=txtDesc.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {
                }
                else {
                    document.getElementById('<%=txtDesc.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtAmount.ClientID %>').focus();
                return false;
            }
        }

    }    
</script>

