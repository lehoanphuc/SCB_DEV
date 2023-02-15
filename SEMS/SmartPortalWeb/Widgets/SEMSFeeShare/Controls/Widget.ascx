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
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.FeeShareCode %></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtShareFeecode" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.FeeShareName %></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtShareFeeName" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.loaigiaodich %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlTransactionType" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server" OnTextChanged="cbIsLadder_CheckedChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%=Resources.labels.phibacthang %></label>
                                            <div class="col-sm-7">
                                                <asp:CheckBox ID="cbIsLadder" runat="server"  AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%=Resources.labels.desc %></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>


                    </div>
                </div>
                <asp:Panel ID="pnBiller" CssClass="panel" runat="server">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnLadderFee" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.billername %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddBiller" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%=Resources.labels.phibacthang %></label>
                                            <div class="col-sm-5">
                                                <asp:CheckBox ID="cbIsLadderBiller" runat="server" AutoPostBack="true" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, add %>" OnClick="btnAdd_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div style="overflow-x: hidden;">
                                    <asp:Panel ScrollBars="Auto" ID="pnresulttable" runat="server">
                                        <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand" OnItemDataBound="rptData_OnItemDataBound">
                                            <HeaderTemplate>
                                                <div class="table-responsive">
                                                    <table class="table table-bordered" style="border: 1px solid rgba(0, 0, 0, 0.1); border-style: Solid;">
                                                        <thead style="background-color: #7A58BF; color: #FFF;">
                                                            <tr>
                                                                <th style="text-align: center; display:none"><%=Resources.labels.billerid%></th>
                                                                <th style="text-align: center"><%=Resources.labels.billername%></th>
                                                                <th style="text-align: center"><%=Resources.labels.phibacthang %></th>
                                                                <th style="text-align: center"><%=Resources.labels.Action%></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style=" display:none">
                                                        <asp:Label ID="txtBillerIDTable" Text='<%#Eval("BillerID") %>' runat="server"></asp:Label>
                                                       
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtBillerNameTable" Text='<%#Eval("BillerName") %>' runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtIsLadderTable" Text='<%#Eval("IsLadder") %>' runat="server"></asp:Label>
                                                    </td>
                                                    <td class="action" style="text-align: center">
                                                        <asp:LinkButton ID="linkID" runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("FeeShareTypeID")%>' CommandName='<%#IPC.ACTIONPAGE.DELETE%>' OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete</asp:LinkButton>
                                                    </td>
                                                     <td  style="display: none">
                                                        <asp:Label ID="txtFeeShareTypeIDTable" Text='<%#Eval("FeeShareTypeID") %>' runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                        </table>
                        </div> 
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
                                        <%--</div>--%>
                                    </asp:Panel>
                                </div>
                               
                            </asp:Panel>

                        </div>
                    </div>
                </asp:Panel>
                <div class="panel">
                    <div class="panel-container">
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

<%--<script>
    function fun_AllowOnlyAmountAndDot(txt, event, len) {

        var charCode = (event.which) ? event.which : event.keyCode
        if (charCode === 46) {
            if (txt.value.indexOf(".") < 0)
                return true;
            else
                return false;
        }
        if (txt.value.indexOf(".") > 0) {
            var txtlen = txt.value.length;
            var dotpos = txt.value.indexOf(".");
            //Change the number here to allow more decimal points than 2
            if ((txtlen - dotpos) > 4)
                return false;
        }
        if (txt.value.length > len) {
            if (!txt.value.includes('.'))
                txt.value = txt.value + '.';
        }
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>--%>
