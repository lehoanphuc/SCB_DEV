<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBHeader_Widget" %>

<%@ Register Src="~/Controls/Poster/Poster.ascx" TagName="Poster" TagPrefix="p1" %>


<asp:Literal runat="server" ID="Literalcss"></asp:Literal>

<!--[if lt IE 9]>
<link rel="stylesheet" type="text/css" media="all" href="widgets/IBHeader/css-ie8.css"/>
<![endif]-->

<%--<script src="Template/Face2/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
    var jQueryMarquee = jQuery.noConflict();
</script>--%>
<%--<script src="widgets/IBHeader//jquery.marquee.min.js" type="text/javascript"></script>--%>

<div class='banner'>
    <div class="container" style="position: relative">
        <div class="logo hidden-xs hidden-sm">
            
            <asp:Literal runat="server" ID="ltrHome"></asp:Literal>
            <%--<img id="logo_img" src="/Images/img_corporate.png" />--%>
        </div>
    </div>
    <div class="container">
        <div class="row">
        <div class="bg-right hidden-xs hidden-sm"></div>
        <div class="login_info">
            <asp:Literal runat="server" ID="litInfo">
            </asp:Literal>
        </div>
        <div class="bg-left hidden-xs hidden-sm"></div>
        <input type="button" id="mobile_button" />
        <div id="mobile_logo">Siam Commercial Bank iBanking</div>
        <p1:Poster ID="topPoster" Position="top" runat="server" />
        </div>
    </div>
</div>



