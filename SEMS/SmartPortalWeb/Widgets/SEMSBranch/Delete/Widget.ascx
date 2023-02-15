<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBranchDelete_Widget" %>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.xoachinhanh %>
    </h1>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <asp:Label ID="lblConfirm" runat="server" Text="<%$ Resources:labels, xacnhan %>"></asp:Label>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content text-center">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblError" runat="server" Text="<%$ Resources:labels, banchacchanmuonxoachinhanh %>"></asp:Label>
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




