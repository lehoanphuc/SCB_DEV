<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFeeManagement_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <asp:Panel ID="pnFee" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.thongtinphi%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaiphi %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlType" CssClass="form-control select2 infinity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeType_OnChange">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group" id="PnTran" runat="server" visible="false">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.giaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlTrans" Enabled="false" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                            <div class="form-group" id="divAdd" runat="server" visible="true">
                                            <div class="col-sm-12 col-xs-12">
                                                <asp:Button ID="btnAddTran" runat="server" Text="<%$ Resources:labels, them %>" CssClass="btn btn-primary" OnClick="btnAddTran_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tenphi %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFeeName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaitien %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Label ID="lbIDGen" runat="server" Visible="false" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnTranfer" runat="server">
            <div id="divTranfer">
                <asp:GridView ID="gvTranfer" CssClass="table table-hover" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvTranfer_RowDataBound" PageSize="15"
                    OnPageIndexChanging="gvTranfer_PageIndexChanging"
                    OnRowDeleting="gvTranfer_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="TranCode" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblTrancode" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FeeID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFeeID" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transaction">
                            <ItemTemplate>
                                <asp:Label ID="lblTransaction" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fee Type">
                            <ItemTemplate>
                                <asp:Label ID="lblFeeType" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Currency">
                            <ItemTemplate>
                                <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                </asp:GridView>
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </div>
        </asp:Panel>

        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.chitietphi%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnDetailsFee" runat="server" DefaultButton="btsave">
                                <div class="row" id="trBiller" runat="server">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.billername %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlBiller" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7 col-xs-12">
                                    </div>
                                </div>
                                <div class="row" runat="server" id="PnAmount">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.sotien %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" CssClass="form-control" MaxLength="22"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.phibacthang %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:CheckBox ID="cbIsLadder" runat="server" OnCheckedChanged="cbIsLadder_CheckedChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-12 col-xs-12">
                                                <asp:Button ID="btnSaveDetailsrg" runat="server" Text="<%$ Resources:labels, them %>" CssClass="btn btn-primary" OnClick="btnSaveDetails_Click" OnClientClick="return validate();" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="tbLadderFee" runat="server">
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tu %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtFrom" Text="0" CssClass="form-control" MaxLength="21" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.den %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtTo" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <div class="form-group">
                                                <div class="col-sm-12 col-xs-12">
                                                    <asp:CheckBox ID="cbToLimit" OnCheckedChanged="cbToLimit_OnCheckedChanged" AutoPostBack="True" runat="server" />
                                                    <%=Resources.labels.unlimit %>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.phitoithieu %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtMin" CssClass="form-control" MaxLength="22" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.phitoida %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtmax" CssClass="form-control" MaxLength="22" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <div class="form-group">
                                                <div class="col-sm-12 col-xs-12">
                                                    <asp:Button ID="btnSaveDetails" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, them %>" Visible="false" OnClick="btnSaveDetails_Click" OnClientClick="return validate();" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tinhphi %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtRate" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-7 col-xs-12"></div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading();return validate1();" OnClick="btsave_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" OnClientClick="Loading();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnGV" runat="server" Visible="False">
            <div id="divResult">
                <asp:GridView ID="gvAppTransDetailsList" CssClass="table table-hover" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvAppTransDetailsList_RowDataBound" PageSize="15"
                    OnPageIndexChanging="gvAppTransDetailsList_PageIndexChanging"
                    OnRowDeleting="gvAppTransDetailsList_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="FEEID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFeeId" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FkID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFkID" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tu %>">
                            <ItemTemplate>
                                <asp:Label ID="lblfrom" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, toequal %>">
                            <ItemTemplate>
                                <asp:Label ID="lblto" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, phitoithieu %>">
                            <ItemTemplate>
                                <asp:Label ID="lblmin" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, phitoida %>">
                            <ItemTemplate>
                                <asp:Label ID="lblmax" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tinhphi %>">
                            <ItemTemplate>
                                <asp:Label ID="lblRate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, fixedfee %>">
                            <ItemTemplate>
                                <asp:Label ID="lblfixedfee" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, phibacthang %>">
                            <ItemTemplate>
                                <asp:Label ID="lblisladder" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, billername %>">
                            <ItemTemplate>
                                <asp:Label ID="lblBillerName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                </asp:GridView>
                <asp:Literal ID="litPager" runat="server"></asp:Literal>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnWarning" runat="server">
            <div class="row" style="padding-top: 20px;">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.warningbillerfee%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <asp:Label runat="server" ID="lblBillerNameFee" Style="color: darkred"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script src="Widgets/SEMSFeeManagement/JS/mask.js" type="text/javascript"></script>
