<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransferTemplate_Widget" %>
<asp:ScriptManager runat="server">
</asp:ScriptManager>

<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div class="th">
    <span><%=Resources.labels.themmoimauchuyenkhoan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="divError">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <asp:Panel ID="pnInfomation" runat="server" class="divcontent">
            <figure>
                <div class="handle">
                    <label class="bold"><%=Resources.labels.thongtinmauchuyenkhoan %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label55" runat="server" Text='<%$ Resources:labels, tenmau %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtTemplateName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label44" runat="server" Text='<%$ Resources:labels, loaichuyenkhoan %>'></asp:Label>
                            &nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:DropDownList ID="ddlTransferType" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="Button8" runat="server" CssClass="btn btn-warning" Text='<%$ Resources:labels, back %>' OnClick="Button8_Click" />
                    <asp:Button ID="Button7" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>'
                        OnClick="Button7_Click" />
                </div>
            </figure>
        </asp:Panel>
        <asp:Panel ID="pnTIB" runat="server" class="divcontent">
            <figure>
                <div class="handle">
                    <label class="bold"><%=Resources.labels.thongtinnguoitratien %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:DropDownList ID="ddlSenderAccountTIB" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccountTIB_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="handle">
                    <label class="bold"><%=Resources.labels.thongtinnguoinhantien %></label>
                </div>

                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, nguoithuhuong %>'></asp:Label>
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:DropDownList ID="ddlReceiverNameTIB" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReceiverNameTIB_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, taikhoanbaoco %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtRecieverAccountTIB" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="handle">
                    <label class="bold"><%=Resources.labels.noidungchuyenkhoan %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtAmountTIB" CssClass="amount" runat="server" MaxLength="21"></asp:TextBox>
                            &nbsp;<asp:Label ID="lblCurrencyTIB" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">&nbsp;</div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:Label ID="lblTextTIB" runat="server" Font-Size="7pt" Font-Italic="True"
                                ForeColor="#0066FF" Width="200px"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtDescTIB" runat="server" TextMode="MultiLine" Height="50px" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">&nbsp;</div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:Label ID="lblNote" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666"
                                Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="button-group">
                    <asp:Button ID="btnTIBNext0" CssClass="btn btn-warning" runat="server" Text='<%$ Resources:labels, quaylai %>'
                        OnClick="btnTIBNext0_Click" />
                    <asp:Button ID="btnTIBNext" CssClass="btn btn-primary" runat="server" OnClientClick="return validate2();" Text='<%$ Resources:labels, tieptuc %>'
                        OnClick="btnTIBNext_Click" />
                </div>
            </figure>

        </asp:Panel>
        <asp:Panel ID="pnTBAC" runat="server" class="divcontent">
            <figure>
                <div class="handle">
                    <label class="bold"><%=Resources.labels.thongtinnguoitratien %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:DropDownList ID="ddlSenderAccountBAC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSenderAccountBAC_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </figure>
            <figure>
                <div class="handle">
                    <label class="bold"><%=Resources.labels.thongtinnguoinhantien %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label45" runat="server" Text='<%$ Resources:labels, taikhoanbaoco %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:DropDownList ID="ddlReceiverAccountBAC" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </figure>
            <figure>
                <div class="handle">
                    <label class="bold"><%=Resources.labels.noidungchuyenkhoan %></label>
                </div>
                <div class="content_table">
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label48" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtAmountBAC" CssClass="amount" runat="server" MaxLength="21"></asp:TextBox>
                            &nbsp;<asp:Label ID="lbCCYIDBAC" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">&nbsp;</div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:Label ID="lblTextBAC" runat="server" Font-Size="7pt" Font-Italic="True" ForeColor="#0066FF"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">
                            <asp:Label ID="Label52" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>&nbsp;*
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtDescBAC" runat="server" TextMode="MultiLine" Width="300px" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-5 col-sm-4 right">&nbsp;</div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:Label ID="Label10" runat="server" Font-Italic="True" Font-Size="9pt" ForeColor="#666666" Text='<%$ Resources:labels, ghichunoidung %>'></asp:Label>
                        </div>
                    </div>
                </div>
            </figure>
            <div class="button-group">
                <asp:Button ID="Button9" runat="server" CssClass="btn btn-warning" Text='<%$ Resources:labels, quaylai %>' OnClick="Button9_Click" />
                <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" OnClientClick="return validate1();" Text='<%$ Resources:labels, tieptuc %>'
                    OnClick="btnTIBNext_Click" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnConfirm" runat="server" class="divcontent">
            <div class="handle">
                <asp:Label CssClass="bold" ID="Label377" runat="server" Text='<%$ Resources:labels, thongtinmauchuyenkhoan %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label378" runat="server" Text='<%$ Resources:labels, tenmau %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbtenmau" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label379" runat="server" Text='<%$ Resources:labels, loaichuyenkhoan %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbhinhthuc" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <asp:Label CssClass="bold" ID="Label380" runat="server" Text='<%$ Resources:labels, thongtinnguoitratien %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, hotennguoitratien %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbNCD" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label381" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbTKCD" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <asp:Label CssClass="bold" ID="Label382" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien %>'></asp:Label>
            </div>
            <div class="content_table" id="PanelCFTIB" runat="server">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label383" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbNN_TIB" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label384" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbTKD_TIB" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="content_table" id="PanelCFBAC" runat="server">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label392" runat="server" Text="<%$ Resources:labels, hotennguoinhantien %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbNN_BAC" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label393" runat="server" Text="<%$ Resources:labels, taikhoanbaoco %>"></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbTKD_BAC" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="handle">
                <asp:Label CssClass="bold" ID="Label394" runat="server" Text='<%$ Resources:labels, noidungchuyenkhoan %>'></asp:Label>
            </div>
            <div class="content_table">
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label395" runat="server" Text='<%$ Resources:labels, sotien %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbsotien" runat="server" Text=""></asp:Label>
                        &nbsp;
                        <asp:Label ID="lbCCYID" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-6 right">
                        <asp:Label ID="Label397" runat="server" Text='<%$ Resources:labels, noidungthanhtoan %>'></asp:Label>
                    </div>
                    <div class="col-xs-7 col-sm-6 line30">
                        <asp:Label ID="lbnoidung" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="Button3" CssClass="btn btn-warning" runat="server" Text='<%$ Resources:labels, quaylai %>' OnClick="Button3_Click" />
                <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, hoanthanh %>'
                    OnClick="Button2_Click" />
                <div class="clearfix"></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnResultTransaction" runat="server" class="divcontent">
            <div class="header-title">
                <asp:Label CssClass="bold" ID="Label8" runat="server" Text='<%$ Resources:labels, ketquagiaodich %>'></asp:Label>
            </div>
            <div class="content">
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <div style="text-align: center; padding-bottom: 10px; padding-top: 10px; font-weight: bold;">
                            <asp:Label ID="Label58" runat="server" ForeColor="Red" Text="<%$ Resources:labels, taomaumoithanhcong %>"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnExit" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, thoat %>" OnClick="btnExit_Click" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="JS/docso.js" type="text/javascript"></script>
