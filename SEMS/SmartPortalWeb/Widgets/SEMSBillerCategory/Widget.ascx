<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBillerCategory_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%= Resources.labels.billerCat %>  
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lbMessage" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12"> 
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.catid %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtcatID" CssClass="form-control" MaxLength="20" runat="server">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.catName %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox runat="server" ID="txtCatName" MaxLength="100" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.catShortName %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtcatShortName" CssClass="form-control" MaxLength="70" runat="server">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div> 
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status%></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList Width="100%" ID="ddlStatus" CssClass="form-control select2 infinity" runat="server">
                                                            <asp:ListItem Value="ALL" Text="<%$ Resources:labels, all %>" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="A" Text="<%$ Resources:labels, active %>"></asp:ListItem>
                                                            <asp:ListItem Value="I" Text="<%$ Resources:labels, inactive %>"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>  
                                            </div>
                                        </div>
                                    </div> 
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click"
                                            Text="<%$ Resources:labels, timkiem %>" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<%--        <div id="divToolbar">
            <asp:Button runat="server" Text="<%$ Resources:labels, themmoi %>" ID="btnAdd" OnClick="btnAdd_Click"
                CssClass="btn btn-primary" OnClientClick="Loading();" />
            <asp:Button runat="server" Text="<%$ Resources:labels, delete %>" ID="btnDelete" OnClick="btnDelete_Click"
                CssClass="btn btn-secondary" OnClientClick="return ConfirmDelete2();" />
        </div>--%>
        <div id="divResult" runat="server">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView CssClass="table table-hover" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AutoGenerateColumns="False" ID="gvBiller" Style="margin-top: 0px" OnRowDeleting="gvBiller_RowDeleting"
                OnRowDataBound="gvBiller_RowDataBound" OnRowCommand="gvBiller_RowCommand" PageSize="15">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate> 
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, catID%>">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnDetail"
                                CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("CatID") %>' OnClientClick="Loading();"> 
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, catName%>">
                        <ItemTemplate>
                            <asp:Label ID="lbCatName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="<%$ Resources:labels, catShortName%>">
                        <ItemTemplate>
                            <asp:Label ID="lbCatShortName" runat="server"></asp:Label>
                        </ItemTemplate>   
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="<%$ Resources:labels, logo%>">
                        <ItemTemplate> 
                            <asp:Image runat="server" ID="imgLogo" CssClass="img-rounded"
                                ImageUrl='<%# Eval("CatLogoBin") %>'
                                Width="70px" Height="64px" />
                        </ItemTemplate>       
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, DateCreate%>">
                        <ItemTemplate>
                            <asp:Label ID="lbDateCreated" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, status%>">
                        <ItemTemplate>
                            <asp:Label ID="lbStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnEdit" Text="Edit"
                                CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CssClass="btn btn-primary"
                                CommandArgument='<%#Eval("CatID") %>'> 
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnDelete" Text="Delete"
                                CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CssClass="btn btn-secondary"
                                CommandArgument='<%#Eval("CatID") %>' OnClientClick="return Confirm();">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
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
<script type="text/javascript">
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvBiller.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvBiller.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvBiller_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvBiller_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvBiller.ClientID %>');
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
    function Loading() {
        if (document.getElementById('<%=lbMessage.ClientID%>').innerHTML != '') {
            document.getElementById('<%=litError.ClientID%>').innerHTML = '';
        }
    }
</script>
