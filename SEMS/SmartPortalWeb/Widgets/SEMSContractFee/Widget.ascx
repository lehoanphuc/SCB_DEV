<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractFee_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.quanlyphichohopdong %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mahopdong %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtContractno" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tenkhachhang %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtcustomername" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.giaodich %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddltran" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tiente %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlccyid" CssClass="form-control select2 infinity" OnSelectedIndexChanged="ddlCCYID_OnSelectedIndexChanged" AutoPostBack="True" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tenphi %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlFee" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaiphi %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlFeeType" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClientClick="Loading();" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" OnClientClick="Loading();" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();" />
        </div>
        <div id="divResult" class="table-responsive" runat="server">
            <asp:Literal runat="server" ID="ltrError"></asp:Literal>
            <asp:GridView ID="gvContractFee" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvContractFee_RowDataBound" PageSize="15" OnRowCommand="gvContractFee_RowCommand" OnRowDeleting="gvContractFee_RowDeleting">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblProductName" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("CONTRACTNO")+"|"+ Eval("TranCode")+"|"+ Eval("FEEID")+"|"+ Eval("CCYID")+"|"+ Eval("STATUS")%>' OnClientClick="Loading();"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>">
                        <ItemTemplate>
                            <asp:Label ID="lblfullname" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, giaodich %>">
                        <ItemTemplate>
                            <asp:Label ID="lblTrans" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenphi %>">
                        <ItemTemplate>
                            <asp:Label ID="lblFeeName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, loaiphi %>">
                        <ItemTemplate>
                            <asp:Label ID="lblFeeType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, nguoitao %>">
                        <ItemTemplate>
                            <asp:Label ID="lblusercreated" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, ngaytao %>">
                        <ItemTemplate>
                            <asp:Label ID="lbldatecreated" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, nguoiduyet %>">
                        <ItemTemplate>
                            <asp:Label ID="lbluserapproved" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, ngayduyet %>">
                        <ItemTemplate>
                            <asp:Label ID="lbldateapproved" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tiente %>">
                        <ItemTemplate>
                            <asp:Label ID="lblccyid" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("CONTRACTNO")+"|"+ Eval("TranCode")+"|"+ Eval("FEEID")+"|"+ Eval("CCYID")+"|"+ Eval("STATUS")%>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("CONTRACTNO")+"|"+ Eval("TranCode")+"|"+ Eval("FEEID")+"|"+ Eval("CCYID")+"|"+ Eval("STATUS")%>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
            <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvContractFee.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvContractFee.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvContractFee_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvContractFee_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvContractFee.ClientID %>');
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
</script>

