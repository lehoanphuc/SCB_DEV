<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCard_Controls_Widget" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="Widgets/PagePermission/Scripts/JScript.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="widgets/SEMSUser/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSUser/js/subModal.js"> </script>
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script>
<!-- Add this to have a specific theme-->
<link href="widgets/IBCorpUser/css/subModal.css" rel="stylesheet" type="text/css">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div id="divCustHeader">
    <img alt="" src="widgets/SEMSCard/Images/icon.png" style="width: 40px; height: 40px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.registercard %>
</div>

<div id="divError">
    <asp:Label ID="ltrError" runat="server" ForeColor="Red"></asp:Label>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="divGetInfoCust">
            <div class="divHeaderStyle">
                <%=Resources.labels.thongtinhopdong %>/<%=Resources.labels.thongtinkhachhang %>
            </div>
            <table class="style1" cellspacing="0" cellpadding="5">
                <tr>
                    <td style="width: 15%">
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, customercode %>"></asp:Label>
                    </td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtOwnCustCode" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label>
                    </td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtOwnContractNo" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwnFullName" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, email %>"></asp:Label>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwnEmail" runat="server" Width="161px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, ngaysinh %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwnDOB" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, gioitinh %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOwnSex" runat="server" Enabled="false">
                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam %>"></asp:ListItem>
                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu %>"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, dienthoai %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwnPhone" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, hochieuchungminh %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwnLic" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, diachi %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwnAddress" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </td>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, branch %>" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOwnBranch" runat="server" Width="174px" Enabled="false" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
            </table>
        </div>
        <div class="divGetInfoCust">
            <div class="divHeaderStyle">
                <%=Resources.labels.cardholderinformation %>
            </div>
            <asp:HiddenField runat="server" ID="hdfCardHolderCustCode" />
            <table class="style1" cellspacing="0" cellpadding="5">
                <%--                        <%if (SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"] != null && SmartPortal.Common.Encrypt.GetURLParam(System.Web.HttpContext.Current.Request.RawUrl)["a"] == "add")
                          { %>--%>
                <tr>
                    <td>
                        <asp:Label ID="Label54" runat="server" Text="<%$ Resources:labels, customercode %>"></asp:Label>
                        &nbsp;*
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardHolderCustCode" runat="server"></asp:TextBox>
                        <asp:Button ID="btnCardHolderDetail" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                            OnClick="btnCardHolderDetail_Click" CausesValidation="false" />
                        <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1"
                            runat="server">
                            <ProgressTemplate>
                                <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                                <%=Resources.labels.loading %>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <%-- <%} %>--%>
                <tr>
                    <td style="width: 15%">
                        <asp:Label ID="Label27" runat="server" Text="<%$ Resources:labels, tendaydu %>" Enabled="false"></asp:Label>
                    </td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtCardHolderFullName" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="Label32" runat="server" Text="<%$ Resources:labels, email %>"></asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtCardHolderEmail" runat="server" Width="161px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label29" runat="server" Text="<%$ Resources:labels, ngaysinh %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardHolderDOB" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label30" runat="server" Text="<%$ Resources:labels, gioitinh %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCardHolderSex" runat="server" Enabled="false">
                            <asp:ListItem Value="M" Text="<%$ Resources:labels, nam %>"></asp:ListItem>
                            <asp:ListItem Value="F" Text="<%$ Resources:labels, nu %>"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label31" runat="server" Text="<%$ Resources:labels, dienthoai %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardHolderPhone" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, hochieuchungminh %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardHolderLic" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, diachi %>"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCardHolderAddress" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, branch %>" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCardHolderBranch" runat="server" Width="174px" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
            </table>
        </div>
        <div class="divGetInfoCust">
            <div class="divHeaderStyle">
                <%=Resources.labels.registeruser %>
            </div>
            <div id="divUserRegister" style="margin: 5px; height: 150px; overflow: auto;">
                <asp:GridView ID="gvUserRegister" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" PageSize="50"
                    OnRowDataBound="gvUserRegister_RowDataBound">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbCheckBox" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, nguoidung %>">
                            <ItemTemplate>
                                <asp:Label ID="lblUserID" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tendaydu %>">
                            <ItemTemplate>
                                <asp:Label ID="lblFullName" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, email %>">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, sodienthoai %>">
                            <ItemTemplate>
                                <asp:Label ID="lblPhone" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:GridView>
            </div>
        </div>
        <div class="divGetInfoCust">
            <div class="divHeaderStyle">
                <%=Resources.labels.registercard %>
            </div>
            <div id="divCardRegister" style="margin: 5px; height: 150px; overflow: auto;">
                <asp:GridView ID="gvCardRegister" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" PageSize="50"
                    OnRowDataBound="gvCardRegister_RowDataBound">
                    <RowStyle ForeColor="#000066" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbCheckBox" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, sothe %>">
                            <ItemTemplate>
                                <asp:Label ID="lblCardNo" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tendaydu %>">
                            <ItemTemplate>
                                <asp:Label ID="lblFullName" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, cardtype %>">
                            <ItemTemplate>
                                <asp:Label ID="lblCardType" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tiente %>">
                            <ItemTemplate>
                                <asp:Label ID="lblCCYID" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" HeaderText="" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:GridView>
            </div>
        </div>

        <div class="divGetInfoCust" style="height: 180px; margin-top: 10px;">
            <table id="tblIB" class="style1" cellspacing="0" border="0" cellpadding="4">
                <tr>
                    <%if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                        { %>
                    <td style="background-color: #F5F5F5; color: #38277c;">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="<%$ Resources:labels, internetbanking %>"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                    </td>
                    <%}
                        if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                        { %>
                    <td style="background-color: #F5F5F5; color: #38277c;">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="<%$ Resources:labels, mobilebanking %>"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="<%$ Resources:labels, quyensudung %>"></asp:Label>
                    </td>
                    <%} %>
                </tr>
                <tr>
                    <% if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                        { %>
                    <td valign="top" rowspan="2" style="width: 50%;">
                        <div style="width: 100%; height: 150px; overflow: auto;">
                            <asp:TreeView ID="tvIBQT" runat="server">
                            </asp:TreeView>
                        </div>
                    </td>
                    <%}
                        if (TabCustomerInfoHelper.TabMobileVisibility == 1)
                        { %>
                    <td rowspan="2" style="width: 50%;" valign="top">
                        <div style="width: 100%; height: 150px; overflow: auto;">
                            <asp:TreeView ID="tvMBQT" runat="server">
                            </asp:TreeView>
                        </div>
                    </td>
                    <%} %>
                </tr>
            </table>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<div style="text-align: right;">
    &nbsp;<asp:Button ID="btnThemNQT" runat="server" Text='<%$ Resources:labels, them %>' OnClick="btnThemNQT_Click" OnClientClick="return validate4();" />
    &nbsp;<asp:Button ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="<%$ Resources:labels, huy %>" />
</div>
<asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
<div id="div3" class="divResult" style="margin-top: 20px; height: 150px; overflow: auto;">
    <asp:GridView ID="gvResultQuanTri" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        OnPageIndexChanging="gvResultQuanTri_PageIndexChanging" OnRowDeleting="gvResultQuanTri_RowDeleting">
        <RowStyle ForeColor="#000066" />
        <Columns>
            <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colUserID" />
            <asp:BoundField DataField="colUserName" HeaderText="<%$ Resources:labels, username %>" />
            <asp:BoundField HeaderText="<%$ Resources:labels, sothe %>" DataField="colCardNo" />
            <asp:BoundField HeaderText="<%$ Resources:labels, quyensudung %>" DataField="colRole" />
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle CssClass="gvHeader" />
    </asp:GridView>
</div>
<br />
<div style="text-align: center; padding-top: 10px;">
    <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:labels, themmoi %>" Visible="false"
        OnClick="btaddnew_Click" Width="69px" />

    <asp:Button ID="btsaveandcont" runat="server" Text="<%$ Resources:labels, save %>"
        OnClick="btsaveandcont_Click" Width="69px" />
    &nbsp;
    <asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" PostBackUrl="javascript:history.go(-1);" />

    <asp:Button ID="btnBackHome" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnBackHome_Click" Visible="false" />

</div>

<!--end-->
<script type="text/javascript">
    //On Page Load

    //On UpdatePanel Refresh
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
<%--        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                var activetab = initTabs('dhtmlgoodies_tabView1', Array(<%=TabCustomerInfoHelper.TabName%>), 0, 948, 188, Array(false, false, false, false));
                showTab('dhtmlgoodies_tabView1', 1);
                showTab('dhtmlgoodies_tabView1', 0);
            }
        });--%>
    };
    function gvUserRegister_CheckAll(oCheckbox) {
        var GridView2 = document.getElementById("<%=gvUserRegister.ClientID %>");
        for (i = 1; i < GridView2.rows.length; i++) {
            GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }
    function gvCardRegister_CheckAll(oCheckbox) {
        var GridView2 = document.getElementById("<%=gvCardRegister.ClientID %>");
        for (i = 1; i < GridView2.rows.length; i++) {
            GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }
</script>
