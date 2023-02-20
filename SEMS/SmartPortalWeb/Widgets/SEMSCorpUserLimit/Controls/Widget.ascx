<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCorpUserLimit_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button Visible="false" ID="btnOverwrite" CssClass="btn btn-primary" runat="server" Text="Overwrite Transaction"
                    OnClick="btnOverwrite_Click" />
                <asp:Button Visible="false" ID="btnAddMissTrans" runat="server" CssClass="btn btn-primary" Text="Add miss Transaction"
                    OnClientClick="return validate();" OnClick="btnAddMissTrans_Click" />
                <asp:Button Visible="false" ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel"
                    OnClick="btnCancel_Click" />
            </div>
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
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.userid %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtUserID" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.fullname %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtFullName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mahopdong %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtContractNo" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
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
                <asp:GridView ID="gvUser" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" PageSize="15">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="rbUserID" AutoPostBack="True" OnCheckedChanged="rbUserID_onChange" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, userid %>">
                            <ItemTemplate>
                                <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("USERID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, username %>">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, fullname %>">
                            <ItemTemplate>
                                <asp:Label ID="lblFullname" runat="server" Text='<%# Eval("FULLNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>">
                            <ItemTemplate>
                                <asp:Label ID="lblContractNo" runat="server" Text='<%# Eval("CONTRACTNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle />
                </asp:GridView>
                <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
                <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
                <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnLimt">
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
                                                        <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.userid %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtUserIDStep2" CssClass="form-control" Enabled="False" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.loaigiaodich %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlTransType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.hanmucmotgiaodich %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtLimitPerTrans" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tonghanmucngay %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtLimitPerDay" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sogiaodichtrenngay %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:TextBox ID="txtTotalTransPerDay" CssClass="form-control" MaxLength="11" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-xs-12">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tiente %></label>
                                                        <div class="col-sm-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlCcyid" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-xs-12">
                                            <div class="form-group">
                                                <div class="col-sm-12 col-xs-12">
                                                    <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:labels, them %>" CssClass="btn btn-primary" OnClick="btnAdd_Click" OnClientClick="return validate();" />
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
                <asp:GridView ID="gvUserLimit" CssClass="table table-hover" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" OnRowDataBound="gvUserLimit_RowDataBound"
                    OnPageIndexChanging="gvUserLimit_PageIndexChanging" OnRowDeleting="gvUserLimit_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, transaction %>">
                            <ItemTemplate>
                                <asp:Label ID="lblTransaction" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, userid %>">
                            <ItemTemplate>
                                <asp:Label ID="lblUserID" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, fullname %>">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, approvallimit %>">
                            <ItemTemplate>
                                <asp:Label ID="lblTransLimit" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, approvaltotallimitperday %>">
                            <ItemTemplate>
                                <asp:Label ID="lblTransLimitPerDay" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, approvaltransperday %>">
                            <ItemTemplate>
                                <asp:Label ID="lblTransPerDay" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tiente %>">
                            <ItemTemplate>
                                <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
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
        </asp:Panel>

        <div class="panel-content div-btn border-left-0 border-right-0 border-bottom-0 text-muted">
            <asp:Button ID="btnNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="Loading();" OnClick="btnNext_OnClick" />
            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading();" OnClick="btnSave_Click" />
            <asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:return poponload()" Visible="false" Text="<%$ Resources:labels, inketqua %>" />
            <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading()" OnClick="btnBack_OnClick" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">

    function poponload() {
        testwindow = window.open("widgets/SEMSCorpUserLimit/print.aspx", "UserLimit",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }
    function validate() {
        if (!validateEmpty('<%=txtLimitPerTrans.ClientID %>', '<%=Resources.labels.limitpertranscannotbeempty %>')) {
            document.getElementById('<%=txtLimitPerTrans.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtLimitPerDay.ClientID %>', '<%=Resources.labels.limitperdaycannotbeempty %>')) {
            document.getElementById('<%=txtLimitPerDay.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtTotalTransPerDay.ClientID %>', '<%=Resources.labels.transperdaycannotbeempty %>')) {
            document.getElementById('<%=txtTotalTransPerDay.ClientID %>').focus();
            return false;
        }
        return true;
    }

</script>
