<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCardDetail_Widget" %>


<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBCardDetail/CSS/css.css" rel="stylesheet" type="text/css" />
<div class="al">
    <span>
        <%=Resources.labels.cardlistdetail %>
    </span>
    <br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<div>
    <asp:HiddenField ID="hdfCardNo" runat="server" />

    <!--thong tin tai khoan vay-->
    <asp:Panel ID="pnLN" runat="server">
        <figure class="divcontent">
            <legend class="handle"><%=Resources.labels.thongtinthe %></legend>
            <div class="content_table_4c_cl">


                <div class="row">
                    <div class="col-xs-5 col-sm-3 line30">
                        <label class="bold">
                            <%= Resources.labels.sothe %>
                        </label>
                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lblcardNo" runat="server" Text="00009102"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-3 line30">
                        <label class="bold">
                            <%= Resources.labels.cardholdername %>
                        </label>
                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lblcardholderName" runat="server" Text="Unknown"></asp:Label>
                    </div>
                </div>
                <div class="row">

                    <div class="col-xs-5 col-sm-3 line30">
                        <label class=" bold">
                            <%= Resources.labels.cardtype %>
                        </label>
                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lblcardType" runat="server" Text="00009102"></asp:Label>
                    </div>

                    <div class="col-xs-5 col-sm-3 line30">
                        <label class="bold">
                            <%= Resources.labels.creditlimit %>
                        </label>
                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lblcreditLimit" runat="server" Text="Unknown"></asp:Label>
                    </div>
                </div>
                <div class="row">

                    <div class="col-xs-5 col-sm-3 line30">
                        <label class=" bold">
                            <%= Resources.labels.currency %>
                        </label>
                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lblCCYID" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-3 line30">
                        <label class=" bold">
                            <%= Resources.labels.availablelimit %>
                        </label>
                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lblavaiLimit" runat="server" Text="1.000.000"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-3 line30">
                        <label class=" bold">
                            <%= Resources.labels.cardstatus %>
                        </label>
                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lblcardStatus" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-5 col-sm-3 line30">
                        <label class=" bold">
                            <%= Resources.labels.outstandingamount %>
                        </label>

                    </div>
                    <div class="col-sm-3 col-xs-7 line30">
                        <asp:Label ID="lbloutstandingAmt" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </figure>

    </asp:Panel>
    <!--end-->
    <br />
    <!--chi tiet giao dich -->
    <figure>
        <legend class="handle"><%=Resources.labels.lasttentransaction %></legend>
        <div class="content">

            <asp:Literal runat="server" ID="ltrError"></asp:Literal>
            <asp:Repeater runat="server" ID="rptData">
                <HeaderTemplate>
                    <table class="table table-border footable" data-paging="true" style="border-color: #CCCCCC; border-width: 1px;">
                        <thead>
                            <tr>
                                <th>Card No</th>
                                <th data-breakpoints="xs">Type</th>
                                <th data-breakpoints="xs"><%= Resources.labels.currency %></th>
                                <th><%= Resources.labels.sotien %></th>
                                <th><%= Resources.labels.ngaygiogiaodich %></th>
                                <th data-breakpoints="xs"><%= Resources.labels.sogiaodichcore %></th>
                                <th data-breakpoints="xs">Merchant Name</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("trxnCardNo") %></td>
                        <td><%#Eval("trxnTypeId") %></td>
                        <td><%#Eval("trxnCurrency") %></td>
                        <td><%#Eval("trxnAmount") %></td>
                        <td><%#Eval("trxnDate") %></td>
                        <td><%#Eval("trxnRef") %></td>
                        <td><%#Eval("trxnMerchantName") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
            </table>
                </FooterTemplate>
            </asp:Repeater>
            <div style="max-height: 700px; overflow: auto; margin-top: 10px; padding: 0 2px; text-align: center">
                <asp:Literal ID="ltrTH" runat="server"></asp:Literal>
                <asp:Button ID="back" CssClass="btn btn-warning" runat="server" Text="Back" />
            </div>
        </div>
    </figure>


    <!--end-->
</div>
