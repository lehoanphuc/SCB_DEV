<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSSMSNotify_Widget" %>
<script src="widgets/SEMSSMSNotify/JS/common.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSSMSNotify/Images/smsnotification.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.configsmsnotification %>
</div>
<div id="divError">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSSMSNotify/Images/ajaxloader.gif" style="width: 16px; height: 16px;" />
            <%=Resources.labels.loading %>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div id="divSearch">
    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
        <table class="style1">
            <tr>
                <td class="thlb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, role %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlRole" runat="server"></asp:DropDownList>
                </td>
                <td class="thlb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, loaigiaodich %>"></asp:Label>
                </td>
                <td class="thtds">
                    <asp:DropDownList ID="ddlTranType" runat="server"></asp:DropDownList>
                </td>
                <td class="thbtn">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:labels, timkiem %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, configname %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConfigName" runat="server"></asp:TextBox>
                </td>
                <%--            <td>
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, mota%>"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtdesc" runat="server"></asp:TextBox>
            </td>--%>
            </tr>
        </table>
    </asp:Panel>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
    <div id="divToolbar">
        &nbsp;<asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:labels, themmoi %>" onclick="btnAddNew_Click" />
        &nbsp;<asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>" Width="73px" onclick="btnDelete_Click" Visible="false" />
    </div>
<div id="divResult">
    <asp:Literal ID="litError" runat="server"></asp:Literal>
    <asp:GridView ID="gvProductList" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" 
        onrowdatabound="gvProductList_RowDataBound" PageSize="15" 
        onpageindexchanging="gvProductList_PageIndexChanging" 
        onsorting="gvProductList_Sorting" AllowSorting="True">
        <RowStyle ForeColor="#000000" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server"/>                   
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CFID" SortExpression="CFID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCFID" runat="server" Visible="true"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, configname %>" SortExpression="PRODUCTNAME">
                <ItemTemplate>
                    <asp:HyperLink ID="hpConfigName" runat="server"></asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, rolename %>">
                <ItemTemplate>
                    <asp:Label ID="lblRole" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, loaigiaodich%>" SortExpression="DESCRIPTION">
                <ItemTemplate>
                    <asp:Label ID="lblTranType" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, mota%>" SortExpression="DESCRIPTION">
                <ItemTemplate>
                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>" Visible="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                </ItemTemplate>
                 <HeaderStyle CssClass="gvHeader" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>" Visible="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                </ItemTemplate>
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

</contenttemplate>
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
