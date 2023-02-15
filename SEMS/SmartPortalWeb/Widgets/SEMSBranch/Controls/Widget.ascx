<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBranch_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"/>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""/>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%= Resources.labels.thongtinchinhanh %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12   control-label required"><%= Resources.labels.machinhanh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtmacn" CssClass="form-control" runat="server" MaxLength="4" onkeypress="return isNumberKey(event)"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label required"><%= Resources.labels.branchname %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txttencn" CssClass="form-control" runat="server" MaxLength="255"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label"><%= Resources.labels.address %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtaddress" CssClass="form-control" TextMode="MultiLine" runat="server" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label"><%= Resources.labels.phone %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtphone" CssClass="form-control" runat="server" MaxLength="100"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label"><%= Resources.labels.latitude %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtLatitude" CssClass="form-control" runat="server" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label"><%= Resources.labels.longitude %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtLongitude" CssClass="form-control" runat="server" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label required"><%= Resources.labels.country %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCountry" CssClass="form-control select2 col-xs-12" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label required"><%= Resources.labels.thanhpho %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlcity" CssClass="form-control select2 col-xs-12" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label"><%= Resources.labels.quanhuyen %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlDist" CssClass="form-control select2 col-xs-12" Width="100%" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  control-label"><%= Resources.labels.tenvungphi %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlRegion" CssClass="form-control select2 col-xs-12" Width="100%" runat="server" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 "><%= Resources.labels.taxcode %></label>
                                            <div class="col-sm-8 col-xs-12 custom-control">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTaxCode" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 "><%= Resources.labels.biccode %></label>
                                            <div class="col-sm-8 col-xs-12 custom-control">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtBicCode" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 "><%= Resources.labels.swiftcode %></label>
                                            <div class="col-sm-8 col-xs-12 custom-control">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSwiftCode" MaxLength="50"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 "><%= Resources.labels.email %></label>
                                            <div class="col-sm-8 col-xs-12 custom-control">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" MaxLength="250"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  required">
                                                <%= Resources.labels.timeopen %>
                                            </label>
                                            <div class="col-sm-8 col-xs-12 custom-control">
                                                <asp:TextBox runat="server" CssClass="form-control igtxtTime" ID="txtTimeOpen" MaxLength="8"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12  required"><%= Resources.labels.timeclose %></label>
                                            <div class="col-sm-8 col-xs-12 custom-control">
                                                <asp:TextBox runat="server" CssClass="form-control igtxtTime" ID="txtTimeClose" MaxLength="8"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return checkValidation()" OnClick="btsave_Click"/>
                            <asp:Button runat="server" ID="btnClear" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear%>" OnClick="btnClear_OnClick"/>
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function checkValidation() {
        var branchId = '<%= txtmacn.ClientID %>';
        var branchName = '<%= txttencn.ClientID %>';
        var country = '<%= ddlCountry.ClientID %>';
        var city = '<%= ddlcity.ClientID %>';
        var email = '<%= txtEmail.ClientID %>';
        var timeOpen = '<%= txtTimeOpen.ClientID %>';
        var timeClose = '<%= txtTimeClose.ClientID %>';
        if (!validateEmpty(branchId,'<%= Resources.labels.bancannhapmachinhanh %>')) {
            document.getElementById(branchId).focus();
            return false;
        }
        if(document.getElementById(branchId).value.length < 4){
            alert("Branch Id require 4 number character!");
            document.getElementById(branchId).focus()
            return false
        }
        if (!validateEmpty(branchName,'<%= Resources.labels.bancannhaptenchinhanh %>')) {
            document.getElementById(branchName).focus();
            return false;
        }
        if (!checkEmail(email, '<%= Resources.labels.emailkhongdinhdang %>')) {
            document.getElementById(email).focus();
            return false;
        }
        if (!validateDropdownList(country, '<%= Resources.labels.selectcountryrequired %>')) {
            $('#' + country).select2('open');
            return false;
        }
        if (!validateDropdownList(city, '<%= Resources.labels.selectcityrequired %>')) {
            $('#' + city).select2('open');
            return false;
        }
        if (!validateEmpty(timeOpen, '<%= Resources.labels.timeopenrequired %>')) {
            document.getElementById(timeOpen).focus();
            return false;
        }
        if (!validateEmpty(timeClose, '<%= Resources.labels.timecloserequired %>')) {
            document.getElementById(timeClose).focus();
            return false;
        }

    }

    function checkTextAreaMaxLength(textBox, e, length) {
        var mLen = textBox["MaxLength"];
        if (null == mLen)
            mLen = length;

        var maxLength = parseInt(mLen);
        if (textBox.value.length > maxLength - 1) {
            if (window.event)//IE
                e.returnValue = false;
            else//Firefox
                e.preventDefault();
        }
    }

    function validateDropdownList(id, msg) {
        var value = document.getElementById(id).value.trim();
        if (value == "" || value == "0") {
            window.alert(msg);
            return false;
        }
        else {
            return true;
        }
    }

</script>
