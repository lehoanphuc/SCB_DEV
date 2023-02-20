<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSViewLogTransactions_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.moneytranschannel %>
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
                    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                        <div class="row">
                            <div class="col-sm-10 col-xs-12">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.BankName %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control select2" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divToolbar">
            <asp:Button ID="btnAdd_New" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAdd_New_Click" OnClientClick="Loading();" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" OnClientClick="Loading();" />
        </div>
<div id="divResult" runat="server" class="table-responsive">
    <asp:Literal runat="server" ID="ltrError"></asp:Literal>
    <asp:GridView ID="gvLTWA" CssClass="table table-hover" runat="server" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
        CellPadding="5" Width="100%" OnRowDataBound="gvLTWA_RowDataBound" OnRowCommand="gvLTWA_OnRowCommand" PageSize="15">
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:labels, refnumber%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbTranID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("IPCTRANSID")%>' OnClientClick="Loading();"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, ngaygiogiaodich %>'>
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, loaigiaodich %>'>
                <ItemTemplate>
                    <asp:Label ID="lblTrantype" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, debitaccount %>'>
                <ItemTemplate>
                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, sotien %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, phi %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblFee" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, totalAmount %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, loaitien %>'>
                <ItemTemplate>
                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, errordesc %>'>
                <ItemTemplate>
                    <asp:Label ID="lblErrDesc" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, mota %>'>
                <ItemTemplate>
                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, malo %>' Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblBatchRef" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, sogiaodichcore %>'>
                <ItemTemplate>
                    <asp:Label ID="lblRefCore" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, customercodecore %>'>
                <ItemTemplate>
                    <asp:Label ID="lblcustcodecore" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, trangthai %>'>
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, trangthaiduyet %>" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblResult" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
</div>
<asp:Label ID="lblheaderFile" runat="server" Text="<%$ Resources:labels, nganhangphuongnamnhatkygiaodich %>" Visible="false" />
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    <%--function Validate() {
        if ((document.getElementById('<%=txtFromDate.ClientID %>').value != "") && (document.getElementById('<%=txtToDate.ClientID %>').value != "")) {
            if (!IsDateGreater('<%=txtToDate.ClientID %>','<%=txtFromDate.ClientID %>','<%=Resources.labels.ngayketthuclonhonngaybatdau %>')) {
                document.getElementById('<%=txtFromDate.ClientID %>').focus();
                return false;
            }
        }
        return true;
    }--%>
</script>
