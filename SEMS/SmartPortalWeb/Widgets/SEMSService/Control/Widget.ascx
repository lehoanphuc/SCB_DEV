<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSService_Control_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<link href="CSS/bootstrap.min.css" rel="stylesheet" />
<link href="CSS/smallBoostrap.css" rel="stylesheet" />
<link href="widgets/SEMSHeader/css/style.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .tbcus tr, .tbcus tr a {
        color: #867E7E;
        font-size: 14px;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="al">
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
        </div>
        <div id="divError">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img alt="" src="widgets/SEMSKRCBank/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" runat="server" />
        </div>
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-2">
                    &nbsp;
                </div>
                <div class="col-sm-8">
                    <div class="content form-horizontal">
                        <asp:Panel ID="pnAdd" runat="server" DefaultButton="btnSave">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <label style="color: red; font-weight: bold">
                                                <%=Resources.labels.serviceid %>*
                                            </label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtServiceId" onkeyup="this.value=this.value.toUpperCase()" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <label style="color: red; font-weight: bold">
                                                <%=Resources.labels.shortname %>*
                                            </label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtServiceCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <label style="color: red; font-weight: bold">
                                                <%=Resources.labels.servicename %>*
                                            </label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtServiceName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, servicetype%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control select2 infinity">
                                                <asp:ListItem Value="ONLINE" Text="<%$ Resources:labels, ONLINE%>" />
                                                <asp:ListItem Value="OFFLINE" Text="<%$ Resources:labels, OFFLINE%>" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, corpname%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCorp" runat="server" CssClass="form-control select2" AutoPostBack="true"  OnSelectedIndexChanged="ddlCorp_OnSelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, catalogname%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8" style="padding-top: 7px; font-size: 14px;">
                                            <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label runat="server" Text="<%$ Resources:labels, desc%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-4 control-label">
                                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, status%>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2 infinity">
                                                <asp:ListItem Value="A" Text="<%$ Resources:labels, active%>" />
                                                <asp:ListItem Value="N" Text="<%$ Resources:labels, inactive%>" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="row center p-tb-15">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-submit" Text="<%$ Resources:labels, save %>" OnClientClick="Loading();return validate();" OnClick="btnSave_Click" />
                            &nbsp;&nbsp; 
                            <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" OnClientClick="Loading();return Confirm1();" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-2">
                    &nbsp;
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
    function validate() {
        if (validateEmpty('<%=txtServiceCode.ClientID %>', '<%=Resources.labels.bancannhapservicecode %>')) {
            if (validateEmpty('<%=txtServiceName.ClientID %>', '<%=Resources.labels.bancannhapservicename %>')) {
            }
            else {
                document.getElementById('<%=txtServiceName.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtServiceCode.ClientID %>').focus();
            return false;
        }
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function Confirm1() {
        return confirm('Are you sure you want to exit?');
    }
    function Confirm2() {
        return confirm('Are you sure you want to delete?');
    }
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
</script>
