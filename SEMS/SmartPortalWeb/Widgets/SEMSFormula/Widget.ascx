<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSFormula_Widget" %>
<%@ Register Src="~/Controls/Formula/FormulaDevExpress.ascx" TagPrefix="uc1" TagName="FormulaDevExpress" %>


<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">Test Formula
            </h1>
        </div>
        <%--  <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>--%>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="panel">
                <div class="panel-content">
                    <asp:Panel ID="Panel1" runat="server">
                        <%--   <uc1:Formula runat="server" ID="Formula" />--%>
                        <uc1:FormulaDevExpress runat="server" ID="FormulaDevExpress" />
                    </asp:Panel>
            </div>
        </div>
        <asp:Label runat="server" ID="lblTest"></asp:Label>
        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
