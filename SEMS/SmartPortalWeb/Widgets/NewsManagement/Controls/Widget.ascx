<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_NewsManagement_Controls_Widget" %>
<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>
<link href="Widgets/newsmanagement/Controls/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script>
    function ValidateContentText() {      
        if (document.getElementById('<%=txtTitle.ClientID %>').value =="") {
            alert('<%=Resources.labels.newstitlerequire %>');
            return false;
        }
        else {
            var EditorInstance = FCKeditorAPI.GetInstance('<%=fckSummary.ClientID %>'); //message is name of field to be validate
            if(EditorInstance.EditorDocument.body.innerText.length<=0)
            {
                alert('<%=Resources.labels.newssummaryrequire %>');
                EditorInstance.EditorDocument.body.focus();
                return false;
            }            
            else {
                var EditorInstance = FCKeditorAPI.GetInstance('<%=fckContent.ClientID %>'); //message is name of field to be validate
                if (EditorInstance.EditorDocument.body.innerText.length <= 0) {
                    alert('<%=Resources.labels.newscontentrequire %>');
                    EditorInstance.EditorDocument.body.focus();
                    return false;
                }   
                else {
                    return true;
                }
            }
        }       
    }

</script>


<div style="padding:5px 0px 5px 5px; text-align:center;">    
    <asp:Image runat="server" ID="imgSave" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" style="width: 16px; height: 16px" />
   <asp:LinkButton ID="btnSave1" runat="server" Text='<%$ Resources:labels, save %>' 
                 OnClientClick="return ValidateContentText()" onclick="btnSave_Click" />
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
        <td class="tdleft" style=" width:20%">
            <asp:Label ID="Label01" runat="server" Text='<%$ Resources:labels, category %>'></asp:Label>            
            :            
        </td>
        <td style=" width:80%">
            <asp:DropDownList ID="ddlCategory" runat="server">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td class="tdleft">
            <asp:Label ID="Label9" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
&nbsp;<asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, title %>" 
                SkinID="lblImportant"></asp:Label>
            :
        </td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td class="tdleft" valign="top">
            <asp:Label ID="Label12" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
            <asp:Label ID="Label13" runat="server" SkinID="lblImportant" 
                Text="<%$ Resources:labels, summary %>"></asp:Label>
            :</td>
        <td>
            <asp:Label ID="lblSummary" runat="server" Width="100%"></asp:Label>
            <FCKeditorV2:FCKeditor ID="fckSummary" runat="server" ToolbarSet="Basic" 
                BasePath="fckeditor/">
            </FCKeditorV2:FCKeditor>
        </td>
    </tr>
    <tr>
        <td class="tdleft" valign="top">
            <asp:Label ID="Label14" runat="server" SkinID="lblImportant" Text="*"></asp:Label>
            <asp:Label ID="Label15" runat="server" SkinID="lblImportant" 
                Text="<%$ Resources:labels, content %>"></asp:Label>
            :</td>
        <td>
            <asp:Label ID="lblContent" runat="server"  Width="100%"></asp:Label>
            <FCKeditorV2:FCKeditor ID="fckContent" runat="server" BasePath="fckeditor/" 
                Height="400px">
            </FCKeditorV2:FCKeditor>
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
   <asp:LinkButton ID="btnSave" runat="server" Text='<%$ Resources:labels, save %>' 
                 OnClientClick="return ValidateContentText()" onclick="btnSave_Click" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
      <asp:LinkButton ID="btnExit" runat="server" Text='<%$ Resources:labels, exit %>' 
                CausesValidation="False" onclick="btnExit_Click" />
     &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
     <a ID="A2" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>    
     
</div>
</asp:Panel>