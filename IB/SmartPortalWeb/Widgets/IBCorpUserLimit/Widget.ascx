<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserLimit_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="widgets/IBCorpUserLimit/css/login.css" rel="stylesheet" type="text/css" />
<link href="css/css.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="col-sm-12 "  style="padding: 0">
            <div  class="col-sm-6 al ">
                <div class="">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, thietlaphanmucgiaodichuser %>"></asp:Label><br />
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
        <div class="content padding-top10">

            <div class="u_container">
                <div class="u_inside_top">
                    <div class="col-sm-3 pading0">
                        <div class="col-xs-3 hidden-sm hidden-md hidden-lg form-group" style="text-align: right">
                            <label class="bold">
                                <%=Resources.labels.nguoisudung %>
                            </label>
                        </div>
                        <div class="col-sm-12 col-xs-8 pading0">
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
                            <asp:Repeater runat="server" ID="rptUser" OnItemCommand="rptUser_OnItemCommandommand" OnItemDataBound="rptUser_OnItemDataBounddaBound">
                                <HeaderTemplate>
                                    <table class="table table-bordered table-hover footable" data-paging="true" style="margin-bottom: 0">
                                        <thead>
                                            <tr>
                                                <th class="checkbox" style="text-align: center;">
                                                    <input id="ckhAll" type="checkbox" onclick="CheckboxAll(this)">
                                                    <label for="ckhAll"></label>
                                                </th>
                                                <th><%= Resources.labels.loaigiaodich %></th>
                                                <th data-breakpoints="xs"><%= Resources.labels.hanmuctrengiaodich %></th>
                                                <th data-breakpoints="xs md"><%= Resources.labels.tonghanmuctrenngay %></th>
                                                <th data-breakpoints="xs sm  md lg"><%= Resources.labels.sogiaodichtrenngay %></th>
                                                <th data-breakpoints="xs sm  md lg"><%= Resources.labels.tiente %></th>
                                                <th><%= Resources.labels.edit %></th>
                                                <th><%= Resources.labels.huy %></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="checkbox">
                                            <input class="check" id="<%#"check"+Container.ItemIndex %>" value="<%#Eval("USERID") +"|"+Eval("TRANCODE")+"|"+Eval("CCYID")%>" onclick="ConfigCheckbox(this)" type="checkbox">
                                            <label for="<%#"check"+Container.ItemIndex %>"></label>
                                        </td>
                                        <td><%#Eval("PAGENAME") %></td>

                                        <td><%#SmartPortal.Common.Utilities.Utility.FormatMoney(Eval("TRANLIMIT").ToString(), Eval("CCYID").ToString().Trim()) %>
                                        </td>
                                        <td><%#SmartPortal.Common.Utilities.Utility.FormatMoney(Eval("TOTALLIMITDAY").ToString(), Eval("CCYID").ToString().Trim()) %>
                                        </td>
                                        <td><%#SmartPortal.Common.Utilities.Utility.IsInt(Eval("COUNTLIMIT").ToString()) %>
                                        </td>
                                        <td><%#Eval("CCYID") %></td>
                                        <td>
                                            <asp:LinkButton ID="lbEdit" runat="server" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("USERID")+"&trcod="+Eval("TRANCODE")+"&cyid="+Eval("CCYID")%>' OnClientClick="Loading();"><%= Resources.labels.edit %></asp:LinkButton>
                                            <%--<a href="<%#"/default.aspx?po=3&p=238&a=edit&uid="+Eval("USERID")+"&trcod="+Eval("TRANCODE")+"&cyid="+Eval("CCYID") %>"><%= Resources.labels.sua %></a>--%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbDelete" runat="server" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("USERID")+"|"+Eval("TRANCODE")+"|"+Eval("CCYID")%>' OnClientClick="Loading(); return sweetAlertConfirm(this);"><%= Resources.labels.huy %></asp:LinkButton>
                                            <%--<a href="<%#"/default.aspx?po=3&p=239&uid="+Eval("USERID")+"&trcod="+Eval("TRANCODE")+"&cyid="+Eval("CCYID") %>"><%= Resources.labels.huy %></a>--%>
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
                        <div class="button-group" style="padding: 15px;">
                            <asp:Button ID="lbDeleteLimit" CssClass="btn btn-danger" runat="server" Text='<%$ Resources:labels, huy %>'  OnClick="lbDeleteLimit_OnClick"  OnClientClick="Loading(); return ConfirmDelete(this);" />
                            <asp:Button runat="server" ID="lbUserInsert" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>"
                                OnClick="lbUserInsert_Click"></asp:Button>

                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
        <script>
            onLoad();
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

            function ConfirmDelete(lbDeleteLimit) {
                var hdf = document.getElementById("<%= hdId.ClientID %>");
                if (hdf.value.trim() == "") {
                    swalWarning('<%=Resources.labels.pleaseselectbeforedeleting %>');
                    return false;
                } else {
                    return sweetAlertConfirm(lbDeleteLimit);
                }
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>

<style>
    .checkbox label::after {
        margin-left: -15px !important;
    }
    select[multiple], select[size] {
        overflow: auto !important;
        min-height: 200px !important;
    }
</style>