<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpApprovedTransactions_Widget" %>
<%@ Import Namespace="SmartPortal.Common.Utilities" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager runat="server">
</asp:ScriptManager>

<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="al">
    <%=Resources.labels.approvedtransactionshistory %>
    <br>
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png">
</div>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <asp:Panel ID="pnSearch" runat="server">
            <figure>
                <legend class="handle">
                    <%=Resources.labels.transactionsearch %>
                </legend>
                <div class="content display-label">
                    <div class="row form-group">
                        <div class="col-xs-4 col-md-2 right">
                            <%=Resources.labels.sogiaodich %>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtTranID" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, loaigiaodich %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, taikhoanbaoco %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtCreditAcct" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, debitaccount %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:DropDownList ID="ddlDebitAcct" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, tungay %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="dateselect"></asp:TextBox>
                        </div>
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, denngay %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="dateselect"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, trangthai %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                                <asp:ListItem Value="1" Text="<%$ Resources:labels, hoanthanh %>"></asp:ListItem>
                                <asp:ListItem Value="2" Text="<%$ Resources:labels, loi %>"></asp:ListItem>
                                <asp:ListItem Value="3" Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>
                                <asp:ListItem Value="4" Text="<%$ Resources:labels, huy %>"></asp:ListItem>
                                <asp:ListItem Value="5" Text="<%$ Resources:labels, conpending %>"></asp:ListItem>
                                <asp:ListItem Value="9" Text="<%$ Resources:labels, thanhtoanthatbai %>"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, malo %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:TextBox ID="txtBatchRef" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="lblIsBatch" runat="server" Visible="False" Text='<%$ Resources:labels, giaodichlo %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:CheckBox ID="ckbIsBatch" runat="server" Visible="False" Font-Bold="True" Text="<%$ Resources:labels, giaodichlo %>" />
                        </div>
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label1" runat="server" Visible="False" Text='<%$ Resources:labels, ketqua %>'></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:DropDownList ID="DDLAppSta" runat="server" Visible="False">
                                <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                                <asp:ListItem Value="1" Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>
                                <asp:ListItem Value="3" Text="<%$ Resources:labels, duyet %>"></asp:ListItem>
                                <asp:ListItem Value="6" Text="<%$ Resources:labels, hoantien %>"></asp:ListItem>
                                <asp:ListItem Value="4" Text="<%$ Resources:labels, huy %>"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, huy %>' Visible="False"></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:CheckBox ID="ckbIsDelete" runat="server" Font-Bold="True" Text="<%$ Resources:labels, huy %>" Visible="False" />
                        </div>
                        <div class="col-xs-4 col-md-2 right">
                            <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, giaodichlich %>' Visible="False"></asp:Label>
                        </div>
                        <div class="col-xs-8 col-md-4">
                            <asp:CheckBox ID="cbIsschedule" runat="server" Font-Bold="True" Visible="false"
                                Text="<%$ Resources:labels, giaodichlich %>" />
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="btnSearch" runat="server" Text='<%$ Resources:labels, timkiem %>' class="btn btn-primary" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </figure>
        </asp:Panel>

        <div class="content" style="overflow: auto">
            <figure>
                <legend class="handle">Approved transactions list
                </legend>
            </figure>
            <asp:Repeater runat="server" ID="gvLTWA" OnItemCommand="gvLTWA_ItemCommand" OnItemDataBound="gvLTWA_OnItemDataBound">
                <HeaderTemplate>
                    <table class="table table-bordered table-hover footable" data-paging="true">
                        <thead>
                            <tr>
                                <th><%=Resources.labels.sogiaodich %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.ngaygiogiaodich %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.loaigiaodich %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.debitaccount %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.sotien %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.phi %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.loaitien %></th>
                                <th><%=Resources.labels.errordesc %></th>
                                <th data-breakpoints="xs sm lg"><%=Resources.labels.mota %></th>
                                <th data-breakpoints="xs sm lg"><%=Resources.labels.malo %></th>
                                <th data-breakpoints="xs "><%=Resources.labels.sogiaodichcore %></th>
                                <th><%=Resources.labels.trangthai %></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbDetails" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("IPCTRANSID")%>' OnClientClick="Loading();"><%#Eval("IPCTRANSID") %></asp:LinkButton>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "IPCTRANSDATE", "{0:d/M/yy HH:mm}")%>
                        </td>
                        <td><%#Eval("PAGENAME") %></td>
                        <td><%#Eval("CHAR01") %></td>
                        <td style="text-align: right;">
                            <%#Utility.FormatMoney(Eval("AMOUNT").ToString(), Eval("CCYID").ToString().Trim()) %> </td>
                        <td><%#Utility.FormatMoney(Eval("NUM02").ToString(), Eval("CCYID").ToString().Trim()) %></td>
                        <td><%#Eval("CCYID") %></td>
                        <td><%#Eval("ERRORDESC") %></td>
                        <td><%#Eval("TRANDESC") %></td>
                        <td><%#Eval("BATCHREF") %></td>
                        <td><%#double.Parse(Eval("NUM04").ToString())!=0?Eval("NUM04").ToString().Replace(".00",""):string.Empty %></td>
                        <td><%# GetStatus(Container.DataItem) %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
        </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });
    cal.manageFields("<%=txtFromDate.ClientID %>", "<%=txtFromDate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtToDate.ClientID %>", "<%=txtToDate.ClientID %>", "%d/%m/%Y");
</script>

