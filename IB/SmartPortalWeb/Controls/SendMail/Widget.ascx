<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Controls_SendMail_Widget" %>
 
 <script type="text/javascript">
    //check email
    function echeck(str) {

        var at = "@"
        var dot = "."
        var lat = str.indexOf(at)
        var lstr = str.length
        var ldot = str.indexOf(dot)
        if (str.indexOf(at) == -1) {
           
            return false
        }

        if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {            
            return false
        }

        if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
           
            return false
        }

        if (str.indexOf(at, (lat + 1)) != -1) {
           
            return false
        }

        if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
           
            return false
        }

        if (str.indexOf(dot, (lat + 2)) == -1) {
           
            return false
        }

        if (str.indexOf(" ") != -1) {
           
            return false
        }

        return true
    }

    function validate() {
        //validate from
        if (document.getElementById('<%=txtFrom.ClientID%>').value == '') {
            alert('<%=Resources.labels.fromrequire %>');
            return false;
        }
        else {
            if (document.getElementById('<%=txtTo.ClientID%>').value == '') {
                alert('<%=Resources.labels.torequire %>');
                return false;
            }
            else {
                if (document.getElementById('<%=txtTitle.ClientID%>').value == '') {
                    alert('<%=Resources.labels.titlerequire %>');
                    return false;
                }
                else {
                    if (document.getElementById('<%=txtContent.ClientID%>').value == '') {
                        alert('<%=Resources.labels.contentrequire %>');
                        return false;
                    }
                    else {
                        if (echeck('<%=txtTo.ClientID%>').value) {
                            alert('<%=Resources.labels.toisemail %>');
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
 <style type="text/css">
        .style1
        {
            width: 100%;
            font-family:Verdana;
            font-size:9pt;
        }
        .tdEmailLeft
        {
        	width:40%;        	
        }
        .emailrequired
        {
        	color:Maroon;
        }
    </style>
 <div>   
        
    
        <table class="style1" style="background-image:url(widgets/banknews/images/banner_bg.gif);background-repeat:repeat-x;">
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td CssClass="tdEmailLeft" align="right">
                    &nbsp;</td>
                <td style="width:70%">
                    <asp:Label ID="lblAlert" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
            <tr>
                <td CssClass="tdEmailLeft" align="right">
                    <asp:Label ID="Label5" CssClass="emailrequired" runat="server" Text="*"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, from %>'></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" Width="400px" SkinID="txtTwoColumn"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td CssClass="tdEmailLeft" align="right">
                    <asp:Label ID="Label6" CssClass="emailrequired" runat="server" Text="*"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, to %>'></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="txtTo" runat="server" Width="400px" SkinID="txtTwoColumn"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td CssClass="tdEmailLeft" align="right">
                    <asp:Label ID="Label7" CssClass="emailrequired" runat="server" Text="*"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, title %>'></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="400px" SkinID="txtTwoColumn"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" CssClass="tdEmailLeft" align="right">
                    <asp:Label ID="Label8" CssClass="emailrequired" runat="server" Text="*"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, content %>'></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="400px" 
                        Height="86px" SkinID="txtTwoColumn"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td CssClass="tdEmailLeft">
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/Captcha/imgsecuritycode.aspx"/>
                    </td>
            </tr>
            <tr>
                <td CssClass="tdEmailLeft">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text='<%$ Resources:labels, send %>' 
                        Width="95px" OnClientClick="return validate();" onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
        
    
    </div>