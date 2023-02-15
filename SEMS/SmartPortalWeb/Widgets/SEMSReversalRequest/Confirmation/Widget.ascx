<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCDefinition_ADD_Widget" %>
<%@ Register Src="~/Controls/SearchTextBox/Reason.ascx" TagPrefix="uc1" TagName="Reason" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.RequestReversalTransaction %>
                </h1>
            </div>
            <div class="panel-container form-horizontal p-b-0">
                <div class="search_box">
                    <div class="row">
                        <div class="container">
                            <div class="form-group">
                                <div class="col-sm-2"></div>
                                <label class="col-sm-2 control-label required"><%=Resources.labels.ReasonforReversal %></label>
                                <div class="col-sm-6">
                                    <uc1:Reason ID="txtReason" CssClass="form-control" IsRequired="true" runat="server"></uc1:Reason>
                                </div>
                                <div class="col-sm-2"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="wrap-collabsible">
                    <div class="SearchAdvance">
                        <div class="panel-container">
                            <asp:Panel ID="pnGeneral" runat="server">
                                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                    <asp:Button ID="btConfirm" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xacnhan %>" OnClick="btConfirm_OnClick" />
                                    <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading();" OnClick="btback_OnClick" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
