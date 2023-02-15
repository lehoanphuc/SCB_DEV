<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Activity.ascx.cs" Inherits="Widgets_SEMSConsumerProfile_Controls_Activity" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<div class="panel-container" id="BigPnAcitivity" runat="server">
    <div class="panel-content form-horizontal p-b-0">
        <asp:Panel ID="pnCard" runat="server">
            <div class="divToolbar">
                &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
               </div>
        </asp:Panel>
       
        <asp:Panel ScrollBars="Auto" runat="server">
            <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                <HeaderTemplate>
                    <div class="pane">
                        <div class="table-responsive">
                            <table class="table table-hover footable c_list">
                                <thead class="thead-light repeater-table">
                                    <tr>
                                        
                                        <th><%=Resources.labels.date%></th>
                                        <th><%=Resources.labels.Action%></th>
                                        <th><%=Resources.labels.ByUser%></th>
                                    </tr>
                                </thead>
                                <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        
                        <td><%#Eval("TransDate") %></td>
                        <td><%#Eval("TransDesc") %></td>
                        <td>
                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("ByUser")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("ByUser") %>
                            </asp:LinkButton>
                        </td>
                        
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                        </table>
                        </div> </div>
                </FooterTemplate>
            </asp:Repeater>
            <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
            <asp:UpdatePanel ID="UpdatePanelGrid" UpdateMode="Always" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <uc1:GridViewPaging ID="GridViewPaging" runat="server"></uc1:GridViewPaging>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="GridViewPaging" />
            </Triggers>
        </asp:UpdatePanel>
            <%--</div>--%>
        </asp:Panel>
        
    </div>
</div>
