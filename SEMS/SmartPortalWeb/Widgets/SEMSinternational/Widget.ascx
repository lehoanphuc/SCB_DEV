<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSinternational_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">International Transfer
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>

        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnSearch" runat="server">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 col-xs-12 control-label"><%=Resources.labels.sogiaodich %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtTranNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                           <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                                           <asp:ListItem Value="A" Text="<%$ Resources:labels, send %>"></asp:ListItem>
                                                            <asp:ListItem Value="P" Text="<%$ Resources:labels, conpending %>"></asp:ListItem>
                                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, loi %>"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tungay %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtTranDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.denngay %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txttodate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.BankName %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlBankName" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.lcynm %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="dllCCYID" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.senderphone %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtsenderphone" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClientClick="return Loading();" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divresult1" class="row" runat="server">
            <asp:Literal runat="server" ID="ltrError"></asp:Literal>
            <div id="divResult" class="table-responsive;">
                <asp:GridView ID="gvLTWA" CssClass="table table-hover white-space" runat="server"
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="5" Width="100%" PageSize="15"
                    OnRowDataBound="gvLTWA_RowDataBound"
                    OnRowCommand="gvLTWA_RowCommand"
                    OnPageIndexChanging="gvLTWA_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich%>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbTranID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("TransactionNo")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, ngaygiogiaodich %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, amount1 %>'  >
                            <ItemTemplate>
                                <asp:Label ID="lbAmountsend" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                              <HeaderStyle CssClass="gvLTWA" />
                        </asp:TemplateField >
                        <asp:TemplateField HeaderText='<%$ Resources:labels, lcynm %>'>
                            <ItemTemplate>
                                <asp:Label ID="lbSendCCYID" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, swiftcode %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblSwiftCode" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, BankName %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblBankName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText='<%$ Resources:labels, taikhoanden %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblReceiverAccount" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, senderphone %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblSenderPhone" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, idtype %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblSenderIDType" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText='<%$ Resources:labels, idnumber %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblSenderIDNumber" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText='<%$ Resources:labels, taikhoannguon %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblSenderAccount" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText='<%$ Resources:labels, hotennguoitratien %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText='<%$ Resources:labels, sogiaodichcore %>'>
                            <ItemTemplate>
                                <asp:Label ID="lblTransref" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="hpEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("TransactionNo")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
            <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    var TotalChkBx;
    var Counter;

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function ChildClick(CheckBox) {
        Counter = parseInt(document.getElementById('<%=hdCounter.ClientID %>').value);
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);

        var grid = document.getElementById('<%= gvLTWA.ClientID %>  
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

    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }

    

</script>
