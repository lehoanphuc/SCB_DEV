<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_WEBFooter_Widget" %>
<%@ Register Src="~/Controls/Poster/Poster.ascx" TagName="Poster" TagPrefix="p1" %>
<link href="widgets/IBFooter/css.css" rel="stylesheet" type="text/css" />

<p1:Poster ID="bottomPoster" Position = "bottom" runat="server"/>
<div class="bgfooter">
    <div class="footer container">
    <span class="footer_info">
        <%=Resources.labels.footerinfo %>
    </span>
</div>
</div>
<style>
    .footer_info div:nth-child(2) a:hover{
        opacity: 1;
        z-index: 99;
        color: #fff;
    }
    .footer_info div:nth-child(2) a{
        color: #fff
    }
    
</style>
