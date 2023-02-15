<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_rptReport_Widget" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<style type="text/css">
    /*lazy hardcode vutt :D*/
    .insetBorder {
        background-color: white !important;
        border: 0px white !important;
    }

    .hideableFrame {
        background-image: none !important;
    }

    .crSEMS_toptoolbar_palette .td {
        /*align:center!important;*/
        padding-left: 50px !important;
    }

    img[id*="bobjid_"] {
        width: 0px !important;
    }

    div[id*="bobjid_"] {
        background-image: none !important;
    }

    div.crtoolbar {
        padding-left: 19px;
    }
    /*lazy hardcode vutt :D*/
</style>
<div class="subheader">
    <h1 class="subheader-title">
        <asp:Label ID="lblReportName" runat="server"></asp:Label>
    </h1>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <asp:HyperLink runat="server" ID="hpParam"></asp:HyperLink>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0" style="min-height: .01%; overflow-x: auto;">
                    <CR:CrystalReportViewer ID="crSEMS" runat="server" BestFitPage="True" ToolPanelView="None" HasToggleGroupTreeButton="false" HasDrilldownTabs="False"
                        ToolbarStyle-BorderStyle="None" ToolbarStyle-BorderWidth="0px" ToolbarStyle-CssClass="" ToolbarStyle-BackColor="White"
                        DisplayStatusbar="false" has="False"
                        AutoDataBind="False" HasSearchButton="True" Width="100%"
                        HasGotoPageButton="True" CssClass="luoi" HasCrystalLogo="False" />
                </div>
            </div>
        </div>
    </div>
</div>
