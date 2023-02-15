<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Card_Widget" %>
<script type="text/javascript" src="widgets/SEMSPrefix/js/mask.js"> </script>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCashcodemanager/Images/tax.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <div class="subheader">
        <h1 class="subheader-title">
            <asp:Label ID="lbltitle" runat="server"></asp:Label>
        </h1>
    </div>
</div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </div>
        <%--Card--%>
        <div id="divSearchCardID" class="" runat="server">
            <asp:Panel ID="pnSearchCard" runat="server" DefaultButton="btnSearchCard">
                <div class="">
                    <div class="panel ">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.shortname %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtShortNameSearch" CssClass="form-control" runat="server" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.trangthai %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="ddlStatusSearch" Width="100%" runat="server" CssClass="form-control select2 infinity">
                                                        <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                                        <asp:ListItem Value="B" Text="Block"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.cardamount %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtCardamountSearch" CssClass="form-control" runat="server" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);" onkeypress="allowNumbersOnly(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div class=" col-xs-12">
                                            <asp:Button ID="btnSearchCard" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>"
                                                OnClick="btnSearchCard_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </asp:Panel>
        </div>
        </div>
        <div id="divButtonbar" runat="server" class="divToolbar">
           <asp:Button ID="btnAdd" CssClass="btn btn-primary " runat="server" Text="<%$ Resources:labels, addcard %>" OnClick="Button2_Click" />
            <asp:Button ID="btnDelete" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete2();" />
        </div>
        <div id="divTable" runat="server" class="divResult">
            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
            <asp:GridView ID="gvCardid" runat="server" BackColor="White" CssClass="table table-hover"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="15"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnRowDeleting="delete_Click" OnRowCommand="gvCard_RowCommand"
                OnRowDataBound="gvCardid_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="gvCenter">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, Cardid %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcardid" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, TelcoName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblTelconame" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, shortname %>">
                        <ItemTemplate>
                            <asp:Label ID="lblshortname" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, cardamount %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcardamount" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, realmoney %>">
                        <ItemTemplate>
                            <asp:Label ID="lblrealmoney" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, bcynm %>">
                        <ItemTemplate>
                            <asp:Label ID="lblccyid" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, status %>">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, type %>">
                        <ItemTemplate>
                            <asp:Label ID="lbltype" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEditcard" runat="server" CommandArgument='<%#Eval("CardID") %>' CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="btn btn-secondary" ID="hpDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gvFooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager hidden" />
            </asp:GridView>
            <uc1:gridviewpaging runat="server" id="GridViewPaging" />
            <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
        <p class="auto-style1">
            <div style="text-align: center; margin-top: 10px;">
                <asp:Button ID="Button8" CssClass="btn btn-secondary btnGeneral" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button8_Click1" />
            </div>
        </p>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvCardid.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvCardid.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvCardid_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }
        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvCardid_ctl01_cbxSelectAll') {
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
        var grid = document.getElementById('<%= gvCardid.ClientID %>');
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
