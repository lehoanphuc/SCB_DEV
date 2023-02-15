<%@ Control Language="C#" AutoEventWireup="true" CodeFile="General.ascx.cs" Inherits="Widgets_SEMSReason_Controls_General" %>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="panel-container" id="BigPannelGeneral">
    <div class="panel-content form-horizontal p-b-0">
        <asp:Panel ID="pnGeneral" runat="server">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.ReasonCode %> </label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtReasonsCode" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.ReasonName%> </label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtReasonName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.reasonAction %> </label>
                        <div class=" col-sm-8">
                            <asp:DropDownList ID="ddreasonaction" runat="server" style="width:100%!important" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.reasonType %> </label>
                        <div class=" col-sm-8">
                            <asp:DropDownList ID="ddreasontype" runat="server" style="width:100%!important" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels._event %> </label>
                        <div class=" col-sm-8">
                            <asp:DropDownList ID="ddEvent" runat="server" style="width:100%!important" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.status %> </label>
                        <div class=" col-sm-8">
                            <asp:DropDownList ID="ddStatus" runat="server" Style="width: 100%!important" CssClass="form-control select2"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="col-sm-4 control-label "><%=Resources.labels.desc %> </label>
                        <div class=" col-sm-8">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" MaxLength="250"/>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                </div>
            </div>
        </asp:Panel>
    </div>

    <div style="margin-top: 10px; text-align: center;">
        &nbsp;<asp:Button ID="btsave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, save %>" OnClick="btnSave_Click" />
        &nbsp;<asp:Button ID="btClear" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, clear %>" OnClick="btclear_Click" />
        &nbsp;<asp:Button ID="btback" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
    </div>

</div>
