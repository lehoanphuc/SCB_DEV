<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSInterFeeManagement_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:DropDownList ID="ddlRegion" CssClass="hidden" runat="server" />
        <asp:DropDownList ID="ddlFeeside" CssClass="hidden" runat="server">
            <asp:ListItem Value="SENDER" Text="<%$ Resources:labels, sender %>"></asp:ListItem>
            <asp:ListItem Value="RECEIVER" Text="<%$ Resources:labels, receiver %>"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnFeeSide" CssClass="hidden" OnClick="btnFeeSide_OnClick" runat="server" />
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
                            <div class="row">
                                <div class="col-sm-5 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.otherbank %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlBank" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 col-xs-12"></div>
                            </div>
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
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a class="tabSender"><%=Resources.labels.sender %></a>
                        </li>
                        <li>
                            <a class="tabReceiver"><%=Resources.labels.receiver %></a>
                        </li>
                    </ul>
                    <div class="tab-content form-horizontal">
                        <div class="row">
                            <div class="col-sm-5 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.debittype %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlDebitType" CssClass="form-control select2 infinity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDebitType_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.credittype %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlCreditType" CssClass="form-control select2 infinity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCreditType_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClientClick="Loading();" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-5 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.abankregion %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlDebitRegion" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.creditregion %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlCreditRegion" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="pnDetailsFee" runat="server">
                            <div class="row">
                                <div class="col-sm-5 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.servicefee %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtServiceFee" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 col-xs-12"></div>
                            </div>

                            <div class="row">
                                <div class="col-sm-5 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.sotien %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtAmount" CssClass="form-control" MaxLength="22" runat="server"></asp:TextBox>
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
                                                <asp:CheckBox ID="cbToLimit" OnCheckedChanged="cbToLimit_OnCheckedChanged" AutoPostBack="True" Text="<%$ Resources:labels, unlimit %>" runat="server" />
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
                            <div class="row">
                                <div class="col-sm-12 col-xs-12">
                                    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                        <asp:Button ID="btnSaveDetails" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, them %>" OnClick="btnSaveDetails_Click" OnClientClick="return validate();" />
                                        <asp:Button ID="btnDelete" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>


        <asp:Panel ID="pnGV" runat="server" Visible="False">
            <asp:GridView ID="gvFeeInterDetails" CssClass="table table-hover" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvFeeInterDetails_RowDataBound" PageSize="15"
                OnPageIndexChanging="gvFeeInterDetails_PageIndexChanging"
                OnRowDeleting="gvFeeInterDetails_RowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FEEID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblFeeId" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FkID" Visible="False">
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
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, debitregion %>">
                        <ItemTemplate>
                            <asp:Label ID="lbldebitregion" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, creditregion %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcreditregion" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, feeside %>">
                        <ItemTemplate>
                            <asp:Label ID="lblfeeside" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, BankName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblBankName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, servicefee %>">
                        <ItemTemplate>
                            <asp:Label ID="lblServiceFee" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
        </asp:Panel>
        <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
        <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function validate() {
        if (!validateEmpty('<%=txtServiceFee.ClientID %>', '<%=Resources.labels.servicefeekhongrong %>')) {
            document.getElementById('<%=txtServiceFee.ClientID %>').focus();
            return false;
        }
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
        var toLimit = document.getElementById('<%=cbToLimit.ClientID %>').checked;
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
        return true;
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

    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvFeeInterDetails.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvFeeInterDetails.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvFeeInterDetails_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvFeeInterDetails_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = false;
                    if (Counter > 0)
                        Counter--;
                }
            }
        }
        hdf.value = Counter.toString();
    }

    var TotalChkBx;
    var Counter;

    window.onload = function () {
        document.getElementById('<%=hdCounter.ClientID %>').value = '0';
    }

    function ChildClick(CheckBox) {
        Counter = parseInt(document.getElementById('<%=hdCounter.ClientID %>').value);
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);

        var grid = document.getElementById('<%= gvFeeInterDetails.ClientID %>');
        var cbHeader = grid.rows[0].cells[0].childNodes[0];

        if (CheckBox.checked)
            Counter++;
        else if (Counter > 0)
            Counter--;

        if (Counter < TotalChkBx)
            cbHeader.checked = false;
        else if (Counter == TotalChkBx)
            cbHeader.checked = true;
        document.getElementById('<%=hdCounter.ClientID %>').value = Counter.toString();
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function ConfirmDelete2() {
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
        }
    }

    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }

    $(document).ready(function () {
        $('.nav-tabs a.tabSender').on('click', function (e) {
            document.getElementById('<%=ddlFeeside.ClientID%>').value = 'SENDER';
            e.preventDefault();
            $(this).tab('show');
            document.getElementById("<%= btnFeeSide.ClientID %>").click();
        });
        $('.nav-tabs a.tabReceiver').on('click', function (e) {
            document.getElementById('<%=ddlFeeside.ClientID%>').value = 'RECEIVER';
            e.preventDefault();
            $(this).tab('show');
            document.getElementById("<%= btnFeeSide.ClientID %>").click();
        });

        if (document.getElementById('<%=ddlFeeside.ClientID%>').value == 'RECEIVER') {
            $('.nav-tabs a.tabReceiver').tab('show');
        } else {
            $('.nav-tabs a.tabSender').tab('show');
        }
    });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                $('.nav-tabs a.tabSender').on('click', function (e) {
                    document.getElementById('<%=ddlFeeside.ClientID%>').value = 'SENDER';
                    e.preventDefault();
                    $(this).tab('show');
                    document.getElementById("<%= btnFeeSide.ClientID %>").click();
                });
                $('.nav-tabs a.tabReceiver').on('click', function (e) {
                    document.getElementById('<%=ddlFeeside.ClientID%>').value = 'RECEIVER';
                    e.preventDefault();
                    $(this).tab('show');
                    document.getElementById("<%= btnFeeSide.ClientID %>").click();
                });
                if (document.getElementById('<%=ddlFeeside.ClientID%>').value == 'RECEIVER') {
                    $('.nav-tabs a.tabReceiver').tab('show');
                } else {
                    $('.nav-tabs a.tabSender').tab('show');
                }
            }
        });
    };
</script>
