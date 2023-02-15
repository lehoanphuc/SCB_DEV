<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserManagement_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="/CSS/css.css" rel="stylesheet" type="text/css" />

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
    <asp:Label ID="Label6" Font-Bold="True" runat="server" Text='<%$ Resources:labels, nguoisudung %>'></asp:Label><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblAlert" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <figure>
            <div class="col-sm-12 col-xs-12" style="text-align: center;">
                <asp:Button ID="btnAddNew" CssClass="btn btn-success" runat="server" Text='<%$ Resources:labels, themmoi %>'
                    OnClick="btnAddNew_Click" />
            </div>
            <asp:DropDownList ID="dllDept" runat="server" Visible="False"></asp:DropDownList>
        </figure>
        <figure>
            <legend class="handle"><%=Resources.labels.nguoisudung %></legend>
            <div class="content">
                <asp:Repeater ID="gvUser" runat="server" OnItemDataBound="gvUser_OnItemDataBound" OnItemCommand="gvUser_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover footable" data-paging="true">
                            <thead>
                                <tr>
                                    <th><%= Resources.labels.tennguoidung %></th>
                                    <th><%= Resources.labels.fullname %></th>
                                    <th><%= Resources.labels.sodienthoai %></th>
                                    <th><%= Resources.labels.email %></th>
                                    <th><%= Resources.labels.trangthai %></th>
                                    <th><%= Resources.labels.edit %></th>
                                    <th><%= Resources.labels.huy %></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lblUserID" runat="server" CommandName='<%#IPC.DETAILS %>' CommandArgument='<%#Eval("USERID") %>' Text='<%#Eval("USERNAME") %>'></asp:LinkButton>
                            </td>
                            <td><%#Eval("FULLNAME") %></td>
                            <td><%#Eval("PHONE") %></td>
                            <td><%#Eval("EMAIL") %></td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbEdit" runat="server" CommandName='<%#IPC.EDIT %>' CommandArgument='<%#Eval("USERID") %>'>Edit</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.DELETE %>' CommandArgument='<%#Eval("USERID") %>' OnClientClick="Loading(); return sweetAlertConfirm(this);">Delete</asp:LinkButton>
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
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        onReady();
    }
</script>

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
        if (document.getElementById('<%=lblAlert.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblAlert.ClientID%>').innerHTML = '';
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
