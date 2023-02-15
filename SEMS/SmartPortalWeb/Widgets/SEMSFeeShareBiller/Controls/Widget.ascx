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
        <asp:Panel ID="pnFee1" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.thongtinphi%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.FeeShareCode %></label>
                                            <div class="col-sm-7 col-xs-12">
                                                <asp:DropDownList ID="ddlFeeShareCode" CssClass="form-control select2 infinity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-5 col-xs-12 control-label required"><%=Resources.labels.billername %></label>
                                            <div class="col-sm-7 col-xs-12">
                                                <asp:DropDownList ID="ddlBillerID" CssClass="form-control select2 infinity" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="txtChoose" runat="server" CssClass="btn btn-primary" Text="Choose" OnClick="txtChoose_Click" />
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="row">
            <div class="col-sm-12">
                <asp:Panel class="panel" runat="server" ID="pnFeeShareDetail">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.chitietphi%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnRegion" runat="server">
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-5 col-xs-12 control-label"><%=Resources.labels.phibacthang %></label>
                                            <div class="col-sm-5 col-xs-12">
                                                <asp:CheckBox ID="chbIsLadder" runat="server" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%=Resources.labels.loaigiaodich %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlTransactionType" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                            <asp:Label ID="lbFeeShareType" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-5 col-xs-12 control-label required"><%=Resources.labels.tu %></label>
                                            <div class="col-sm-7 col-xs-12">
                                                <asp:TextBox ID="txtFrom" Text="0" MaxLength="21" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-5 col-xs-12 control-label required"><%=Resources.labels.den %></label>
                                            <div class="col-sm-7 col-xs-12">
                                                <asp:TextBox ID="txtTo" CssClass="form-control" MaxLength="21" runat="server" Text="0"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-12 col-xs-12">
                                                <asp:CheckBox ID="cbToLimit" OnCheckedChanged="cbToLimit_OnCheckedChanged" AutoPostBack="True" runat="server" />
                                                <%=Resources.labels.unlimit %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.BeneficiarySide %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlBeneficiarySide" CssClass="form-control select2" Style="width: 100%;" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.ShareType %></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ddlShareType" CssClass="form-control select2" Style="width: 100%;" OnTextChanged="loadShareSide" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%=Resources.labels.Percentage%></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPercentage" MaxLength="6" CssClass="form-control" onkeypress="return FormatPercent2(this,event,6)" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1" style="padding-left: 6px; padding-top: 7px">
                                                <label class="fa fa-percent">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label"><%=Resources.labels.FlatAmount %></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtFlatAmount" MaxLength="24" CssClass="form-control" onkeypress="return FlatNumberFixed(this,event,24)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-5 control-label required"><%=Resources.labels.Priority %></label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtPriority" CssClass="form-control" Style="width: 100%;" MaxLength="2" onKeyPress="return onlyDotsAndNumbers(this, event,3);" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-5">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btsave_Click" />
                            <asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                        </div>

                        <panel runat="server" id="pnResult">
                           
                            <div id="divResult" >
                                <asp:Panel ScrollBars="Auto" runat="server">
                                    <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand">
                                        <HeaderTemplate>
                                            <div class="pane">
                                                <div class="table-responsive">
                                                    <table class="table table-hover footable c_list">
                                                        <thead class="thead-light repeater-table">
                                            <tr>
                                                <th class="title-repeater"><%=Resources.labels.tu%></th>
                                                <th class="title-repeater"><%=Resources.labels.den%></th>
                                                <th class="title-repeater"><%=Resources.labels.BeneficiarySide%></th>
                                                <th class="title-repeater"><%=Resources.labels.ShareType%></th>
                                                <th class="title-repeater"><%=Resources.labels.FlatAmount%></th>
                                                <th class="title-repeater"><%=Resources.labels.Percentage%></th>
                                                <th class="title-repeater"><%=Resources.labels.Priority%></th>
                                                <th class="title-repeater"> <%=Resources.labels.edit%><asp:Label runat="server" id="thedit"></asp:Label></th>
                                                <th id="thdelete" class="title-repeater" ><%=Resources.labels.delete%></th>
                                                
                                            </tr>
                                        </thead>
                                                        <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                            <tr>
                                
                                <td class="tr-boder" id="txtFromLimittb"> <%#Eval("FromLimit")%></td>
                                <td class="tr-boder"  id="txtToLimittb"><%#Eval("ToLimit") %></td>
                                <td class="tr-boder"  id="txtBenSidetb" ><%#Eval("BenSideName") %></td>
                                <td class="tr-boder"  id="txtShareTypetb" ><%#Eval("ShareTypeName") %></td>
                                <td class="tr-boder"  id="txtFlatAmounttb" style="text-align: right"><%#string.Format("{0:#,0.00;;''}", decimal.Parse(Eval("FlatAmount").ToString()))%></td>
                                <td class="tr-boder"  id="txtPerrcentTb" style="text-align: right"><%#string.Format("{0:0.00 % ;0.00 %;''}", decimal.Parse(Eval("Percentage").ToString())) %></td>
                                <td class="tr-boder"   id="txtPriorityTb" style="text-align: right"><%#Eval("Priority") %></td>
                                <%--<td class="tr-boder"><%#Eval("Description") %></td>--%>
                                <td id="tdedit" class="tr-boder">
                                <asp:LinkButton ID="lbEdit" runat="server" class="btn btn-primary" CommandArgument='<%#Eval("FeeShareTypeID") + "+" + Eval("FromLimit").ToString() + "+" + Eval("ToLimit").ToString()  + "+" + Eval("ShareType").ToString()+ "+" + Eval("BenSide").ToString()%>' CommandName="edit">Edit</asp:LinkButton>
                            </td>
                            <td id="tddelete" class="tr-boder">
                                <asp:LinkButton ID="lbDelete" runat="server" class="btn btn-secondary" CommandArgument='<%#Eval("FeeShareTypeID") + "+" + Eval("FromLimit").ToString() + "+" + Eval("ToLimit").ToString()  + "+" + Eval("ShareType").ToString()+ "+" + Eval("BenSide").ToString()%>' CommandName="delete" OnClientClick="return ConfirmDelete('Are you sure you want to delete this entry?');">Delete</asp:LinkButton>
                            </td>
                            </tr>
                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                            </table>
                                            </div> </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
                         
                                </asp:Panel>
                            </div>
                        </panel>


                    </div>
                </asp:Panel>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<%--<script src="/JS/Common.js"></script>--%>
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
