<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPoster_Controls_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <br />
        <div id="divCustHeader">
            <asp:Image ID="imgLoGo" runat="server" Style="width: 32px; height: 32px; margin-bottom: 10px;"
                align="middle" />
            <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
        </div>
        <div id="divError">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1"
                runat="server">
                <ProgressTemplate>
                    <img alt="" src="widgets/SEMSProduct/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="divGetInfoCust">
            <asp:Panel ID="pnAdd" runat="server">
                <div class="divHeaderStyle">
                    <%=Resources.labels.posterinformation %>
                </div>
                <table class="style1" cellpadding="3">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, chonfile %>"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:FileUpload ID="FileUpload" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:Panel ID="PosterPanel" runat="server">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, filetype %>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFileType" runat="server" Width="57%">
                                <asp:ListItem Value="image" Text="<%$ Resources:labels, image %>"></asp:ListItem>
                                <asp:ListItem Value="flash" Text="<%$ Resources:labels, flash %>"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, position%>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPosition" runat="server" Width="57%">
                                <asp:ListItem Value="top" Text="<%$ Resources:labels, top %>"></asp:ListItem>
                                <asp:ListItem Value="right" Text="<%$ Resources:labels, right %>"></asp:ListItem>
                                <asp:ListItem Value="bottom" Text="<%$ Resources:labels, bottom %>"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, chieurong %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, chieucao%>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHeight" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, index %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIndex" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, isshow%>"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbxPublish" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdfFileName" runat="server"/>
                <asp:HiddenField ID="hdfPath" runat="server"/>
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div style="text-align: center; margin-top: 10px;">
    <asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();"
        OnClick="btsave_Click" />&nbsp;&nbsp;
    <asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1)" />
</div>


<script type="text/javascript">

    function ntt(sNumber, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            return;
        }
    }

    function executeComma(id, event) {
        if (document.getElementById(id).value.substring(0, 1) == "0") {
            document.getElementById(id).value = "";
        }
        else if ((event.keyCode >= 96 && event.keyCode <= 105)) {
            executeCommaDo(id);
        }
        else if (event.keyCode >= 48 && event.keyCode <= 57) {
            executeCommaDo(id);
        }
        else if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
            executeCommaDo(id);
        }
        else {
            alert("Please input number !");
            document.getElementById(id).value = "";
        }
    }

</script>