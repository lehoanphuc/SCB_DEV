<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBHorizontalMenu_Widget" %>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:Literal runat="server" ID="Literalcss"></asp:Literal>
<div class="divroot">
    <div class="IBmenu ">
        <div style="height: 100%;
             display: flex;" class="container">
            <div class="nav-logo">
                <a href="<%# SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=86")%>"><asp:Image ID="imglogo" ImageUrl="/Images/logo_nonbg.png" runat="server"/></a>
            </div>
        <div class="nav-menu">
            <div class ="aheader" >
            <%--<p style='float:left; margin-right:20px;'>ທະນະຄານພົງສະຫວັນ ມຸ່ງໝັ້ນໃຫ້ທ່ານຫຼາຍກວ່າ...</p>
            <p style='float:left; margin-right:20px;'>You're not just another customer.</p>
            <p style='float:left'>We're not just another Bank…</p>--%>
        </div>
        <div class="pnMenu">
            <div class="menu_button">
                <asp:Literal runat="server" ID="ltrHome"></asp:Literal>
                <asp:Literal runat="server" ID="ltrGioiThieu"></asp:Literal>
                <asp:Literal runat="server" ID="ltrLienHe"></asp:Literal>
                <asp:Literal runat="server" ID="ltrHuongDanSuDung"></asp:Literal>
                <asp:Literal runat="server" ID="ltrDieuKhoan"></asp:Literal>
                <asp:Literal runat="server" ID="litChangePass"></asp:Literal>
                <asp:Literal runat="server" ID="litLogout"></asp:Literal>
                <asp:Literal runat="server" ID="litProfile"></asp:Literal>
                <div id="divLang">
                    <a href=" <% = SmartPortal.Common.Encrypt.EncryptURL("Default.aspx?p=88")%>">
                        <img src="../../Images/profile.png" style="width:25px; height:25px" />
                    </a>
                    <a href="<%= SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US" %> ">
						<img src="App_Themes/InternetBanking/images/flag-english.png" style='border: 1px solid #dde6ea;' width="25" height="20" alt="English" title="English" />
                    </a>
                    <%--<a href="<%= SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=lo-LA" %> ">
                        <img src="../../Images/lo-LA.png" style='border: 1px solid #dde6ea;' width="25" height="20" />
					</a>--%>
                       <a href="<%= SmartPortal.Common.Utilities.Utility.LinkMultiLang(true)+"l=en-US" %> ">
						<img src="../../Images/flag-thailand.png" style='border: 1px solid #dde6ea;' width="25" height="20"/>
                    </a>
                    
                </div>
            </div>
        </div></div>
        </div>
    </div>

</div>
<script>
        document.getElementById("logout").addEventListener("click", function(e)
        {
            var check = confirm("<%=Resources.labels.confirmlogout %>");
            if(!check)
            {
                e.preventDefault();
            }
        }
            
    )
</script>