<script type="text/javascript">
    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""), '<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";
    }
    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);


        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }

        return str;
    }
    function validate() {
        if (!validateEmpty('<%=txtTemplateName.ClientID %>','<%=Resources.labels.dattenchomau %>')) {
            return false;
        }
        return true;
    }

    function validate1() {
        if (!validateSameAccount('<%=ddlSenderAccountBAC.ClientID%>', '<%=ddlReceiverAccountBAC.ClientID%>', '<%=Resources.labels.Accountnotsame%>')) {
            return false;
        }
        if (!validateMoney('<%=txtAmountBAC.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
            return false;
        }
        if (!validateEmpty('<%=txtDescBAC.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {
            return false;
        }
        return true;
    }

    function validate2() {
        if (!validateEmpty('<%=txtRecieverAccountTIB.ClientID %>', '<%=Resources.labels.bancannhaptaikhoannguoinhan %>')) {
            return false;
        }
        if (!validateSameAccount('<%=ddlSenderAccountTIB.ClientID%>', '<%=txtRecieverAccountTIB.ClientID%>', '<%=Resources.labels.Accountnotsame%>')) {
            return false;
        }
        if (!validateMoney('<%=txtAmountTIB.ClientID %>', '<%=Resources.labels.bancannhapsotien %>')) {
            return false;
        }
        if (!validateEmpty('<%=txtDescTIB.ClientID %>', '<%=Resources.labels.bancannhapnoidung %>')) {
            return false;
        }
        return true;
    }
</script>
