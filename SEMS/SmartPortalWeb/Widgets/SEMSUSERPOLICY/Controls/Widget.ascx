<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUSERPOLICY_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlpwdcplx" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
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
                            <%=Resources.labels.thongtinpolicy%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row hidden">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.maanninh %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txPolicyID" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tendichvu %></label>
                                            <div class="col-sm-4 col-xs-12 custom-control">
                                                <asp:CheckBoxList AutoPostBack="true" ID="cblServiceID" Width="100%" Enabled="true" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.motaveanninh %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txDescr" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.cohieuluctu %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txEffrom" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.cohieulucdenunlimit %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txefto" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.chopheplichsupassword %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txPwdhis" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tuoithomatkhautoida %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txPwdagemax" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.dodaimatkhautoithieu %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txMinpwdlen" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.matkhauyeucauphuctap %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList onchange="hidepwdcpdetail()" ID="ddlpwdcplx" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="True" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-4 col-xs-12">
                                                <%--(<%=Resources.labels.khuyencaosetnochodichvuSMS %>)--%>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row trcplx" id="trlc" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <label class="col-sm-4 col-xs-12 control-label indenling"><%=Resources.labels.itnhatcomotkytuthuong %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="indenling form-control select2 infinity" ID="ddlpwdcplxlc" Width="100%" runat="server">
                                                    <asp:ListItem Value="True" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row trcplx" id="truc" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <label class="col-sm-4 col-xs-12 control-label indenling"><%=Resources.labels.itnhatcomotkytuhoa %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="indenling form-control select2 infinity" ID="ddlpwdcplxuc" Width="100%" runat="server">
                                                    <asp:ListItem Value="True" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row trcplx" id="trsc" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <label class="col-sm-4 col-xs-12 control-label indenling"><%=Resources.labels.itnhatcomotkytudacbiet %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="indenling form-control select2 infinity" ID="ddlpwdcplxsc" Width="100%" runat="server">
                                                    <asp:ListItem Value="True" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row trcplx" id="trnc" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <label class="col-sm-4 col-xs-12 control-label indenling"><%=Resources.labels.itnhatcomotkytuso %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="indenling form-control select2 infinity" ID="ddlpwdcplxnc" Width="100%" runat="server">
                                                    <asp:ListItem Value="True" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="tr1" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.thoigianloginphaitheothoigiandattruoc %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList onchange="hidetimelogindetail()" ID="ddltimelogin" runat="server" CssClass="form-control select2 infinity" Width="100%">
                                                    <asp:ListItem Value="True" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-4 col-xs-12">
                                                <%--(<%=Resources.labels.khuyencaosetnochodichvuSMS %>)--%>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row trtimelgin" id="trlginfr" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.cothedangnhaptu %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox class="indenling form-control igtxtTime" ID="txlginfr" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row trtimelgin" id="trlginto" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-1 col-xs-12"></div>
                                            <label class="col-sm-4 col-xs-12 control-label indenling required"><%=Resources.labels.cothedangnhapden %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox class="indenling form-control igtxtTime" ID="txlginto" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.solandangnhapsaichophep %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txLKOUTTHRS" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.sophutseresetsolandangnhapsaive0 %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txRESETLKOUT" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.lapolicymacdinh %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList ID="ddlisdefault" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="False" Text="<%$ Resources:labels, khong %>"></asp:ListItem>
                                                    <asp:ListItem Value="True" Text="<%$ Resources:labels, co %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading(); return useConfirmation()" OnClick="btsave_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_OnClick" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading();" OnClick="btback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        url = url.toLowerCase(); // This is just to avoid case sensitiveness  
        name = name.replace(/[\[\]]/g, "\\$&").toLowerCase();// This is just to avoid case sensitiveness for query parameter name
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }
    function useConfirmation() {
        var sid = getParameterByName('sid');
        var name = document.getElementById("<%= btsave.ClientID %>").value;

        if (name == "<%=Resources.labels.capnhat %>" & sid != 'sems') {

            if (confirm("This will change all Policy of account base on this Policy. Are you sure to do?"))
                return true;
            else
                return false;
        }
    }

    function hidepwdcpdetail() {
        jQuery(function ($) {
            var ddlpwd = $("[id*=ddlpwdcplx]");

            var selectedValue = ddlpwd.val();
            if (selectedValue == "True") {
                $(".trcplx").show();
            }
            else {

                $(".trcplx").hide();
            }
        });
    }
    function hidetimelogindetail() {
        jQuery(function ($) {
            var ddlpwd = $("[id*=ddltimelogin]");
            var selectedValue = ddlpwd.val();
            if (selectedValue == "True") {
                $(".trtimelgin").show();
            }
            else {
                $(".trtimelgin").hide();
            }
        });
    }
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                hidepwdcpdetail();
                hidetimelogindetail();
            }
        });
    };
</script>
