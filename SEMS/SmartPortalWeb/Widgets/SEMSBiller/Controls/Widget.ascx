<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBiller_Controls_Widget" %>
<asp:ScriptManager runat="server" ID="ScriptManager1" />

<asp:Panel runat="server" ID="UpdatePanel1">
    <div class="subheader">
        <h1 class="subheader-title">
            <asp:Label runat="server" ID="lblTitle" />
        </h1>
    </div>

    <div id="divError">
        <asp:Label runat="server" ID="lblError" Text="" Font-Bold="True" ForeColor="Red" />
    </div>
    <div class="row">
        <div class="col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-hdr">
                    <h2><%= Resources.labels.billerinfo %></h2>
                </div>
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel runat="server" ID="pnAdd">
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label required"><%= Resources.labels.billerid %></label>
                                        <div class=" col-sm-8 col-xs-12">
                                            <asp:TextBox runat="server" ID="txtBillerID" CssClass="form-control" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label required"><%= Resources.labels.billercode %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox runat="server" ID="txtBillerCode" CssClass="form-control" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label required"><%= Resources.labels.billername %></label>
                                        <div class=" col-sm-8 col-xs-12">
                                            <asp:TextBox runat="server" ID="txtBillerName" CssClass="form-control" MaxLength="200" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.shortname %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox runat="server" ID="txtShortName" CssClass="form-control" MaxLength="100" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.website %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtWebsite" CssClass="form-control" runat="server" MaxLength="100" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.phone %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server" MaxLength="100" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.countryname %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlCountryID" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.catName %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlCatID" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.thanhpho %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlCity" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.showasbill %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList runat="server" ID="ddlBill" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label required"><%= Resources.labels.timeopen %></label>
                                        <div class="col-sm-8 col-xs-12 custom-control">
                                            <asp:TextBox runat="server" CssClass="form-control igtxtTime" ID="txtTimeOpen" MaxLength="8" />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label required"><%= Resources.labels.timeclose %></label>
                                        <div class="col-sm-8 col-xs-12 custom-control">
                                            <asp:TextBox runat="server" CssClass="form-control igtxtTime" ID="txtTimeClose" MaxLength="8" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <div class="col-sm-4 col-xs-12 ">
                                            <asp:Label runat="server" CssClass="control-label" ID="Label1" Text="<%$ Resources:labels, SUNDRYACCTNOBANK %>" Visible="true" />
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtBankSundry" CssClass="form-control" runat="server" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <div class="col-sm-4 col-xs-12 ">
                                            <asp:Label runat="server" CssClass="control-label" ID="Label2" Text="<%$ Resources:labels, INCOMEACCTNOBANK %>" Visible="true" />
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtBankIncome" CssClass="form-control" runat="server" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <div class="col-sm-4 col-xs-12 ">
                                            <asp:Label runat="server" CssClass="control-label" ID="Label3" Text="<%$ Resources:labels, SUNDRYACCTNOWALLET %>" Visible="true" />
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtWalletSundry" CssClass="form-control" runat="server" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <div class="col-sm-4 col-xs-12 ">
                                            <asp:Label runat="server" CssClass="control-label" ID="Label4" Text="<%$ Resources:labels, INCOMEACCTNOWALLET %>" Visible="true" />
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtWalletIncome" CssClass="form-control" runat="server" MaxLength="50" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.status %></label>
                                        <div class=" col-sm-8 col-xs-12">
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control select2 infinity">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox ID="txtDesc" CssClass="form-control" TextMode="MultiLine" MaxLength="250"  onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <div class="col-sm-4 col-xs-12 ">
                                            <asp:Label runat="server" CssClass="control-label" ID="lbUpload" Text="<%$ Resources:labels, logobin %>" Visible="true" />
                                        </div>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:TextBox runat="server" ID="txtLogoBin" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:Label runat="server" CssClass="control-label" ID="lbImg" Text="<%$ Resources:labels, txtImg %>" Visible="false" />
                                        </div>
                                        <div class="col-sm-8 col-xs-12 control-label">
                                            <asp:Image ID="imgLogo" CssClass="img-rounded" runat="server" Visible="false" Height="100px" Width="100px" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row">
                                    <div class="col-sm-4 col-xs-12 ">
                                            <asp:Label runat="server" CssClass="control-label" ID="lbPath" Text="<%$ Resources:labels, txtPath %>" Visible="false" />
                                        </div>
                                    <div class="form-group">
                                        
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                            <asp:FileUpload runat="server" ID="fuLogoBin" CssClass="form-control" />
                                </div>
                                <div class="col-sm-2 col-xs-12">
                                            <asp:CheckBox CssClass="custom-control" ID="cbxType" runat="server" AutoPostBack="true" OnCheckedChanged="cbxType_CheckedChanged" Text="<%$ Resources:labels, url  %>" />
                                        </div>
                            </div>--%>

                            <%--<div class="row">
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12 control-label"><%= Resources.labels.region %></label>
                                        <div class="col-sm-8 col-xs-12">
                                            <asp:DropDownList ID="ddlRegion" CssClass="form-control select2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </div>--%>
                        </asp:Panel>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>"
                                OnClientClick="return checkValidation()" OnClick="btnSave_Click" />
                            <asp:Button Text="<%$ Resources:labels, Clear %>" ID="btnClear" CssClass="btn btn-secondary" runat="server" OnClick="btnClear_Click" />
                            <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function checkValidation() {
        if (!validateEmpty('<%= txtBillerID.ClientID %>', '<%= Resources.labels.billeridrequired %>')) {
            document.getElementById('<%= txtBillerID.ClientID %>').focus();
            return false;
        }
        if (!hasWhiteSpace('<%=txtBillerID.ClientID %>', '<%=Resources.labels.billeridwhitespace %>')) {
            document.getElementById('<%=txtBillerID.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtBillerCode.ClientID %>','<%= Resources.labels.billercoderequired %>')) {
            document.getElementById('<%= txtBillerCode.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtBillerName.ClientID %>','<%= Resources.labels.billernamerequired %>')) {
            document.getElementById('<%= txtBillerName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtTimeOpen.ClientID %>','<%= Resources.labels.timeopenrequired %>')) {
            document.getElementById('<%= txtBillerName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtTimeClose.ClientID %>','<%= Resources.labels.timecloserequired %>')) {
            document.getElementById('<%= txtBillerName.ClientID %>').focus();
            return false;
        }
        if (document.getElementById("<%=txtBillerName.ClientID %>").value.length > 100) {
            alert("The system not allow input >100 Character");
            return false;
        }
        <%--if (!validateDropdownList('<%= ddlCountryID.ClientID %>', '<%= Resources.labels.selectcountryrequired %>')) {
            $('#' + country).select2('open');
            return false;
        }
        if (!validateDropdownList('<%= ddlRegion.ClientID %>', '<%= Resources.labels.selectregionrequired %>')) {
            $('#' + region).select2('open');
            return false;
        }
        if (!validateDropdownList('<%= ddlCity.ClientID %>', '<%= Resources.labels.selectcityrequired %>')) {
            $('#' + city).select2('open');
            return false;
        }--%>
        <%--if (document.getElementById('<%= cbxType.ClientID %>').checked == true) {
            if (!validateEmpty('<%= txtLogoBin.ClientID %>','<%= Resources.labels.logobinrequired %>')) {
              document.getElementById('<%= txtLogoBin.ClientID %>').focus();
              return false;
          }
        }--%>
        <%-- else {
          if (!validateEmpty('<%= fuLogoBin.ClientID %>','<%= Resources.labels.logobinuploadrequired %>')) {
              document.getElementById('<%= fuLogoBin.ClientID %>').focus();
                return false;
            }
        } --%>
    return true;
    }
    var maxFileSize = '<%= System.Configuration.ConfigurationManager.AppSettings["imageuploadec"].ToString() %>';
    var validFiles = ["bmp", "gif", "png", "jpg", "jpeg"];
    function CheckExt(obj) {
        var source = obj.value;
        var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
        for (var i = 0; i < validFiles.length; i++) {
            if (validFiles[i] == ext)
                break;
        }
        if (i >= validFiles.length) {
            obj.value = "";
            alert("THAT IS NOT A VALID IMAGE\nPlease load an image with an extention of one of the following:\n\n" + validFiles.join(", "));
        }
        console.log(obj.files[0].size)
        if (obj.files[0].size > maxFileSize) {
            obj.value = "";
            alert('Your file is too big! Please choose file below 4MB.');
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
