<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransferOutBank1_Widget" %>


<style type="text/css">
    .style1
    {
        width: 100%;
        background-color: #EAEDD8;
    }
    .tibtd
    {
    }
    .tibtdh
    {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
    }
    .style2
    {
        width: 100%;
    }
</style>

<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>

<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>

<script src="JS/mask.js" type="text/javascript"></script>

<script src="JS/docso.js" type="text/javascript"></script>

<script src="widgets/IBTransferOutBank1/JS/common.js" type="text/javascript"></script>

<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransferInBank1/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<!--Transfer In Bank-->


<div style="text-align: center; color: Red;">
    <asp:Label runat="server" ID="lblTextError"></asp:Label></div>
<asp:Panel ID="pnTIB" runat="server">
    <figure>
        <legend class="handle"><%= Resources.labels.chitietgiaodich %></legend>
        
    </figure>
    <div class="block1">
        <div class="handle">
            <asp:Label ID="Label39" runat="server" Text='<%$ Resources:labels, chitietgiaodich %>'></asp:Label>
        </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table class="style1" cellspacing="0" cellpadding="5">
                        <tr>
                            <td colspan="2" class="tibtdh">
                                <asp:Label ID="Label20" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>&nbsp;*
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;<asp:HiddenField ID="txtChu" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label80" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>&nbsp;*
                            </td>
                            <td>
                                <asp:TextBox ID="txtSenderName" runat="server" Width="51%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="tibtdh">
                                <asp:Label ID="Label23" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, nguoithuhuong %>'></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNguoiThuHuong" runat="server" AutoPostBack="True" SkinID="extDDL7"
                                    OnSelectedIndexChanged="ddlNguoiThuHuong_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, hinhthucnhan %>'></asp:Label>
                            </td>
                            <%--      <td >
                                    <asp:TextBox ID="txtReceiverNameAccount" runat="server"></asp:TextBox>
                                </td> --%>
                            <td class="tibtd">
                                <asp:RadioButton ID="radCMND" runat="server" Text='<%$ Resources:labels, quasocmnd %>'
                                    GroupName="AccNoTo" onclick="enableCMND();" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                &nbsp;
                            </td>
                            <td class="tibtd">
                                <table cellpadding="2" class="style2">
                                    <tr>
                                        <td width="25%">
                                            <asp:Label ID="Label73" runat="server" Text='<%$ Resources:labels, socmnd %>'></asp:Label>&nbsp;*
                                        </td>
                                        <td width="75%">
                                            <asp:TextBox ID="txtCMND" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" Text='<%$ Resources:labels, ngaycap %>'></asp:Label>&nbsp;*
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtissuedate" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label37" runat="server" Text='<%$ Resources:labels, noicap %>'></asp:Label>&nbsp;*
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtissueplace" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%--                            <tr>
                                <td colspan="2" class="tibtdh">
                                    Thông tin người nhận
                                </td>
                            </tr>--%>
                        <tr>
                            <td class="tibtd">
                                &nbsp;
                            </td>
                            <td class="tibtd">
                                <asp:RadioButton ID="radAcctNo" runat="server" Checked="true" GroupName="AccNoTo"
                                    onclick="enableAcc();" Text="<%$ Resources:labels, quasotaikhoan %> " />
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                &nbsp;
                            </td>
                            <td class="tibtd">
                                <table cellpadding="2" class="style2">
                                    <tr>
                                        <td style="width: 25%">
                                            <asp:Label ID="Label74" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                                            &nbsp;*
                                        </td>
                                        <td style="width: 75%">
                                            <asp:TextBox ID="txtReceiverAccount" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label38" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>&nbsp;*
                            </td>
                            <td>
                                <asp:TextBox ID="txtReceiverName" runat="server" Width="51%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label75" runat="server" Text="<%$ Resources:labels, diachinguoinhantien %>"></asp:Label>&nbsp;*
                            </td>
                            <td>
                                <asp:TextBox ID="txtReceiverAdd" runat="server" Width="51%" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label57" runat="server" Text="<%$ Resources:labels, tinhthanh %>"></asp:Label>
                                &nbsp;*
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label70" runat="server" Text="<%$ Resources:labels, nganhang %>"></asp:Label>&nbsp;*
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBankRecieve" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBankRecieve_SelectedIndexChanged"
                                    SkinID="extDDL2">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlNation" runat="server" Visible="False">
                                    <asp:ListItem>Việt Nam</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label72" runat="server" Text='<%$ Resources:labels, chinhanhphonggiaodich %>'></asp:Label>
                                &nbsp;*
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlChildBank" runat="server" SkinID="extDDL2">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBranchDesc" runat="server" Width="40%" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtdh" colspan="2">
                                <asp:Label ID="Label24" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="15"></asp:TextBox>
                                &nbsp;<asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                &nbsp;<asp:Label ID="lblText" runat="server" Font-Bold="True" Font-Italic="True"
                                    Font-Size="7pt" ForeColor="#0066FF" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label44" runat="server" Text="<%$ Resources:labels, nguoitraphi %>"
                                    Visible="false">*</asp:Label>
                                <asp:RadioButtonList ID="rdPhi" runat="server" RepeatDirection="Horizontal" Visible="false">
                                    <asp:ListItem Selected="True" Text="<%$ Resources:labels, nguoigui %>"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:labels, nguoinhan %>"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                                &nbsp;*
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesc" runat="server" Height="50px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                &nbsp;<div style="width: 220px; vertical-align: text-top; float: right;">
                                    <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                        Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtdh" colspan="2">
                                <asp:Label ID="Label26" runat="server" Text='<%$ Resources:labels, luunoidungchuyenkhoannaythanhmau %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, luuthanhmau %>'></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbmau" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, dattenchomau %>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txttenmau" runat="server" Width="300px"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="tibtd">
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="cbQuaNgay" runat="server" Text="<%$ Resources:labels, giaodichgiuquangayketiep %>"
                                    Checked="True" />
                            </td>
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

    <script type="text/javascript">//<![CDATA[

  var cal = Calendar.setup({
      onSelect: function(cal) { cal.hide() }
  });
  cal.manageFields("<%=txtissuedate.ClientID %>", "<%=txtissuedate.ClientID %>", "%d/%m/%Y");           
