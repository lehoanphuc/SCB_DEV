<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Activity.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_Activity" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<div class="panel-container" id="BigPnAcitivity">
    <div class="panel-content form-horizontal p-b-0">
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="card card-body">
            <div class="panel-content form-horizontal p-b-0" style="display: block;">
                <asp:Panel ScrollBars="Auto" runat="server">
                    <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                    <asp:Repeater runat="server" ID="rptData"  OnItemCommand="rptData_ItemCommand">
                        <HeaderTemplate>
                            <div class="pane">
                                <div class="table-responsive">
                                    <table class="table table-hover footable c_list" style="margin-bottom: 0px;">
                                        <thead class="thead-light repeater-table">
                                            <tr>
                                                <th class="title-repeater"><%=Resources.labels.date%></th>
                                                <th class="title-repeater"><%=Resources.labels.Action%></th>
                                                <th class="title-repeater"><%=Resources.labels.ByUser%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="tr-boder">
                                    <%#Eval("Daydata") %>
                                </td>
                                <td class="tr-boder">
                                    <%#Eval("Action") %>
                                </td>
                                <td class="tr-boder">
                                    <asp:LinkButton  runat="server" CommandArgument='<%#Eval("UserID")%>'><%#Eval("username") %></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </div> </div>
                        </FooterTemplate>
                    </asp:Repeater>
                    <%--</div>--%>
                </asp:Panel>
            </div>

        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanelGridViewPaging" UpdateMode="Always" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridViewPaging" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
        &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
        &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" style="display:none" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click1" />
    </div>
</div>


