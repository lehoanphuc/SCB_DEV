<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBNTH_Widget" %>

<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<script src="JS/common.js" type="text/javascript"></script>

<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="al">
    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, themmoinguoithuhuong %>"></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<div class="divError">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnTIB" runat="server" class="divcontent">
            <div class="handle">
                <asp:Label runat="server" Text="<%$ Resources:labels, loaichuyenkhoan %>"></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label58" runat="server" Text="<%$ Resources:labels, loaichuyenkhoan %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-5">
                        <asp:DropDownList ID="ddlTransferType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTransferType_Changed">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnNTHCNH" runat="server" class="divcontent">
            <div class="handle">
                Create beneficiary in inner bank
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, taikhoan %>"></asp:Label>
                        &nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-5">
                        <asp:TextBox ID="txtNTHAccount" runat="server" Width="237px" OnTextChanged="txtNTHAccount_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:Label ID="lberror2" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>

                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tennguoithuhuong %>"></asp:Label>
                        &nbsp;*
                    </div>
                    <div class="col-xs-7 col-sm-5">
                        <asp:TextBox ID="txtNTHName" runat="server" Width="237px" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-5">
                        <asp:TextBox ID="txtNTHDesc" runat="server" Skin="txtML"></asp:TextBox>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnNTHNNH">
            <div class="block1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="handle">
                            Create beneficiary in outer bank
                        </div>
                        <div class="content">
                            <table class="style11" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td colspan="2" class="tibtdh">
                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, thongtinnguoithuhuong %>"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tibtd" width="25%">
                                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, tennguoithuhuong %>"></asp:Label>
                                        &nbsp;*
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNTHName1" runat="server" Width="237px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table class="style11" cellspacing="0" cellpadding="5">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RadioButton ID="radCMND" runat="server" Text="<%$ Resources:labels, quasocmnd %>"
                                                        GroupName="STK" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, socmnd %>"></asp:Label>&nbsp;*
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNTHLicense" runat="server" Width="237px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, ngaycap %>"></asp:Label>&nbsp;*
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNTHissuedate" CssClass="dateselect" runat="server" Width="237px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, noicap %>"></asp:Label>&nbsp;*
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNTHIssuePlace" runat="server" Width="237px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="style11" cellspacing="0" cellpadding="5">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RadioButton ID="radAccount" runat="server" Text="<%$ Resources:labels, quasotaikhoan %>"
                                                        Checked="True" GroupName="STK" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="25%">
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                                                    &nbsp;*
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNTHAccount1" runat="server" Width="237px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label57" runat="server" Text="<%$ Resources:labels, tinhthanh %>"></asp:Label>
                                                    &nbsp;*
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label70" runat="server" Text="<%$ Resources:labels, nganhang %>"></asp:Label>&nbsp;*
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlBankRecieve" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBankRecieve_SelectedIndexChanged"
                                                        SkinID="extDDL1">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label72" runat="server" Text='<%$ Resources:labels, chinhanhphonggiaodich %>'></asp:Label>
                                                    &nbsp;*
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlChildBank" runat="server" SkinID="extDDL1">
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtBranchDesc" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tibtd">
                                        <asp:Label ID="Label89" runat="server" Text="<%$ Resources:labels, diachinguoinhantien %>"></asp:Label>
                                        &nbsp; *
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" runat="server" Width="237px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tibtd">
                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNTHDesc1" runat="server" Width="237px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--Button next-->
                <div style="text-align: center; margin-top: 10px;">
                    <asp:Button ID="btbank2" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="btbank2_Click" />

                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, tieptuc %>" OnClick="Button4_Click1"
                        OnClientClick="return validate1();" />
                    <div class="clearfix"></div>
                </div>
            </div>

        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<!--Button next-->
<div runat="server" id="divNext" style="text-align: center; margin-top: 10px;">
    <asp:Button ID="btnNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, tieptuc %>" OnClick="btnNext_Click"
        OnClientClick="return validate();" />
