<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCorpApproveStructure_Controls_Widget" %>
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

        <asp:Panel runat="server" ID="pnGroup">
            <asp:Panel ID="pnAdd" runat="server">
                <div class="row">
                    <div class="col-sm-12 col-xs-12">
                        <div class="panel">
                            <div class="panel-hdr">
                                <h2>
                                    <%=Resources.labels.grouptab%>
                                </h2>
                            </div>
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <div class="row">
                                        <div class="col-sm-10 col-xs-12">
                                            <div class="row">
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.groupcode %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlGroup" CssClass="form-control select2" Width="100%" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.groupname %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtNameGroup" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.groupshortname %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtShortNameGroup" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.desc %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtDescGroup" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <div class="form-group">
                                                <div class="col-sm-12 col-xs-12">
                                                    <asp:Button ID="btnAddGroup" runat="server" Text="<%$ Resources:labels, them %>" CssClass="btn btn-primary" OnClick="btnAddGroup_Click" OnClientClick="return validate();" />
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
            <asp:Panel ID="pnGV" runat="server" Visible="False">

                <asp:GridView ID="gvGroup" CssClass="table table-hover" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False" PageSize="15"
                    OnPageIndexChanging="gvGroup_PageIndexChanging" OnRowDeleting="gvGroup_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, groupid %>">
                            <ItemTemplate>
                                <asp:Label ID="colGroupID" Text='<%# Eval("GroupID") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, groupname %>">
                            <ItemTemplate>
                                <asp:Label ID="colGroupName" Text='<%# Eval("GroupName") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, groupshortname %>">
                            <ItemTemplate>
                                <asp:Label ID="colGroupShortName" Text='<%# Eval("GroupShortName") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, desc %>">
                            <ItemTemplate>
                                <asp:Label ID="colGroupDesc" Text='<%# Eval("GroupDesc") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbDelete" CommandArgument='<%# Eval("GroupID") %>' runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                </asp:GridView>
                <asp:Literal ID="litPager" runat="server"></asp:Literal>
            </asp:Panel>
        </asp:Panel>

        <div class="panel-content div-btn border-left-0 border-right-0 border-bottom-0 text-muted">
            <asp:Button ID="btnNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="Loading();" OnClick="btnNext_OnClick" />
            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading();" OnClick="btnSave_Click" />
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
    function validate() {
        if (!validateEmpty('<%=txtNameGroup.ClientID %>', '<%=Resources.labels.pleaseinputgroupname %>')) {
            document.getElementById('<%=txtNameGroup.ClientID %>').focus();
            return false;
        }
        return true;
    }

</script>
