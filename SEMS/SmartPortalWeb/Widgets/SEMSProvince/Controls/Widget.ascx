<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSProvince_Controls_Widget" %>
<asp:ScriptManager runat="server" ID="ScriptManager1">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblProvinceHeader" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtintinhthanh%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnFocus" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.matinh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCityCode" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tentinh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCityName" CssClass="form-control" MaxLength="1000" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countryname %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtmasms" CssClass="form-control" MaxLength="50" runat="server" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="ddlCountry" AutoPostBack="true" CssClass="form-control select2" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label "><%=Resources.labels.tentinhMM %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCityNameMM" CssClass="form-control" MaxLength="255" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>                              
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label  required"><%=Resources.labels.order %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtOrder" CssClass="form-control" MaxLength="3" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                   
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btsave_Click" />
                            <asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>"  OnClick="btClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function validate() {
        if (validateEmpty('<%=txtCityCode.ClientID %>', '<%=Resources.labels.bancannhapmatinh %>')) {
            if (validateEmpty('<%=txtCityName.ClientID %>', '<%=Resources.labels.bancannhaptentinh %>')) {
                if (validateEmpty('<%=txtOrder.ClientID %>', '<%=Resources.labels.validate_order %>')) {
                    return true;    
                }
                else {
                    document.getElementById('<%=txtOrder.ClientID %>').focus();
                    return false;
                }           
            }
            else {
                document.getElementById('<%=txtCityName.ClientID %>').focus();
                return false;
            }           
        }
        else {
            document.getElementById('<%=txtCityCode.ClientID %>').focus();
            return false;
        }
    }
    function validateEmpty(id, msg) {
        if (document.getElementById(id).value == "0" || document.getElementById(id).value == "") {
            window.alert(msg);
            return false;
        }
        else {
            return true;
        }
    }
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>