<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReportManagement_NewControls_Widget" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSProductLimit/JS/mask.js" type="text/javascript"></script>
<script src="widgets/SEMSTellerApproveTrans/JS/common.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script type="text/javascript" src="widgets/SEMSUser/js/commonjs.js"> </script>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divCustHeader">
    <asp:Image ID="imgLoGo" runat="server" Style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
</div>
<div id="divError">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
            <%=Resources.labels.loading %>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
</div>
<div style="text-align: center; font-weight: bold">
    <asp:Label runat="server" ID="lblWarning" ForeColor="Red"></asp:Label>
</div>
<div class="divAddInfoPro">
    <asp:Panel ID="pnFee" runat="server">
        <div class="divGetInfoCust">
            <div class="divHeaderStyle">
                <%=Resources.labels.thongtinbaocao %>
            </div>
            <table class="style1" cellpadding="3">
                <tr>
                    <td width="23%">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tenbaocao %>"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text=" *"></asp:Label>
                    </td>
                    <td width="27%">
                        <asp:TextBox ID="txtReportName" runat="server"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, phanhe %>"></asp:Label>
                    </td>
                    <td width="23%">
                        <asp:DropDownList ID="ddlSubSystem" runat="server">
                            <asp:ListItem Value="SEMS">SEMS</asp:ListItem>
                            <asp:ListItem Value="IB" Text="<%$ Resources:labels, internetbanking %>"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="25%"></td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="cbIsDisPlay" runat="server"
                            Text="<%$ Resources:labels, hienthi %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label3" runat="server"
                            Text="<%$ Resources:labels, trangxembaocao %>" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlViewReportPage" runat="server" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server"
                            Text="<%$ Resources:labels, filebaocao %>"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUploadRPT" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server"
                            Text="<%$ Resources:labels, trangthamso %>" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlParameterPage" runat="server" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <asp:Label ID="lbtemp" runat="server" Visible="false"></asp:Label>
            </table>
        </div>
    </asp:Panel>
