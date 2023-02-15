<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCMerchantReason_Widget" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Controls/SearchTextBox/Reason.ascx" TagPrefix="uc1" TagName="Reason" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script>

</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
          <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.KYCConsumerReason%>
                </h1>
            </div>
        <div class="panel-container">
            <div class="panel-content form-horizontal p-b-0">
            <asp:Panel ID="panel" runat="server">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.ReasonName %></label>
                        <div class="col-sm-8">
                            <uc1:Reason ID="txtReason" CssClass="form-control" IsRequired="true" runat="server"></uc1:Reason>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="col-sm-4 control-label"><%=Resources.labels.desc %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDescription" CssClass="form-control" IsRequired="true" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            </asp:Panel>
            </div>
        </div>
        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
            <asp:Button ID="btnApprove" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, Complete %>" OnClick="btnComplete_Click" />
            <asp:Button ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_Click"/>
        </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>