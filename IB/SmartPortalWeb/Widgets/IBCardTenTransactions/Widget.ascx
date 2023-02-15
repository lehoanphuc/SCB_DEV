<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCardTenTransactions_Widget" %>


<style type="text/css">
    .style1 {
        width: 100%;
    }

    .thtdbold {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }

    .thtd {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }

    .thtdf {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        border-top: solid 1px #b9bfc1;
    }

    .thtdff {
        border-top: solid 1px #b9bfc1;
    }

    .thtr {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
    }

    .thtds {
        padding: 5px 5px 5px 5px;
    }

    .al {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
    }
</style>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />
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
    <%=Resources.labels.lasttentransactions %><br />
    <img src="Images/WidgetImage/underline.png" />
</div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError" style="text-align: center;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <asp:HiddenField ID="hdfCardNo" runat="server" />
        <!--chi tiet giao dich -->

        <figure id="Ownerdiv" runat="server">
            <legend class="handle">Own user card list</legend>
            <div class="content display-label">
                <div class="row">
                    <div class="col-md-5 col-sm-5">
                        <div class="row form-group">
                            <label class="col-md-3 col-xs-5 bold">Card type</label>
                            <div class="col-md-9 col-xs-7">
                                <asp:DropDownList ID="ddlCardType" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged">
                                    <asp:ListItem Text="Own user cards" Value="OWN"></asp:ListItem>
                                    <asp:ListItem Text="Other user's cards" Value="OTH"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-5">
                        <div class="row form-group">
                            <label class="col-md-3 col-xs-5 bold">Card number</label>
                            <div class="col-md-9 col-xs-7">
                                <asp:DropDownList ID="ddlAccount" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1 col-sm-2" style="text-align: center;">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, view %>' OnClick="Button1_Click" />
                    </div>
                </div>
            </div>
        </figure>
        <div style="text-align: center; font-weight: bold; color: red">
            <asp:Literal runat="server" ID="ltrError"></asp:Literal>
        </div>
        <asp:Repeater runat="server" ID="rptProcessList">
            <HeaderTemplate>
                <figure>
                    <legend class="handle"><%=Resources.labels.danhsachgiaodich %></legend>
                    <div class="content">
                        <table class="table table-bordered table-hover footable" data-paging="true" style="background-color: white; border-color: rgb(204, 204, 204); border-width: 1px; border-style: none; border-collapse: collapse; max-width: 1000px;">
                            <thead>
                                <tr>
                                    <th>Card No</th>
                                    <th data-breakpoints="xs">Type</th>
                                    <th data-breakpoints="xs"><%= Resources.labels.currency %></th>
                                    <th data-breakpoints="xs"><%= Resources.labels.sotien %></th>
                                    <th data-breakpoints="xs sm"><%= Resources.labels.ngaygiogiaodich %></th>
                                    <th><%= Resources.labels.sogiaodichcore %></th>
                                    <th data-breakpoints="xs sm">Merchant Name</th>
                                </tr>
                            </thead>
                            <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("trxnCardNo") %>
                    </td>
                    <td>
                        <%#Eval("trxnTypeId") %>
                    </td>
                    <td><%#Eval("trxnCurrency") %>
                    </td>
                    <td><%#Eval("trxnAmount") %></td>
                    <td><%#Eval("trxnDate") %>
                    </td>
                    <td><%#Eval("trxnRef") %>
                    </td>
                    <td><%#Eval("trxnMerchantName") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
			</table>
            </div>
        </figure>
        
            </FooterTemplate>
        </asp:Repeater>
        <script>
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                onReady();
            }
        </script>
        <!--end-->
    </ContentTemplate>
</asp:UpdatePanel>