//]]>
    </script>

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
                        <asp:Label ID="Label28" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd" width="35%">
                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                    </td>
                    <td width="65%">
                        <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                        <asp:Label ID="lblSenderBranch" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, sodutruockhighino %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBalanceSender" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblSenderCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="tibtdh">
                        <asp:Label ID="Label30" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblReceiverName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd" colspan="2">
                        <asp:Panel ID="pnConfirmCMND" runat="server" Width="100%">
                            <table cellpadding="0" cellspacing="0" class="style2">
                                <tr>
                                    <td width="35%" style="padding-bottom: 10px;">
                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, socmnd %>"></asp:Label>
                                    </td>
                                    <td width="65%" style="padding-left: 4px; padding-bottom: 10px;">
                                        <asp:Label ID="lblLicense" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 10px;">
                                        <asp:Label ID="Label54" runat="server" Text="<%$ Resources:labels, ngaycap %>"></asp:Label>
                                    </td>
                                    <td style="padding-left: 4px; padding-bottom: 10px;">
                                        <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, noicap %>"></asp:Label>
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <asp:Label ID="lblIssuePlace" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd" colspan="2">
                        <asp:Panel ID="pnConfirmAccount" runat="server" Width="100%">
                            <table cellpadding="0" cellspacing="0" class="style2">
                                <tr>
                                    <td width="35%">
                                        <asp:Label ID="Label60" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                                    </td>
                                    <td width="65%" style="padding-left: 4px;">
                                        <asp:Label ID="lblReceiverAccount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label76" runat="server" Text="<%$ Resources:labels, diachinguoinhantien %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblConfirmReceiverAdd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label78" runat="server" Text="<%$ Resources:labels, chinhanhphonggiaodich %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblConfirmChildBank" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtdh" colspan="2">
                        <asp:Label ID="Label32" runat="server" Text="<%$ Resources:labels, noidungchuyenkhoan %>"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmount" runat="server" Text="0"></asp:Label>
                        &nbsp;<asp:Label ID="lblAmountCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label46" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPhi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label49" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPhiAmount" runat="server" Text="0"></asp:Label>
                        &nbsp;<asp:Label ID="lblFeeCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label59" runat="server" Text="Số dư sau chuyển"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label60" runat="server" Text="80,000,000"></asp:Label>
                                    <asp:Label ID="Label61" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>--%>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label16" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click" Text='<%$ Resources:labels, xacnhan %>' />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text='<%$ Resources:labels, quaylai %>' />
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
                        <asp:Label ID="Label71" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLoaiXacThuc" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label59" runat="server" Text="<%$ Resources:labels, maxacthuc %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOTP" runat="server" AutoCompleteType="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        &nbsp;
                    </td>
                    <td>
                        <img alt="" src="widgets/IBTransferInBank1/Images/otp.gif" style="width: 100px; height: 60px" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px;">
            <asp:Button ID="btnAction" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAction_Click" Text="<%$ Resources:labels, thuchien %>" />
            &nbsp;
            <asp:Button ID="btnBackTransfer" runat="server" OnClick="btnBackTransfer_Click" Text="<%$ Resources:labels, quaylai %>" />
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
                        <asp:Label ID="Label25" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd" width="35%">
                        <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, hotennguoitratien %>'></asp:Label>
                    </td>
                    <td width="65%">
                        <asp:Label ID="lblEndSenderName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndSenderAccount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="visibility: collapse">
                    <td class="tibtd">
                        <asp:Label ID="Label62" runat="server" Text='<%$ Resources:labels, sodusaukhighino %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndBalanceSender" runat="server"></asp:Label>
                        <asp:Label ID="lblEndSenderCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="tibtdh">
                        <asp:Label ID="Label33" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label29" runat="server" Text='<%$ Resources:labels, hotennguoinhantien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndReceiverName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd" colspan="2">
                        <asp:Panel ID="pnEndCMND" runat="server" Width="100%">
                            <table cellpadding="0" cellspacing="0" class="style2">
                                <tr>
                                    <td width="35%" style="padding-bottom: 10px;">
                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, socmnd %>"></asp:Label>
                                    </td>
                                    <td width="65%" style="padding-left: 4px; padding-bottom: 10px;">
                                        <asp:Label ID="lblEndLicense" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 10px;">
                                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:labels, ngaycap %>"></asp:Label>
                                    </td>
                                    <td style="padding-left: 4px; padding-bottom: 10px;">
                                        <asp:Label ID="lblEndIssueDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, noicap %>"></asp:Label>
                                    </td>
                                    <td style="padding-left: 4px;">
                                        <asp:Label ID="lblEndIssuePlace" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd" colspan="2">
                        <asp:Panel ID="pnEndAccount" runat="server">
                            <table cellpadding="0" cellspacing="0" class="style2">
                                <tr>
                                    <td width="35%">
                                        <asp:Label ID="Label27" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                                    </td>
                                    <td width="65%" style="padding-left: 4px;">
                                        <asp:Label ID="lblEndReceiverAccount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label77" runat="server" Text='<%$ Resources:labels, diachinguoinhantien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndReceiverAdd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label79" runat="server" Text='<%$ Resources:labels, chinhanhphonggiaodich %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndChildBank" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtdh" colspan="2">
                        <asp:Label ID="Label35" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label31" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lbEndCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label51" runat="server" Text='<%$ Resources:labels, nguoitraphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndPhi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, sotienphi %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndPhiAmount" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="lblEndFeeCCYID" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label65" runat="server" Text="Số dư sau chuyển"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label66" runat="server" Text="80,000,000"></asp:Label>
                                    <asp:Label ID="Label67" runat="server" Text="VNĐ"></asp:Label>
                                </td>
                            </tr>--%>
                <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label34" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEndDesc" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--                            <tr>
                                <td class="tibtd">
                                    <asp:Label ID="Label36" runat="server" Text="Số dư "></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label37" runat="server" Text="50.000.000 LAK"></asp:Label>
                                </td>
                            </tr>--%>
            </table>
        </div>
        <!--Button next-->
        <div style="text-align: center; margin-top: 10px; margin-bottom: 10px;">
            <asp:Button ID="btnView" runat="server" OnClientClick="javascript:return poponloadview()"
                Text="<%$ Resources:labels, viewphieuin %>" OnClick="btnView_Click" />
            &nbsp;
            <asp:Button ID="btnPrint" runat="server" OnClientClick="javascript:return poponload()"
                Text="<%$ Resources:labels, inketqua %>" OnClick="btnPrint_Click" />
            &nbsp;
            <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text='<%$ Resources:labels, lammoi %>' />
        </div>
    </div>
