<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBCorpUserApproveLimit_Widget" %>


<style type="text/css">
    .style1 {
        width: 100%;
    }

    #divSearch {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 5px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }

    #divToolbar {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 20px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }

    #divLetter {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 20px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }

    #divResult {
        margin: 20px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }

    .gvHeader {
        text-align: left;
    }

    #divEmployHeader {
        width: 100%;
        font-weight: bold;
        padding: 5px 5px 5px 5px;
    }

    #divError {
        width: 100%;
        font-weight: bold;
        height: 10px;
        text-align: center;
        padding: 0px 5px 5px 5px;
    }

    .hightlight {
        background-color: #EAFAFF;
        color: #003366;
    }

    .nohightlight {
        background-color: White;
    }
</style>
<link href="widgets/ibcorpusermanagement/UserManagement.css" rel="stylesheet" type="text/css" />
<link href="widgets/ibcorpusermanagement/login.css" rel="stylesheet" type="text/css" />


<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="login_right_content">
            <div class="login_right_content_content">
                <div class="login_right_content_content">
                    <div class="login_right_content_content_header">
                        <div class="login_left_right_content_header">
                        </div>
                        <div class="login_middle_right_content_header">
                            <div class="login_right_menu_icon"></div>
                            <asp:Label ID="Label3" runat="server"
                                Text="<%$ Resources:labels, thietlaphanmucduyetgiaodichtheocapbac %>"></asp:Label><br />
                        </div>

                        <div class="login_right_right_content_header">
                        </div>
                        <div class="backToIndex">
                            <li><a href="Default.aspx?po=3&p=86"><%=Resources.labels.trolaitrangchu %></a></li>
                        </div>
                    </div>

                    <div class="u_container">
                        <div style="height: 16px; text-align: center;">
                            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>

                            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                <ProgressTemplate>
                                    <img alt="" src="Images/WidgetImage/ajaxloader.gif" style="width: 16px; height: 16px;" />
                                    <%=Resources.labels.loading %>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="u_inside_top">
                            <div class="u_depart">
                                <asp:ListBox ID="lstDept" runat="server" Width="100%" Height="401px"
                                    AutoPostBack="True" OnSelectedIndexChanged="lstDept_SelectedIndexChanged"></asp:ListBox>
                            </div>
                            <div class="u_employ">
                                <asp:Literal runat="server" ID="ltrError"></asp:Literal>
                                <asp:GridView ID="gvUser" runat="server" BackColor="White" CssClass="table table-border footable"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                    OnRowDataBound="gvUser_RowDataBound" PageSize="15"
                                    OnPageIndexChanging="gvUser_PageIndexChanging"
                                    OnSorting="gvUser_Sorting" AllowSorting="True">
                                    <RowStyle ForeColor="#000000" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbxSelect" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUID" runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IPCTRANCODE" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRANCODE" runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, loaigiaodich %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrans" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, hanmuc %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLimit" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, tiente %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %>">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, huy %>">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#349AC0" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                                <br />
                                <asp:Literal ID="litPager" runat="server"></asp:Literal>
                                &nbsp;
                            </div>

                        </div>

                        <div class="u_inside_bottom">
                            <div class="crazy_ie6"></div>
                            <div class="u_inside_bottom_left">

                                <%--                        	<li style="background-image:url(images/line_menu_83.gif);background-repeat:no-repeat">
                            	<asp:LinkButton runat="server" ID="lbDeptInsert" Text="Thêm" onclick="lbDeptInsert_Click"  
                                        ></asp:LinkButton></li> 
                            	 <li style="background-image:url(images/line_menu_83.gif);background-repeat:no-repeat">
                                     <asp:LinkButton runat="server" ID="lbDeptEdit" Text="Sửa" onclick="lbDeptEdit_Click" 
                                        ></asp:LinkButton></li>
                            	<li><asp:LinkButton runat="server" ID="lbDeptDelete" Text="Xóa" onclick="lbDeptDelete_Click" 
                                        ></asp:LinkButton></li>--%>
                            </div>
                            <div class="u_inside_bottom_right">
                                <li>
                                    <asp:LinkButton runat="server" ID="lbDeleteLimit" Text="<%$ Resources:labels, huy %>"
                                        OnClick="lbUserDelete_Click"></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lbUserInsert" Text="<%$ Resources:labels, themmoi %>"
                                        OnClick="lbUserInsert_Click"></asp:LinkButton></li>
                            </div>
                        </div>
                    </div>

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
