<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSKYCDefinition_Controls_Widget" %>
<%@ Register Src="General.ascx" TagPrefix="uc1" TagName="General" %>
<%@ Register Src="Activity.ascx" TagPrefix="uc1" TagName="Activity" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                 <asp:Label ID="lblTitleKycDefinition" runat="server"></asp:Label>
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
                <div class="nav-tabs-custom" id="tabs">
                    <ul class="nav nav-tabs"  id="pnadd" runat="server">
                        <li class="active" id="liTabDetailContract" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.general %></a></li>
                        <li id="liTabActivity" runat="server"><a href="#tab_2" data-toggle="tab"><%=Resources.labels.Activity %></a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                            <uc1:General ID="General_Tab" runat="server" />
                        </div>
                        <div class="tab-pane" id="tab_2">
                           <uc1:Activity ID="Activity_Tab" runat="server" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    $(document).ready(function () {
        $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
            localStorage.setItem('activeTab', $(e.target).attr('href'));
        });
        var activeTab = localStorage.getItem('activeTab');
        if (activeTab) {
            $('#tabs a[href="' + activeTab + '"]').tab('show');
        }
    });
</script>

