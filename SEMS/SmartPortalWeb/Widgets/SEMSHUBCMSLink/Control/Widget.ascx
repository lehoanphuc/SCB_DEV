<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_HUBCMSConfig_Widget" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="widgets/SEMSCustomerList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSCustomerList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSCustomerList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />

<link href="widgets/SEMSCustomerList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="widgets/SEMSCustomerList/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/subModal.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script>

<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<!-- Add this to have a specific theme-->
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSHUBCMSLink/Images/cms.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.hubcmsconfig %>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#990000"></asp:Label>
</div>

<asp:Panel ID="pnContract" runat="server" DefaultButton="btnSearch">
    <div id="divSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtContractCode" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, cmndgpkd %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtlicenseid" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:labels, socifcorebanking %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtcustcode" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn"></td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ngaymo %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtOpenDate" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, ngayhethan %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn"></td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, nguoimo %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtopenPer" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, loaihopdong %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlContractType" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="thbtn"></td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="thlb"></td>
                <td class="thtds"></td>
                <td class="thbtn"></td>
            </tr>
        </table>
    </div>

    <div id="divResult">
        <div style="height: 150px; overflow: auto;">
            <asp:GridView ID="gvUserList" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="98%" AutoGenerateColumns="False" PageSize="15"
                OnRowDataBound="gvUserList_RowDataBound">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="cbxSelect" runat="server" GroupName="88" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcontractno" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcustName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, cmndgpkd %>" SortExpression="LICENSEID">
                        <ItemTemplate>
                            <asp:Label ID="lbllicense" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, nguoimo %>" SortExpression="USERCREATE">
                        <ItemTemplate>
                            <asp:Label ID="lblOpen" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, ngaymo %>" SortExpression="CREATEDATE">
                        <ItemTemplate>
                            <asp:Label ID="lblOpendate" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhethan %>" SortExpression="ENDDATE">
                        <ItemTemplate>
                            <asp:Label ID="lblClosedate" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gvFooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                <SelectedRowStyle />
                <HeaderStyle CssClass="gvHeader" />
            </asp:GridView>
        </div>
    </div>

    <div style="text-align: center; padding-top: 10px;">
        <asp:Button ID="btnExit" runat="server" OnClick="btnExit_Click"
            Text="<%$ Resources:labels, thoat %>" Width="69px" />
        &nbsp;
        <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="<%$ Resources:labels, next %>"
            Width="61px" />
        &nbsp; &nbsp;
    </div>

</asp:Panel>
<!--end-->
<asp:Panel ID="pnCorp" runat="server" DefaultButton="btnSearchCorp">
    <div id="divSearch">
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, madoanhnghiep %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCorpCode" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:labels, tendoanhnghiep %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCorpName" runat="server"></asp:TextBox>
                </td>
                <td></td>
                <td width="10%">
                    <asp:Button ID="btnSearchCorp" runat="server" Text="<%$ Resources:labels, xemchitiet %>"
                        OnClick="btnSearchCorp_Click" />
                </td>
            </tr>
        </table>
    </div>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <div id="divResultCorp">
        <div style="height: 150px; overflow: auto;">
            <asp:GridView ID="gvCorpList" runat="server" BackColor="White" HorizontalAlign="Center"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="99%" AutoGenerateColumns="False" PageSize="15"
                OnRowDataBound="gvCorpList_RowDataBound">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="cbxSelect" runat="server" GroupName="887" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$ Resources:labels, madoanhnghiep %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcorpID" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tendoanhnghiep %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcorpName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="gvFooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                <SelectedRowStyle />
                <HeaderStyle CssClass="gvHeader" />
            </asp:GridView>
        </div>
    </div>

    <div style="text-align: center; padding-top: 10px;">
        <asp:Button ID="btnBackC" runat="server" OnClick="btnBackC_Click"
            Text="<%$ Resources:labels, back %>" Width="69px" />
        &nbsp;
        <asp:Button ID="btnNextC" runat="server" OnClick="btnNextC_Click" Text="<%$ Resources:labels, next %>"
            Width="61px" />
        &nbsp; &nbsp;
    </div>

</asp:Panel>
<asp:Panel ID="pnInfor" runat="server">
    <div id="divSearch">

        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:labels, contractno %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIContractNo" runat="server" Enabled="False" Style="width: 300px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:labels, customername %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtICustName" runat="server" Enabled="False" Style="width: 300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="<%$ Resources:labels, madoanhnghiep %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtICorpID" runat="server" Enabled="False" Style="width: 300px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="<%$ Resources:labels, tendoanhnghiep %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtICorpName" runat="server" Enabled="False" Style="width: 300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:labels, diachi %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIAddr" runat="server" Style="width: 300px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:labels, sodienthoai %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIPhoneNo" runat="server" Style="width: 300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="<%$ Resources:labels, email %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIEmail" runat="server" Style="width: 300px"></asp:TextBox>
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    <div style="text-align: center; padding-top: 10px;">
        &nbsp;
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="<%$ Resources:labels, back %>" />
        &nbsp;
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="<%$ Resources:labels, save %>" />
        &nbsp; 
        <asp:Button ID="btnExit2" runat="server" OnClick="btnExit_Click" Text="<%$ Resources:labels, thoat %>" Visible="False" />
    </div>

</asp:Panel>
<asp:HiddenField ID="hidContractNo" runat="server" />
<asp:HiddenField ID="hidCorpID" runat="server" />


<script type="text/javascript">
    function SelectRADCorp(obj, obj1) {
        document.getElementById('<%=hidCorpID.ClientID%>').value = obj1;
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'radio' && elements[i].id != obj.id) {
                    elements[i].checked = false;
                }
            }

        }
    }
    function SelectRADContract(obj, obj1) {
        document.getElementById('<%=hidContractNo.ClientID%>').value = obj1;
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'radio' && elements[i].id != obj.id) {
                    elements[i].checked = false;
                }
            }

        }
    }
    function HighLightCBX(obj, obj1) {
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }
</script>
