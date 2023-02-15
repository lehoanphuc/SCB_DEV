<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Pages_Controls_Widget" %>
<link href="Widgets/Pages/Controls/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function pagevalidate() {

        if (document.getElementById('<%=txtPageId.ClientID %>').value == '') {
            alert('<%=Resources.labels.pageidrequire %>');
            return false;
        }
        else if (document.getElementById('<%=txtPageName.ClientID %>').value == '') {
            alert('<%=Resources.labels.pagenamerequire %>');
            return false;
        }
        else {
            var iChars = "!@#$%^&*+=-[]\\\';,./{}|\":<>?";

            for (var i = 0; i < document.getElementById('<%=txtPageName.ClientID%>').value.length; i++) {
                if (iChars.indexOf(document.getElementById('<%=txtPageName.ClientID%>').value.charAt(i)) != -1) {
                    alert('<%=Resources.labels.pagenamespecialcharactervalidate %>');
                    return false;
                }
            }
            return true;
        }
    }
    function RefreshUpdatePanel() {
        __doPostBack('txtSearch', '');
    };
</script>

<div style="padding: 5px 0px 5px 5px; text-align: center;">

    <asp:Image runat="server" ID="imgSave1" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" Style="width: 16px; height: 16px" />
    <asp:LinkButton ID="btnSave1" runat="server" Text='<%$ Resources:labels, save %>' OnClientClick="return pagevalidate();"
        OnClick="btnSave_Click" />
    &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif" style="width: 16px; height: 16px" />
    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%$ Resources:labels, exit %>'
        CausesValidation="False" OnClick="btnExit_Click" />
    &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
    <a id="A1" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>
    <hr />
</div>
<div style="text-align: left; padding: 5px 1px 5px 1px; height: auto;">
    <asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
</div>
<div style="text-align: right; margin: 5px 1px 5px 1px; padding-right: 5px;">
    <asp:Label ID="Label7" runat="server" Font-Bold="False" SkinID="lblImportant">*</asp:Label>
    <asp:Label ID="Label8" runat="server" SkinID="lblImportant" Text='<%$ Resources:labels, requiredfield %>'></asp:Label>
</div>
<style>
    .combobox{
        height:20px;
    }
