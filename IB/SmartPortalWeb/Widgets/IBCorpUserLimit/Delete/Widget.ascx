<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserLimit_Delete_Widget" %>


<link href="css/css.css" rel="stylesheet" type="text/css" />
<div class="al">
    <asp:Label ID="Label7" runat="server"
        Text="<%$ Resources:labels, thietlaphanmucthuchiengiaodich %>"></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:Panel runat="server" ID="pnRole">
    <figure>
        <legend class="handle"><%=Resources.labels.thongtinxacnhan %></legend>
        <div class="content" style="text-align:center">
            <label class="bold"><%=Resources.labels.bancochacchanmuonhuykhong %></label>
            <div class="button-group">
                
                <asp:Button ID="btback" runat="server" CssClass="btn btn-warning" PostBackUrl="javascript:history.go(-1)"
                            Text="<%$ Resources:labels, quaylai %>" />
                &nbsp;
                

                <asp:Button ID="btsaveandcont" CssClass="btn btn-danger" runat="server" OnClick="btsaveandcont_Click"
                            Text="<%$ Resources:labels, huy %>" Width="71px" />
            </div>
        </div>
    </figure>

</asp:Panel>

