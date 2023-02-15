<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSAgentBank_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadCountry.ascx" TagPrefix="uc1" TagName="LoadCountry" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadCity.ascx" TagPrefix="uc1" TagName="LoadCity" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadRegion.ascx" TagPrefix="uc2" TagName="LoadRegion" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadBranch.ascx" TagPrefix="uc1" TagName="LoadBranch" %>






<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleAgentBank" runat="server"></asp:Label>
            </h1>
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
                        <div class="panel-content form-horizontal p-b-0" style="margin-left:2%">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.departmentcode %> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox Width="76%" ID="txtDepartmentCode" CssClass="form-control" runat="server" MaxLength="5"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.branch %> </label>
                                            <div class=" col-sm-8">
                                                <uc1:LoadBranch ID="txtBrachID" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.departmentname%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox Width="76%" ID="txtDepartmentName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.status %> </label>
                                            <div class=" col-sm-8">
                                                <asp:DropDownList Width="76%" ID="ddStatus" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row" runat="server" id="fcreatedate">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.createddate%> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtCreatedate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                                <div class="row" id="fmodifydate" runat="server">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.LastModifiedDate %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtLastmodifydate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                                <div class="row" id="fcreateby" runat="server">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.nguoithuchien %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtCreateby" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                                <div class="row" id="fapproveby" runat="server">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.duyetboi %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtApproveby" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btsave_Click" />
                            <asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, clear %>" OnClick="btclear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

