<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractLevel_Controls_Widget" %>
<%@ Register Src="General.ascx" TagPrefix="uc1" TagName="General" %>
<%@ Register Src="Activity.ascx" TagPrefix="uc1" TagName="Activity" %>
<%@ Register Src="Document.ascx" TagPrefix="uc1" TagName="Document" %>
<%@ Register Src="MoneySourceLinkage.ascx" TagPrefix="uc1" TagName="MoneySourceLinkage" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script>
    $(document).ready(function () {
        $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
            localStorage.setItem('activeTab', $(e.target).attr('href'));
        });
        var activeTab;
        if (localStorage.getItem('activeTab')) {
            activeTab = localStorage.getItem('activeTab');
        }
        else activeTab = '#tab_1';
        
        if (activeTab) {
            $('#tabs a[href="' + activeTab + '"]').tab('show');
        }
    });
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
               <asp:Label ID="lblTitleMerchantProfile" runat="server"></asp:Label>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row" runat="server" id="divAccount">
            <div class="col-sm-12">
                <div id="tabs" class="nav-tabs-custom" data-tab="<%=hidTAB.ClientID %>">
                    <asp:HiddenField runat="server" ID="hidTAB" Value="#tab_1" />
                    <ul class="nav nav-tabs">
                        <li id="liTabDetailContract" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.general %></a></li>
                        <li id="liTabDetailCustomer" runat="server"><a href="#tab_2" data-toggle="tab"><%=Resources.labels.document %></a></li>
                        <li id="liTabWorkingCard" runat="server"><a href="#tab_3" data-toggle="tab"><%=Resources.labels.MoneySourceLinkage %></a></li>
                        <li id="liTabActivity" runat="server"><a href="#tab_4" data-toggle="tab"><%=Resources.labels.Activity %></a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                            <uc1:General ID="General_Tab" runat="server" />
                        </div>
                        <div class="tab-pane" id="tab_2">
                            <uc1:Document ID="Document_Tab" runat="server" />
                        </div>
                        <div class="tab-pane" id="tab_3">
                           <uc1:MoneySourceLinkage ID="MoneySourceLinkage_Tab" runat="server" />
                        </div>
                        <div class="tab-pane" id="tab_4">
                           <uc1:Activity ID="Activity_Tab" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
    </ContentTemplate>
</asp:UpdatePanel>