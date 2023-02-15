<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserApproveLimit_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>


<script src="JS/mask.js" type="text/javascript"></script>

<script src="JS/docso.js" type="text/javascript"></script>

<link href="widgets/IBCorpUserApproveLimit/css/login.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="bg-wh">
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-sm-12 "  style="padding: 0">
                <div  class="col-sm-6 al ">
                    <div class="">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, thietlaphanmucduyetgiaodich %>"></asp:Label><br />
                        <img style="margin-bottom: 5px;" src="Images/WidgetImage/underline.png" />
                    </div>
                </div>
                <div  class="col-sm-6 al " style="padding:8px 0">
                    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="Images/WidgetImage/ajaxloader.gif" style="width: 16px; height: 16px;" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                
            </div>
            <div class="content">
                <div class="u_container">
                    <div class="u_inside_top">
                        <div class="col-sm-3 pading0">
                            <div class="col-xs-3 hidden-sm hidden-md hidden-lg form-group" style="text-align: right">
                                <label class="bold">
                                    <%=Resources.labels.nguoisudung %>
                                </label>
                            </div>
                            <div class="col-sm-12 col-xs-9 pading0">
                                <asp:ListBox ID="lstDept" runat="server" CssClass="form-control margin-bt15"
                                    AutoPostBack="True" OnSelectedIndexChanged="lstDept_SelectedIndexChanged"></asp:ListBox>
                            </div>
                 
                            <div class="clearfix"></div>
                        </div>
                            <div class="col-sm-9">

                                <div style="text-align: center;">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                </div>
                                    <div class="content" style="overflow: auto;">
                                    <asp:Literal runat="server" ID="ltrError"></asp:Literal>
                                    <asp:Repeater ID="gvUser" runat="server" OnItemDataBound="gvUser_OnItemDataBound" OnItemCommand="gvUser_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-bordered table-hover footable" data-paging="true">
                                                <thead>
                                                    <tr>
                                                        <th class="checkbox" style="text-align: center;">
                                                            <input id="ckhAll" type="checkbox" onclick="CheckboxAll(this)">
                                                            <label for="ckhAll"></label>
                                                        </th>
                                                        <%--<asp:CheckBox ID="ckhAll" runat="server" />--%>
                                                        <th><%= Resources.labels.giaodich %></th>
                                                        <th><%= Resources.labels.duyethanmuc %></th>
                                                        <th><%= Resources.labels.duyethanmucngay %></th>
                                                        <th><%= Resources.labels.tiente %></th>
                                                        <th><%= Resources.labels.edit %></th>
                                                        <th><%= Resources.labels.huy %></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%--<asp:CheckBox ID="cbxSelect" runat="server" />--%>
                                                <td class="checkbox" style="text-align: center;">
                                                    <input class="check" id="<%#"check"+Container.ItemIndex %>" value="<%#Eval("USERID")+"|" +Eval("TRANCODE") +"|" +Eval("CCYID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                                                    <label for="<%#"check"+Container.ItemIndex %>"></label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTrans" runat="server"></asp:Label>
                                                    <asp:Label ID="lblTRANCODE" runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblUID" runat="server" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLimit" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLimitday" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbEdit" runat="server" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("USERID")+"|" +Eval("TRANCODE") +"|" +Eval("CCYID")%>' OnClientClick="Loading();"><%= Resources.labels.edit %></asp:LinkButton>
                                                    <%--<asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>--%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("USERID")+"|" +Eval("TRANCODE") +"|" +Eval("CCYID")%>' OnClientClick="Loading(); return sweetAlertConfirm(this);"><%= Resources.labels.huy %></asp:LinkButton>
                                                    <%--<asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>--%>
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

                                <div class="button-group" style="padding: 15px; text-align: center">
                                    <asp:Button runat="server" ID="lbDeleteLimit" CssClass="btn btn-danger" Text="<%$ Resources:labels, huy %>"
                                        OnClick="lbUserDelete_Click" OnClientClick="Loading(); return ConfirmDelete(this);"></asp:Button>

                                    <asp:Button runat="server" ID="lbUserInsert" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>"
                                        OnClick="lbUserInsert_Click"></asp:Button>
                                </div>
                            </div>

                    </div>


                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</div>


<script>
    onLoad();
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

<style>
    .content {
        background: no-repeat !important;
    }

    select[multiple], select[size] {
     overflow: auto !important;
    min-height: 200px !important;
    }
</style>
