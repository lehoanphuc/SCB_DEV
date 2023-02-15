<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBankUser_Controls_Widget" %>
<script type="text/javascript">
    //check email
    function echeck(str) {

        var at = "@"
        var dot = "."
        var lat = str.indexOf(at)
        var lstr = str.length
        var ldot = str.indexOf(dot)
        if (str.indexOf(at) == -1) {

            return false
        }

        if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
            return false
        }

        if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {

            return false
        }

        if (str.indexOf(at, (lat + 1)) != -1) {

            return false
        }

        if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {

            return false
        }

        if (str.indexOf(dot, (lat + 2)) == -1) {

            return false
        }

        if (str.indexOf(" ") != -1) {

            return false
        }

        return true
    }

    //check special character
    function data_change(field) {
        var check = true;
        var value = field.value.trim(); //get characters
        //check that all characters are digits, ., -, or ""
        for (var i = 0; i < field.value.length; ++i) {
            var new_key = value.charAt(i); //cycle through characters
            if (((new_key < "0") || (new_key > "9")) &&
                !(new_key == "")) {
                check = false;
                break;
            }
        }
        //apply appropriate colour based on value
        if (!check) {
            return false;
        }
        else {
            return true;
        }
    }

    function validate() {
        //validate username
        if (document.getElementById('<%=txtUserName.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.usernamerequire %>');
            document.getElementById('<%=txtUserName.ClientID %>').focus();
            return false;
        }
        var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
        for (var i = 0; i < document.getElementById('<%=txtUserName.ClientID%>').value.length; i++) {
            if (iChars.indexOf(document.getElementById('<%=txtUserName.ClientID%>').value.charAt(i)) != -1) {
                alert('<%=Resources.labels.usernamespecialcharactervalidate %>');
                document.getElementById('<%=txtUserName.ClientID %>').focus();
                return false;
            }
        }
        //kiem tra khoang trang
        if (!hasWhiteSpace('<%=txtUserName.ClientID %>', '<%=Resources.labels.usernamewhitespace %>')) {
            document.getElementById('<%=txtUserName.ClientID %>').focus();
            return false;
        }
        //validate password
        if (document.getElementById('<%=txtPass.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.passwordrequire %>');
            document.getElementById('<%=txtPass.ClientID %>').focus();
            return false;
        }
        //validate password
        if (document.getElementById('<%=txtPass.ClientID%>').value.trim().length < 6) {
            alert('<%=Resources.labels.passwordlength %>');
            document.getElementById('<%=txtPass.ClientID %>').focus();
            return false;
        }
        //kiem tra khoang trang
        if (!hasWhiteSpace('<%=txtPass.ClientID %>', '<%=Resources.labels.passwordwhitespace %>')) {
            document.getElementById('<%=txtPass.ClientID %>').focus();
            return false;
        }
        //validate retype password
        if (document.getElementById('<%=txtRePass.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.retypepasswordrequire %>');
            document.getElementById('<%=txtRePass.ClientID %>').focus();
            return false;
        }
        //kiem tra khoang trang
        if (!hasWhiteSpace('<%=txtRePass.ClientID %>', '<%=Resources.labels.retypepasswordwhitespace %>')) {
            document.getElementById('<%=txtRePass.ClientID %>').focus();
            return false;
        }
        //validate retype pass with pass
        if (document.getElementById('<%=txtPass.ClientID%>').value.trim() != document.getElementById('<%=txtRePass.ClientID%>').value.trim()) {
            alert('<%=Resources.labels.passwordcompare %>');
            document.getElementById('<%=txtRePass.ClientID %>').focus();
            return false;
        }
        //validate firstname
        if (document.getElementById('<%=txtFirstName.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.firstnamerequire %>');
            document.getElementById('<%=txtFirstName.ClientID %>').focus();
            return false;
        }
        //validate lastname
        if (document.getElementById('<%=txtLastName.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.lastnamerequire %>');
            document.getElementById('<%=txtLastName.ClientID %>').focus();
            return false;
        }
        //validate address
        if (document.getElementById('<%=txtAddress.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.addressrequire %>');
            document.getElementById('<%=txtAddress.ClientID %>').focus();
            return false;
        }
        //validate email
        if (document.getElementById('<%=txtEmail.ClientID%>').value.trim() != '') {
            //validate retype pass with pass                                           
            str = document.getElementById('<%=txtEmail.ClientID%>').value.trim();
            if (echeck(str) == false) {
                alert('<%=Resources.labels.emailvalidate %>');
                document.getElementById('<%=txtEmail.ClientID %>').focus();
                return false;
            }
        }
        else {
            alert('<%=Resources.labels.bancannhapemailnguoisudung %>');
            document.getElementById('<%=txtEmail.ClientID %>').focus();
            return false;
        }
        //validate phone
        if (!data_change(document.getElementById('<%=txtPhone.ClientID%>'))) {
            alert('<%=Resources.labels.phonevalidate %>');
            document.getElementById('<%=txtPhone.ClientID %>').focus();
            return false;
        }
        return true;
    }

    function updatevalidate() {
        //validate firstname
        if (document.getElementById('<%=txtFirstName.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.firstnamerequire %>');
            document.getElementById('<%=txtFirstName.ClientID %>').focus();
            return false;
        }
        //validate lastname
        if (document.getElementById('<%=txtLastName.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.lastnamerequire %>');
            document.getElementById('<%=txtLastName.ClientID %>').focus();
            return false;
        }
        //validate address
        if (document.getElementById('<%=txtAddress.ClientID%>').value.trim() == '') {
            alert('<%=Resources.labels.addressrequire %>');
            document.getElementById('<%=txtAddress.ClientID %>').focus();
            return false;
        }
        //validate email
        if (document.getElementById('<%=txtEmail.ClientID%>').value.trim() != '') {
            //validate retype pass with pass                                           
            str = document.getElementById('<%=txtEmail.ClientID%>').value.trim();
            if (echeck(str) == false) {
                alert('<%=Resources.labels.emailvalidate %>');
                document.getElementById('<%=txtEmail.ClientID %>').focus();
                return false;
            }
        }
        else {
            alert('<%=Resources.labels.bancannhapemailnguoisudung %>');
            document.getElementById('<%=txtEmail.ClientID %>').focus();
            return false;
        }
        //validate phone
        if (!data_change(document.getElementById('<%=txtPhone.ClientID%>'))) {
            alert('<%=Resources.labels.phonevalidate %>');
            document.getElementById('<%=txtPhone.ClientID %>').focus();
            return false;
        }
        return true;
    } 

