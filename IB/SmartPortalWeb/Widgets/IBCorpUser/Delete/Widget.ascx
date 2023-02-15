<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUser_Detele_Widget" %>


<link href="CSS/css.css" rel="stylesheet" />

<div class="th">
    <span><%=Resources.labels.huythongtinnguoidung %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<div id="divError" style="text-align: center;">
    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
</div>
<!-- Thong tin nguoi dai dien-->
<asp:Panel runat="server" ID="pnRole">
    <figure>
        <legend class="handle"><%=Resources.labels.huynguoisudung %></legend>
        <div class="content">
            <div class="col-sm-12 col-xs-12" style="text-align: center;">
                <label class="bold"><%=Resources.labels.banchacchanmuonhuynguoisudungkhong %></label>
            </div>
            <div class="col-sm-12 col-xs-12" style="text-align: center; padding-top: 10px;">
                <asp:Button ID="btback" runat="server" CssClass="btn btn-warning" PostBackUrl="javascript:history.go(-1)"
                            Text="<%$ Resources:labels, quaylai %>" />
                <asp:Button ID="btsaveandcont" CssClass="btn btn-danger" runat="server" OnClick="btsaveandcont_Click"
                    Text="<%$ Resources:labels, huy %>" />
            </div>
        </div>
    </figure>
</asp:Panel>