</style>
<asp:Panel runat="server" ID="pnFocus" DefaultButton="btnSave">
    <table id="pageadd" cellspacing="1">
        <tr>
            <td class="tdleft">
                <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, service %>'></asp:Label>
                :            
            </td>
            <td>
                <asp:DropDownList ID="ddlPortalID"  class="combobox" runat="server" AutoPostBack="true" OnTextChanged="ddlPortalID_OnTextChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <asp:Label ID="Label9" runat="server" Text='<%$ Resources:labels, subsystem %>'></asp:Label>
                :</td>
            <td>
                <asp:DropDownList ID="ddlSubSystem" runat="server" class="combobox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <asp:Label runat="server" Text="*" SkinID="lblImportant"></asp:Label>
                <asp:Label ID="Label13" runat="server" Text='<%$ Resources:labels, pageid %>'
                    SkinID="lblImportant"></asp:Label>

            </td>
            <td>
                <asp:TextBox ID="txtPageId" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <asp:Label runat="server" Text="*" SkinID="lblImportant"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text='<%$ Resources:labels, pagename %>'
                    SkinID="lblImportant"></asp:Label>
                :
            </td>
            <td>
                <asp:TextBox ID="txtPageName" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
                <img alt="" src="App_Themes/Bank2/images/help.gif" onmouseover="<%=Resources.labels.pagenametip %>" onmouseout="UnTip()"
                    style="width: 17px; height: 17px" /></td>
        </tr>
        <tr>
            <td class="tdleft">
                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:labels, title %>'></asp:Label>
                :
            </td>
            <td>
                <asp:TextBox ID="txtPageTitle" runat="server" SkinID="txtTwoColumn"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <asp:Label ID="Label5" runat="server" Text='<%$ Resources:labels, masterpage %>'></asp:Label>
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlMasterPage" runat="server" class="combobox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <asp:Label ID="Label6" runat="server" Text='<%$ Resources:labels, theme %>'></asp:Label>
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlTheme" runat="server" class="combobox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdleft"></td>
            <td>
                <asp:CheckBox ID="cbIsShow" Text='<%$ Resources:labels, isshow %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsDefault" Text='<%$ Resources:labels, isdefault %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsApprove" Text='<%$ Resources:labels, isapprove %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsNotification" Text='<%$ Resources:labels, isnotification %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsSchedule" Text='<%$ Resources:labels, isschedule %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsTemplate" Text='<%$ Resources:labels, istemplate %>'
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdleft"></td>
            <td>
                <asp:CheckBox ID="cbIsReceive" Text='<%$ Resources:labels, isreceive %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsProductFee" Text='<%$ Resources:labels, isproductfee %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsReport" Text='<%$ Resources:labels, isreport %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsViewReport" Text='<%$ Resources:labels, isviewreport %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsbeneficiary" Text='<%$ Resources:labels, isbeneficiary %>'
                    runat="server" />
                <asp:CheckBox ID="cbIsReversal" Text='<%$ Resources:labels, isreversal %>'
                    runat="server" />
            </td>
        </tr>

        <tr>
            <td class="tdleft">
                <asp:Label ID="Labell" runat="server" Text='<%$ Resources:labels, trancode %>'></asp:Label>
                :
            </td>
            <td>
                <asp:TextBox ID="txtTrancode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <asp:Label ID="Label15" runat="server" Text='<%$ Resources:labels, linkapprove %>'></asp:Label>
                : 
            </td>
            <td>
                <asp:TextBox ID="txtLinkaApprove" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft">

                <asp:Label ID="Label12" runat="server" Text="Search page"></asp:Label>
                :
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSearch" onkeyup="FilterItems1(this.value)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft">

                <asp:Label ID="Label10" runat="server" Text="Reference Page"></asp:Label>
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlReference" runat="server" class="combobox">
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td class="tdleft">

                <asp:Label ID="Label11" runat="server" Text="Action"></asp:Label>
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlAction" runat="server" class="combobox">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdleft">

                <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, desc %>'></asp:Label>
                :
            </td>
            <td>
                <asp:TextBox ID="txtPageDesc" TextMode="MultiLine" runat="server"
                    SkinID="txtTwoColumn" Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

    </table>
    <div style="padding: 5px 0px 5px 5px; text-align: center;">
        <hr />
        <asp:Image runat="server" ID="imgSave" AlternateText="" ImageUrl="~/widgets/pages/controls/images/save.gif" Style="width: 16px; height: 16px" />
        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click"
            OnClientClick="return pagevalidate();" Text="<%$ Resources:labels, save %>" />
        &nbsp;
    <img alt="" src="widgets/pages/controls/images/exit.gif"
        style="width: 16px; height: 16px" />
        <asp:LinkButton ID="btnExit" runat="server" Text='<%$ Resources:labels, exit %>'
            CausesValidation="False" OnClick="btnExit_Click" />
        &nbsp;
     <img alt="" src="widgets/pages/view/images/back.gif" style="width: 16px; height: 16px" />
        <a id="btnBack" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>

    </div>
</asp:Panel>
<script type="text/javascript">
    var ddlText1, ddlValue1, ddl1, lblMesg1;
    function CacheItems() {
        ddlText1 = new Array();
        ddlValue1 = new Array();
        ddl1 = document.getElementById("<%=ddlReference.ClientID %>");
        //lblMesg = document.getElementById("<%=lblAlert.ClientID%>");
        for (var i = 0; i < ddl1.options.length; i++) {
            ddlText1[ddlText1.length] = ddl1.options[i].text;
            ddlValue1[ddlValue1.length] = ddl1.options[i].value;
        }
    }
    window.onload = CacheItems;

    function FilterItems1(value) {
        ddl1.options.length = 0;
        for (var i = 0; i < ddlText1.length; i++) {
            if (ddlText1[i].toLowerCase().indexOf(value) != -1) {
                AddItem1(ddlText1[i], ddlValue1[i]);
            }
        }
        lblMesg1.innerHTML = ddl.options.length;
        if (ddl1.options.length == 0) {
            AddItem1("No items found.", "");
        }
    }

    function AddItem1(text, value) {
        var opt = document.createElement("option");
        opt.text = text;
        opt.value = value;
        ddl1.options.add(opt);
    }
</script>
<style>
    #nav li ul ul {
        margin :0px !important;
    }
</style>