<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractLevel_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleContracLevel" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.ContractLevelInfo%>
                        </h2>
                    </div>
                    <asp:Panel ID="pannel1" runat="server" DefaultButton="btsave">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <asp:Panel ID="pnAdd" runat="server">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.ContractLevelCode %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtContractLevelCode" MaxLength="50" CssClass="form-control" IsRequired="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.ContractLevelName %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtContractLevelName" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.order %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOrder" CssClass="form-control" onKeyPress="return onlyDotsAndNumbers(this, event,3);" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.Priority %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPriority" CssClass="form-control" onKeyPress="return onlyDotsAndNumbers(this, event,3);" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.status %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.Condition %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCondition" TextMode="MultiLine" CssClass="form-control" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btsave_Click" />
                                <asp:Button ID="btnClear" runat="server" Visible="false" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                                <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
