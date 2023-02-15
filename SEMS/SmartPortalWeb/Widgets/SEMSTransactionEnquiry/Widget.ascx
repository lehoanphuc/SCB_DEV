<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTransactionEnquiry_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Controls/SearchTextBox/Transaction.ascx" TagPrefix="uc1" TagName="Transaction" %>
<%@ Import Namespace="SmartPortal.Constant" %>


<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleContracLevel" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label></div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.TransactionEnquiry%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.PhoneNumber %> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" MaxLength="50" runat="server"  IsRequired="true" OnTextChanged="loadInfo" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.giaodich %> </label>
                                            <div class="col-sm-8">
                                                <uc1:Transaction ID="txtTransaction" runat="server"></uc1:Transaction>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.tungay %> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFromDate" CssClass="form-control datetimepicker" MaxLength="10"  runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.denngay%> </label>
                                            <div class="col-sm-8">
                                                <asp:textBox ID="txtToDate" CssClass="form-control datetimepicker" MaxLength="10" runat="server"></asp:textBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="panel-content form-horizontal p-b-0">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="well well-sm" style="background-color:#f5f5f500">
                                                    <div class="row">
                                                            <label class="col-sm-6 control-label label-bold colon"><%=Resources.labels.sanpham%></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtProduct" CssClass="form-control" BackColor="#ffffff" BorderStyle="None" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="col-sm-6 control-label label-bold colon"><%=Resources.labels.hopdong%></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtContract" CssClass="form-control" BackColor="#ffffff" BorderStyle="None" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="col-sm-6 control-label label-bold colon"><%=Resources.labels.WalletAccountLevel%></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtWalletLevel" CssClass="form-control" BackColor="#ffffff" BorderStyle="None" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                           <label class="col-sm-6 control-label label-bold colon"><%=Resources.labels.KycLevel%></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtKYCLevel" CssClass="form-control" BackColor="#ffffff" BorderStyle="None" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="col-sm-6 control-label label-bold colon"><%=Resources.labels.LinkageBankAccountOrBankCardNumber%></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtAccNo" CssClass="form-control" BackColor="#ffffff" BorderStyle="None" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="col-sm-6 control-label label-bold colon"><%=Resources.labels.nguoitrahoadon%></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtBiller" CssClass="form-control" BackColor="#ffffff" BorderStyle="None" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                           <label class="col-sm-6 control-label label-bold colon"><%=Resources.labels.ByUser%></label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox ID="txtUserid" CssClass="form-control" BackColor="#ffffff" BorderStyle="None" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                   </div>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btEnquiry" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, enquiry %>" OnClick="btnEnquiry_click"/>
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click"/>
                        </div>
                        <%-- Table view --%>
                        <asp:Panel ID="panList" ScrollBars="Auto" runat="server">
                        <div class="card container-fluid result">
                            <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand">
                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-hover footable c_list">
                                            <thead class="thead-light repeater-table">
                                                <tr>
                                                    <%--<th>
                                                        <input name="item1" data-control='<%=hdTransactionEnquiry.ClientID %>' type="checkbox" onclick="CheckboxAll(this)">
                                                    </th>--%>
                                                    <th class="title-repeater"><%=Resources.labels.TransactionNumber %></th>
                                                    <th class="title-repeater"><%=Resources.labels.TransactionDate %></th>
                                                    <th class="title-repeater"><%=Resources.labels.TransactionName %></th>
                                                    <th class="title-repeater"><%=Resources.labels.PhoneNumber %></th>
                                                    <th class="title-repeater"><%=Resources.labels.name %></th>
                                                    <th class="title-repeater"><%=Resources.labels.TransactionAmount %></th>
                                                    <th class="title-repeater"><%=Resources.labels.BonusAmount %></th>
                                                    <th class="title-repeater"><%=Resources.labels.FeeAmount %></th>
                                                    <th class="title-repeater"><%=Resources.labels.currency %></th>
                                                    <th class="title-repeater"><%=Resources.labels.TransactionStatus %></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <%--<td class="tdcheck">
                                            <input name="item1" class="checkbox-tick" value="<%#Eval("Txref")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                                        </td>--%>
                                        <td class="tr-boder">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("TransactionNumber")%>'  CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' > <%#Eval("TransactionNumber") %> </asp:LinkButton>
                                        </td>
                                        <td hidden="hidden">
                                            <label id="lblByUser" runat="server"> <%#Eval("ByUser") %> </label>
                                        </td>
                                        <td hidden="hidden">
                                            <label id="lblBiller" runat="server"><%#Eval("Biller") %></label>
                                        </td>
                                        <td class="tr-boder"><%#Eval("TransactionDate") %></td>
                                        <td class="tr-boder"><%#Eval("TransactionName") %></td>
                                        <td class="tr-boder"><%#Eval("PhoneNumber") %></td>
                                        <td class="tr-boder"><%#Eval("FullName") %></td>
                                        <td class="tr-boder"><%#Eval("Amount")%></td>
                                        <td class="tr-boder"><%#Eval("Bonus")%></td>
                                        <td class="tr-boder"><%#Eval("AmountFee")%></td>
                                        <td class="tr-boder"><%#Eval("Currency")%></td>
                                        <td class="tr-boder"><%#Eval("Status")%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
</table>
                            </div class="table-responsive">
                                    <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="Data not found." Visible="false"></asp:Label>
                                </FooterTemplate>
                            </asp:Repeater>

                            <uc1:GridViewPaging runat="server" Visible="false" ID="GridViewPaging" />
                            <asp:HiddenField runat="server" ID="hdTransactionEnquiry" />
                        </div>
                </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>