<script type="text/javascript">
    function validate() {
        var toLimit = document.getElementById('<%=cbToLimit.ClientID %>').checked;

        if (document.getElementById("<%=txtAmount.ClientID %>").disabled == false) {
            if (!validateEmpty('<%=txtAmount.ClientID %>', '<%=Resources.labels.tienphikhongrong %>')) {
                document.getElementById('<%=txtAmount.ClientID %>').focus();
                return false;
            }
        }
        if (!validateEmpty('<%=txtFrom.ClientID %>', '<%=Resources.labels.sotienkhoidiemkhongrong %>')) {
            document.getElementById('<%=txtFrom.ClientID %>').focus();
            return false;
        }
        if (!toLimit) {
            if (!validateEmpty('<%=txtTo.ClientID %>', '<%=Resources.labels.sotienketthuckhogrong %> ')) {
                document.getElementById('<%=txtTo.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtTo.ClientID %>').value.trim() == "0") {
                alert('<%=Resources.labels.sotienketthucinvalid %>');
                document.getElementById('<%=txtTo.ClientID %>').focus();
                return false;
            }
            if (!validateFormTo('<%=txtFrom.ClientID %>', '<%=txtTo.ClientID %>', '<%=Resources.labels.sotienketthucphailonhonsotienkhoidiem %>')) {
                document.getElementById('<%=txtFrom.ClientID %>').focus();
                return false;
            }
        }
        if (!validateEmpty('<%=txtMin.ClientID %>', '<%=Resources.labels.phitoithieukhongrong %>')) {
            document.getElementById('<%=txtMin.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtmax.ClientID %>', '<%=Resources.labels.phitoidakhongrong %> ')) {
            document.getElementById('<%=txtmax.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtmax.ClientID %>').value.trim() == "0") {
            alert('<%=Resources.labels.phitoidainvalid %>');
            document.getElementById('<%=txtmax.ClientID %>').focus();
            return false;
        }
        if (!validateFormTo('<%=txtMin.ClientID %>', '<%=txtmax.ClientID %>', '<%=Resources.labels.phitoidaphailonhonphitoithieu %>')) {
            document.getElementById('<%=txtMin.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtRate.ClientID %>', '<%=Resources.labels.phantramphikhongrong %>')) {
            document.getElementById('<%=txtRate.ClientID %>').focus();
            return false;
        }
        return true;
    }

    function validate1() {
        if (!validateEmpty('<%=txtFeeName.ClientID %>', '<%=Resources.labels.tenphikhongrong %>')) {
            document.getElementById('<%=txtFeeName.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=ddlType.ClientID %>').value.trim() != 'BPM') {
            if (document.getElementById("<%=txtAmount.ClientID %>").disabled == false) {
                if (!validateEmpty('<%=txtAmount.ClientID %>', '<%=Resources.labels.tienphikhongrong %>')) {
                    document.getElementById('<%=txtAmount.ClientID %>').focus();
                    return false;
                }
            }
        }
        return true;
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }
    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }
    }
    function isNumberK(evt) {
        if ('<%= allowNegativeFee %>' == 'False') {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31 && (charCode < 45 || charCode > 57))
                return false;
            var charStr = document.getElementById(evt.target.id).value.replace(',', '');
            if (charCode == 45 && charStr.length > 0) {
                document.getElementById(evt.target.id).focus();
                return false;
            }
        }
        var so = document.getElementById(evt.target.id).value;
        so = so.toString().replace(/\$|\,/g, '');
        if (so != "" && so != "-") {
            so = Math.floor(so * 100 + 0.50000000001);
            so = Math.floor(so / 100).toString();
            if (Number(so) < 0) {
                for (var i = 0; i < Math.floor((so.length - 1 - (1 + i)) / 3); i++) {
                    so = so.substring(0, so.length - (4 * i + 3)) + ',' +
                        so.substring(so.length - (4 * i + 3));
                }
            }
            else {
                document.getElementById(evt.target.id).setAttribute("MaxLength", "21");
                for (var i = 0; i < Math.floor((so.length - (1 + i)) / 3); i++) {
                    if (Math.floor((so.length - (1 + i)) / 3) <= 5) {
                        so = so.substring(0, so.length - (4 * i + 3)) + ',' +
                            so.substring(so.length - (4 * i + 3));
                    }
                }
            }
        }
        document.getElementById(evt.target.id).value = so

    }
</script>
