<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBAccountListDD_Widget" %>


<link rel="stylesheet" type="text/css" href="Widgets/IBAccountList/css.css" />
<link rel="stylesheet" type="text/css" href="CSS/css.css" />

<asp:ScriptManager runat="server">
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
    <span><%=Resources.labels.danhsachtaikhoanthanhtoan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<div>
    <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <div style="text-align: center; display: none">
                <asp:Image runat="server" ID="imgLoading" alt="" src="Images/WidgetImage/ajaxloader.gif" Style="width: 16px; height: 16px; color: #0066E1" />
            </div>
            <div class="divAcct">
                <asp:Repeater runat="server" ID="rptAccount">
                    <ItemTemplate>
                        <div class="act_item">
                            <div class="act_header">
                                <div class="row">
                                    <div class="col-xs-6 col-md-6"><span style="font-weight: bold"><%#Eval("Desc") %></span></div>
                                    <div class="col-xs-6 col-md-6" style="text-align: right"><span class="mb_hide">Account number: </span><span class="act-color"><%#Eval("Account") %></span></div>
                                </div>
                            </div>
                            <div>
                                <div class="row">
                                    <div class="col-xs-2 col-md-1">
                                        <img alt="" src="<%#"Images/WidgetImage/act_CD.png" %>">
                                    </div>
                                    <div class="col-xs-10 col-md-5">
                                        <span><%#Eval("Total") %></span><br>
                                        <a href="<%#Eval("Detail") %>">View account detail</a>
                                    </div>
                                    <div class="col-xs-6 col-md-6 mb_hide" style="text-align: right; ">
                                        <img alt="" src="<%#"Images/WidgetImage/" + Eval("StatusImg").ToString().Trim() %>"><br>
                                        <span style="font-size: 10px; font-weight: bold;"><%#Eval("Status") %></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="ltrDD" runat="server" CssClass="bold" ForeColor="Red"></asp:Label>
                <asp:Timer ID="Timer1" runat="server" Interval="50" OnTick="OnGetAccountListFinish">
                </asp:Timer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
