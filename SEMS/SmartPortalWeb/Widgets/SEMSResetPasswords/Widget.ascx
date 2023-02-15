<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSResetPasswords_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.laylaimatkhaupinchonguoidung%>
            </h1>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.resetpaspininfo%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-12  col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.dichvu %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddlService"  OnSelectedIndexChanged="ddlService_OnSelectedIndexChanged" AutoPostBack="True" runat="server" >
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.loginmethod %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddlloginmethod" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"  ><%=Resources.labels.username %> </label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:TextBox ID="txtphone" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:Button ID="btcheckUs" CssClass="btn btn-primary" OnClick="btcheckUs_OnClick" Text="Check" OnClientClick="return validate()" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                      
                            
                            </asp:Panel>
                        </div>

                        <div id="ResCheck" runat="server">
                            <div class="panel">
                                <div class="panel-hdr">
                                    <h2>
                                        <%=Resources.labels.thongtinnguoidung%>
                                    </h2>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="col-sm-6 form-group">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.fullname %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtReFullName" Enabled="True" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.address %></label>
                                                <div class="col-sm-8">
                                                    <asp:textbox ID="txtReAddress" Enabled="True" CssClass="form-control" runat="server"></asp:textbox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">

                                        <div class="col-sm-6 form-group">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.birthday %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtRebirtday" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" runat="server">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.gioitinh %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtReSex" Enabled="True" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">

                                        <div class="col-sm-6 form-group">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.phone %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtRephone" Enabled="True" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" runat="server">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.email %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtReEmail" Enabled="True" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="col-sm-6 form-group">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.authentype %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtReauthentype" Enabled="True" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" runat="server">

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                            <div class="row" runat="server" id="dAuthenType">
                                <div class="col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.authentype %></label>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddlauthentype" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-xs-12"></div>
                                    </div>
                                </div>
                            </div>
                         
                            <div class="row" runat="server" id="dSeninfo">
                                <div class="col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="col-sm-2 col-xs-12 col-sm-offset-2 control-label required"><%=Resources.labels.loaithongbao %></label>
                                        <div class="col-sm-4 col-xs-12">
                                            <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddlsendinfo" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-xs-12"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                            </div>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, lamlai %>" OnClientClick="return validate()" OnClick="btsaveandcont_Click" />
                            <asp:Button ID="Button1" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1);" />
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
        if (!validateEmpty('<%=txtphone.ClientID %>', '<%=Resources.labels.bannhapuserphone %>')) {
            document.getElementById('<%=txtphone.ClientID %>').focus();
            return false;
        }
        return true;
    }
</script>

<style>
    .panel-hdr {
        margin-bottom: 10px;
    }
     
</style>