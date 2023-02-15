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
                            <%=Resources.labels.ReversalTranLimitInfo%>
                        </h2>
                    </div>
                    <asp:Panel ID="pannel1" runat="server" DefaultButton="btsave">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <asp:Panel ID="pnAdd" runat="server">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4  control-label required"><%=Resources.labels.loaigiaodich %></label>
                                                <div class="col-sm-8 ">
                                                    <asp:DropDownList ID="ddlTransactionType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                             <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.reversal %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlIsreversal" CssClass="form-control select2 infinity" Width="100%" OnTextChanged="checkIsReversal_OnCheckedChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Value="1" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.hanmuc %></label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtLimit" MaxLength="4" onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <asp:CheckBox ID="checktxtlimit"  OnCheckedChanged="checktxtlimit_OnCheckedChanged" AutoPostBack="True" runat="server" /> Unlimit
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4  control-label required"><%=Resources.labels.unittype %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlUnit" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                        <%--<asp:ListItem Value="H" Text="<%$ Resources:labels, gio %>"></asp:ListItem>--%>
                                                        <asp:ListItem Value="D" Text="<%$ Resources:labels, ngaysegui %>"></asp:ListItem>
                                                        <%--<asp:ListItem Value="M" Text="<%$ Resources:labels, month %>"></asp:ListItem>--%>
                                                    </asp:DropDownList>
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
