<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPrefix_Controls_Widget" %>
<script type="text/javascript" src="widgets/SEMSPrefix/js/mask.js"> </script>
<script src="/JS/Common.js"></script>
<div id="divCustHeader">
    <div class="subheader">
        <h1 class="subheader-title">
            <img alt="" src="widgets/SEMSCashcodemanager/Images/tax.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
            <asp:Label ID="lbltitle" runat="server"></asp:Label>
        </h1>
    </div>
</div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </div>
        <div id="divSearch" runat="server" class="">

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.PREFIXMANAGEMENT%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <asp:Panel ID="pnSearch" runat="server">
                                    <div class="row">
                                        <div class="col-sm-6 hidden">
                                            <div class="form-group ">
                                                <label class="col-sm-4 control-label required">
                                                    <%=Resources.labels.SupplierID%>
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlSupplierID" runat="server" CssClass="form-control select2 infinity">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.TelcoName %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddltelcoid" runat="server" CssClass="form-control select2 infinity">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.Prefix %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtPrefix" runat="server" CssClass="form-control " onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                       <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.countryname %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList Width="100%" ID="txtCountryName" runat="server" CssClass="form-control select2 infinity">
                                                        <asp:ListItem Value="LA" Text="Thailand"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.CountryPrefix %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCountryPrefix" runat="server" CssClass="form-control " onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);" MaxLength="20"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                          <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneLength %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList Width="100%" ID="txtphonelen" runat="server" CssClass="form-control select2 infinity">
                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label required"><%=Resources.labels.groupid %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList Width="100%" ID="ddlGroupid" runat="server" CssClass="form-control select2 infinity">
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="txtgroupid" runat="server" CssClass="form-control " onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);" MaxLength="50"></asp:TextBox>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                     
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <div class="col-sm-8">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<p class="auto-style1">
    <div style="text-align: center; margin-top: 10px;">
        &nbsp;<asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();" OnClick="btsave_Click" />
        &nbsp;
           <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_Click" />
        <asp:Button ID="Button8" CssClass="btn btn-secondary btnGeneral" runat="server" Text="<%$ Resources:labels, back %>"
            OnClick="btback_OnClick" />
    </div>
</p>

<script>
    function backhistory() {
        var currentUrl = window.location.href;
        window.history.length = 1;
        window.history.back(1);
    }

    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = true;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = false;
                }
            }
        }
    }
    function HighLightCBX(obj, obj1) {
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }
    function checkboxtrue() {
        var checkbox = document.getElementById("cbxSelect");
        var deletebutton = document.getElementById("btnDelete");
    }
    function ValidateID(obj) {
        var maxchar = 50;
        if (this.id) obj = this;
        replaceSQLChar(obj);
        obj.value = obj.value.replace(/[^\w+$]/gi, '');
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }
    function replaceSQLChar(obj) {
        if (obj.value.length > 0) {
            obj.value = obj.value.replace(/'|!|@|#|\$|%|\^|&|\*|;|xp_|script|iframe|delete|drop|exec|execute|--|<|>/g, "");
        }
    }
    function ValidateID1(obj) {
        var maxchar = 50;
        if (this.id) obj = this;
        replaceSQLChar1(obj);
        var remaningChar = maxchar - obj.value.length;

        if (remaningChar <= 0) {
            obj.value = obj.value.substring(maxchar, 0);
        }
    }
    function replaceSQLChar1(obj) {
        if (obj.value.length > 0) {
            obj.value = obj.value.replace(/'|!|@|#|\$|%|\^|&|\*|;|xp_|script|iframe|delete|drop|exec|execute|--|<|\[|\]|{|}|\/|\||\+|=|\(|\)|~|`|:|"|>|\ |\./g, "");
        }
    }

    function allowNumbersOnly(e) {
        var code = (e.which) ? e.which : e.keyCode;
        if (code > 31 && (code < 48 || code > 57)) {
            e.preventDefault();
        }
    }


</script>



