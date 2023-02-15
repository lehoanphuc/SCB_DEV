<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractListCorp_Controls_Widget" %>
<%@ Register Src="../../../Controls/LetterSearch/LetterSearch.ascx" TagName="LetterSearch" TagPrefix="uc1" %>

<link href="widgets/SEMSContractList/CSS/tab-view.css" rel="stylesheet" type="text/css" />
<script src="widgets/SEMSContractList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransactionHistory1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/IBTransactionHistory1/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="widgets/SEMSCustomerList/js/common.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/subModal.js"> </script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script>
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSContractList/Images/handshake.png" style="width: 40px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.thongtinhopdong %>
</div>
<div style="text-align: center; color: Red; font-weight: bold;">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>
<div id="dhtmlgoodies_tabView1">
    <div class="dhtmlgoodies_aTab">
        <table class="style1" cellspacing="1" cellpadding="3">
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, mahopdong %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontractno" runat="server"></asp:TextBox>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, loaihinhsanpham %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProductType" runat="server" SkinID="extDDL1">
                    </asp:DropDownList>
                    <asp:Label runat="server" ID="lblProductType" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, loaihopdong %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlContractType" runat="server">
                        <asp:ListItem Value="PCO" Text="<%$ Resources:labels, canhan %>"></asp:ListItem>
                        <asp:ListItem Value="CCO" Text="<%$ Resources:labels, doanhnghiep %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, tenkhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcustname" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:labels, chinhanh %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server">
                        <asp:ListItem Value="0">Chi nhánh Sài Gòn</asp:ListItem>
                        <asp:ListItem Value="0">Chi nhánh Hà Nội</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:labels, nguoitao %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtusercreate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:labels, ngaymo %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtopendate" runat="server"></asp:TextBox>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:labels, ngayhethan %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtenddate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlstatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:labels, nguoiduyet %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtuserapprove" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label25" runat="server" Text="<%$ Resources:labels, nguoisuadoi %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtuserlastmodify" runat="server"></asp:TextBox>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label27" runat="server" Text="<%$ Resources:labels, ngaysuadoi %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmodifydate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkRenew" runat="server" Checked="True" Text="<%$ Resources:labels, autorenewlabel %>" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:CheckBox ID="cbIsReceiver" runat="server"
                        Text="<%$ Resources:labels, chichophepchuyenkhoantrongdanhsachnguoithuhuong %>" />
                </td>
                <td></td>
            </tr>
        </table>
        <script type="text/javascript">//<![CDATA[

            var cal = Calendar.setup({
                onSelect: function (cal) { cal.hide() }
            });
            cal.manageFields("<%=txtopendate.ClientID %>", "<%=txtopendate.ClientID %>", "%d/%m/%Y");
            cal.manageFields("<%=txtenddate.ClientID %>", "<%=txtenddate.ClientID %>", "%d/%m/%Y");
    //]]></script>

    </div>
    <div class="dhtmlgoodies_aTab">
        <table class="style1" cellspacing="1" cellpadding="4">
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label29" runat="server" Text="<%$ Resources:labels, makhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbcustid" runat="server"></asp:Label>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label31" runat="server" Text="<%$ Resources:labels, tendaydu %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbfullnameCust" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label33" runat="server" Text="<%$ Resources:labels, tenviettat %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbshortname" runat="server"></asp:Label>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label35" runat="server" Text="<%$ Resources:labels, loaikhachhang %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbcusttype" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label41" runat="server" Text="<%$ Resources:labels, phone %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbmobi" runat="server"></asp:Label>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label43" runat="server" Text="Email"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbemail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label69" runat="server" Text="<%$ Resources:labels, ghichu %>"></asp:Label>

                </td>
                <td>
                    <asp:Label ID="lbnote" runat="server"></asp:Label>

                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label37" runat="server" Text="<%$ Resources:labels, ngaythanhlap %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbbirth" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label49" runat="server" Text="<%$ Resources:labels, masothuegpkd %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbpassportcmdn" runat="server"></asp:Label>
                </td>
                <td class="tdVDC">
                    <asp:Label ID="Label61" runat="server" Text="<%$ Resources:labels, sofax %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbfax" runat="server"></asp:Label>
                </td>
            </tr>



            <tr>
                <td class="tdVDC">
                    <asp:Label ID="Label45" runat="server" Text="<%$ Resources:labels, address %>"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lbresidentaddress" runat="server"></asp:Label>
                </td>

            </tr>
        </table>
    </div>
    <div class="dhtmlgoodies_aTab">
        <asp:Panel ID="pnToolbar" runat="server">
            <div id="divToolbar">
                <asp:Button ID="btnAddUser" runat="server" Text="<%$ Resources:labels, themmoi %>"
                    OnClick="btnAddNew_Click" />
                &nbsp;
            </div>
        </asp:Panel>
        <div class="divResult">
            <asp:GridView ID="gvCustomerList" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvCustomerList_RowDataBound" PageSize="15">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="USERID" Visible="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpUID" runat="server" Visible="false"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tennguoidung %>" SortExpression="FULLNAME">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpUserFullName" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, phone %>">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, kieunguoidung %>">
                        <ItemTemplate>
                            <asp:Label ID="lblUserType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
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

    <div class="dhtmlgoodies_aTab">
        <asp:Panel ID="pnCard" runat="server">
            <div class="divToolbar">
                &nbsp;<asp:Button ID="btnAddCard" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddCard_Click" />
                &nbsp;<asp:Button ID="btnDeleteCard" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDeleteCard_Click" />
            </div>
        </asp:Panel>
        <div class="divResult">
            <asp:GridView ID="gvCard" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                OnRowDataBound="gvCard_RowDataBound" PageSize="15">
                <RowStyle ForeColor="#000000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="USERID" Visible="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpUID" runat="server" Visible="false"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, makhachhang %>" SortExpression="FULLNAME">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpHolderCIF" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, tendaydu %>">
                        <ItemTemplate>
                            <asp:Label ID="lblFullName" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, numberofcard %>">
                        <ItemTemplate>
                            <asp:Label ID="lblNoCard" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Link type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
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

    <div style="margin-top: 10px; text-align: center;">
        &nbsp;<asp:Button ID="btSave" runat="server" Text="<%$ Resources:labels, save %>" OnClick="btSave_Click" OnClientClick="return validate();" />
        &nbsp;<asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button1_Click" />
    </div>

