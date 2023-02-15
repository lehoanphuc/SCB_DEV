<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSPoster_Widget" %>
<script src="widgets/SEMSProduct/JS/common.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/SEMSProduct/Images/product.png" style="width: 32px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.listposter %>
</div>

<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
        <ProgressTemplate>
            <img alt="" src="widgets/SEMSCustomerList/Images/ajaxloader.gif" style="width: 16px;
                height: 16px;" />
            <%=Resources.labels.loading %>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <div id="divToolbar">
                &nbsp;
                <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>"  OnClick="btnDelete_Click" />
            </div>
            <div id="divResult">
                <asp:GridView ID="gvPosterList" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="100%" AllowPaging="True"
                    AutoGenerateColumns="False" OnRowDataBound="gvPosterList_RowDataBound" PageSize="15"
                    AllowSorting="True">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxSelect" runat="server" />
                                <asp:HiddenField ID="hdfPosterId" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, filename%>" SortExpression="FileName">
                            <ItemTemplate>
                                <asp:Label ID="lblFileName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, path%>" SortExpression="Path">
                            <ItemTemplate>
                                <asp:Label ID="lblPath" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, chieurong%>" SortExpression="Path">
                            <ItemTemplate>
                                <asp:Label ID="lblWidth" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, chieucao%>" SortExpression="Path">
                            <ItemTemplate>
                                <asp:Label ID="lblHeight" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, filetype%>" SortExpression="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, position%>" SortExpression="Position">
                            <ItemTemplate>
                                <asp:Label ID="lblPosition" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, index%>" SortExpression="Idx">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, isshow%>" SortExpression="Idx">
                            <ItemTemplate>
                                <asp:Label ID="lblPublish" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                            <ItemTemplate>
                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
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
