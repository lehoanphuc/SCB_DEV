<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCountry_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleCountry" runat="server"></asp:Label>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.countryinformation%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countryid %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCountryID" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countrycode %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCountryCode" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countryname %> </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCountryName" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.m_countryname %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtM_CountryName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.capital %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCapital" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.currency %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCurrency" CssClass="form-control select2" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.timezone %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlTimeZone" CssClass="form-control select2" runat="server" Width="100%">
                                                    <asp:ListItem Selected="true" Value="">Please choose a timezone! </asp:ListItem>
                                                    <asp:ListItem Value="0"> (gmt-12:00) international date line west </asp:ListItem>
                                                    <asp:ListItem Value="1"> (gmt-11:00) midway island, samoa </asp:ListItem>
                                                    <asp:ListItem Value="2"> (gmt-10:00) hawaii </asp:ListItem>
                                                    <asp:ListItem Value="3"> (gmt-09:00) alaska </asp:ListItem>
                                                    <asp:ListItem Value="4"> (gmt-08:00) pacific time (us & canada) </asp:ListItem>
                                                    <asp:ListItem Value="5"> (gmt-07:00) mountain time (us & canada) </asp:ListItem>
                                                    <asp:ListItem Value="6"> (gmt-06:00) central america </asp:ListItem>
                                                    <asp:ListItem Value="7"> (gmt-05:00) indiana (east) </asp:ListItem>
                                                    <asp:ListItem Value="8"> (GMT-04:30) Caracas </asp:ListItem>
                                                    <asp:ListItem Value="9"> (gmt-04:00) atlantic time (canada) </asp:ListItem>
                                                    <asp:ListItem Value="10"> (gmt-03:30) newfoundland </asp:ListItem>
                                                    <asp:ListItem Value="11"> (gmt-03:00) greenland </asp:ListItem>
                                                    <asp:ListItem Value="12"> (gmt-02:00) mid-atlantic</asp:ListItem>
                                                    <asp:ListItem Value="13"> (gmt-01:00) azores </asp:ListItem>
                                                    <asp:ListItem Value="14"> (gmt+01:00) sarajevo, skopje, warsaw, zagreb </asp:ListItem>
                                                    <asp:ListItem Value="15"> (gmt+02:00) athens, bucharest, istanbul </asp:ListItem>
                                                    <asp:ListItem Value="16"> (GMT+03:00) Baghdad </asp:ListItem>
                                                    <asp:ListItem Value="17"> (GMT+03:30) Tehran </asp:ListItem>
                                                    <asp:ListItem Value="18"> (gmt+04:00) yerevan </asp:ListItem>
                                                    <asp:ListItem Value="19"> (GMT+04:30) Kabul </asp:ListItem>
                                                    <asp:ListItem Value="20"> (gmt+05:00) islamabad, karachi, tashkent </asp:ListItem>
                                                    <asp:ListItem Value="21"> (GMT+05:30) Sri Jayawardenepura </asp:ListItem>
                                                    <asp:ListItem Value="22"> (GMT+05:45) Kathmandu </asp:ListItem>
                                                    <asp:ListItem Value="23"> (gmt+06:00) almaty, novosibirsk </asp:ListItem>
                                                    <asp:ListItem Value="24"> (gmt+06:30) Yangon (Rangoon) </asp:ListItem>
                                                    <asp:ListItem Value="25"> (gmt+07:00) bangkok, hanoi, jakarta </asp:ListItem>
                                                    <asp:ListItem Value="26"> (gmt +8:00) beijing, perth, singapore, hong kong </asp:ListItem>
                                                    <asp:ListItem Value="27"> (gmt+09:00) seoul </asp:ListItem>
                                                    <asp:ListItem Value="28"> (gmt+09:30) darwin </asp:ListItem>
                                                    <asp:ListItem Value="29"> (gmt+10:00) guam, port moresby </asp:ListItem>
                                                    <asp:ListItem Value="30"> (gmt+11:00) magadan, solomon is., new caledonia </asp:ListItem>
                                                    <asp:ListItem Value="31"> (gmt+12:00) auckland, wellington </asp:ListItem>
                                                    <asp:ListItem Value="32"> (gmt+13:00) nuku'alofa </asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.language %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <%--<asp:TextBox ID="txtLanguage" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlLanguage" CssClass="form-control select2" runat="server" Width="100%"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server" Width="100%">
                                                    <asp:ListItem Selected="True" Value="A"> Active </asp:ListItem>
                                                    <asp:ListItem Value="I"> InActive </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.countryphone %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtContryPhone" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDescription" CssClass="form-control" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.order %>  </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtOrder" CssClass="form-control" MaxLength="3" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return checkValidation()" OnClick="btsave_Click" />
                            <asp:Button ID="Clear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="Clear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1);" OnClick="btback_Click" />
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
        if (!validateEmpty('<%=txtCountryID.ClientID %>','<%=Resources.labels.validate_countryid %>')) {
            document.getElementById('<%=txtCountryID.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtCountryCode.ClientID %>','<%=Resources.labels.validate_countrycode %>')) {
            document.getElementById('<%=txtCountryCode.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtCountryName.ClientID %>','<%=Resources.labels.validate_countryname %>')) {
            document.getElementById('<%=txtCountryName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtContryPhone.ClientID %>','<%=Resources.labels.validate_phonecode %>')) {
            document.getElementById('<%=txtContryPhone.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtOrder.ClientID %>','<%=Resources.labels.validate_order %>')) {
            document.getElementById('<%=txtOrder.ClientID %>').focus();
            return false;
        }
        return true;
    }
</script>
