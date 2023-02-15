<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSProduct_Widget" %>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<link href="widgets/SEMSProduct/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSCustomerList/css/style.css" rel="stylesheet" type="text/css">
<link href="widgets/SEMSCustomerList/css/subModal.css" rel="stylesheet" type="text/css">
<script src="widgets/IBTransferInBank1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransferInBank1/JS/lang/en.js" type="text/javascript"></script>
<link href="widgets/IBTransferInBank1/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<script src="widgets/IBScheduleTransfer/JS/jquery-1.5.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSIBMBNOTIFICATION/Images/system_config_services.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.danhsachnotifyIBMB %>
</div>
<div id="divError">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
            <%=Resources.labels.loading %>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, dichvu %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlService" runat="server"></asp:DropDownList>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label2" runat="server" Text="Varname"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlVarname" runat="server"></asp:DropDownList>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ngayhieuluc %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdatestart" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, ngayhethieuluc%>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdateend" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <script src="widgets/IBScheduleTransfer/JS/jquery.maskedinput-1.3.min.js"
            type="text/javascript"></script>
        <script type="text/javascript">
            var maskedinput = jQuery;
            jQuery.noConflict(true);
        </script>

        <script type="text/javascript">//<![CDATA[
            runcalendar();
            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            function runcalendar() {
                maskedinput(document).ready(function () {
                    maskedinput("input[type=text][id*=txttimestart]").mask("99:99:00");
                });
                maskedinput(document).ready(function () {
                    maskedinput("input[type=text][id*=txttimeend]").mask("99:99:00");
                });



                var cal = Calendar.setup({
                    onSelect: function (cal) { cal.hide() }
                });
                cal.manageFields("<%=txtdatestart.ClientID %>", "<%=txtdatestart.ClientID %>", "%d/%m/%Y");
                cal.manageFields("<%=txtdateend.ClientID %>", "<%=txtdateend.ClientID %>", "%d/%m/%Y");
            }
             //]]></script>
    </asp:Panel>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <div id="divToolbar">
                &nbsp;<asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
                &nbsp;<asp:Button ID="btnDelete" Visible="false" runat="server" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" />
            </div>
            <div id="divResult">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:GridView ID="gvProductList" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvProductList_RowDataBound" PageSize="15"
                    OnPageIndexChanging="gvProductList_PageIndexChanging">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" SortExpression="PRODUCTID">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblID" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" SortExpression="PRODUCTNAME">
                            <ItemTemplate>
                                <asp:Label ID="lblvarname" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, dichvu %>" SortExpression="PRODUCTNAME">
                            <ItemTemplate>
                                <asp:Label ID="lblserviceid" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhieuluc %>" SortExpression="CUSTTYPE">
                            <ItemTemplate>
                                <asp:Label ID="lblstartdate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, ngayhethieuluc%>" SortExpression="DESCRIPTION">
                            <ItemTemplate>
                                <asp:Label ID="lblenddate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="gvFooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                    <SelectedRowStyle />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:GridView>
                <br />
                <asp:Literal ID="litPager" runat="server"></asp:Literal>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script language="javascript">
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className="hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != '') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className="nohightlight";
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
</script>
