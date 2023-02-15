<%@ Control Language="C#" AutoEventWireup="true" CodeFile="General.ascx.cs" Inherits="Widgets_SEMSKYCDefinition_Controls_General" %>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div class="panel-container">
    <div class="panel-content form-horizontal p-b-0" style="display: block;">
        <asp:Panel ID="pnGeneral" runat="server">
            <div class="row" style="margin-left: 2%">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.KycCode %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtKycCode" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1">
                </div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.Kycname %></label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtKycName" MaxLength="250" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-left: 2%">
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="col-sm-4 control-label required"><%=Resources.labels.status %></label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" Style="width: 100%;" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-1">
                </div>
                <div class="col-sm-5">
                     <div class="form-group" id="createddate" runat="server">
                            <label class="col-sm-4 control-label"><%=Resources.labels.createddate %></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCreateDate" CssClass="form-control datetimepicker" Style="width: 100%;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                </div>
            </div>
            <asp:Panel ID="PnAdd" runat="server">
                <div class="row" style="margin-left: 2%">
                    <div class="col-sm-5">
                      <div class="form-group">
                            <label class="col-sm-4 control-label"><%=Resources.labels.LastModifiedDate %></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtLastModifiedDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1">
                    </div>
                    <div class="col-sm-5">
                        <div class="form-group">
                            <label class="col-sm-4 control-label"><%=Resources.labels.CreatedBy %></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCreatedBy" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-left: 2%">
                    <div class="col-sm-5">
                         <div class="form-group">
                            <label class="col-sm-4 control-label"><%=Resources.labels.ApprovedBy %></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtApprovedBy" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1">
                    </div>
                    <div class="col-sm-5">
                       
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
    <div style="margin-top: 10px; text-align: center;">
        &nbsp;<asp:Button ID="btSave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, save %>" OnClick="btSave_Click" />
        &nbsp;<asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btClear_Click" />
        &nbsp;<asp:Button ID="btCancel" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="btCancel_Click" />
    </div>

</div>
