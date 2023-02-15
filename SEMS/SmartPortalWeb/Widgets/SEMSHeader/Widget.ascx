<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSHeader_Widget" %>
<div class="horizontal-menu">
    <nav class="navbar top-navbar col-sm-12 col-xs-12 p-0">
        <div class="container">
            <div class="text-center navbar-brand-wrapper d-flex align-items-center justify-content-center">
                <a class="nav-link d-flex justify-content-center align-items-center" href="<%= SmartPortal.Common.Encrypt.EncryptURL("?p=129") %>">
                    <img src="../../../Images/logo.png" />
            </div>
            <div class="navbar-menu-wrapper d-flex align-items-center justify-content-end">
                <ul class="navbar-nav navbar-nav-right">
                    
                    <li class="nav-item">
                        <a class="nav-link d-flex justify-content-center align-items-center" href="<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(false)+"l=en-US") %>">
                            <img src="/Images/en_US.png" class="nav-icon-title">
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link d-flex justify-content-center align-items-center" href="<%= SmartPortal.Common.Encrypt.EncryptURL(SmartPortal.Common.Utilities.Utility.LinkMultiLang(false)+"l=en-US") %>">
                            <img src="/Images/thailand.png" class="nav-icon-title">
                        </a>
                    </li>
                    
                    <li class="nav-item nav-profile dropdown" id="liProfile" runat="server">
                        <a class="nav-link dropdown-toggle" href="#" data-toggle="dropdown" id="profileDropdown-navbar" aria-expanded="false">
                            <img src="/Images/no-photo.jpg" alt="profile">
                        </a>
                        <div class="dropdown-menu dropdown-menu-right navbar-dropdown flat-dropdown" aria-labelledby="profileDropdown-navbar">
                            <div class="flat-dropdown-header">
                                <div class="d-flex">
                                    <img src="/Images/no-photo.jpg" alt="profile" class="profile-icon mr-2">
                                    <div>
                                        <span class="profile-name font-weight-bold">
                                            <asp:Label ID="lblUser" runat="server"></asp:Label></span>
                                        <p class="profile-designation">
                                            <asp:Label ID="lblRole" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="profile-dropdown-body">
                                <ul class="list-profile-items">
                                    <li class="profile-item">
                                        <asp:HyperLink runat="server" Text='<%$ Resources:labels, changepassword %>' ID="changepass"></asp:HyperLink>
                                    </li>
                                    <li class="profile-item">
                                        <asp:LinkButton ID="lbLogout" Text='<%$ Resources:labels, logout %>' runat="server" OnClick="lbLogout_Click"></asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <nav class="bottom-navbar">
        <div class="container">
            <a class="animateddrawer" id="ddsmoothmenu-mobiletoggle" href="#">
                <span></span>
            </a>
            <div id="smoothmenu1" class="ddsmoothmenu">
                <ul class="nav page-navigation">
                    <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
    </nav>
</div>
