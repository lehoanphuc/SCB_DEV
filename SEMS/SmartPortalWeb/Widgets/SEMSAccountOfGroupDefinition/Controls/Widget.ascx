<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSAccountOfGroupDefinition_Controls_Widget" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadMdlAccList.ascx" TagPrefix="uc3" TagName="LoadMdlAccList" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadGroupDefinition.ascx" TagPrefix="uc2" TagName="LoadGroupDefinition" %>


<%@ Import Namespace="SmartPortal.Constant" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleAccountOfGroupDefinition" runat="server"></asp:Label>
            </h1>
        </div>
         <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2></h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0 " style="margin-left: 2%">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-9">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.accountinggroup %> </label>
                                            <div class="col-sm-7">
                                                <uc2:LoadGroupDefinition runat="server" ID="txtGroupCode" OnPreRender="loadInfo" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-9">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.groupname %> </label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtgroupName" Width="72%" MaxLength="150" runat="server" CssClass="form-control2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-9">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.accountnumber%> </label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtAcNo" Width="72%" MaxLength="50" runat="server" CssClass="form-control2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-9">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.systemaccountname %> </label>
                                            <div class=" col-sm-7">
                                                <uc3:LoadMdlAccList ID="txtAccName" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btsave_Click"/>
                            <asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, clear %>" OnClick="btClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
