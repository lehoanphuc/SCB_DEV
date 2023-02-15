<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSViewLogTransactions_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.nhatkygiaodichconsumer %>
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
                                            <label class="col-sm-4 col-xs-12 col-xs-12 control-label"><%=Resources.labels.sogiaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtTranID" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.usercreated %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtUserApproved" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.debitaccount %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtAccno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.taikhoanbaoco %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCreditAcct" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaigiaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlTransactionType" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.trangthai %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="<%$ Resources:labels, hoanthanh %>"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="<%$ Resources:labels, loi %>"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="<%$ Resources:labels, huy %>"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="<%$ Resources:labels, thanhtoanthatbai %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" OnClientClick="return Validate();" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divToolbar">
    <asp:Button ID="btnExport" CssClass="btn btn-secondary" runat="server" Text='<%$ Resources:labels, exporttofile %>' OnClick="bt_export_Click" />
</div>
<div id="divResult">
    <asp:Literal runat="server" ID="ltrError"></asp:Literal>
    <asp:GridView ID="gvLTWA" CssClass="table table-hover" runat="server" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
        CellPadding="5" Width="100%" OnRowDataBound="gvLTWA_RowDataBound" OnRowCommand="gvLTWA_OnRowCommand" PageSize="15">
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbTranID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("IPCTRANSID")%>' OnClientClick="Loading();"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, ngaygiogiaodich %>'>
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, debitaccount %>'>
                <ItemTemplate>
                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, taikhoanden %>'>
                <ItemTemplate>
                    <asp:Label ID="lblReceiveAccount" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, sotien %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
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
            <asp:TemplateField HeaderText='<%$ Resources:labels, trangthai %>'>
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
</div>
<asp:Label ID="lblheaderFile" runat="server" Text="<%$ Resources:labels, nganhangphuongnamnhatkygiaodich %>" Visible="false" />
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function Validate() {
        if ((document.getElementById('<%=txtFromDate.ClientID %>').value != "") && (document.getElementById('<%=txtToDate.ClientID %>').value != "")) {
            if (!IsDateGreater('<%=txtToDate.ClientID %>','<%=txtFromDate.ClientID %>','<%=Resources.labels.ngayketthuclonhonngaybatdau %>')) {
                document.getElementById('<%=txtFromDate.ClientID %>').focus();
                return false;
            }
        }
        return true;
    }
</script>
