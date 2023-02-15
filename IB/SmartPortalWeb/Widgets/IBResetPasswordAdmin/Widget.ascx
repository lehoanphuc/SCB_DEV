<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSResetPasswords_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="al">
            <label class="bold "><%=Resources.labels.laylaimatkhaupinchouser %></label><br />
            <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""  Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="divcontent">
            <div class="col-sm-12 col-xs-12">
                <div class="">
                
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row">
                                    <div class="col-sm-12  col-xs-12">
                                        <div class="form-group">
                                            <%--<label class="col-sm-2 col-xs-12 col-sm-offset-2  required"><%=Resources.labels.dichvu %></label>--%>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" Visible="False" ID="ddlService" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 col-sm-offset-2  required" ><%=Resources.labels.username %></label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddluser" runat="server">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="dllDept" runat="server" Visible="False"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:Button ID="btcheckUs" CssClass="btn btn-primary" OnClick="btcheckUs_OnClick" Text="Check" OnClientClick="return validate()" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                      
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <asp:label runat="server" Visible="False" class="col-sm-2 col-xs-12 col-sm-offset-2 required"><%=Resources.labels.loginmethod %></asp:label>
                                            <div class="col-sm-4 col-xs-12">
                                                <asp:DropDownList CssClass="form-control select2 infinity" Width="100%" ID="ddlloginmethod" Visible="False"  runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 col-xs-12"></div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div id="ResCheck" runat="server">
                            <div class="">
                                <legend class="handle"><%=Resources.labels.thongtinnguoidung %></legend>
                             
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="col-sm-6 form-group">
                                            <div class="form-group">
                                                <label class="col-sm-4 "><%=Resources.labels.fullname %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtReFullName" Enabled="True" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 "><%=Resources.labels.address %></label>
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
                                                <label class="col-sm-4 "><%=Resources.labels.birthday %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtRebirtday" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" runat="server">
                                            <div class="form-group">
                                                <label class="col-sm-4 "><%=Resources.labels.gioitinh %></label>
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
                                                <label class="col-sm-4 "><%=Resources.labels.phone %></label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="txtRephone" Enabled="True" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6" runat="server">
                                            <div class="form-group">
                                                <label class="col-sm-4 "><%=Resources.labels.email %></label>
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
                                                <label class="col-sm-4 "><%=Resources.labels.authentype %></label>
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
                                        <label class="col-sm-2 col-xs-12 col-sm-offset-2  required"><%=Resources.labels.authentype %></label>
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
                                        <label class="col-sm-2 col-xs-12 col-sm-offset-2  required"><%=Resources.labels.loaithongbao %></label>
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
                            <asp:Button ID="Button1" Visible="False" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1);" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
  <%--  function validate() {
        if (!validateEmpty('<%=txtphone.ClientID %>', '<%=Resources.labels.bannhapuserphone %>')) {
            document.getElementById('<%=txtphone.ClientID %>').focus();
            return false;
        }
        return true;
    }--%>
</script>

<style>
    .panel-hdr {
        margin-bottom: 10px;
    }
    .form-control { padding: 0px 3px !important; }
    .div-btn {
        display: flex;
        justify-content: center;
    }
</style>