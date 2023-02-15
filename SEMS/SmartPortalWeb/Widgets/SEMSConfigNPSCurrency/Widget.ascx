<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCONFIGNPSCURRENCY_Widget" %>
<%@ Register TagPrefix="control" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@Import namespace="SmartPortal.Constant" %>

<asp:ScriptManager runat="server" ID="ScriptManager1">
</asp:ScriptManager>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title"><%= Resources.labels.confignpscurrency %></h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label runat="server" ID="lblError"/>
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
                                            <div class="form-group col-sm-6 col-xs-12">
                                                <label class="col-sm-4 control-label col-xs-12"><%= Resources.labels.currency %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2 infinity" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_OnClick"/>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divResult" runat="server">
            <asp:Literal runat="server" ID="litError"/>
            <asp:GridView ID="gvCurrencyList" CssClass="table table-hover" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvCurrencyList_OnRowDataBound" PageSize="15" OnRowCommand="gvCurrencyList_OnRowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, currency %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbCCYID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("CCYID")%>' OnClientClick="Loading();"/>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, desc %>">
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" runat="server"/>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, status%>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"/>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, nguoisuadoi%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUserModify" runat="server"/>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("CCYID")%>' OnClientClick="Loading();">Edit</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                    </asp:TemplateField>                   
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="pager"/>
            </asp:GridView>
            <control:GridViewPaging runat="server" ID="GridViewPaging"/>
            <asp:HiddenField ID="hdCounter" Value="0" runat="server"/>
            <asp:HiddenField ID="hdPageSize" Value="15" runat="server"/>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script>
    window.onload = function () {
        document.getElementById('<%= hdCounter.ClientID %>').value = '0';
    }
        function Loading() {
            if (document.getElementById('<%= lblError.ClientID %>').innerHTML != '') {
                document.getElementById('<%= lblError.ClientID %>').innerHTML = '';
            }
        }


</script>