<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCardList_Widget" %>

<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<link href="Widgets/IBCardList/css.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager runat="server"></asp:ScriptManager>
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
    <span><%=Resources.labels.cardlist %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
    </Triggers>
    <ContentTemplate>
        <div style="text-align: center; margin: 5px 1px 5px 1px;">
            <asp:Label ID="lblAlert" runat="server" ForeColor="Red" Font-Bold="True"
                SkinID="lblImportant"></asp:Label>
        </div>
        <figure id="Ownerdiv" runat="server" visible="false">
            <legend class="handle">Own user card list</legend>
            <div class="content">
                <asp:Repeater runat="server" ID="rptOwncard">
                    <ItemTemplate>
                        <div class="row card">
                            <div class="col-sm-5 col-xs-12 card-img">
                                <a href="<%#Eval("CardLink") %>">
                                    <img src="images/cardnew.png" class="img-reponsive" />
                                    <%--<img src="ImagesCard/Handler.ashx?idimg=<%#Eval("imgID") %>" class="img-reponsive" />--%>

                                    <div class="card-info">
                                        <p class="card-number"><%#Eval("CardNo") %></p>
                                        <div class="info">
                                            <span>Card holder name</span><br />
                                            <span class="name-cus"><%#Eval("cardholderName") %></span>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="col-sm-7 col-xs-12 right-card line30">
                                <label>Available limit : </label>
                                <span class="green"><%#Eval("avaiLimit") %></span><br />

                                <label>Credit limit : </label>
                                <span class="blue"><%#Eval("creditLimit") %></span><br />
                                <label>Outstanding amount : </label>
                                <span class="red"><%#Eval("outstandingAmt") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <%--
                <asp:Literal ID="ltrDD" runat="server"></asp:Literal>
                <table class='table table-bordered table-hover footable'>
                    <thead>
                        <tr>
                            <th>Card Number</th>
                            <th>Cardholder name</th>
                            <th>Card type</th>
                            <th data-breakpoints='xs'>Credit limit</th>
                            <th data-breakpoints='xs'>Available limit</th>
                            <th data-breakpoints='xs'>Outstanding amount</th>
                        </tr>
                    </thead>
                    <tr>
                        <td><a href='?po=3&p=1057&cardno=BDFemysGJ1r7PeBU59ZzdZE/uh5aadplm1DuKAlTiNM8cKjrdt6il4vNDf7h9SGc'>950510******0180</a></td>
                        <td></td>
                        <td>MYO WIN YEE</td>
                        <td>Credit</td>
                        <td>1,000.00</td>
                        <td>900.00</td>
                        <td>500.00</td>
                    </tr>
                </table>--%>
            </div>
        </figure>
        <figure id="Otherdiv" runat="server" visible="false" style="margin-top: 15px;">
            <legend class="handle">Other user card list</legend>
            <div class="content">
                <asp:Repeater runat="server" ID="rptOtherCard">
                    <ItemTemplate>
                        <div class="row card">
                            <div class="col-sm-5 col-xs-12 card-img">
                                <a href="<%#Eval("CardLink") %>">
                                    <img src="/Images/card.png" class="img-reponsive" />
                                    <%--<img src="ImagesCard/Handler.ashx?idimg=<%#Eval("imgID") %>" class="img-reponsive" />--%>
                                    <div class="card-info">
                                        <p class="card-number"><%#Eval("CardNo") %></p>
                                        <div class="info">
                                            <span>Card holder name</span><br />
                                            <span class="name-cus"><%#Eval("cardholderName") %></span>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="col-sm-7 col-xs-12 right-card line30">
                                <label>Available limit : </label>
                                <span class="green"><%#Eval("avaiLimit") %></span><br />

                                <label>Credit limit : </label>
                                <span class="blue"><%#Eval("creditLimit") %></span><br />
                                <label>Outstanding amount : </label>
                                <span class="red"><%#Eval("outstandingAmt") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </figure>
        <asp:Timer ID="Timer1" runat="server" Interval="50" OnTick="OnGetAccountListFinish" />
    </ContentTemplate>
</asp:UpdatePanel>
