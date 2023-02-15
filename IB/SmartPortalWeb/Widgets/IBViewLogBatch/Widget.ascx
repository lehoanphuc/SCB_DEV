<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBListTransWaitApprove_Widget" %>

<div class="al">
    <%=Resources.labels.nhatkygiaodichlo %><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<div id="divError" style="text-align: center;">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
<!--chi tiet giao dich -->
<figure>
    <legend class="handle"><%=Resources.labels.timkiemgiaodich %></legend>
    <div class="content">
        <div class="row">
            <div class="col-xs-5 col-sm-2">
                <label class="bold"><%= Resources.labels.magiaodich %></label>
            </div>
            <div class="col-xs-7 col-sm-4">
                <asp:TextBox ID="txtTranID" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-2">
                <label class="bold"><%= Resources.labels.accountnumber %></label>
            </div>
            <div class="col-xs-7 col-sm-4">
                <asp:TextBox ID="txtAccno" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-5 col-sm-2">
                <label class="bold"><%= Resources.labels.fromdate %></label>
            </div>
            <div class="col-xs-7 col-sm-4">
                <asp:TextBox ID="txtFromDate" CssClass="dateselect form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-5 col-sm-2">
                <label class="bold"><%= Resources.labels.todate %></label>
            </div>
            <div class="col-xs-7 col-sm-4">
                <asp:TextBox ID="txtToDate" CssClass="dateselect form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-5 col-sm-2">
                <label class="bold"><%= Resources.labels.trangthai %></label>
            </div>
            <div class="col-xs-7 col-sm-4">
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                    <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                    <asp:ListItem Value="1" Text="<%$ Resources:labels, hoanthanh %>"></asp:ListItem>
                    <asp:ListItem Value="2" Text="<%$ Resources:labels, loi %>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-xs-5 col-sm-2">
                <label class="bold"><%= Resources.labels.ketqua %></label>
            </div>
            <div class="col-xs-7 col-sm-4">

                <asp:DropDownList ID="DDLAppSta" runat="server">
                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                    <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                    <asp:ListItem Value="1" Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>
                    <asp:ListItem Value="3" Text="<%$ Resources:labels, duyet %>"></asp:ListItem>
                    <asp:ListItem Value="4" Text="<%$ Resources:labels, huy %>"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div style="padding-top: 10px; margin-left: -3px; text-align: center">
            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, timkiem %>"
                OnClick="btnSearch_Click" />
             <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success" Text="<%$ Resources:labels, duyet %>"
                OnClick="btnApprove_Click" />
            <div class="clearfix"></div>
        </div>
    </div>
</figure>
<figure>
    <legend class="handle"><%=Resources.labels.danhsachgiaodich %></legend>
    <div class="content">
        <%--<asp:Literal runat="server" ID="ltrError"></asp:Literal>--%>
        <asp:Repeater runat="server" ID="rptLTWA">
            <HeaderTemplate>
                <table class="table table-bordered table-hover footable" data-paging="true" style="background-color: white; border-color: rgb(204, 204, 204); border-width: 1px; border-style: none; border-collapse: collapse; max-width: 1000px;">
                    <thead>
                        <tr style="background-color: #009CD4; font-weight: bold;">
                            <th class="checkbox" style="text-align: center;">
                                <input id="ckhAll" type="checkbox" onclick="CheckboxAll(this)">
                                <label for="ckhAll"></label>
                            </th>
                            <th><%= Resources.labels.magiaodich %></th>
                            <th data-breakpoints="xs"><%= Resources.labels.taikhoanghino %></th>
                            <th data-breakpoints="xs"><%= Resources.labels.ngaygiogiaodich %></th>
                            <th data-breakpoints="xs sm"><%= Resources.labels.mota %></th>
                            <th><%= Resources.labels.trangthai %></th>
                            <th><%= Resources.labels.ketqua %></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="checkbox" style="text-align: center">
                        <input class="check" id="<%#"check"+Container.ItemIndex %>" value="<%#Eval("IPCTRANSID") %>" onclick="ConfigCheckbox(this)" type="checkbox">
                        <label for="<%#"check"+Container.ItemIndex %>"></label>
                    </td>
                    <td>
                        <a href="<%#"/default.aspx?po=3&p=278&tranid="+Eval("IPCTRANSID") %>"><%#Eval("IPCTRANSID") %></a>
                    </td>
                    <td><%#Eval("CHAR01") %></td>
                    <td><%#SmartPortal.Common.Utilities.Utility.IsDateTime2(Eval("IPCTRANSDATE").ToString()).ToString("dd/MM/yyyy HH:mm") %></td>
                    <td><%#Eval("TRANDESC") %></td>
                    <td><%#GetStatus(Container.DataItem) %></td>
                    <td><%#GetResult(Container.DataItem) %></td>
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
<!--end-->
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
</script>
