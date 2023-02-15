<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGIONFEE_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <%@ Import Namespace="SmartPortal.Constant" %>
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"></asp:Label>
            </h1>
        </div>
         <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnRegion" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.TransactionName %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlTransactionName" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" OnTextChanged="ddlTransactionName_TextChanged" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.FeeShareCode %></label>
                                            <div class="col-sm-7">
                                                 <asp:DropDownList ID="ddlFeeShareCode" CssClass="form-control select2 infinity" runat="server" >
                                                        </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </asp:Panel>
                        </div>

                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btsave_Click" />
                            <asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script>
    $(document).on('keydown', 'input[pattern]', function (e) {
        var input = $(this);
        var oldVal = input.val();
        var regex = new RegExp(input.attr('pattern'), 'g');

        setTimeout(function () {
            var newVal = input.val();
            if (!regex.test(newVal)) {
                input.val(oldVal);
            }
        }, 0);
    });
</script>

