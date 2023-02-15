<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTelco_Control_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="~/Widgets/SEMSTelco/ViewCard/Widget.ascx" TagPrefix="uc1" TagName="Widget" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSUserApprove/Images/userApprove.png" style="width: 40px; height: 32px; margin-bottom: 10px;" align="middle" />
    <div class="subheader">
        <h1 class="subheader-title">
            <asp:Label ID="lbltitle" runat="server"></asp:Label>
        </h1>
    </div>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <asp:Panel runat="server" DefaultButton="btnsavetelco">
                <div class="panel-hdr">
                    <h2>
                        <%=Resources.labels.telco%>
                    </h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <div id="divSearch" class="" runat="server">
                            <asp:Panel runat="server" ID="pnTelco">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.TelcoName %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblTelecom" runat="server" CssClass="form-control" onkeyup="ValidateID1(this);" onkeyDown="ValidateID1(this);" onpaste="ValidateID1(this);" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.TelcoShort %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblShortName" runat="server" CssClass="form-control" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.SUNDRYACCTNOBANK %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblGLAccBalance" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.INCOMEACCTNOBANK %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblGLAccFee" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.SUNDRYACCTNOWALLET %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblWlBalance" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.INCOMEACCTNOWALLET %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblWlFee" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ELoadBillerCode %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblEloadBillerCode" runat="server" CssClass="form-control" onkeyDown="ValidateID(this);" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.Eload %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="lblEloadTeco" Width="100%" CssClass="form-control select2 infinity" runat="server">
                                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.EPinBillerCode %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lblEPinBillerCode" runat="server" CssClass="form-control" onkeyDown="ValidateID(this);" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.EPin %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="lblEpinTeco" Width="100%" runat="server" CssClass="form-control select2 infinity">
                                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12 ">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.trangthai %></label>

                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="lblstatustelco" Width="100%" runat="server" CssClass="form-control select2 infinity">
                                                    <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="B" Text="Block"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</div>

<div id="divButton1" runat="server" style="text-align: center; margin: 10px;">
    &nbsp;<asp:Button CssClass="btn btn-primary" ID="btnsavetelco" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading(); return validate()" OnClick="btsave_Click" />
    <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_Click" />
    &nbsp;<asp:Button ID="btnback" CssClass="btn btn-secondary btnGeneral" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button8_Click" />
</div>
<div class="" id="divaddnew" runat="server">
    <asp:Panel runat="server" ID="pnCard" Visible="false">
        <table class="style1" cellspacing="1" cellpadding="3">
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, Cardid %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="lblcardidpn" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, shortname %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="lblshortnamepn" runat="server" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, tiente %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:DropDownList Width="100%" ID="lblccyidpn" runat="server">
                        <asp:ListItem Value="LAK" Text="LAK"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, cardamount %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="lblcardamountpn" runat="server" onkeypress="allowNumbersOnly(event)" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, type %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:DropDownList Width="100%" ID="lbltypepn" runat="server">
                        <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, realmoney %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="lblrealmoneypn" runat="server" onkeypress="allowNumbersOnly(event)" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:DropDownList Width="100%" ID="lblstatuspn" runat="server">
                        <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                        <asp:ListItem Value="D" Text="Delete"></asp:ListItem>
                        <asp:ListItem Value="B" Text="Block"></asp:ListItem>
                    </asp:DropDownList>

                </td>

            </tr>
        </table>
    </asp:Panel>
</div>


<div id="divSearchCardID" class="divSearch" runat="server">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearchCard">
        <table class="style1" cellspacing="1" cellpadding="3">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, shortname %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtShortNameSearch" runat="server" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList Width="100%" ID="ddlStatusSearch" runat="server">
                        <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                        <asp:ListItem Value="B" Text="Block"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="10%">
                    <asp:Button ID="btnSearchCard" runat="server" Text="<%$ Resources:labels, search %>"
                        OnClick="btnSearchCard_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, cardamount %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCardamountSearch" runat="server" onkeyup="ValidateID(this);" onkeyDown="ValidateID(this);" onpaste="ValidateID(this);" onkeypress="allowNumbersOnly(event)"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</div>
<div id="divButtonbar" runat="server" class="divButtonbar">
    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:labels, addcard %>" CssClass="btnGeneral" OnClick="Button2_Click" />
</div>


<div id="divTable" class="divResult" runat="server">
    <uc1:widget runat="server" id="Widget" />
    <div id="divResultPrefix" class="divResult" runat="server">
        <asp:GridView ID="gvPrefix" runat="server" BackColor="White" CssClass="table table-hover"
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="15"
            Width="100%" AllowPaging="True" AutoGenerateColumns="False"
            OnRowDataBound="gvPrefix_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText=" <%$ Resources:labels, TelcoName %>">
                    <ItemTemplate>
                        <asp:Label ID="lbltelconame" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, Prefix %>">
                    <ItemTemplate>
                        <asp:Label ID="lblprefix" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, CountryPrefix %>">
                    <ItemTemplate>
                        <asp:Label ID="lblcountryprefix" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, groupid %>">
                    <ItemTemplate>
                        <asp:Label ID="lblgroupid" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, PhoneLength %>">
                    <ItemTemplate>
                        <asp:Label ID="lblphoenlen" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="gvFooterStyle hidden" />
            <PagerStyle HorizontalAlign="Center" CssClass="pager hidden" />
        </asp:GridView>
        <uc1:gridviewpaging runat="server" id="GridViewPaging" />
        <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
        <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
    </div>
</div>


<script>
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className="nohightlight";
                }
            }
        }
    }

    function confirmDelete(msg, item) {
        debugger;
        if (item != undefined) {
            if (item.href == "") return false;
            else return confirm(msg);;
        }
        debugger;
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        for (i = 0; i < count; i++) {
            if (elements[i].type == 'checkbox' && elements[i].disabled != true && elements[i].id != 'cbxSelectAll') {
                if (elements[i].checked == true) return confirm(msg);
            }
        }
        alert("You must choose record(s) to delete")
        return false;
    }

    function allowNumbersOnly(e) {
        var code = (e.which) ? e.which : e.keyCode;
        if (code > 31 && (code < 48 || code > 57)) {
            e.preventDefault();
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
    function ShowError(msg) {
        msg = msg != undefined ? msg : "";
        var lblError = document.getElementById('<%=lblError.ClientID%>');
        lblError.innerText = msg;
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
            obj.value.replace(/'|!|@|#|\$|%|\^|&|\*|;|xp_|script|iframe|delete|drop|exec|execute|--|<|\[|\]|{|}|\/|\||\+|=|\(|\)|~|`|:|"|>/g, "");
        }
    }

    function validate() {
        if (!validateEmpty('<%=lblTelecom.ClientID %>', '<%=Resources.labels.TelecoNamenotnull %>')) {
            document.getElementById('<%=lblTelecom.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=lblShortName.ClientID %>', '<%=Resources.labels.TelecoshortNamenotnull %>')) {
            document.getElementById('<%=lblShortName.ClientID %>').focus();
            return false;
        }
        return true;
    }
</script>

<div style="text-align: center; margin: 10px;">
    &nbsp;<asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();" OnClick="btsave_Click" />
    &nbsp;<asp:Button ID="Button8" runat="server" Text="<%$ Resources:labels, back %>" CssClass="btn btn-secondary btnGeneral" OnClick="Button8_Click" />
</div>
