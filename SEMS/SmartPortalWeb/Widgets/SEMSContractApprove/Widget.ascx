<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractApprove_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.duyethopdong %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.mahopdong %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtcontractno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.tenkhachhang %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtcustname" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ngaymo %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtOpenDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ngayhethan %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtEndDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                   <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.nguoimo %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtcreateuser" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <%--<label class="col-sm-4 control-label"><%=Resources.labels.subusertype %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlUserType" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>--%>
                                            <label class="col-sm-4 control-label"><%=Resources.labels.trangthai %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2 infinity">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divToolbar">
            <div id="pnbutton" runat="server">
                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, duyet %>" OnClick="btnApprove_Click" />
                <asp:Button ID="btnReject" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, reject %>" OnClick="btnReject_Click" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="divResult" runat="server">
                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                    <asp:GridView ID="gvcontractList" runat="server" BackColor="White" CssClass="table table-hover"
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnRowCommand="gvcontractList_RowCommand"
                        Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                        OnRowDataBound="gvcontractList_RowDataBound" PageSize="15"
                        OnPageIndexChanging="gvcontractList_PageIndexChanging"
                        OnSorting="gvcontractList_Sorting" AllowSorting="True">
                        <RowStyle ForeColor="#000000" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>" SortExpression="CONTRACTNO">
                                <ItemTemplate>
                                       <asp:LinkButton ID="hpcontractCode" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("ContractNo")%>'>[hpDetails]</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, khachhang %>" SortExpression="FULLNAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblcustName" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, nguoimo %>" SortExpression="USERCREATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpen" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, ngaymo %>" SortExpression="CREATEDATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpendate" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhethan %>" SortExpression="ENDDATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosedate" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, subusertype %>" SortExpression="USERTYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubUserCode" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>" SortExpression="STATUS">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle />
                    </asp:GridView>
                    <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
                    <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
                    <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">//<![CDATA[

    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });
    cal.manageFields("<%=txtOpenDate.ClientID %>", "<%=txtOpenDate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtEndDate.ClientID %>", "<%=txtEndDate.ClientID %>", "%d/%m/%Y");
    //]]></script>


<script>
    function HighLightCBX(obj, obj1) {
        //var obj2=document.getElementById(obj1);
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }

    function checkColor(obj, obj1) {
        var obj2 = document.getElementById(obj);
        if (obj2.checked) {
            obj1.className = "hightlight";
        }
        else {
            obj1.className = "nohightlight";
        }
    }
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvcontractList.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvcontractList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvcontractList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvcontractList_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvcontractList.ClientID %>');
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

    function pop(obj) {
        if (window.confirm(obj)) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
