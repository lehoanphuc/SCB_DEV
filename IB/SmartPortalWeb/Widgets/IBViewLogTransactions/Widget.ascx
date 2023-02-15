<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBViewLogTransactions_Widget" %>

<%@ Import Namespace="SmartPortal.Common" %>
<%@ Import Namespace="SmartPortal.Common.Utilities" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<div class="th">
    <span><%=Resources.labels.nhatkygiaodich %></span><br>
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png">
</div>
<div id="divError" style="text-align: center;">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
<asp:Panel ID="pnSearch" runat="server" class="divcontent">
    <figure>
        <legend class="handle"><%=Resources.labels.timkiemgiaodich %></legend>

        <div class="content search display-label">
            <div class="row">
                <div class="col-xs-5 col-md-2">
                    <label class="bold"><%= Resources.labels.sogiaodich %></label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:TextBox ID="txtTranID" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-5 col-md-2">
                    <label class="bold"><%= Resources.labels.trangthai %></label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
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
            <div class="row form-group">
                <div class="col-xs-5 col-md-2">
                    <label class="bold"><%= Resources.labels.debitaccount %></label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:DropDownList ID="ddlDebitAcct" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-xs-5 col-md-2">
                    <label class="bold"><%= Resources.labels.taikhoanbaoco %></label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:TextBox ID="txtCreditAcct" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-5 col-md-2">
                    <label class="bold"><%= Resources.labels.tungay %></label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:TextBox ID="txtFromDate" autocomplete="off" CssClass="dateselect form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-5 col-md-2">
                    <label class="bold"><%= Resources.labels.denngay %></label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:TextBox ID="txtToDate" autocomplete="off" CssClass="dateselect form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-5 col-md-2">
                    <label class="bold"><%= Resources.labels.loaigiaodich %></label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-xs-5 col-md-2">
                    <asp:CheckBox ID="ckbIsDelete" runat="server" Font-Bold="True" Text="<%$ Resources:labels, huy %>" Visible="False" />
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:CheckBox ID="ckbIsBatch" runat="server" Font-Bold="True" Text="<%$ Resources:labels, giaodichlo %>" />
                </div>

            </div>
            <div class="row" style="text-align: center; padding-top: 10px;">
                <asp:CheckBox ID="cbIsschedule" runat="server" Font-Bold="True" Text="<%$ Resources:labels, giaodichlich %> " Visible="false" />
                <asp:Button ID="btnSearch" runat="server" Text='<%$ Resources:labels, timkiem %>' CssClass="btn btn-primary"
                    OnClick="btnSearch_Click" />
            </div>
            <div class="row">
                <div class="col-xs-5 col-md-2">
                    <asp:Label ID="Label9" Font-Bold="True" runat="server" CssClass="bold" Text="<%$ Resources:labels, malo %>" Visible="false"></asp:Label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:TextBox ID="txtBatchRef" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xs-5 col-md-2">
                    <asp:Label ID="Label10" Font-Bold="True" runat="server" CssClass="bold" Text="<%$ Resources:labels, ketqua %>" Visible="False"></asp:Label>
                </div>
                <div class="col-xs-7 col-md-4">
                    <asp:DropDownList ID="DDLAppSta" runat="server" Visible="False" CssClass="form-control">
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
            </div>
        </div>
    </figure>
</asp:Panel>
<asp:Panel ID="Panel1" runat="server" class="divcontent">
    <figure>
        <legend class="handle"><%=Resources.labels.danhsachgiaodich %></legend>

        <div class="content" style="overflow: auto">
            <asp:Literal runat="server" ID="ltrError"></asp:Literal>
            <asp:Repeater runat="server" ID="rptLTWA" OnItemCommand="gvLTWA_ItemCommand" OnItemDataBound="gvLTWA_OnItemDataBound">
                <HeaderTemplate>
                    <table class="table table-bordered table-hover footable" data-paging="true">
                        <thead>
                            <tr>
                                <th><%=Resources.labels.sogiaodich %></th>
                                <th><%=Resources.labels.ngaygiogiaodich %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.loaigiaodich %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.debitaccount %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.sotien %></th>
                                 <th data-breakpoints="xs"><%=Resources.labels.phi %></th>
                                <th data-breakpoints="xs"><%=Resources.labels.loaitien %></th>
                                <th data-breakpoints="xs sm md lg"><%=Resources.labels.errordesc %></th>
                                <th data-breakpoints="xs sm md lg"><%=Resources.labels.mota %></th>
                                <th data-breakpoints="xs sm md lg"><%=Resources.labels.malo %></th>
                                <th data-breakpoints="xs sm md lg"><%=Resources.labels.sogiaodichcore %></th>
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
                        <td>
                            <%#Eval("CHAR01") %>
                        </td>
                        <td align="right" style="width: 100px;">
                            <%#Utility.FormatMoney(Eval("AMOUNT").ToString(), Eval("CCYID").ToString().Trim()) %>
                        </td>
                        <td><%#Utility.FormatMoney(Eval("NUM02").ToString(),Eval("CCYID").ToString().Trim())%> </td>
                        <td><%#Eval("CCYID") %></td>
                        <td><%#Eval("ERRORDESC") %></td>
                        <td><%#Eval("TRANDESC") %></td>
                        <td><%#Eval("BATCHREF") %></td>
                        <td><%#Eval("CHAR20") %></td>
                        <td align="center"><%# GetStatus(Container.DataItem) %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
        </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </figure>
</asp:Panel>
<script type="text/javascript">
    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });
    cal.manageFields("<%=txtFromDate.ClientID %>", "<%=txtFromDate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=txtToDate.ClientID %>", "<%=txtToDate.ClientID %>", "%d/%m/%Y");
</script>