</script>
<div class="subheader">
    <h1 class="subheader-title">
        <asp:Label ID="lblTitle" runat="server"></asp:Label>
    </h1>
</div>
<div id="divError">
    <asp:Label ID="lblAlert" runat="server"></asp:Label>
</div>
<asp:Panel runat="server" ID="pnLogin">
    <div class="row">
        <div class="col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.login%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.username %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.password %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtPass" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.repassword %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtRePass" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<div class="row">
    <div class="col-sm-12 col-xs-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.thongtinnguoidung%>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnProfile" runat="server">
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.firstname %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.middlename %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtMiddleName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.lastname %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.gender %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlGender" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.address %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.email %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.birthday %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtBirth" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.phone %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.branch %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlBranch" CssClass="form-control select2" Width="100%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.kieunguoidung %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlUserType" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.capbac %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlLevel" CssClass="form-control select2 infinity" Width="100%" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.dungpolicy %></label>
                                    <div class="col-sm-8 col-xs-12">
                                        <asp:DropDownList ID="ddlpolicySEMS" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-xs-12"></div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-sm-12 col-xs-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.assignusergroup%>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <div class="row">
                        <div class="col-sm-12 col-xs-12">
                            <div class="form-group">
                                <asp:Panel ID="pnRole" runat="server">
                                    <asp:Repeater runat="server" ID="rptRole">
                                        <ItemTemplate>
                                            <div class="col-sm-4 col-xs-12 custom-control">
                                                <asp:HiddenField runat="server" ID="hdRole" Value='<%# DataBinder.Eval(Container.DataItem, "RoleID") %>' />
                                                <asp:CheckBox runat="server" ID="cbRole" Text='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>' Checked='<%# DataBinder.Eval(Container.DataItem, "Checked").ToString().Equals("1") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                    <asp:Button ID="btnSaveAdd" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, save %>' OnClientClick="Loading(); return validate();" OnClick="btOK_Click" />
                    <asp:Button ID="btnSave" Visible="False" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="Loading(); return updatevalidate();" OnClick="btOK_Click" />
                    <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_OnClick" />
                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClientClick="Loading();" OnClick="btCancel_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function Loading() {
        if (document.getElementById('<%=lblAlert.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblAlert.ClientID%>').innerHTML = '';
        }
    }
</script>
