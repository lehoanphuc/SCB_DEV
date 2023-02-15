<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_User_Controls_Widget" %>
<%@ Register assembly="FSSMaskEdit" namespace="SCBiCMS" tagprefix="cc1" %>
<link href="Widgets/User/Controls/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

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
    
    //check special character
    function data_change(field) {
        var check = true;
        var value = field.value; //get characters
        //check that all characters are digits, ., -, or ""
        for (var i = 0; i < field.value.length; ++i) {
            var new_key = value.charAt(i); //cycle through characters
            if (((new_key < "0") || (new_key > "9")) &&
                    !(new_key == "")) {
                check = false;
                break;
            }
        }
        //apply appropriate colour based on value
        if (!check) {
            return false;
        }
        else {
            return true;
        }
    }

    //check white space
    function hasWhiteSpace(s) {

       var re = /\s/ig;

        // Check for white space
        if (re.test(s)) {           
            return false;
        }
        return true;
    }
    
    function validate() {
        //validate username
        if (document.getElementById('<%=txtUserName.ClientID%>').value == '') {
            alert('<%=Resources.labels.usernamerequire %>');
            return false;
        }
        else {
            //validate password
            if (document.getElementById('<%=txtPass.ClientID%>').value == '') {
                alert('<%=Resources.labels.passwordrequire %>');
                return false;
            }
            else {
                //validate retype password
                if (document.getElementById('<%=txtRePass.ClientID%>').value == '') {
                    alert('<%=Resources.labels.retypepasswordrequire %>');
                    return false;
                }
                else {
                    //validate firstname
                    if (document.getElementById('<%=txtFirstName.ClientID%>').value == '') {
                        alert('<%=Resources.labels.firstnamerequire %>');
                        return false;
                    }
                    else {
                        //validate retype pass with pass
                        if (document.getElementById('<%=txtPass.ClientID%>').value != document.getElementById('<%=txtRePass.ClientID%>').value) {
                            alert('<%=Resources.labels.passwordcompare %>');
                            return false;
                        }
                        else {
                            //validate password
                            if (document.getElementById('<%=txtPass.ClientID%>').value.length < 6) {
                                alert('<%=Resources.labels.passwordlength %>');
                                return false;
                            }
                            else {
                                if (!data_change(document.getElementById('<%=txtPhone.ClientID%>'))) {
                                    alert('<%=Resources.labels.phonevalidate %>');
                                    return false;
                                }
                                else {
                                    var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";

                                    for (var i = 0; i < document.getElementById('<%=txtUserName.ClientID%>').value.length; i++) {
                                        if (iChars.indexOf(document.getElementById('<%=txtUserName.ClientID%>').value.charAt(i)) != -1) {
                                            alert('<%=Resources.labels.usernamespecialcharactervalidate %>');
                                            return false;
                                        }
                                    }
                                    
                                    
                                        //kiem tra khoang trang
                                        if (hasWhiteSpace(document.getElementById('<%=txtUserName.ClientID%>').value) == false) {
                                            alert('<%=Resources.labels.usernamewhitespace %>');
                                            return false;
                                        }
                                        else {
                                            //validate email
                                        if (document.getElementById('<%=txtEmail.ClientID%>').value != '') {
                                            //validate retype pass with pass                                           
                                            str = document.getElementById('<%=txtEmail.ClientID%>').value;
                                            if (echeck(str) == false) {
                                                alert('<%=Resources.labels.emailvalidate %>');
                                                return false;
                                            } else {return true;
                                            }
                                        }
                                        

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }


    function updatevalidate() {
       
                    //validate firstname
                    if (document.getElementById('<%=txtFirstName.ClientID%>').value == '') {
                        alert('<%=Resources.labels.firstnamerequire %>');
                        return false;
                    }
                    else {                       
                        if (!data_change(document.getElementById('<%=txtPhone.ClientID%>'))) {
                            alert('<%=Resources.labels.phonevalidate %>');
                            return false;
                        }
                        else {                            
                            //validate email
                            if (document.getElementById('<%=txtEmail.ClientID%>').value != '') {
                                //validate retype pass with pass
                                str = document.getElementById('<%=txtEmail.ClientID%>').value;
                               
                                if (echeck(str) == false) {
                                    alert('<%=Resources.labels.emailvalidate %>');
                                    return false;
                                } else {
                                    return true;
                                }
                            }
                        }
        }
    } 
</script>

<link href="widgets/user/controls/CSS/dhtmlgoodies_calendar.css?random=20051112" rel="stylesheet" type="text/css" />

<script src="widgets/user/controls/Scripts/dhtmlgoodies_calendar.js?random=20060118" type="text/javascript"></script>


<div style="padding:5px 0px 5px 5px; text-align:center">
     
    <asp:Image runat="server" ID="imgSave" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btnSave1" runat="server"  onclick="btOK_Click" 
                Text="<%$ Resources:labels, save %>" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton2" runat="server"  onclick="btCancel_Click" CausesValidation="false"
                Text="<%$ Resources:labels, exit %>" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
    <hr />
</div>

<div style=" text-align:center; margin:5px 1px 5px 1px;">
<asp:Label ID="lblAlert" runat="server" ForeColor="Red" Font-Bold="True" 
        SkinID="lblImportant"></asp:Label>
</div>

<div style=" text-align:right; margin:5px 1px 5px 1px; padding-right:5px;">
<asp:Label ID="Label1" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
<asp:Label ID="Label2" runat="server" SkinID="lblImportant" Text='<%$ Resources:labels, requiredfield %>'></asp:Label>
</div>

<div>
<asp:Panel runat="server" ID="pnLogin" GroupingText="<%$ Resources:labels, login %>">
<table class="pageadd">
    <tr>
        <td class="usertdleft">
            <asp:Label ID="Label3" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
            <asp:Label ID="Label16" runat="server" SkinID="lblImportant" Text="<%$ Resources:labels, username %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtUserName" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            <asp:Label ID="Label5" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
            <asp:Label ID="Label15" runat="server" SkinID="lblImportant" Text="<%$ Resources:labels, password %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtPass" runat="server" SkinID="txtTwoColumn" 
                TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            <asp:Label ID="Label17" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
            <asp:Label ID="Label4" runat="server" SkinID="lblImportant"
                Text="<%$ Resources:labels, repassword %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtRePass" runat="server" SkinID="txtTwoColumn" 
                TextMode="Password"></asp:TextBox>
        </td>
    </tr>
</table>
</asp:Panel>
</div>
<br />
<div>
<asp:Panel runat="server" ID="pnProfile" GroupingText="<%$ Resources:labels, profile %>">
<table class="pageadd">
    <tr>
        <td class="usertdleft">
            <asp:Label ID="Label18" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
            <asp:Label ID="Label6" runat="server" SkinID="lblImportant" Text='<%$ Resources:labels, firstname %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtFirstName" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            <asp:Label ID="Label7" runat="server" Text='<%$ Resources:labels, middlename %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtMiddleName" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            
            <asp:Label ID="Label8" runat="server" Text='<%$ Resources:labels, lastname %>'></asp:Label>
            
            :</td>
        <td>
            <asp:TextBox ID="txtLastName" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            
            <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, gender %>'></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlGender" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            
            <asp:Label ID="Label10" runat="server" Text='<%$ Resources:labels, address %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" SkinID="txtTwoColumn" Height="50px" 
                TextMode="MultiLine"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td class="usertdleft">
            
            <asp:Label ID="Label11" runat="server" Text='<%$ Resources:labels, email %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            
            <asp:Label ID="Label12" runat="server" Text='<%$ Resources:labels, birthday %>'></asp:Label>
            :</td>
        <td>
            <cc1:FSSMaskEdit ID="txtBirth" runat="server" Mask="nn/nn/nnnn"></cc1:FSSMaskEdit>
            &nbsp;<img alt="" onclick="displayCalendar(document.forms[0].ctl00$ctl09$Widget1$txtBirth,'dd/mm/yyyy',this,false)" src="widgets/user/controls/Images/calendar2.gif" align="center" /> 
            <img alt="" src="App_Themes/Bank2/images/help.gif" onmouseover="<%=Resources.labels.birthdaytip %>" onmouseout="UnTip()"
                style="width: 17px; height: 17px" align="center" /></td>
    </tr>
    <tr>
        <td class="usertdleft">
            
            <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, phone %>'></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtPhone" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="usertdleft">
            
            <asp:Label ID="Label14" runat="server" Text='<%$ Resources:labels, status %>'></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    
</table>
<div style="padding:5px 0px 5px 5px; text-align:center">
     <hr />
    <asp:Image runat="server" ID="imgSave1" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btOK" runat="server"  onclick="btOK_Click" 
                Text="<%$ Resources:labels, save %>" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btCancel" runat="server" onclick="btCancel_Click" CausesValidation="false"
                Text="<%$ Resources:labels, exit %>" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="btnBack" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
    
</div>
</asp:Panel>
</div>

