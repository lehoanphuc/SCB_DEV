<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadGroupDefinition.ascx.cs" Inherits="Controls_SearchTextBox_LoadGroupDefinition" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<div class="dropdown-search">
    <asp:UpdatePanel runat="server">

        <ContentTemplate>
            <div class="box" style="width: 100%">
                <asp:TextBox ID="txtgrpdef" runat="server" Width="72%" ReadOnly="true" Style="padding-left: 8px; outline: none" ></asp:TextBox>
                <button type="button" runat="server" id="btnPopup" class="search-popup fa fa-search" style="background: transparent; border: none; box-shadow: none"
                    data-toggle="modal" data-target="#GroupDef">
                </button>
            </div>
            <asp:HiddenField runat="server" ID="hdID" />
            <asp:HiddenField runat="server" ID="hdfSearchValue" />
            <asp:HiddenField runat="server" ID="hdfSearchText" />
            <asp:HiddenField runat="server" ID="hdfNameSearchText" />


        </ContentTemplate>
    </asp:UpdatePanel>

</div>
<asp:Panel runat="server" class="modal fade" ID="GroupDef" data-backdrop="static" role="dialog">
    <div class="modal-dialog <%=Size %>">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title"><%=Resources.labels.groupDefinitionViewForm %></h4>
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
                                        <div class="row col-sm-12" style="text-align: center; margin: auto;">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%=Resources.labels.accountinggroup%></label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtGroupID" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-5 control-label"><%=Resources.labels.modulename%></label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtModuleName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row col-sm-12">
                                                <div>
                                                    <asp:Button ID="btnSearch" runat="server" data-close="<%GroupDef.ClientID%>" CssClass="btn btn-primary" Text="<%$ Resources:labels, timkiem %>"
                                                        OnClick="btnSearch_Click" />
                                                </div>
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
                                                                    <input name='<%="item"+GroupDef.ClientID%>' data-control='<%=hdID.ClientID %>' data-display='<%=hdfSearchText.ClientID %>' data-name='<%=hdfNameSearchText.ClientID %>' style="visibility: hidden" type="checkbox" onclick="CheckboxAll(this)">
                                                                </th>
                                                                <th><%=Resources.labels.accountinggroup%></th>
                                                                <th><%=Resources.labels.modulename%></th>
                                                                <th><%=Resources.labels.groupname%></th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="tdcheck">
                                                    <input name='<%="item"+GroupDef.ClientID%>' class="check keepvalue" value="<%#Eval("GrpID") %>" data-text="<%#Eval("GrpID") %>" data-name="<%#Eval("Module") %>" onclick="ConfigRatioPlus(this)" type="radio">
                                                </td>
                                                <td><%#Eval("GrpID") %></td>
                                                <td><%#Eval("Module") %></td>
                                                <td><%#Eval("ACGrpdef") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </table>
                                        </div> </div>
                                         <div class="margintop20 button-group panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                             <asp:Button runat="server" class="btn btn-primary" data-check="itemBranch111" ID="btnOK" OnClick="btnOK_Click" data-close="<%=GroupDef.ClientID%>" Text='<%$Resources:labels,ok %>' />
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
