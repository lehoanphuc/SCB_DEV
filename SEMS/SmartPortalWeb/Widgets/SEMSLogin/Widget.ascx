<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSLogin_Widget" %>
<link href="CSS/login.css" rel="stylesheet" />
<script src="JS/sha256.js"></script>

<asp:Panel ID="pnLogin" runat="server" DefaultButton="btnLogin">
    <div class="page-wrapper" >
        <div class="bg-brand-gradient">
            <div class="blankpage-form-field">
                <div class="page-logo m-0 w-100 align-items-center justify-content-center rounded border-bottom-left-radius-0 border-bottom-right-radius-0 px-4">
                    <a href="javascript:void(0)" class="page-logo-link press-scale-down d-flex align-items-center">
                        <%--<img src="Images/favlist2.png">--%><img src = "../../../Images/logo.png" />
                    </a>
                </div>
                <div class="card p-4 border-top-left-radius-0 border-top-right-radius-0">
                    <div>
                        <div class="form-group">
                            <asp:Label ID="lblAlert" ForeColor="Red" runat="server" Font-Bold="False"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="<%$ Resources:labels, matkhau %>"></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="form-group text-left hidden">
                            <div class="custom-control">
                                <asp:CheckBox ID="cbRememberMe" runat="server" Text="<%$ Resources:labels, luuthongtindangnhap %>" />
                            </div>
                        </div>
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block float-right" OnClientClick="clientValidate()" OnClick="btnLogin_Click" Text="<%$ Resources:labels, dangnhapthuong %>" />
                    </div>
                    <div class="blankpage-footer text-center" style="margin-top: 20px; margin-bottom: -10px">
                    <asp:LinkButton ID="lbForgotPassword" OnClick="lbForgotPassword_OnClick" Text='<%$ Resources:labels, quenmatkhau %>' runat="server"></asp:LinkButton>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
                        <%--<a href="<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=lo-LA") %> "><img src="../../Images/lao_flag_icon.png" /></a>&nbsp;&nbsp;--%>
                        <a href="<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US") %> "><img src="Images/en_US.png" />&nbsp;</a>
                        <a href="<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US") %> "><img src="Images/thailand.png" /></a>
                        <%--<a href="<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=zh-CN") %> "><img src="Images/sh-CN.png" /></a>--%>
                        
                </div>
                </div>
                
            </div>
        </div>
    </div>
</asp:Panel>
<script type="text/javascript">
    function clientValidate() {
        var hash = sha256.create();
        var combine = document.getElementById('<%= txtUserName.ClientID %>').value.toUpperCase().concat(document.getElementById('<%= txtPassword.ClientID %>').value);
        hash.update(combine);
        hash.hex();
        document.getElementById('<%= txtPassword.ClientID %>').value = hash;
    }
</script>
