<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Category_Controls_Widget" %>
<link href="Widgets/Category/Controls/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
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
    
    function categoryvalidate() {
        if (document.getElementById('<%=txtTitle.ClientID %>').value == '') {
            alert('<%=Resources.labels.categorytitlerequire %>');
            return false;
        }
        else {
            var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";

            for (var i = 0; i < document.getElementById('<%=txtTitle.ClientID%>').value.length; i++) {
                if (iChars.indexOf(document.getElementById('<%=txtTitle.ClientID%>').value.charAt(i)) != -1) {
                    alert('<%=Resources.labels.titlespecialcharactervalidate %>');
                    return false;
                }
            }
            return true;
        }
    }
</script>


<div style="padding:5px 0px 5px 5px; text-align:center;">
    
    <asp:Image runat="server" ID="imgSave" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave1" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="return categoryvalidate();" onclick="btnSave_Click" 
                />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="LinkButton2" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     <hr />
</div>
<div style=" text-align:left; padding:5px 1px 5px 1px; height:auto;">
<asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>

    <br />

</div>
<div style=" text-align:right; margin:5px 1px 5px 1px; padding-right:5px;">
<asp:Label ID="Label7" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
<asp:Label ID="Label8" runat="server" SkinID="lblImportant" Text='<%$ Resources:labels, requiredfield %>'></asp:Label>
</div>

<asp:Panel runat="server" ID="pnFocus" DefaultButton="btnSave">
<table id="pageadd" cellspacing="1">
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label9" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
            <asp:Label ID="Label4" runat="server" SkinID="lblImportant" 
                Text="<%$ Resources:labels, title %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, link %>"></asp:Label>
            :</td>
        <td>
            
            <asp:TextBox ID="txtLink" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, tag %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtTag" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, parent %>"></asp:Label>
            :</td>
        <td>
            <asp:DropDownList ID="ddlParent" runat="server">
            </asp:DropDownList>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="tdleft">
        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, order %>" 
                ></asp:Label>:</td>
        <td>
            <asp:DropDownList ID="ddlOrder" runat="server" >
            </asp:DropDownList>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="tdleft">
        </td>
        <td>
            <asp:CheckBox ID="cbIsShow" runat="server" 
                Text="<%$ Resources:labels, isshow %>" Visible="False" />
        </td>
    </tr>
    <tr>
        <td class="tdleft" valign="top">
            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, desc %>"></asp:Label>
            :</td>
        <td>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" 
                Height="58px" SkinID="txtTwoColumn"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
   
</table>
<div style="padding:5px 0px 5px 5px; text-align:center;">
    <hr />
    <asp:Image runat="server" ID="imgSave1" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="return categoryvalidate();" onclick="btnSave_Click" 
                />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
     <asp:LinkButton ID="btnExit" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>
</asp:Panel>