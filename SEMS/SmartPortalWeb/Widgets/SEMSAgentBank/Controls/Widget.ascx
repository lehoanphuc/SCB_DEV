<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSAgentBank_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadCountry.ascx" TagPrefix="uc1" TagName="LoadCountry" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadCity.ascx" TagPrefix="uc1" TagName="LoadCity" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadRegion.ascx" TagPrefix="uc2" TagName="LoadRegion" %>
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
                        <h2></h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.BankCode %> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBankCode" CssClass="form-control" runat="server" MaxLength="5"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.shortname %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.BankName%> </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtBankName" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.othername %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtOthername" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.banktype %> </label>
                                            <div class=" col-sm-8">
                                                <asp:DropDownList ID="ddbanktype" runat="server" CssClass="form-control select2" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.biccode %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtBiccode" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.status %> </label>
                                            <div class=" col-sm-8">
                                                <asp:DropDownList ID="ddStatus" runat="server" CssClass="form-control select2" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.swiftcode %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtSwiftcode" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.PhoneNumber %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtPhonenum" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.taxcode %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtTaxCode" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    
                                    <div class="col-sm-6 ">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.country%> </label>
                                            <div class=" col-sm-8">
                                                <uc1:LoadCountry runat="server" ID="txtCountryID" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.ngaythanhlap1 %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtEstablishdate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.thanhpho %> </label>
                                            <div class=" col-sm-8">
                                                <uc1:LoadCity runat="server" ID="txtCityID" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.website %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.addressdesc %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox Style="width: 100%" ID="txtAddressdesc" runat="server" CssClass="form-control" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.email %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="250"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 " style="visibility: hidden">
                                        <div class="form-group" visible="false">
                                            <label class="col-sm-4 control-label "><%=Resources.labels.region %> </label>
                                            <div class=" col-sm-8">
                                                <uc2:LoadRegion runat="server" ID="txtRegion" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" runat="server" id="fcreatedate">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.createddate %> </label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtCreatedate" runat="server" CssClass="form-control datetimepicker"></asp:TextBox>
                                            </div>
                                        </div>
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
<script src="/JS/Common.js"></script>

