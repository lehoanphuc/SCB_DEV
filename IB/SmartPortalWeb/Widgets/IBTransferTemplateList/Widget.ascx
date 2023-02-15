<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBTransferTemplateList_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>


<div class="al">
    <span><%=Resources.labels.quanlymauchuyenkhoan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <figure>
            <legend class="handle"><%=Resources.labels.timkiemmauchuyenkhoan %></legend>
            <div class="content display-label">
                <div class="row form-group">
                    <div class="col-xs-3 col-sm-2">
                        <label class="bold">
                            <%= Resources.labels.tenmau %>
                        </label>
                    </div>
                    <div class="col-xs-9 col-sm-4">
                        <asp:TextBox ID="txtTempName" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-3 col-sm-2">
                        <label class="bold">
                            <%= Resources.labels.loaigiaodich %>
                        </label>
                    </div>
                    <div class="col-xs-9 col-sm-4">
                        <asp:DropDownList ID="ddlTransactionType" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, timkiem %>' OnClick="btnSearch_Click" OnClientClick="Loading();" />
                <asp:Button ID="btnAddNew" CssClass="btn btn-success" runat="server" Text='<%$ Resources:labels, themmoi %>' OnClick="btnAddNew_Click" OnClientClick="Loading();" />
                <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text='<%$ Resources:labels, huy %>' OnClick="btnDelete_Click" OnClientClick="Loading(); return ConfirmDelete(this);" />
            </div>
        </figure>
        <figure>
            <legend class="handle"><%=Resources.labels.danhsachmauchuyenkhoan %></legend>
            <div class="content">
                        <asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                <asp:Repeater runat="server" ID="rptSTV" OnItemCommand="rptSTV_ItemCommand" OnItemDataBound="rptSTV_OnItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover footable" data-paging="true">
                            <thead>
                                <tr>
                                    <th class="checkbox" style="text-align: center;">
                                        <input id="ckhAll" type="checkbox" onclick="CheckboxAll(this)">
                                        <label for="ckhAll"></label>
                                    </th>
                                    <th><%= Resources.labels.tenmau %></th>
                                    <th data-breakpoints="xs"><%= Resources.labels.debitaccount %></th>
                                    <th><%= Resources.labels.taikhoanbaoco %></th>
                                    <th data-breakpoints="xs sm"><%= Resources.labels.sotien %></th>
                                    <th data-breakpoints="xs sm"><%= Resources.labels.tiente %></th>
                                    <th><%= Resources.labels.huy %></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="checkbox" style="text-align: center;">
                                <input class="check" id="<%#"check"+Container.ItemIndex %>" value="<%#Eval("TemplateID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                                <label for="<%#"check"+Container.ItemIndex %>"></label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbDetail" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("TemplateID")+"|" +Eval("IPCTRANCODE")%>' OnClientClick="Loading();">
                                    <%#Eval("TEMPLATENAME")%>
                                </asp:LinkButton>
                            </td>
                            <td><%#Eval("SenderAccount") %></td>
                            <td><%#Eval("ReceiverAccount") %></td>
                            <td><%# SmartPortal.Common.Utilities.Utility.FormatMoney(Eval("Amount").ToString(), Eval("CCYID").ToString().Trim()) %></td>
                            <td><%#Eval("CCYID") %>
                            <td>
                                <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("TemplateID")+"|" +Eval("IPCTRANCODE")%>' OnClientClick="Loading(); return sweetAlertConfirm(this);"><%= Resources.labels.huy %></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
			</table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:HiddenField ID="hdId" runat="server"></asp:HiddenField>

            </div>
        </figure>
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
