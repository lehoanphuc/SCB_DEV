<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSSwiftFeeManagement_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.setswiftfee %>
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
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.senderccy %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2 infinity" runat="server" OnSelectedIndexChanged="ddlCCYID_OnSelectedIndexChanged"  AutoPostBack="True" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.nguoitraphi %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlPayer" CssClass="form-control select2" runat="server">
                                                    <asp:ListItem Value="OUR" Text="CODE OUR"></asp:ListItem>
                                                    <asp:ListItem Value="SHA" Text="SHA"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaiphi %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlFeetype" CssClass="form-control select2" runat="server">
                                                    <asp:ListItem Value="" Text="All"></asp:ListItem>
                                                    <asp:ListItem Value="SWIFTFEE" Text="Swift fee"></asp:ListItem>
                                                    <asp:ListItem Value="OTHERBANKFEE" Text="Other bank fee"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.BankName %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlBank" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div>
                                            <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" hidden>
                                     <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.BankName %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlBankSWCode" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnFeeDetails" runat="server">
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
                                <div class="subheader">
                                    <h1 class="subheader-title">
                                        <%=Resources.labels.swiftfee %>
                                    </h1>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.sotien %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtAmountSW" CssClass="form-control" MaxLength="22" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.phibacthang %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:CheckBox ID="cbIsLadderSW" runat="server" OnCheckedChanged="cbIsLadderSW_CheckedChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div>
                                            <asp:Button ID="btnSaveDetailsSW" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, them %>" OnClientClick="return validateSW();" OnClick="btnSaveDetailsSW_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div id="tbLadderFeeSwift" visible="false" runat="server">
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tu %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtFromSW" Text="0" CssClass="form-control" MaxLength="21" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.den %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtToSW" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <div class="form-group">
                                                <div class="col-sm-12 col-xs-12">
                                                    <asp:CheckBox ID="cbToLimitSW" OnCheckedChanged="cbToLimitSW_OnCheckedChanged" AutoPostBack="True" Text="<%$ Resources:labels, unlimit %>" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.phitoithieu %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtMinSW" CssClass="form-control" MaxLength="22" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.phitoida %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtMaxSW" CssClass="form-control" MaxLength="22" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tinhphi %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtRateSW" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-xs-12"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <div class="subheader">
                                        <h1 class="subheader-title">
                                            <%=Resources.labels.otherbankfee %>
                                        </h1>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.sotien %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtAmountOTHB" CssClass="form-control" MaxLength="22" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.phibacthang %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:CheckBox ID="cbIsLadderOTHB" runat="server" OnCheckedChanged="cbIsLadderOTHB_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-2 col-xs-12">
                                            <div>
                                                <asp:Button ID="btnSaveDetailsOTHB" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, them %>" OnClick="btnSaveDetailsOTHB_Click" OnClientClick="return validateOTHB();" />
                                            </div>
                                        </div>

                                    </div>
                                    <div id="tbLadderFeeOtherbank" visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-sm-5 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tu %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtFromOTHB" Text="0" CssClass="form-control" MaxLength="21" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.den %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtToOTHB" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <div class="form-group">
                                                    <div class="col-sm-12 col-xs-12">
                                                        <asp:CheckBox ID="cbToLimitOTHB" OnCheckedChanged="cbToLimitOTHB_OnCheckedChanged" AutoPostBack="True" Text="<%$ Resources:labels, unlimit %>" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.phitoithieu %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtMinOTHB" CssClass="form-control" MaxLength="22" runat="server" Text="0"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.phitoida %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtMaxOTHB" CssClass="form-control" MaxLength="22" runat="server" Text="0"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tinhphi %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtRateOTHB" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5 col-xs-12"></div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="panel">
            <div class="panel-container">
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading();return validate();" OnClick="btsave_Click" />
                    <asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                    <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>"
                        OnClick="btback_Click" />
                </div>
            </div>
        </div>
        <asp:Panel ID="pnGV" runat="server" Visible="False">
            <asp:GridView ID="gvFeeSwiftDetails" CssClass="table table-hover" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvFeeSwiftDetails_RowDataBound" PageSize="15"
                OnPageIndexChanging="gvFeeSwiftDetails_PageIndexChanging"
                OnRowDeleting="gvFeeSwiftDetails_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, loaiphi %>">
                        <ItemTemplate>
                            <asp:Label ID="lblfeetype" runat="server"></asp:Label>
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
                    <asp:TemplateField HeaderText="<%$ Resources:labels, phibacthang %>">
                        <ItemTemplate>
                            <asp:Label ID="lblLadder" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, fixedfee %>">
                        <ItemTemplate>
                            <asp:Label ID="lblfixedfee" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, payer %>">
                        <ItemTemplate>
                            <asp:Label ID="lblfeepayer" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Currency">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, BankName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblBankName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, swiftcode %>">
                        <ItemTemplate>
                            <asp:Label ID="lblswcode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, huy %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbdelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' >  <%=Resources.labels.huy %></asp:LinkButton>
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
    function validateSW() {      
        if (document.getElementById("<%=txtAmountSW.ClientID %>").disabled == false) {
            if (!validateEmpty('<%=txtAmountSW.ClientID %>', '<%=Resources.labels.tienphikhongrong %>')) {
                document.getElementById('<%=txtAmountSW.ClientID %>').focus();
                return false;
            }
        }
        if (!validateEmpty('<%=txtFromSW.ClientID %>', '<%=Resources.labels.sotienkhoidiemkhongrong %>')) {
            document.getElementById('<%=txtFromSW.ClientID %>').focus();
            return false;
        }
        var toLimit = document.getElementById('<%=cbToLimitSW.ClientID %>').checked;
        if (!toLimit) {
            if (!validateEmpty('<%=txtToSW.ClientID %>', '<%=Resources.labels.sotienketthuckhogrong %> ')) {
                document.getElementById('<%=txtToSW.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtToSW.ClientID %>').value.trim() == "0") {
                alert('<%=Resources.labels.sotienketthucinvalid %>');
                document.getElementById('<%=txtToSW.ClientID %>').focus();
                return false;
            }
            if (!validateFormTo('<%=txtFromSW.ClientID %>', '<%=txtToSW.ClientID %>', '<%=Resources.labels.sotienketthucphailonhonsotienkhoidiem %>')) {
                document.getElementById('<%=txtFromSW.ClientID %>').focus();
                return false;
            }
        }
        if (!validateEmpty('<%=txtMinSW.ClientID %>', '<%=Resources.labels.phitoithieukhongrong %>')) {
            document.getElementById('<%=txtMinSW.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtMaxSW.ClientID %>', '<%=Resources.labels.phitoidakhongrong %> ')) {
            document.getElementById('<%=txtMaxSW.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtMaxSW.ClientID %>').value.trim() == "0") {
            alert('<%=Resources.labels.phitoidainvalid %>');
            document.getElementById('<%=txtMaxSW.ClientID %>').focus();
            return false;
        }
        if (!validateFormTo('<%=txtMinSW.ClientID %>', '<%=txtMaxSW.ClientID %>', '<%=Resources.labels.phitoidaphailonhonphitoithieu %>')) {
            document.getElementById('<%=txtMinSW.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtRateSW.ClientID %>', '<%=Resources.labels.phantramphikhongrong %>')) {
            document.getElementById('<%=txtRateSW.ClientID %>').focus();
            return false;
        }
        return true;
    }

    function validateOTHB() {
        if (document.getElementById("<%=txtAmountOTHB.ClientID %>").disabled == false) {
            if (!validateEmpty('<%=txtAmountOTHB.ClientID %>', '<%=Resources.labels.tienphikhongrong %>')) {
                document.getElementById('<%=txtAmountOTHB.ClientID %>').focus();
                return false;
            }
        }
        if (!validateEmpty('<%=txtFromOTHB.ClientID %>', '<%=Resources.labels.sotienkhoidiemkhongrong %>')) {
            document.getElementById('<%=txtFromOTHB.ClientID %>').focus();
            return false;
        }
        var toLimit = document.getElementById('<%=cbToLimitOTHB.ClientID %>').checked;
        if (!toLimit) {
            if (!validateEmpty('<%=txtToOTHB.ClientID %>', '<%=Resources.labels.sotienketthuckhogrong %> ')) {
                document.getElementById('<%=txtToOTHB.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtToOTHB.ClientID %>').value.trim() == "0") {
                alert('<%=Resources.labels.sotienketthucinvalid %>');
                document.getElementById('<%=txtToOTHB.ClientID %>').focus();
                return false;
            }
            if (!validateFormTo('<%=txtFromOTHB.ClientID %>', '<%=txtToOTHB.ClientID %>', '<%=Resources.labels.sotienketthucphailonhonsotienkhoidiem %>')) {
                document.getElementById('<%=txtFromOTHB.ClientID %>').focus();
                return false;
            }
        }
        if (!validateEmpty('<%=txtMinOTHB.ClientID %>', '<%=Resources.labels.phitoithieukhongrong %>')) {
            document.getElementById('<%=txtMinOTHB.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtMaxOTHB.ClientID %>', '<%=Resources.labels.phitoidakhongrong %> ')) {
            document.getElementById('<%=txtMaxOTHB.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=txtMaxOTHB.ClientID %>').value.trim() == "0") {
            alert('<%=Resources.labels.phitoidainvalid %>');
            document.getElementById('<%=txtMaxOTHB.ClientID %>').focus();
            return false;
        }
        if (!validateFormTo('<%=txtMinOTHB.ClientID %>', '<%=txtMaxOTHB.ClientID %>', '<%=Resources.labels.phitoidaphailonhonphitoithieu %>')) {
            document.getElementById('<%=txtMinOTHB.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtRateOTHB.ClientID %>', '<%=Resources.labels.phantramphikhongrong %>')) {
            document.getElementById('<%=txtRateOTHB.ClientID %>').focus();
            return false;
        }
        return true;
    }

    

    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
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

        function SelectCbx(obj) {
            Counter = 0;
            var hdf = documentClientIDentById("<%= hdCounter.ClientID %>");
            TotalChkBx = parseInt(document.getClientIDyId('<%=hdPageSize.ClientID %>').value);
            var count = document.getElementById('<%=gvFeeSwiftDetails.ClientID %>').rows.length;
            var elements = document.getElementById('<%=gvFeeSwiftDetails.ClientID %>').rows;
            if (obj.checked) {
                for (i = 0; i < count; i++) {
                    if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvFeeSwiftDetails_ctl01_cbxSelectAll') {
                        elements[i].cells[0].children[0].checked = true;
                        Counter++;
                    }
                }

            } else {
                for (i = 0; i < count; i++) {
                    if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvFeeSwiftDetails_ctl01_cbxSelectAll') {
                        elements[i].cells[0].children[0].checked = false;
                        if (Counter > 0)
                            Counter--;
                    }
                }
            }
            hdf.value = Counter.toString();
        }

        vaClientIDhkBx;
        var Counter;

        var TotalChkBx;
        var Counter;

        window.onload = function () {
            document.getElementById('<%=hdCounter.ClientID %>').value = '0';
        }

        function ChildClick(CheckBox) {
            Counter = parseInt(document.getElementById('<%=hdCounter.ClientID %>').value);
            TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);

            var grid = document.getElementById('<%= gvFeeSwiftDetails.ClientID %>');
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



</script>
