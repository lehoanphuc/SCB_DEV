<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUser_Detele_Widget" %>

<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <asp:Label ID="lbxacnhan" runat="server" Text="<%$ Resources:labels, xacnhan %>"></asp:Label>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content text-center" id="divconfirm" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <h1 class="subheader-title" style ="text-align:center">
                                <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                            </h1>
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, banchacchanmuonxoanguoisudungnaykhong %>"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btsaveandcont" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btsaveandcont_Click" />
                    <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                </div>
            </div>
        </div>
    </div>
</div>

