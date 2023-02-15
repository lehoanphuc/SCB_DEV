<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCURRENCYFX_Controls_Widget" %>

<asp:ScriptManager runat="server" ID="ScriptManager1"/>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label runat="server" ID="lblTitleCurrency"/>
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
            <asp:Label runat="server" ID="lblError" Text="" Font-Bold="True" ForeColor="Red"/>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.currencyinfo %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel runat="server" ID="pnAdd">
                                <div class="row">
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 required">
                                            <%= Resources.labels.FromCCY %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlFromCCYID" CssClass="form-control select2 infinity" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12 required">
                                            <%= Resources.labels.ToCCY %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlToCCYID" CssClass="form-control select2 infinity" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12">
                                            <%= Resources.labels.desc %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server" MaxLength="250"/>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-6 col-xs-12">
                                        <label class="col-sm-4 control-label col-xs-12">
                                            <%= Resources.labels.status %>
                                        </label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btnSave_OnClick"/>
                                <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary" Text='<%$ Resources:labels, Clear %>' OnClick="btnClear_Click"/>
                                <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
