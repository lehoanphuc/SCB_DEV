<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Group_Delete_Widget" %>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.deletetrole %>
    </h1>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <asp:Label ID="lblConfirm" runat="server" Text="<%$ Resources:labels, thongtinxacnhan %>"></asp:Label>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content text-center">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblError" runat="server" Text="<%$ Resources:labels, areyousuredeletethisrecord %>"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnDelete" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnExit_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
