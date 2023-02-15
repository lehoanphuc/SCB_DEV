<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserApproveProcess_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<div class="al">
    <asp:Label ID="Label6" Font-Bold="True" runat="server" Text='<%$ Resources:labels, thietlapquytrinhduyetgiaodich %>'></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <asp:Label ID="lblContractNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
            <figure>
                <legend class="handle"><%=Resources.labels.thongtintimkiem %></legend>
                <div class="content display-label">
                    <div class="row form-group">
                        <div class="col-xs-4 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.maquytrinh %>
                            </label>
                        </div>
                        <div class="col-xs-8 col-sm-4">
                            <asp:TextBox ID="txtapptranID" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-4 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.loaigiaodich %>
                            </label>
                        </div>
                        <div class="col-xs-8 col-sm-4">
                            <asp:DropDownList ID="ddltrans" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-xs-4 col-sm-2">
                            <label class="bold">
                                <%= Resources.labels.tiente %>
                            </label>
                        </div>
                        <div class="col-xs-8 col-sm-4">
                            <asp:DropDownList ID="ddlCCYID" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" OnClientClick="Loading();" />
                        <asp:Button ID="btnAddNew" CssClass="btn btn-success" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" OnClientClick="Loading();" />
                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="<%$ Resources:labels, huy %>" OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete(this);" />
                    </div>
                </div>
            </figure>
        </asp:Panel>


        <div>
            <figure>
                <legend class="handle"><%=Resources.labels.thongtinchitiet %></legend>
                <div class="content display-label">
                    <asp:Repeater runat="server" ID="rptProcessList" OnItemCommand="rptProcessList_ItemCommand" OnItemDataBound="rptProcessList_OnItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover footable" data-paging="true">
                                <thead>
                                    <tr>
                                        <th class="checkbox" style="text-align: center;">
                                            <input id="ckhAll" type="checkbox" onclick="CheckboxAll(this)">
                                            <label for="ckhAll"></label>
                                        </th>
                                        <th><%= Resources.labels.maquytrinh %></th>
                                        <th data-breakpoints="xs"><%= Resources.labels.loaigiaodich %></th>
                                        <th data-breakpoints="xs"><%= Resources.labels.accountnumber %></th>
                                        <th data-breakpoints="xs"><%= Resources.labels.tu %></th>
                                        <th data-breakpoints="xs"><%= Resources.labels.den %></th>
                                        <th data-breakpoints="xs"><%= Resources.labels.tiente %></th>
                                        <th><%= Resources.labels.edit %></th>
                                        <th><%= Resources.labels.huy %></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="checkbox" style="text-align: center;">
                                    <input class="check" id="<%#"check"+Container.ItemIndex %>" value="<%#Eval("WorkflowID") %>" onclick="ConfigCheckbox(this)" type="checkbox">
                                    <label for="<%#"check"+Container.ItemIndex %>"></label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbDetails" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("WorkflowID")%>' OnClientClick="Loading();"><%#Eval("WorkflowID") %></asp:LinkButton>
                                </td>
                                <td><%#Eval("PAGENAME") %></td>
                                <td><%#Eval("AcctNo") %></td>
                                <td style="text-align: right"><%#SmartPortal.Common.Utilities.Utility.FormatMoney(Eval("FromLimit").ToString(), Eval("CCYID").ToString().Trim()) %></td>
                                <td style="text-align: right"><%#SmartPortal.Common.Utilities.Utility.FormatMoney(Eval("ToLimit").ToString(), Eval("CCYID").ToString().Trim()) %></td>
                                <td><%#Eval("CCYID") %></td>
                                <td>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("WorkflowID")%>' OnClientClick="Loading();"><%= Resources.labels.edit %></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("WorkflowID")%>' OnClientClick="Loading(); return sweetAlertConfirm(this);"><%= Resources.labels.huy %></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
			</table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:HiddenField runat="server" ID="hdId" />

                </div>
            </figure>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>

    function ConfigCheckbox(el) {

        var lst = $('#<%=hdId.ClientID%>').val();
        if ($(el).is(":checked")) {
            lst += '#' + $(el).val();
            if (ischeckall()) {
                $('#ckhAll').prop('checked', el.checked);
            }
        }
        else {
            lst = lst.replace('#' + $(el).val(), '');
            $('#ckhAll').prop('checked', el.checked);
        }
        $('#<%=hdId.ClientID%>').val(lst);

    }
    function CheckboxAll(el) {
        $('input:checkbox').not(el).prop('checked', el.checked);
        var lst = '';
        if ($(el).is(":checked")) {
            var lstCheck = $('.check');
            $.each(lstCheck, function (index, item) {
                lst += '#' + $(item).val();
            });
        }
        $('#<%=hdId.ClientID%>').val(lst);
    }
    function ischeckall() {
        var lstCheck = $("input.check:checkbox:not(:checked)");
        return lstCheck == null || lstCheck.length == 0;
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function ConfirmDelete(btnDelete) {
        var hdf = document.getElementById("<%= hdId.ClientID %>");
        if (hdf.value.trim() == "") {
            swalWarning('<%=Resources.labels.pleaseselectbeforedeleting %>');
            return false;
        } else {
            return sweetAlertConfirm(btnDelete);
        }
    }
</script>
