<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Invoice.ascx.cs" Inherits="Widgets_Invoice_Invoice" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Width="1000" RenderMode="Standard" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
</div>