</div>
<asp:Panel runat="server" ID="pnConfirm" class="divcontent">
    <div class="handle">
        Confirm
        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, thongtinnguoithuhuong %>"></asp:Label>
    </div>
    <div class="content_table">
        <div class="row">
            <div class="col-xs-5 col-sm-6 right">
                <asp:Label ID="Label88" runat="server" Text="<%$ Resources:labels, loaichuyenkhoan %>"></asp:Label>
                <asp:Label ID="lbtemp" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="col-xs-7 col-sm-6 line30">
                <asp:Label ID="lblTranferType" runat="server" Text="Inner"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-5 col-sm-6 right">
                <asp:Label ID="Label86" runat="server" Text="<%$ Resources:labels, tennguoithuhuong %>"></asp:Label>
            </div>
            <div class="col-xs-7 col-sm-6 line30">
                <asp:Label ID="lblReceiverName" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-5 col-sm-6 right">
                <asp:Label ID="Label87" runat="server" Text="<%$ Resources:labels, taikhoan %>"></asp:Label>
            </div>
            <div class="col-xs-7 col-sm-6 line30">
                <asp:Label ID="lblAcctNo" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-5 col-sm-6 right">
                <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, mota %>"></asp:Label>
            </div>
            <div class="col-xs-7 col-sm-6 line30">
                <asp:Label ID="lblDesc" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <!--Button next-->
    <div style="text-align: center; margin-top: 10px;">
        <asp:Button ID="btback3" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="btback3_Click" />

        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, hoanthanh %>"
            OnClick="Button2_Click" />
        <div class="clearfix"></div>
    </div>
</asp:Panel>

<!--end-->
<asp:Panel ID="pnResult" runat="server" class="divcontent">
    <div class="header-title">
        <label class="bold"><%= Resources.labels.ketquagiaodich %></label>
    </div>
    <div class="content">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <div style="padding-top: 10px; padding-bottom: 10px; text-align: center;">
                    <asp:Label ID="lblTB" runat="server" ForeColor="Red" Text="Create beneficiary sucessful"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div style="text-align: center; margin-top: 10px;">
        <asp:Button ID="Button3" CssClass="btn btn-warning" runat="server" Text="<%$ Resources:labels, thoat %>" OnClick="Button3_Click" />

        <asp:Button ID="btnNew" CssClass="btn btn-success" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnNew_Click" />

        <div class="clearfix"></div>
    </div>
</asp:Panel>
<script type="text/javascript">

    function validate() {
        if (!validateEmpty('<%=txtNTHName.ClientID %>', '<%=Resources.labels.bancannhaptennguoithuhuong %>', '<%=lblError.ClientID%>')) {
            if (validateEmpty('<%=txtNTHAccount.ClientID %>', '<%=Resources.labels.bancannhaptennguoithuhuong %>', '<%=lberror2.ClientID%>')) {

            }
            document.getElementById('<%=lblError.ClientID %>').innerHTML = '';
            document.getElementById('<%=txtNTHAccount.ClientID %>').focus();
            return false;
        }
    }


    function validate1() {
        if (validateEmpty('<%=txtNTHName1.ClientID %>', '<%=Resources.labels.bancannhaptennguoithuhuong %>','<%=lblError.ClientID%>')) {

            if (validateEmpty('<%=txtAddress.ClientID %>', '<%=Resources.labels.bancannhapdiachi %>', '<%=lblError.ClientID%>')) {
                if (document.getElementById('<%=radCMND.ClientID %>').checked) {
                        if (validateEmpty('<%=txtNTHLicense.ClientID %>', '<%=Resources.labels.bancannhapcmndhochieu %>', '<%=lblError.ClientID%>')) {
                            if (validateEmpty('<%=txtNTHissuedate.ClientID %>', '<%=Resources.labels.bancannhapngaycap %>', '<%=lblError.ClientID%>')) {
                                if (validateEmpty('<%=txtNTHIssuePlace.ClientID %>', '<%=Resources.labels.bancannhapnoicap %>', '<%=lblError.ClientID%>')) {
                                }
                                else {
                                    document.getElementById('<%=txtNTHIssuePlace.ClientID %>').focus();
                                    return false;
                                }
                            }
                            else {
                                document.getElementById('<%=txtNTHissuedate.ClientID %>').focus();
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtNTHLicense.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        if (validateEmpty('<%=txtNTHAccount1.ClientID %>', '<%=Resources.labels.bancannhapsotaikhoan %>')) {
                            if (validateEmpty('<%=ddlChildBank.ClientID %>', '<%=Resources.labels.bancanchonnganhangnhan %>')) {
                            }
                            else {
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtNTHAccount1.ClientID %>').focus();
                            return false;
                        }
                    }
                }
                else {
                    document.getElementById('<%=txtAddress.ClientID %>').focus();
                return false;
            }

        }
        else {
            document.getElementById('<%=txtNTHName1.ClientID %>').focus();
            return false;
        }
    }
</script>
