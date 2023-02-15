<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBRequestExportStatement_Widget" %>
<style type="text/css">
    .style1 {
        width: 99.3%;
        margin-left: 3px;
    }

    .thtdbold {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }

    .thtd {
        border-left: solid 1px #b9bfc1;
        border-top: solid 1px #b9bfc1;
    }

    .thtdf {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
        border-top: solid 1px #b9bfc1;
    }

    .thtdff {
        border-top: solid 1px #b9bfc1;
    }

    .thtr {
        background-color: #EAFAFF;
        color: #003366;
        font-weight: bold;
    }

    .thtds {
        padding: 5px 5px 5px 5px;
    }
</style>
<link href="widgets/IBRequestExportStatement/CSS/css.css" rel="stylesheet" type="text/css" />
<script src="widgets/IBRequestExportStatement/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBRequestExportStatement/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBRequestExportStatement/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBRequestExportStatement/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBRequestExportStatement/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<style>
    .al {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
    }
</style>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div style="text-align: center; height: 8px">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <div class="cssProgress">
                <div class="progress1">
                    <div class="cssProgress-bar cssProgress-active" data-percent="100" style="transition: none; width: 100%;"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="al">
<%= Resources.labels.requestexportstatment %><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError" style="text-align: center;">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</div>
        <div>
            <asp:HiddenField runat="server" ID="hdUsername" />
             <asp:HiddenField runat="server" ID="HdFee" />
        </div>
        <asp:Panel ID="pnSearch" runat="server" class="divcontent">
            <div class="content search display-label">
                <div class="row form-group">
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.account %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:DropDownList ID="ddlAccount" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.serialno %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:TextBox ID="lblSerialNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.statementversion %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:DropDownList ID="ddlVersion" runat="server" CssClass="form-control">
                            <asp:ListItem Text="ThaiLand" Value="LA"></asp:ListItem>
                            <asp:ListItem Text="English" Value="EN"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.sodienthoai %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:TextBox ID="txtTel" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.fullname %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:TextBox ID="lblfullname" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.email %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:TextBox ID="lblEmail" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.tungay %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:TextBox ID="txtFromDate" autocomplete="off" CssClass="dateselect1 form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.denngay %></label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:TextBox ID="txtToDate" autocomplete="off" CssClass="dateselect1 form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-7 col-md-4">
                        <label class="bold"><%= Resources.labels.purposeofrequeststatement %></label>
                    </div>
                </div>
                <div>
                    <div style="margin-left: 50px">
                        <asp:RadioButton ID="rdPurpose1" Checked="true" runat="server" Text='<%$ Resources:labels, Purpose1 %>' GroupName="Purpose" AutoPostBack="true" OnCheckedChanged="Purpose_OnCheckedChanged" />
                    </div>
                    <div style="margin-left: 50px">
                        <asp:RadioButton ID="rdPurpose2" runat="server" Text='<%$ Resources:labels, Purpose2 %>' GroupName="Purpose" AutoPostBack="true" OnCheckedChanged="Purpose_OnCheckedChanged" />
                    </div>
                    <div style="margin-left: 50px">
                        <asp:RadioButton ID="rdPurpose3" runat="server" Text='<%$ Resources:labels, Purpose3 %>' GroupName="Purpose" AutoPostBack="true" OnCheckedChanged="Purpose_OnCheckedChanged" />
                    </div>
                </div>
                <div class="row" id="content" runat="server" visible="false">
                    <div class="col-xs-5 col-md-2">
                        <label class="bold"><%= Resources.labels.detail1 %>*</label>
                    </div>
                    <div class="col-xs-7 col-md-4">
                        <asp:TextBox ID="txtDesc" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:CheckBox ID="cbPolicy" runat="server"></asp:CheckBox>
                <span><%=Resources.labels.termsandconditionsofpsvb %></span>
                <asp:LinkButton runat="server" Text='<%$ Resources:labels, termsandconditions %>' ID="LbOpoen" OnClientClick="javascript:return poponload1()"></asp:LinkButton>
                <span><%=Resources.labels.ofpsvb %></span>
            </div>
            <div class="row" style="text-align: center; padding-top: 10px;">
                <asp:Button ID="btnSubmit" runat="server" Text='<%$ Resources:labels, Submit %>' CssClass="btn btn-primary"
                    OnClick="btnSubmit_Click" />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

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

    function validate() {
        if (echeck('<%=lblEmail.ClientID%>').value) {
            alert('<%=Resources.labels.toisemail %>');
            return false;
        }
        else {
            return true;
        }
    }
    function poponload1() {
        testwindow = window.open("widgets/IBRequestExportStatement/viewprint.aspx", "BienLai",
            "menubar=1,scrollbars=1,width=500,height=650");
        testwindow.moveTo(0, 0);
        return false;
    }

    $(function () {
        document.getElementById('<%=txtFromDate.ClientID %>').datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 30,
            onClose: function (selectedDate) {
                $("#to").datepicker("option", "minDate", selectedDate);
            }
        });
        document.getElementById('<%=txtToDate.ClientID %>').datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 20,
            onClose: function (selectedDate) {
                document.getElementById('<%=txtFromDate.ClientID %>').datepicker("option", "maxDate", selectedDate);
            }
        });
    });
</script>
