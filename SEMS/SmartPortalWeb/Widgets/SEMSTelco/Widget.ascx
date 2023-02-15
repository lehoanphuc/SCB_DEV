<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTelco_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.TELECOMMANAGEMENT %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                <div class="col-sm-10 col-xs-12">
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.TelcoName %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="txttelconame" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.shortname %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="txtShortName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.ELoadBillerCode %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="txtEloadBillerCode" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.EPinBillerCode %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="txtEPinBillerCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.SUNDRYACCTNOBANK %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="lblGLAccBalance" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                           <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.SUNDRYACCTNOWALLET %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="lblWlBalance" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row hidden">
                                             <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.INCOMEACCTNOBANK %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="lblGLAccFee" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.INCOMEACCTNOWALLET %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:TextBox ID="lblWlFee" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                                <div class="col-sm-7 col-xs-12">
                                                    <asp:DropDownList ID="txtstatus" Width="100%" CssClass="form-control select2 infinity" runat="server">
                                                        <asp:ListItem Value="A" Text="<%$ Resources:labels, active %>"></asp:ListItem>
                                                        <asp:ListItem Value="B" Text="<%$ Resources:labels, conblock %>"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <div class="col-sm-7 col-xs-12">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary mgb15"  runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                </div>
            </div>
        </div>
        <div id="divToolbar">
            <asp:Button ID="btnAddNew" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" OnClientClick="Loading();" />
            <asp:Button ID="btnDelete" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();" />
        </div>
        <div runat="server" id="divResult">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvTelco" CssClass="table table-hover" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvTelco_RowDataBound" PageSize="15" OnRowCommand="gvTelco_RowCommand" OnRowDeleting="gvTelcoList_RowDeleting"
                OnPageIndexChanging="gvTelco_PageIndexChanging">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Card ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblcardid" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, TelcoName %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblTelecom" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("TelcoID") %>' OnClientClick="Loading();"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, TelcoShort %>">
                        <ItemTemplate>
                            <asp:Label ID="lblShortName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, ELoadBillerCode %>">
                        <ItemTemplate>
                            <asp:Label ID="lblEloadBillCode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, EPinBillerCode %>">
                        <ItemTemplate>
                            <asp:Label ID="lblEPinBillCode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, SUNDRYACCTNOBANK %>">
                        <ItemTemplate>
                            <asp:Label ID="lblAcountBalance" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, SUNDRYACCTNOWALLET %>">
                        <ItemTemplate>
                            <asp:Label ID="lblWalletBalance" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("TelcoID") %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, CARDMANAGEMENT %>">
                        <ItemTemplate>
                            <asp:HyperLink CssClass="btn btn-primary" ID="hpCardManagement" runat="server">
                    <%=Resources.labels.CARDMANAGEMENT %>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, PREFIXMANAGEMENT %>">
                        <ItemTemplate>
                            <asp:HyperLink CssClass="btn btn-primary" ID="hpPrefixManagement" runat="server">
                                 <%=Resources.labels.PREFIXMANAGEMENT %>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                <SelectedRowStyle />
            </asp:GridView>
            <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
                 <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
         
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
   function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvTelco.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvTelco.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvTelco_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvTelco_ctl01_cbxSelectAll') {
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
        var grid = document.getElementById('<%= gvTelco.ClientID %>');
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

    function ConfirmDelete() {
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
       if (hdf.value == 0) {
           alert('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
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
</script>
<style>
    .mgb15 {
        margin-bottom:15px;
    }

</style>