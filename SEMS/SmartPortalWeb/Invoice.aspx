<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Widgets_Invoice_Invoice" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<style>
    .print{
        width: 1000px;
        margin: 0 auto;
    }
    #StiWebViewer1{
        height:auto !important;
    }
</style>

<div class="print">
    <form runat="server">
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" RenderMode="AjaxWithCache" Theme="Windows7" />
    </form>
</div>
