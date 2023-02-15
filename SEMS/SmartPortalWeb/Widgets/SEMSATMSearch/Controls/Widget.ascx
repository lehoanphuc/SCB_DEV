<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSATMSearch_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblATMHeader" runat="server"/>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"/>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.thongtinmayatm %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%= Resources.labels.atmid %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtATMID" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%= Resources.labels.atmcode %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtATMCode" CssClass="form-control" runat="server" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.address %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" TextMode="MultiLine"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.desc %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');"/>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%= Resources.labels.country %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlCountryId_OnSelectedIndexChanged"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%= Resources.labels.thanhpho1 %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_OnSelectedIndexChanged"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%= Resources.labels.quanhuyen %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%= Resources.labels.chinhanh %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList runat="server" ID="ddlBranchId" CssClass="form-control select2"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.latitude %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPosX" CssClass="form-control" runat="server" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%= Resources.labels.longitude %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPosY" CssClass="form-control" runat="server" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                        <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btsave_Click"/>
                        <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear%>" OnClick="btnClear_OnClick"/>
                        <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script>
    var atmId  = '<%= txtATMID.ClientID %>';
    var atmCode = '<%= txtATMCode.ClientID %>';
    var country = '<%= ddlCountry.ClientID %>';
    var city = '<%= ddlCity.ClientID %>';
    var dist = '<%= ddlDistrict.ClientID %>';
    var branch = '<%= ddlBranchId.ClientID %>';

    function validate() {
        if (!validateEmpty(atmId, '<%= Resources.labels.atmidrequired %>')) {
            document.getElementById(atmId).focus();
            return false;
        }
        if (!validateEmpty(atmCode, '<%= Resources.labels.atmcoderequired %>')) {
            document.getElementById(atmCode).focus();
            return false;
        }
        if (!validateDdl(country, '<%= Resources.labels.selectcountryrequired %>')) {
            $('#'+country).select2('open');
            return false;
        }
        if (!validateDdl(city, '<%= Resources.labels.selectcityrequired %>')) {
            $('#'+city).select2('open');
            return false;
        }
        
        if (!validateDdl(branch, '<%= Resources.labels.selectbranchrequired %>')) {
            $('#'+branch).select2('open');
            return false;
        }
        else {
            return true;
        }
    }

    function validateDdl(id, msg) {
      if(document.getElementById(id).value == "" || document.getElementById(id).value == "0"){
          alert(msg);
          return false;
      }
      return true;
    }
</script>
