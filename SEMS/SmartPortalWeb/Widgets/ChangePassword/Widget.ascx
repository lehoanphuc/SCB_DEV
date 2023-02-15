<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_ChangePassword_Widget" %>
<link href="Widgets/ChangePassword/CSS/Default.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

    function validate() {
        //validate old pass
        if (document.getElementById('<%=txtOldPassword.ClientID%>').value == '') {
            alert('<%=Resources.labels.passwordrequire %>');
            return false;
        }
        else {
            //validate password
            if (document.getElementById('<%=txtNewPassword.ClientID%>').value == '') {
                alert('<%=Resources.labels.passwordrequire %>');
                return false;
            }
            else {
                //validate retype password
                if (document.getElementById('<%=txtRePassword.ClientID%>').value == '') {
                    alert('<%=Resources.labels.passwordrequire %>');
                    return false;
                }
                else {
                    //validate retype pass with pass
                    if (document.getElementById('<%=txtNewPassword.ClientID%>').value != document.getElementById('<%=txtRePassword.ClientID%>').value) {
                        alert('<%=Resources.labels.passwordcompare %>');
                        return false;
                    }
                    else {
                        //validate password
                        if (document.getElementById('<%=txtNewPassword.ClientID%>').value.length < 6) {
                            alert('<%=Resources.labels.passwordlength %>');
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                }
            }
        }
    }
</script>

<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton1" runat="server" text='<%$ Resources:labels, change %>' OnClientClick="return validate();"
                onclick="btnLogin_Click" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton2" runat="server" text='<%$ Resources:labels, exit %>' 
                onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>

<div id="login">
<div style=" text-align:center; margin:5px 1px 5px 1px;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
</div>

<asp:Panel ID="pnFocus" DefaultButton="btnLogin" runat="server">
<table class="tblLogin">
    <tr>
        <td class="tdleft">
            <asp:Label ID="lblUserName" runat="server" text='<%$ Resources:labels, oldpass %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="lblPassword" runat="server" text='<%$ Resources:labels, newpass %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="lblPassword0" runat="server" 
                text="<%$ Resources:labels, repassword %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    
</table>
</asp:Panel>
<div style="padding:5px 0px 5px 5px; text-align:center;">
    <hr />
    <img alt="" src="widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnLogin" runat="server" text='<%$ Resources:labels, change %>' OnClientClick="return validate();"
                onclick="btnLogin_Click" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btnExit" runat="server" text='<%$ Resources:labels, exit %>' 
                onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>
</div>