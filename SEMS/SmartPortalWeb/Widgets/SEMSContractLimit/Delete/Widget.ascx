<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractLimit_Delete_Widget" %>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.xoahanmuchopdong %>
    </h1>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <asp:Label ID="lblConfirm" runat="server" Text="<%$ Resources:labels, confirm %>"></asp:Label>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content text-center">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblKq" runat="server" Text="<%$ Resources:labels, areyousuredeletethisrecord %>"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    &nbsp;<asp:Button ID="btsaveandcont" runat="server" CssClass="btn btn-primary" OnClick="btsaveandcont_Click" Text="<%$ Resources:labels, delete %>" Width="71px" />
                    &nbsp;<asp:Button ID="btback" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