</div>

<script>
      function SelectCbx(obj) {
          var count = document.getElementById('aspnetForm').elements.length;
          var elements = document.getElementById('aspnetForm').elements;
          if (obj.checked) {
              for (i = 0; i < count; i++) {
                  if (elements[i].type == 'checkbox' && elements[i].disabled != true && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                      elements[i].checked = true;
                      //elements[i].parentNode.parentNode.className = "hightlight";
                  }
              }

          } else {
              for (i = 0; i < count; i++) {
                  if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                      elements[i].checked = false;
                      //elements[i].parentNode.parentNode.className = "nohightlight";
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

      function checkColor(obj, obj1) {
          var obj2 = document.getElementById(obj);
          if (obj2.checked) {
              obj1.className = "hightlight";
          }
          else {
              obj1.className = "nohightlight";
          }
      }
</script>

<script type="text/javascript">
      initTabs('dhtmlgoodies_tabView1', Array('<%=Resources.labels.chitiethopdong %>', '<%=Resources.labels.chitietkhachhang %>', '<%=Resources.labels.taikhoansudung %>', '<%=Resources.labels.workingcard %>'), 0, 960, 320, Array(false, false, false, false));


    function validate() {
        if (validateEmpty('<%=txtopendate.ClientID %>', 'Ngày mở không rỗng')) {
                if (validateEmpty('<%=txtenddate.ClientID %>', 'Ngày hết hạn không rỗng')) {
                if (IsDateGreater('<%=txtenddate.ClientID %>','<%=txtopendate.ClientID %>', 'Ngày hết hạn phải lớn hơn ngày mở')) {

                }
                else {
                    document.getElementById('<%=txtopendate.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtenddate.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtopendate.ClientID %>').focus();
            return false;
        }

    }

</script>
