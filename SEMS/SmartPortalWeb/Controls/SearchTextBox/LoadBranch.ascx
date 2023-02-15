<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadBranch.ascx.cs" Inherits="Controls_SearchTextBox_Branch" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<div class="dropdown-search">
    <asp:UpdatePanel runat="server">

        <ContentTemplate>
            <asp:TextBox ID="txtBranch" runat="server" Width="76%" ReadOnly="true" style="padding-left: 8px; outline:none"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdID" />
            <asp:HiddenField runat="server" ID="hdfSearchValue" />
            <asp:HiddenField runat="server" ID="hdfSearchText" />
            <asp:HiddenField runat="server" ID="hdfNameSearchText" />

            <button type="button" runat="server" id="btnPopup" class="search-popup fa fa-search" style="background: transparent; border: none; box-shadow: none" data-toggle="modal" data-target="#Branch">
            </button>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>
<asp:Panel runat="server" class="modal fade" ID="Branch" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title"><%=Resources.labels.branch %></h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span></button>
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <%-- Condition search --%>
                                    <div class="form-group">
                                        <div class="row col-sm-12">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%=Resources.labels.branchcode%></label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtbranchId" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%=Resources.labels.branchname%></label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtBranchName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-content div-btn rounded-bottom  border-left-0 border-right-0 border-bottom-0 text-muted">
                                                <asp:Button ID="btnSearch" runat="server" data-close="<%Branch.ClientID%>" CssClass="btn btn-primary" Text="<%$ Resources:labels, timkiem %>"
                                                    OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Result notifycation --%>
                                    <div class="result">
                                        <div class="error">
                                            <asp:Label runat="server" ID="lblError" />
                                        </div>
                                    </div>
                                    <%-- Table view result --%>
                                    <asp:Repeater runat="server" ID="rptData">
                                        <HeaderTemplate>
                                            <div class="pane">
                                                <div class="table-responsive">
                                                    <table class="table table-hover footable c_list">
                                                        <thead class="thead-light">
                                                            <tr>
                                                                <th>
                                                                    <input name='<%="item"+Branch.ClientID%>' data-control='<%=hdID.ClientID %>' data-display='<%=hdfSearchText.ClientID %>' data-name='<%=hdfNameSearchText.ClientID %>' style="visibility: hidden" type="checkbox" onclick="CheckboxAll(this)">
                                                                </th>
                                                                <th><%=Resources.labels.branchcode%></th>
                                                                <th><%=Resources.labels.branchname%></th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="tdcheck">
                                                    <input name='<%="item"+Branch.ClientID%>' class="check keepvalue" value="<%#Eval("BranchID") %>" data-text="<%#Eval("BranchID") %>" data-name="<%#Eval("BranchName") %>" onclick="ConfigRatioPlus(this)" type="radio">
                                                </td>
                                                <td><%#Eval("BranchID") %></td>
                                                <td><%#Eval("BranchName") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </table>
                                        </div> </div>
                                         <div class="margintop20 button-group panel-content div-btn rounded-bottom  border-left-0 border-right-0 border-bottom-0 text-muted">
                                             <asp:Button runat="server" class="btn btn-primary" data-check="itemBranch111" ID="btnOK" OnClick="btnOK_Click" data-close="<%=Branch.ClientID%>" Text='<%$Resources:labels,ok %>'/>
                                             <button type="button" class="btn btn-primary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                         </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <uc1:GridViewPaging ID="gidview" runat="server"></uc1:GridViewPaging>
                                </div>
                            </div>
                            <%-- Button --%>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Panel>
<script>

</script>
