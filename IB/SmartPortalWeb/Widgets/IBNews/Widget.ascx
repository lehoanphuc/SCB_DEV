<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBNews_Widget" %>

<style>
    .h1{
        color:#0ed145;
    }
</style>

<div class="al">
    <asp:Label ID="lblTitle" runat="server"></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<div class="block1" runat="server" id ="divdieukhoansudung" visible="false">
    <br />
    <div class="content">
        <div>
                    <img src="/Images/scb-banner.png" style="height: 500px; width: 100%; padding-left: 20px; padding-right: 20px" />
        </div>
        <div style="padding-left: 5px; padding-top: 5px; padding-bottom: 5px; font-size: 14px; font-weight:bold; color:#5C80B1"">
			<asp:HyperLink Target="_blank" runat="server" ID="linkBarPdf" Text="Siam Commercial Bank iBanking Terms & Conditions Eng" NavigateUrl="~/Widgets/Help/AYA iBanking Terms and Conditions Eng v1.pdf"></asp:HyperLink>
        </div>
    </div>
</div>

<div class="block1" runat="server" id ="divhionline" visible="false">
    <br />
    <div class="content">
        <div>
                    <img src="/Images/scb-banner.png" style="height: 500px; width: 100%; padding-left: 20px; padding-right: 20px" />
        </div>
    </div>
</div>

<div class="block1" runat="server" id ="divlienhe" visible="false">
    <br />
    <div class="content">
        <div>
                    <img src="/Images/scb-banner.png" style="height: 500px; width: 100%; padding-left: 20px; padding-right: 20px" />
        </div>
        <div style="padding-left: 5px; padding-top: 5px; padding-bottom: 5px; font-size: 14px; font-weight:bold; color:#5C80B1"">
            <p>Contact Us:</p>
            <p>Call Center: </p>
            <p>Email: </p>
		</div>
    </div>
</div>

<div class="block1" runat="server" id ="divhuongdansudung" visible="false">
    <br />
    <div class="content">
        <div>
                    <img src="/Images/scb-banner.png" style="height: 500px; width: 100%; padding-left: 20px; padding-right: 20px" />
        </div>
        <div style="padding-left: 5px; padding-top: 5px; padding-bottom: 5px; font-size: 14px; font-weight:bold; color:#5C80B1"">
			<asp:HyperLink Target="_blank" runat="server" ID="linkhuongdansudung" Text="User Guide Eng" NavigateUrl="~/Widgets/Help/HI Business User Guide English.pdf"></asp:HyperLink>
        </div>
    </div>
</div>

