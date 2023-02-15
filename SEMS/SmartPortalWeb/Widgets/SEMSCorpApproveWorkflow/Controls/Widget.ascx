<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCorpApproveWorkflow_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleGroup" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>

        <asp:Panel ID="pnSeachContract" runat="server" DefaultButton="btnSearch">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.contractno %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtSearchContractNo" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.fullname %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtSearchFullname" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.cmndgpkd %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtlicenseid" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.socifcorebanking %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtcustcode" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClientClick="Loading();" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divResult" runat="server">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:GridView ID="gvContractSearch" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" PageSize="15">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="colRbContractNo" runat="server" AutoPostBack="true" OnCheckedChanged="colRbContractNo_onChange" GroupName="Contract" ValidationGroup="Contract" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, contractno %>">
                            <ItemTemplate>
                                <asp:Label ID="colContractNo" runat="server" Text='<%# Eval("ContractNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, fullname %>">
                            <ItemTemplate>
                                <asp:Label ID="colFullname" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle />
                </asp:GridView>
                <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
                <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
                <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnWorkflow">
            <asp:Panel ID="pnAdd" runat="server">
                <div class="row">
                    <div class="col-sm-12 col-xs-12">
                        <div class="panel">
                            <div class="panel-hdr">
                                <h2>
                                    <%=Resources.labels.thongtinduyetgiaodich%>
                                </h2>
                            </div>
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <div class="row">
                                        <div class="col-sm-12 col-xs-12">
                                            <div class="row">
                                                <div class="col-sm-5 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.transaction %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlTransaction" CssClass="form-control select2" Width="100%" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.accountnumber %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlAccNumber" CssClass="form-control select2" Width="100%" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-xs-12"></div>
                                            </div>
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
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.currency %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlCurrency" CssClass="form-control select2" Width="100%" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.allowapproveowntransaction %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:CheckBox runat="server" ID="cbIsAOT" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 col-xs-12"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.transactionneedtoapprove %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:CheckBox runat="server" ID="cbNeedApprove" Checked="true" Text="" AutoPostBack="true" OnCheckedChanged="cbNeedApprove_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-7 col-xs-12"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnFormula" runat="server">
                <asp:GridView ID="gvUserGroup" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" Width="100%" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" OnPageIndexChanging="gvUserGroup_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, bac %>" Visible="false" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="colLevelShortName" runat="server"></asp:Label>
                                <asp:HiddenField ID="colUserLevel" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, groupid %>" ItemStyle-Width="90px">
                            <ItemTemplate>
                                <asp:Label ID="colGroupID" runat="server" Text='<%# Eval("GroupID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, groupname %>">
                            <ItemTemplate>
                                <asp:Label ID="colGroupShortName" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gvFooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                    <SelectedRowStyle />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:GridView>
                <br />
            </asp:Panel>

            <asp:Panel ID="pnApproval" runat="server">
                <div class="row">
                    <div class="col-sm-12 col-xs-12">
                        <div class="panel">
                            <div class="panel-hdr">
                                <h2>
                                    <%=Resources.labels.quitrinhduyet%>
                                </h2>
                            </div>
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <div class="row">
                                        <div class="col-sm-10 col-xs-12">
                                            <div class="row">
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.approveformula %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtFormula" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.desc %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtOrderDesc" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <div class="form-group">
                                                <div class="col-sm-12 col-xs-12">
                                                    <asp:Button ID="btnAddOrder" runat="server" Text="<%$ Resources:labels, them %>" CssClass="btn btn-primary" OnClick="btnAddOrder_onclick" OnClientClick="return validateformula();" />
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

            <asp:Panel ID="pnTransaction" runat="server">
                <asp:GridView ID="gvTransaction" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" Width="100%" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" OnPageIndexChanging="gvTransaction_PageIndexChanging" OnRowDataBound="gvTransaction_onRowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Order">
                            <ItemTemplate>
                                <asp:Label ID="colOrd" runat="server" Text='<%#Eval("Ord") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, approveformula %>">
                            <ItemTemplate>
                                <asp:Label ID="colApproveFormula" runat="server" Text='<%#Eval("Formula") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, desc %>">
                            <ItemTemplate>
                                <asp:Label ID="colDesc" runat="server" Text='<%#Eval("Desc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, sort %>" ItemStyle-Width="15px">
                            <ItemTemplate>
                                <asp:LinkButton ID="colUpArrow" runat="server" CommandArgument='<%# Eval("Ord") %>' OnClick="colUpArrow_onclick">
                                    <i class="fa fa-chevron-up"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="colDownArrow" runat="server" CommandArgument='<%# Eval("Ord") %>' OnClick="colDownArrow_onclick"> 
                                    <i class="fa fa-chevron-down"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="colTransactionDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("Ord") %>' OnClick="colTransactionDelete_onclick" OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gvFooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                    <SelectedRowStyle />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:GridView>
                <asp:Literal ID="litPager" runat="server"></asp:Literal>
            </asp:Panel>
        </asp:Panel>

        <div class="panel-content div-btn border-left-0 border-right-0 border-bottom-0 text-muted">
            <asp:Button ID="btnNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="Loading();" OnClick="btnNext_OnClick" />
            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading(); return validate();" OnClick="btnSave_Click" />
            <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading()" OnClick="btnBack_OnClick" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }
    function validateformula() {
        if (!validateEmpty('<%=txtFormula.ClientID %>', '<%=Resources.labels.pleaseinputapproveformula %>')) {
            document.getElementById('<%=txtFormula.ClientID %>').focus();
            return false;
        }
        return true;
    }
    function validate() {
        var toLimit = document.getElementById('<%=cbToLimit.ClientID %>').checked;

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
        return true;
    }
</script>