</asp:Panel>
<!--end-->

<script type="text/javascript">
    function poponload()
    {
    testwindow= window.open ("widgets/IBTransferOutBank1/print.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=500,height=650");
    testwindow.moveTo(0,0);
    return false;
    }
    function poponloadview()
    {
    testwindow= window.open ("widgets/IBTransferOutBank1/viewprint.aspx?cul="+'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>', "BienLai",
        "menubar=1,scrollbars=1,width=500,height=650");
    testwindow.moveTo(0,0);
    return false;
    }
    
     function enableAcc()
    {
        //document.getElementById("<%=txtCMND.ClientID %>").disabled=true;
        //document.getElementById("<%=txtissueplace.ClientID %>").disabled=true ;
        //document.getElementById("<%=txtissuedate.ClientID %>").disabled=true ;
//        //document.getElementById("<%=txtReceiverAccount.ClientID %>").value="";
//        //document.getElementById("<%=txtCMND.ClientID %>").value="";
//        //document.getElementById("<%=txtissueplace.ClientID %>").value="";
//        //document.getElementById("<%=txtissuedate.ClientID %>").value="";
        //document.getElementById("<%=txtReceiverAccount.ClientID %>").disabled=false;
    
    }
    function enableCMND()
    {
        //document.getElementById("<%=txtReceiverAccount.ClientID %>").disabled=true;
       // document.getElementById("<%=txtCMND.ClientID %>").disabled=false ;
       // document.getElementById("<%=txtissueplace.ClientID %>").disabled=false ;
       // document.getElementById("<%=txtissuedate.ClientID %>").disabled=false ;
//        //document.getElementById("<%=txtCMND.ClientID %>").value="";
//        //document.getElementById("<%=txtReceiverAccount.ClientID %>").value="";
//        //document.getElementById("<%=txtissueplace.ClientID %>").value="";
//        //document.getElementById("<%=txtissuedate.ClientID %>").value="";
    
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
       
        if(document.getElementById("<%=radCMND.ClientID %>").checked==true)
        {
       
                    if(validateEmpty('<%=txtCMND.ClientID %>','<%=Resources.labels.socmndcuataikhoandenkhongrong %> '))
                    {
                        if(IsNumeric('<%=txtCMND.ClientID %>','<%=Resources.labels.socmndcuataikhoandenkhongdungdinhdangso %> '))
                        {
                             if(validateEmpty('<%=txtissuedate.ClientID %>','<%=Resources.labels.ngaycapkhongrong %>'))
                            {
                                if(validateEmpty('<%=txtissueplace.ClientID %>','<%=Resources.labels.noicapkhongrong %>'))
                                {
                                }
                                else
                                {
                                    document.getElementById('<%=txtissueplace.ClientID %>').focus();
                                    return false;
                                }
                                }
                            else
                            {
                                document.getElementById('<%=txtissuedate.ClientID %>').focus();
                                return false;
                            }
                        }
                        else
                        {
                            document.getElementById('<%=txtCMND.ClientID %>').focus();
                            return false;
                        }
                    }
                    else
                    {
                        document.getElementById('<%=txtCMND.ClientID %>').focus();
                        return false;
                    }
        }
        if(document.getElementById("<%=radAcctNo.ClientID %>").checked==true)
        {
            if(validateEmpty('<%=txtReceiverAccount.ClientID %>','<%=Resources.labels.taikhoandenkhongrong %>'))
            {
                    
            }
            else
            {
                document.getElementById('<%=txtReceiverAccount.ClientID %>').focus();
                return false;
            }
        }
    
 
            if(validateMoney('<%=txtAmount.ClientID %>','<%=Resources.labels.bancannhapsotien %>'))
            {
                if(validateEmpty('<%=txtDesc.ClientID %>','<%=Resources.labels.bancannhapnoidung %>'))
                {
                    if(validateEmpty('<%=txtReceiverName.ClientID %>','<%=Resources.labels.bancannhaptennguoinhan %>'))
                    {
                        if(validateEmpty('<%=txtReceiverAdd.ClientID %>','<%=Resources.labels.bancannhapdiachinguoinhan %>'))
                        {
                                if(validateEmpty('<%=txtSenderName.ClientID %>','<%=Resources.labels.bancannhaptennguoigui %>'))
                                {
                                }
                                else
                                {
                                    document.getElementById('<%=txtSenderName.ClientID %>').focus();
                                    return false;
                                }
                        }
                        else
                        {
                            document.getElementById('<%=txtReceiverAdd.ClientID %>').focus();
                            return false;
                        }
                        }
                    else
                    {
                        document.getElementById('<%=txtReceiverName.ClientID %>').focus();
                        return false;
                    }
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
    
</script>