</div>
<br />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnparaminfo" runat="server">
            <div class="divGetInfoCust">
                <div class="divHeaderStyle">
                    <%=Resources.labels.thongtinthamso %>
                </div>
                <table class="style1" cellpadding="3">
                    <tr>
                        <td width="25%">
                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, thamsobaocao %>"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Text=" *"></asp:Label>
                        </td>
                        <td width="30%">
                            <asp:TextBox ID="txtparamname" runat="server"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, tenhienthi %>"></asp:Label>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="txtdisplayname" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%">
                            <asp:Button ID="btnAdd_Details" runat="server" Text="<%$ Resources:labels, them %>" OnClick="btnAdd_Details_Click" OnClientClick="return validate1();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, doituong %>"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlobject" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlobject_SelectedIndexChanged">
                                <asp:ListItem Value="Textbox">TextBox</asp:ListItem>
                                <asp:ListItem Value="HiddenField">HiddenField</asp:ListItem>
                                <asp:ListItem Value="DropxDownList">DropDownList</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style2">
                            <asp:Label ID="Label9" runat="server"
                                Text="<%$ Resources:labels, chieurong %>"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtwidth" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server"
                                Text="<%$ Resources:labels, chieucao %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtheight" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server"
                                Text="<%$ Resources:labels, ngonngu %>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddllang" runat="server">
                                <asp:ListItem Value="vi-VN">Tiếng Việt</asp:ListItem>
                                <asp:ListItem Value="en-US">English</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server"
                                Text="<%$ Resources:labels, sothutu %>"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlorder" runat="server">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td>
                            <asp:Label ID="Label23" runat="server"
                                Text="<%$ Resources:labels, tag %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttag" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hdgenID" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </asp:Panel>
        <asp:Panel ID="pnparamddl" runat="server">
            <div class="divAddInfoPro">
                <div class="divHeaderStyle">
                    <%=Resources.labels.thongtinthamsodropdownlist %>
                </div>
                <table class="style1" cellpadding="3">
                    <tr>
                        <td width="23%">
                            <asp:Label ID="Label14" runat="server" Text="Store Procedure"></asp:Label>
                        </td>
                        <td width="27%">
                            <asp:TextBox ID="txtSPName" runat="server"></asp:TextBox>
                        </td>
                        <td width="17%">
                            <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, giatridropdownlist %>"></asp:Label>
                        </td>
                        <td width="23%">
                            <asp:TextBox ID="txtddlvalue" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server"
                                Text="<%$ Resources:labels, tenhienthidropdownlist %>"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtddltext" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnGV" runat="server">
            <div id="divResult">
                <asp:GridView ID="gvAppTransDetailsList" runat="server" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    OnPageIndexChanging="gvAppTransDetailsList_PageIndexChanging"
                    OnRowDataBound="gvAppTransDetailsList_RowDataBound"
                    OnSorting="gvAppTransDetailsList_Sorting" PageSize="15" Width="100%"
                    OnRowDeleting="gvAppTransDetailsList_RowDeleting">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField HeaderText="FkID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFkID" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reportid" SortExpression="rptid" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblreportId" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tenthamso %>">
                            <%--SortExpression="FROMLIMIT"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblparamname" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, sothutu %>"
                            SortExpression="Orderby">
                            <ItemTemplate>
                                <asp:Label ID="lblordno" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tenhienthi %>">
                            <%--SortExpression="FROMLIMIT"--%>
                            <ItemTemplate>
                                <asp:Label ID="lbldisplayname" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, doituong %>">
                            <%--SortExpression="TOLIMIT"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblobject" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, chieurong %>">
                            <%--SortExpression="MINAMOUNT"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblwidth" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, chieucao %>">
                            <%--SortExpression="MAXAMOUNT"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblheight" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, ngonngu %>" Visible="false">
                            <%--SortExpression="RATE"--%>
                            <ItemTemplate>
                                <asp:Label ID="lbllang" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, ngonngu %>">
                            <ItemTemplate>
                                <asp:Label ID="lbllangname" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tag %>">
                            <%--SortExpression="RATE"--%>
                            <ItemTemplate>
                                <asp:Label ID="lbltag" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StorePro" Visible="false">
                            <%--SortExpression="MAXAMOUNT"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblddlstore" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, giatridropdownlist %>"
                            Visible="false"><%--SortExpression="RATE"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblddlvalue" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, tenhienthidropdownlist %>"
                            Visible="false"><%--SortExpression="RATE"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblddltext" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:CommandField DeleteText="<%$ Resources:labels, huy %>"
                            HeaderText="<%$ Resources:labels, huy %>" ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <PagerStyle BackColor="White" CssClass="pager" ForeColor="#000066"
                        HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#E0ECFF" Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
                <br />
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<div style="text-align: center; margin-top: 10px;">
    &nbsp;<asp:Button ID="btsave" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate();" OnClick="btsave_Click" />
    &nbsp;<asp:Button ID="btback" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" /><%--PostBackUrl="javascript:history.go(-1)"--%>
</div>
<script language="javascript">
    function validate() {
        if (validateEmpty('<%=txtReportName.ClientID %>', '<%=Resources.labels.tenbaocaokhongrong %>')) {
        }
        else {
            document.getElementById('<%=txtReportName.ClientID %>').focus();
            return false;
        }
    }
    function validate1() {

        if (validateEmpty('<%=txtparamname.ClientID %>', '<%=Resources.labels.thamsobaocaokhongrong %>')) {
            if (validateEmpty('<%=txtdisplayname.ClientID %>', '<%=Resources.labels.tenhienthikhongrong %>')) {
                if (validateEmpty('<%=txtwidth.ClientID %>', '<%=Resources.labels.chieurongkhongdungdinhdangso %>')) {
                    if (IsNumeric('<%=txtwidth.ClientID %>', '<%=Resources.labels.chieurongkhongdungdinhdangso %>')) {
                        if (validateEmpty('<%=txtheight.ClientID %>', '<%=Resources.labels.chieucaokhongdungdinhdangso %>')) {
                            if (IsNumeric('<%=txtheight.ClientID %>', '<%=Resources.labels.chieucaokhongdungdinhdangso %>')) {

                            }
                            else {
                                document.getElementById('<%=txtheight.ClientID %>').focus();
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtheight.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtwidth.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtwidth.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtdisplayname.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtparamname.ClientID %>').focus();
            return false;
        }
    }

</script>





