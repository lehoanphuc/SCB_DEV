<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBListTransWaitApprove_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<link href="Widgets/IBListTransWaitApprove/CSS/css.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
    <span><%=Resources.labels.danhsachgiaodichchoduyet %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError" style="text-align: center;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <!--chi tiet giao dich -->
        <figure>
            <legend class="handle"><%=Resources.labels.timkiemgiaodich %></legend>
            <div class="content">
                <div class="row">
                    <div class="col-xs-4 col-sm-2">
                        <label class="bold"><%= Resources.labels.sogiaodich %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtTranID" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <label class="bold"><%= Resources.labels.debitaccount %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:DropDownList ID="ddlDebitAcct" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4 col-sm-2">
                        <label class="bold"><%= Resources.labels.tungay %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtFromDate" CssClass="dateselect" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-4 col-sm-2">
                        <label class="bold"><%= Resources.labels.denngay %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:TextBox ID="txtToDate" CssClass="dateselect" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4 col-sm-2">
                        <label class="bold"><%= Resources.labels.loaigiaodich %></label>
                    </div>
                    <div class="col-xs-8 col-sm-4">
                        <asp:DropDownList ID="ddlTransactionType" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12" style="text-align: center; margin-top: 10px;">
                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" Width="100px"
                            OnClick="btnSearch_Click" />&nbsp;
                        <asp:Button ID="btnApprove" CssClass="btn btn-success" runat="server" Text="<%$ Resources:labels, duyet %>" Width="100px" OnClick="btnApprove_Click" disabled="disabled" />
                        &nbsp;
                        <asp:Button ID="btnReject" CssClass="btn btn-danger" runat="server" Text="<%$ Resources:labels, khongduyet %>" Width="100px"
                            OnClick="btnReject_Click" disabled="disabled" />
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </figure>
        <figure>
            <legend class="handle"><%=Resources.labels.danhsachgiaodichchoduyet %></legend>
            <div class="content">
                <asp:Literal runat="server" ID="ltrError"></asp:Literal>
                <div style="width: 100%; overflow: auto;">
                    <asp:Repeater ID="gvLTWA" runat="server" OnItemDataBound="gvLTWA_OnItemDataBound" OnItemCreated="gvLTWA_ItemCreated" OnItemCommand="gvLTWA_OnItemCommand" >
                        <HeaderTemplate>
                            <table class="table table-bordered table-hover footable" data-paging="true">
                                <thead>
                                    <tr>
                                        <th style="text-align: center;">
                                            <asp:CheckBox ID="ckhAll" runat="server" />
                                        </th>
                                        <th><%= Resources.labels.sogiaodich %></th>
                                        <th>Transaction</th>
                                        <th><%= Resources.labels.ngaygiogiaodich %></th>
                                        <th><%= Resources.labels.debitaccount %></th>
                                        <th><%= Resources.labels.sotien %></th>
                                        <th><%= Resources.labels.phi %></th>
                                        <th><%= Resources.labels.mota %></th>
                                        <th><%= Resources.labels.trangthai %></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="hpTranID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("IPCTRANSID")+"|" +Eval("IPCTRANCODE")%>'>[hpDetails]</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Label ID="tranname" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblCCYID" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFeeAmount" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    <asp:Label ID="lblTranCode" Visible="False" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
			</table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </figure>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function checkall(el) {
        if ($(el).is(":checked")) {
            $(".table [id*=cbxSelect]").prop('checked', 'checked');
        } else {
            $(".table [id*=cbxSelect]").prop('checked', false);
        }
        check();
    };

    function checkitem() {
        if ($(".table [id*=cbxSelect]").length == $(".table [id*=cbxSelect]:checked").length) {
            $(".table [id*=ckhAll]").prop('checked', 'checked');
        } else {
            $(".table [id*=ckhAll]").prop('checked', false);
        }
        check();
    }
    function check() {
        var ck = true;
        $(".table [id*=cbxSelect]").each(function () {
            if ($(this).is(":checked"))
                ck = false;
        });
        var app = document.getElementById('<%=btnApprove.ClientID%>');
        var rej = document.getElementById('<%=btnReject.ClientID%>');
        if (ck) $(app).attr("disabled", true);
        else $(app).removeAttr("disabled");
        if (ck) $(rej).attr("disabled", true);
        else $(rej).removeAttr("disabled");
    }
</script>
