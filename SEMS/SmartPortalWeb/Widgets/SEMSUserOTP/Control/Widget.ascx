<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUserOTP_Widget" %>
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
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSUserOTP/Images/otp.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.dangkysudungotp %>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>

<asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
    <div id="divSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtfullname" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, tendangnhap %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtphone" runat="server"></asp:TextBox>
                </td>
                <td class="thbtn"></td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:TextBox ID="txtContractNo" runat="server"></asp:TextBox>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlstatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="thbtn"></td>
            </tr>
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label744" runat="server" Text="<%$ Resources:labels, loainguoidung %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlusertype" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, capbac %>" Visible="false"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddluserlevel" runat="server" Visible="false">
                        <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                        <asp:ListItem Value="0" Text="<%$ Resources:labels, canhan %>"></asp:ListItem>
                        <asp:ListItem Value="1" Text="<%$ Resources:labels, chutaikhoan %>"></asp:ListItem>
                        <asp:ListItem Value="2" Text="<%$ Resources:labels, ketoantruong %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="thbtn"></td>
            </tr>
        </table>
    </div>
</asp:Panel>

<asp:Panel runat="server" ID="pnCustInfo">
    <div id="divResult">
        <div style="height: 150px; overflow: auto;">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <asp:GridView ID="gvUserList" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AutoGenerateColumns="False" PageSize="15"
                OnRowDataBound="gvUserList_RowDataBound">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="cbxSelect" runat="server" GroupName="88" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$ Resources:labels, nguoidung %>" SortExpression="fullname">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpfullnname" runat="server">[hpDetails]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, username %>">
                        <ItemTemplate>
                            <asp:Label ID="lbluserID" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcustName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>">
                        <ItemTemplate>
                            <asp:Label ID="lblcontractno" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, birthday %>">
                        <ItemTemplate>
                            <asp:Label ID="lblbirth" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server"></asp:Label>
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
</asp:Panel>
<!--end-->

<!-- Thong tin nguoi dai dien-->
<div style="margin-top: 10px;">
    <asp:Panel runat="server" ID="pnToken">
        <div class="divGetInfoCust">
            <div class="divHeaderStyle">
                <asp:Label ID="Label21" runat="server" Text="<%$ Resources:labels, thongtintoken %>"></asp:Label>
            </div>
            <table id="tblIB" class="style1" cellpadding="4">
                <tr>
                    <td class="thlb">
                        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:labels, loaixacthuc %>"></asp:Label>
                    </td>
                    <td class="thtds">
                        <asp:DropDownList ID="ddlAuthenType" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="lblTokenID" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td class="thlb">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, maxacthucotp %>"></asp:Label>
                    </td>
                    <td class="thtds">
                        <asp:TextBox ID="txtserialNumber" runat="server"></asp:TextBox>
                    </td>
                    <td class="thbtn">
                        <asp:Button ID="btnThemChuTaiKhoan" runat="server" Text="<%$ Resources:labels, them %>" OnClientClick="return validate();" OnClick="btnThemChuTaiKhoan_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!-- luoi tam -->
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="pnChuTaiKhoan" runat="server">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
                    <%=Resources.labels.loading %>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" ID="lblAlert" ForeColor="Red"></asp:Label>
                    <div id="div3" style="margin-top: 10px; padding: 0 5px;">
                        <asp:GridView ID="gvResultChuTaiKhoan" runat="server"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                            AllowPaging="True"
                            PageSize="5" OnPageIndexChanging="gvResultChuTaiKhoan_PageIndexChanging"
                            OnRowDeleting="gvResultChuTaiKhoan_RowDeleting">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                                <asp:BoundField HeaderText="<%$ Resources:labels, tenkhachhang %>" DataField="colFullnameCustomer" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="<%$ Resources:labels, nguoidung %>" DataField="colUserFullName" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="<%$ Resources:labels, username %>" DataField="colUserID" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="colAuthenType" HeaderText="<%$ Resources:labels, loaixacthuc %>" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="<%$ Resources:labels, maxacthuc %>" DataField="colAuthenCode" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="<%$ Resources:labels, trangthai %>" DataField="colStatus" ItemStyle-HorizontalAlign="Center" />
                                <asp:CommandField ButtonType="Link" DeleteText="Delete" HeaderText="<%$ Resources:labels, huy %>" ShowDeleteButton="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle CssClass="gvFooterStyle" />
                            <PagerStyle HorizontalAlign="Right" CssClass="pager" />
                            <SelectedRowStyle />
                            <HeaderStyle CssClass="gvHeader" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <!-- end -->
    </asp:Panel>
</div>
<div style="text-align: center; padding-top: 10px;">
    &nbsp;<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="<%$ Resources:labels, save %>" />
    &nbsp;<asp:Button ID="btnExit" runat="server" OnClick="btnExit_Click" Text="<%$ Resources:labels, thoat %>" />
</div>

<asp:HiddenField ID="hidUserID" runat="server" />
<asp:HiddenField ID="hidCustName" runat="server" />


<script type="text/javascript">
    function SelectRAD(obj, obj1) {
        document.getElementById('<%=hidUserID.ClientID%>').value = obj1;
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
        //var obj2=document.getElementById(obj1);
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }

    function validate() {
        if (document.getElementById("<%=txtserialNumber.ClientID %>").value == '') {
            window.alert('<%=Resources.labels.banvuilongnhapmaxacthuc %>');
            document.getElementById("<%=txtserialNumber.ClientID %>").focus();
            return false;
        }
        else {
            return true;
        }
    }
</script>
