<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCashcodemanager_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.cashcodemanagement %>
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
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
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
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sogiaodich %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="tranid" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.currency %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ccyid" CssClass="form-control select2 infinity" runat="server">
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
                                                        <asp:TextBox ID="txtTel" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.hotennguoitratien %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtSendername" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tungay %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtCreateFromDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.denngay %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtCreateTodate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.trangthai %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="txtStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                            <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                                            <asp:ListItem Value="N" Text="<%$ Resources:labels, notyetpaid %>"></asp:ListItem>
                                                            <asp:ListItem Value="P" Text="<%$ Resources:labels, partialpaid %>"></asp:ListItem>
                                                            <asp:ListItem Value="F" Text="<%$ Resources:labels, fullypaid %>"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="<%$ Resources:labels, condelete %>"></asp:ListItem>
                                                            <asp:ListItem Value="E" Text="<%$ Resources:labels, Expried %>"></asp:ListItem>
                                                            <asp:ListItem Value="L" Text="<%$ Resources:labels, locked %>"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="<%$ Resources:labels, canceled %>"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" OnClientClick="Loading(); return Validate();" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divResult" runat="server">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvCashCode" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvCashCode_RowDataBound" PageSize="15" OnRowCommand="gvCashCode_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText='<%$ Resources:labels, sogiaodich %>'>
                        <ItemTemplate>
                            <asp:LinkButton ID="lblTranNo" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("MvID") + "|DETAILS" %>' OnClientClick="Loading();"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, senderphone %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSenderPhone" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, hotennguoitratien %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSenderName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, senderamount %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSenderAmount" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, paidamount %>">
                        <ItemTemplate>
                            <asp:Label ID="lblPaidAmount" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, currency %>">
                        <ItemTemplate>
                            <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, ngaytao %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcreatedate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, resend %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbResend" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.REJECT %>' CommandArgument='<%#Eval("MvID") + "|RESEND"%>' OnClientClick="Loading();">Resend</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, cancel %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbCancel" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.REJECT %>' CommandArgument='<%#Eval("MvID") + "|CANCEL"%>' OnClientClick="Loading();">Cancel</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function Validate() {
        if (!IsDateGreater('<%=txtCreateTodate.ClientID %>','<%=txtCreateFromDate.ClientID %>','<%=Resources.labels.ngayketthuclonhonngaybatdau %>')) {
            document.getElementById('<%=txtCreateFromDate.ClientID %>').focus();
            return false;
        }
        return true;
    }
</script>
