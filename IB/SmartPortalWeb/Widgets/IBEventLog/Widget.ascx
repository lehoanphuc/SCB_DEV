<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBEventLog_Widget" %>

<link rel="stylesheet" type="text/css" href="CSS/css.css" />
<style>
    .al {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <div class="cssProgress">
            <div class="progress1">
                <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<div class="al">
    <span><%=Resources.labels.nhatkytruycap %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<asp:Panel ID="Panel1" runat="server" class="divcontent">
            <figure>
                 <div id="divError">
                     <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
                <legend class="handle"><%=Resources.labels.transactionsearch1year %></legend>
                <div class="content search">
                    <label class="col-xs-3 col-sm-1 bold"><%= Resources.labels.fromdate %></label>
                    <div class="col-xs-9 col-sm-4">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control pull-right dateselect" autocomplete="off" data-name="date1" data-level="0"></asp:TextBox>

                    </div>
                    <label class="col-xs-3 col-sm-1 bold"><%= Resources.labels.todate %></label>
                    <div class="col-xs-9 col-sm-4">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control pull-right dateselect" autocomplete="off" data-name="date1" data-level="1"></asp:TextBox>

                    </div>
                    <div class="col-xs-3 hidden-sm hidden-md hidden-lg"></div>
                    <div class="col-sm-2 col-xs-9">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-right" Text="<%$ Resources:labels, view %>" OnClick="Button2_Click" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </figure>
        <div style="margin-top:10px;">
            <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
            <asp:Repeater runat="server" ID="rptLog">
                <HeaderTemplate>
                    <table class="table table-bordered table-hover footable" data-paging="true" style="background-color: white; border-color: rgb(204, 204, 204); border-width: 1px; border-style: none; border-collapse: collapse; max-width: 1200px; display: table;">
                        <thead>
                            <tr>
                                <th class="thtdff"><%=Resources.labels.ngay %></th>
                                <th class="thtd"><%=Resources.labels.time %></th>
                                <th class="thtd"><%=Resources.labels.mota %></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>
                                <td class="thtdff">
                                    <%#DataBinder.Eval(Container.DataItem, "Time", "{0:d/M/yy}")%></td>
                                <td class="thtd">
                                    <%#DataBinder.Eval(Container.DataItem, "Time", "{0:HH:mm:ss}")%></td>
                                <td class="thtd"><%#Eval("Desc") %></td>
                            </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
</asp:Panel>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script>
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                onReady();
            }
        });
    };
</script>
