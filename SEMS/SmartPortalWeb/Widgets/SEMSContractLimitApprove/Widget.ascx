<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractLimitApprove_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.duyethanmuchopdong %>
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

        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <div id="">
                                <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                                    <div class=" row">
                                        <div class=" col-sm-10">
                                            <div class=" row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.mahopdong %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtContractno" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.giaodich %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddltran" CssClass="form-control select2" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.tiente %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlccyid" runat="server" CssClass="form-control select2 infinity">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.hanmuc %></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtlimit" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.kieuhanmuc %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlLimitType" runat="server" OnSelectedIndexChanged="ddlLimitType_IndexChanged" AutoPostBack="true" CssClass="form-control select2 infinity">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label"><%=Resources.labels.trangthai %></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control select2 infinity">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class=" col-sm-2">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <div id="divToolbar">
                <asp:Button CssClass="btn btn-primary" ID="btnAddNew" runat="server" Text="<%$ Resources:labels, duyet %>" OnClick="btnAddNew_Click" OnClientClick="Loading(); return ConfirmApprove();" />
                <asp:Button CssClass="btn btn-secondary" ID="btnDelete" runat="server" Text="<%$ Resources:labels, khongduyet %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmReject();" />
            </div>
            <div id="divResult" runat="server" >
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:Literal runat="server" ID="ltrError"></asp:Literal>
                <asp:GridView ID="gvUserList" runat="server" CssClass="table table-hover" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvContractLimit_RowDataBound" PageSize="15">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>">
                            <ItemTemplate>
                                <asp:LinkButton AutoPostBack="false" cssClass="cslinkbutton" Enabled="false" CommandArgument='<%#Eval("CONTRACTNO")+"|"+ Eval("TranCode")+"|"+
                                        Eval("CCYID")+"|"+ Eval("STATUS")+"|" + Eval("LIMITTYPE")+"|"%>' ID="lblcontractno" runat="server"></asp:LinkButton>
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
                        <asp:TemplateField HeaderText="<%$ Resources:labels, kieuhanmuc %>">
                            <ItemTemplate>
                                <asp:Label ID="lblLimitType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="trancode" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblTranCode" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, hanmuc %>">
                            <ItemTemplate>
                                <asp:Label ID="lbllimit" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tonghanmucngay %>" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbltotallimitday" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, solangiaodichngay %>" >
                            <ItemTemplate>
                                <asp:Label ID="lblcountlimit" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tiente %>">
                            <ItemTemplate>
                                <asp:Label ID="lblccyid" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, nguoitao %>" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblcreateuser" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, nguoisuadoi %>" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblmodifiuser" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="sttid" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblstatusid" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
               <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
                <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
                <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
            </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script>
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvUserList.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvUserList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                var txtstatus = elements[i].cells[0].children[9];
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvUserList_ctl01_cbxSelectAll'/* && (txtstatus.innerHTML == "Pendding" || txtstatus.innerHTML == "Pending for delete" || txtstatus.innerHTML == "New")*/) {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvUserList_ctl01_cbxSelectAll') {
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
        var totalcboxvalidate = 0;
        Counter = parseInt(document.getElementById('<%=hdCounter.ClientID %>').value);
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        //count validate checkbox status p,n,g
        var elements = document.getElementById('<%=gvUserList.ClientID %>').rows;
        var count = document.getElementById('<%=gvUserList.ClientID %>').rows.length;
        for (i = 0; i < count; i++) {
            if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvUserList_ctl01_cbxSelectAll') {
                totalcboxvalidate++;
            }
        }


        var grid = document.getElementById('<%= gvUserList.ClientID %>');
        var cbHeader = grid.rows[0].cells[0].childNodes[0];

        if (CheckBox.checked)
            Counter++;
        else if (Counter > 0)
            Counter--;

        if (Counter < totalcboxvalidate)
            cbHeader.checked = false;
        else if (Counter == totalcboxvalidate)
            cbHeader.checked = true;
        document.getElementById('<%=hdCounter.ClientID %>').value = Counter.toString();
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
             document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function ConfirmApprove() {
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.vuilongchonhanmuchopdong %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchanmuonduyethanmuchopdong %>');
        }
    }

    function ConfirmReject() {
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        if (hdf.value == 0) {
            alert('<%=Resources.labels.vuilongchonhanmuchopdong %>');
            return false;
        } else {
            return confirm('<%=Resources.labels.banchacchankhonghanmuchopdong %>');
        }
    }
</script>


