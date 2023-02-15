<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="rptTest_Widget" %>
<style type="text/css">
    .custom-table > tbody > tr > td, .custom-table > tbody > tr > th, .custom-table > tfoot > tr > td, .custom-table > tfoot > tr > th, .custom-table > thead > tr > td, .custom-table > thead > tr > th {
        padding: 8px;
    }
</style>
<div class="subheader">
    <h1 class="subheader-title">
        <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>
    </h1>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%= Resources.labels.thamsobaocao %>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnViewReport">
                        <div class="row">
                            <div class="col-sm-6 col-sm-offset-3">
                                <div class="form-group">
                                    <asp:Panel ID="pnControl" runat="server" Width="100%">
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnViewReport" CssClass="btn btn-primary" Text="<%$ Resources:labels, timkiem %>" OnClientClick="return  validatedate()" OnClick="btnViewReport_Click" runat="server" />
                    <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function validatedate() {
        var startDate = $(".fromDate").val();
        var endDate = $(".toDate").val();
        var hour;
        var flag = false;
        if (startDate == undefined || endDate == undefined) {
            return true;
        } else {
            var from = startDate.split("/");
            var to = endDate.split("/");
            if (from.length >= 3 && to.length >= 3) {
                var h = from[2].split(" ");
                var hto = to[2].split(" ");
                if (h.length > 1 && hto.length > 1) {
                    var hourfrom = h[1].split(":");
                    var hourto = hto[1].split(":");
                    if (hourfrom.length > 2 && hourto.length > 2) {
                        if (new Date(h[0], from[1], from[0], hourfrom[0], hourfrom[1], hourfrom[2]) > new Date(hto[0], to[1], to[0], hourto[0], hourto[1], hourto[2])) {
                            window.alert("To Date must be greater than or equal to From Date");
                            $(".fromDate").focus();
                            return false;
                        }
                    }
                } else {
                    var d1 = new Date(from[2], from[1] - 1, from[0]);
                    var to = endDate.split("/");
                    var d2 = new Date(to[2], to[1] - 1, to[0]);
                    if (d1 > d2) {
                        window.alert("To Date must be greater than or equal to From Date");
                        $(".fromDate").focus();
                        return false;
                    }
                }
            }
            return true;
        }
    }
</script>
