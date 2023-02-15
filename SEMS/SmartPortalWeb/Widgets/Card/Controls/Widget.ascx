<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Card_Controls_Widget" %>
<script type="text/javascript" src="widgets/SEMSPrefix/js/mask.js"> </script>

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
        <div id="divaddnew" runat="server" class="">
            <asp:Panel runat="server" ID="pnCard" DefaultButton="btsave">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel">
                            <div class="panel-hdr">
                                <h2>
                                    <%=Resources.labels.CARDMANAGEMENT%>
                                </h2>
                            </div>
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <div class="row hidden">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label">
                                                    <%= Resources.labels.Cardid %>
                                                </label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="lblcardidpn" runat="server" CssClass="form-control "></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required">
                                                    <%= Resources.labels.shortname %>
                                                </label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="lblshortnamepn" runat="server" CssClass="form-control  " onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label">
                                                    <%= Resources.labels.tiente %>
                                                </label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList Width="100%" ID="lblccyidpn" runat="server" CssClass="form-control select2 infinity">
                                                        <asp:ListItem Value="LAK" Text="LAK"></asp:ListItem>
														 <asp:ListItem Value="THB" Text="THB"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required">
                                                    <%= Resources.labels.cardamount %>
                                                </label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="lblcardamountpn" runat="server" CssClass="form-control  " MaxLength="23"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label">
                                                    <%= Resources.labels.type %>
                                                </label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList Width="100%" ID="lbltypepn" runat="server" CssClass="form-control select2 infinity">
                                                        <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required">
                                                    <%= Resources.labels.realmoney %>
                                                </label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="lblrealmoneypn" runat="server" CssClass="form-control " MaxLength="23"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label">
                                                    <%= Resources.labels.trangthai %>
                                                </label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:DropDownList ID="lblstatuspn" runat="server" Width="100%" CssClass="form-control select2 infinity">
                                                        <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                                        <asp:ListItem Value="B" Text="Block"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p class="auto-style1">
                    <div style="text-align: center; margin-top: 10px;">
                        &nbsp;<asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();" OnClick="btsave_Click" />
                        &nbsp;  
        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_Click" />
                        &nbsp;<asp:Button ID="Button8" runat="server" Text="<%$ Resources:labels, back %>" CssClass="btnGeneral btn btn-secondary" OnClick="btback_OnClick" />
                    </div>
                </p>
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>

<script language="javascript">
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
        //var obj2=document.getElementById(obj1);
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
        if (boolean(check) = false) {
            deletebutton.hidden = true;
        }
        else {
            deletebutton.hidden = false;
        }
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

    //03102019
    function ValidateID1(obj) {
        var maxchar = 50;
        if (this.id) obj = this;
        replaceSQLChar1(obj);
        //obj.value = obj.value.replace(/[^\w+$]/gi, '');
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



