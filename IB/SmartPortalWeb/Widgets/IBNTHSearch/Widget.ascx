<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBNTHSearch_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="../../Controls/LetterSearch/LetterSearch.ascx" TagName="LetterSearch" TagPrefix="uc1" %>

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
    <span><%=Resources.labels.quanlynguoithuhuong %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divError">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <asp:Panel ID="Panel1" runat="server" class="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.timkiemthongtinnguoithuhuong %></legend>
                <div class="content dislay-label">
                    <div class="row form-group">
                        <div class="col-xs-5 col-sm-2">
                            <label class="bold"><%= Resources.labels.tennguoithuhuong %></label>
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtReceiver" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-5 col-sm-2">
                            <label class="bold"><%= Resources.labels.taikhoan %></label>
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:TextBox ID="txtAccount" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-5 col-sm-2">
                            <label class="bold"><%= Resources.labels.loaigiaodich %></label>
                        </div>
                        <div class="col-xs-7 col-sm-4">
                            <asp:DropDownList ID="ddltrans" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="button-group">
                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, timkiem %>' OnClick="btnSearch_Click" />
                    <asp:Button ID="btnAddNew" CssClass="btn btn-success" runat="server" Text='<%$ Resources:labels, themmaumoi %>' OnClick="btnAddNew_Click" />
                    <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text='<%$ Resources:labels, huy %>' OnClientClick="Loading(); return ConfirmDelete(this);" OnClick="btnDelete_Click" />
                    <div class="clearfix"></div>
                </div>
            </figure>
        </asp:Panel>

        <asp:Panel ID="pnResult" runat="server" class="divcontent">
            <figure>
                <legend class="handle"><%=Resources.labels.danhsachnguoithuhuong %></legend>
                <div class="content" style="overflow: auto;">
                    <asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                    <%--<asp:Repeater runat="server" ID="rptProcessList">--%>
                    <asp:Repeater runat="server" ID="rptProcessList" OnItemCommand="rptProcessList_OnItemCommandommand" OnItemDataBound="rptProcessList_OnItemDataBounddaBound">
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover footable" data-paging="true">
                                <thead>
                                    <tr>
                                        <th class="checkbox" style="text-align: center;">
                                            <input id="ckhAll" type="checkbox" onclick="CheckboxAll(this)">
                                            <label for="ckhAll"></label>
                                        </th>
                                        <th><%= Resources.labels.tennguoithuhuong %></th>
                                        <th data-breakpoints="xs"><%= Resources.labels.taikhoan %></th>
                                        <th data-breakpoints="xs"><%= Resources.labels.loaigiaodich %></th>
                                        <th data-breakpoints="xs sm"><%= Resources.labels.mota %></th>
                                        <th><%= Resources.labels.huy %></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="checkbox" style="text-align: center">
                                    <input class="check" id="<%#"check"+Container.ItemIndex %>" value="<%#Eval("ID") %>" onclick="ConfigCheckbox(this)" type="checkbox">
                                    <label for="<%#"check"+Container.ItemIndex %>"></label>
                                </td>
                                <td>
                                    <%--<a href="<%#"/default.aspx?po=3&p=287&nid="+Eval("ID") %>"><%#Eval("RECEIVERNAME") %></a>--%>
                                      <asp:LinkButton ID="lblview" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("ID")%>'><%#Eval("RECEIVERNAME") %></asp:LinkButton>
                                </td>
                                <td><%#Eval("ACCTNO") %>
                                </td>
                                <td><%#Eval("PAGENAME") %></td>
                                <td><%#Eval("DESCRIPTION") %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("ID")%>' OnClientClick="Loading(); return sweetAlertConfirm(this);"><%= Resources.labels.huy %></asp:LinkButton>
                                    <%--<a href="<%#"/default.aspx?po=3&p=263&nid="+Eval("ID") %>"><%= Resources.labels.huy %></a>--%>
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
        </asp:Panel>
        <script>
            onLoad();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                onLoad();
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

