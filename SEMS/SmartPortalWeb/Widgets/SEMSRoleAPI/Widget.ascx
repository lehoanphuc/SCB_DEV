<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSRoleAPI_Widget" %>
<link href="Widgets/SEMSRoleAPI/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="Widgets/SEMSRoleAPI/Scripts/JScript.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <br />
        <div id="divCustHeader">
            <img alt="" src="widgets/SEMSUSERPOLICY/Images/policy_icon.jpg" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
            <%=Resources.labels.setroleforAPI %>
        </div>
        <div id="divError">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                    <%=Resources.labels.loading %>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Label ID="lblAlert" runat="server" Font-Bold="False" SkinID="lblImportant"></asp:Label>
        </div>
        <asp:Panel runat="server" ID="pnFocus" DefaultButton="btnSave">
            <div id="divSearch">
                <table id="pageadd" cellspacing="1">
                    <tr>
                        <td style="width: 50%; text-align: right;">
                            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, dichvu %>'></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlService" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlService_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align: right;">
                            <asp:Label ID="Label11" runat="server" Text='<%$ Resources:labels, group %>'></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 50%; text-align: right;">
                            <asp:Label ID="Label3" runat="server" Text='<%$ Resources:labels, role %>'></asp:Label>
                        </td>
                        <td>
                            <div style="height: 300px; width: 400px; overflow: auto">
                                <asp:TreeView ID="tvPage" runat="server" ImageSet="Simple"
                                    OnTreeNodeCheckChanged="tvPage_TreeNodeCheckChanged" ShowLines="True">
                                    <ParentNodeStyle Font-Bold="False" />
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD"
                                        HorizontalPadding="0px" VerticalPadding="0px" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black"
                                        HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                </asp:TreeView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 10px; text-align: center;">
                &nbsp;
                <asp:Button ID="btnSave" runat="server" Text='<%$ Resources:labels, save %>' OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:labels, back %>" CssClass="btnGeneral" PostBackUrl="javascript:history.go(-1)" />
            </div>
        </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
