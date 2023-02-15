<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBSTV_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="al">
    <span><%=Resources.labels.xemlichchuyenkhoan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divError">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <figure>
                <legend class="handle"><%=Resources.labels.timkiemthongtinlich %></legend>
                <div class="content display-label">
                    <div class="row form-group">
                        <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.tenlich %></label>
                        <div class="col-sm-4 col-xs-7">
                            <asp:TextBox ID="txtScheduleName" onkeypress="return this.value.length<=50" runat="server" Width="57%"></asp:TextBox>
                        </div>
                        <label class="col-sm-2 col-xs-5 bold textright-mb"><%= Resources.labels.loaigiaodich %></label>
                        <div class="col-sm-4 col-xs-7">
                            <asp:DropDownList ID="ddlTransactionType" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row button-group" style="margin-top: 10px; text-align: center">

                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, timkiem %>' OnClick="btnSearch_Click" />
                        <asp:Button ID="Button1" CssClass="btn btn-success" runat="server" Text='<%$ Resources:labels, datlich %>'
                            OnClick="Button1_Click1" />
                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text='<%$ Resources:labels, huy %>' Width="52px" OnClientClick="Loading(); return ConfirmDelete(this);"
                            OnClick="Button2_Click" />
                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>

            <!--thong tin tai khoan DD-->
            <asp:Panel ID="pnDD" runat="server">
                <figure>
                    <legend class="handle"><%=Resources.labels.danhsachlichchuyenkhoan %></legend>
                    <div class="content">
                        <asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                        <asp:HiddenField runat="server" ID="hdId" />
                        <%--<asp:Repeater runat="server" ID="rptSTV">--%>
                        <asp:Repeater runat="server" ID="rptSTV" OnItemCommand="rptSTV_OnItemCommandommand" OnItemDataBound="rptSTV_OnItemDataBounddaBound">
                            <HeaderTemplate>
                                <table class="table table-bordered table-hover footable" data-paging="true" id="tbResult" style="background-color: white; border-color: rgb(204, 204, 204); border-width: 1px; border-style: none; border-collapse: collapse; max-width: 1000px;">
                                    <thead>
                                        <tr style="background-color: #009CD4; font-weight: bold;" class="footable-header">
                                            <th class="checkbox" style="text-align: center;">
                                                <input id="ckhAll" type="checkbox" onclick="CheckboxAll(this)">
                                                <label for="ckhAll"></label>
                                            </th>
                                            <th><%= Resources.labels.tenlich %></th>
                                            <th><%= Resources.labels.kieulich %></th>
                                            <th data-breakpoints="xs"><%= Resources.labels.ngaythuchien %></th>
                                            <th><%= Resources.labels.huy %></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="checkbox" style="text-align: center">
                                        <input class="check" id="<%#"check"+Container.ItemIndex %>" value="<%#Eval("SCHEDULEID") %>" onclick="ConfigCheckbox(this)" type="checkbox">
                                        <label for="<%#"check"+Container.ItemIndex %>"></label>
                                    </td>
                                    <td>
                                        <%--<a href="<%#"/Default.aspx?po=3&p=265&sid="+Eval("SCHEDULEID")+"&trcd="+Eval("IPCTRANCODE") %>"><%#Eval("SCHEDULENAME") %></a>--%>
                                          <asp:LinkButton ID="lblview" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("SCHEDULEID")+"|"+Eval("IPCTRANCODE")%>'><%#Eval("SCHEDULENAME") %></asp:LinkButton>
                                    </td>
                                    <td><%#getScheType(Container.DataItem) %>
                                    </td>
                                    <td><%#SmartPortal.Common.Utilities.Utility.FormatDatetime(Eval("NEXTEXECUTE").ToString(),"dd/MM/yyyy HH:mm") %></td>
                                    <td>
                                        <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("SCHEDULEID")%>' OnClientClick="Loading(); return sweetAlertConfirm(this);"><%= Resources.labels.huy %></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
			</table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </figure>
                <br />
                <asp:Literal ID="litPager" runat="server"></asp:Literal>
            </asp:Panel>
            <!--end-->

        </div>
        <script src="JS/dataTables.bootstrap.min.js"></script>
        <script src="JS/jquery.dataTables.min.js"></script>
        <script>
            //$('#tbResult').DataTable();
            //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            //function EndRequestHandler(sender, args) {
            //    onReady();
            //    $('#tbResult').DataTable({
            //        "paging": true,
            //        "search": false,
            //        sorting: false
            //    });
            //}
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                onReady();
            }
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
            function ConfirmDelete(lbDelete) {
                var hdf = document.getElementById("<%= hdId.ClientID %>");
                if (hdf.value.trim() == "") {
                    swalWarning('<%=Resources.labels.pleaseselectbeforedeleting %>');
                    return false;
                } else {
                    return sweetAlertConfirm(lbDelete);
                }
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
