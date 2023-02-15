<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSUserApprove_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<script src="widgets/SEMSUserApprove/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSUserApprove/JS/lang/en.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.duyetnguoidung %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.duyetnguoidung  %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <div class="panel-content form-horizontal p-b-0">
                                <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.tendaydu %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtfullname" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <%--<label class="col-sm-4 control-label"><%=Resources.labels.loainguoidung %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlusertype" runat="server" CssClass="form-control select2 infinity">
                                                    </asp:DropDownList>
                                                </div>--%>
                                                <label class="col-sm-4 control-label"><%=Resources.labels.trangthai %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control select2 infinity">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, xemchitiet %>" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.email %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label"><%=Resources.labels.phone %></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtphone" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label" runat="server" visible="false"><%=Resources.labels.capbac %></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddluserlevel" runat="server" Visible="False" CssClass="form-control select2 infinity">
                                                        <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                                        <asp:ListItem Value="0">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnbutton" Visible="false">


            <div id="divToolbar">
                &nbsp;<asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, duyet %>" OnClick="btnApprove_Click" />
                &nbsp;<asp:Button ID="btnReject" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, khongduyet %>" OnClick="btnReject_Click" />
            </div>
        </asp:Panel>
        <asp:Literal ID="litError" runat="server"></asp:Literal>
        <asp:GridView ID="gvcUserList" runat="server" BackColor="White" CssClass="table table-hover"
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnRowCommand="gvcUserList_RowCommand"
            Width="100%" AllowPaging="True" AutoGenerateColumns="False"
            OnRowDataBound="gvcUserList_RowDataBound" PageSize="15"
            OnPageIndexChanging="gvcUserList_PageIndexChanging"
            OnSorting="gvcUserList_Sorting" AllowSorting="True">
            <RowStyle ForeColor="#000000" />
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxSelect" runat="server"/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="userid" Visible="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="lbluserID" runat="server" Visible="false"></asp:HyperLink>
                        <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, fullname %>" SortExpression="FULLNAME">
                    <ItemTemplate>
                        <asp:LinkButton ID="hpfullnname" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("USERID")+"|" +Eval("TYPEID")%>'>[hpDetails]</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels,phone %>" SortExpression="FULLNAMECUST">
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, tenkhachhang %>" SortExpression="FULLNAMECUST">
                    <ItemTemplate>
                        <asp:Label ID="lblcustName" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, birthday %>" SortExpression="BIRTHDAY">
                    <ItemTemplate>
                        <asp:Label ID="lblbirth" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, loainguoidung %>" SortExpression="USERTYPE">
                    <ItemTemplate>
                        <asp:Label ID="lblusertype" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type ID" SortExpression="USERTYPE">
                    <ItemTemplate>
                        <asp:Label ID="lbltypeID" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email" SortExpression="EMAIL">
                    <ItemTemplate>
                        <asp:Label ID="lblemail" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>" SortExpression="STATUS">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approve">
                    <ItemTemplate>
                        <asp:LinkButton ID="hpApprove" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.APPROVE %>' CommandArgument='<%#Eval("USERID")+"|" +Eval("TYPEID")%>'>[hpApprove]</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Reject">
                    <ItemTemplate>
                        <asp:LinkButton ID="hpReject" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.REJECT %>' CommandArgument='<%#Eval("USERID")+"|" +Eval("TYPEID")%>'>[hpReject]</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
        <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
        <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<script>
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
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
        var count = document.getElementById('<%=gvcUserList.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvcUserList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvcUserList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvcUserList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = false;
                    if (Counter > 0)
                        Counter--;
                }
            }
        }
        hdf.value = Counter.toString();
    }
    var TotalChkBx;
    var Counter;

    window.onload = function () {
        document.getElementById('<%=hdCounter.ClientID %>').value = '0';
    }

    function ChildClick(CheckBox) {
        Counter = parseInt(document.getElementById('<%=hdCounter.ClientID %>').value);
        TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);

        var grid = document.getElementById('<%= gvcUserList.ClientID %>');
        var cbHeader = grid.rows[0].cells[0].childNodes[0];

        if (CheckBox.checked)
            Counter++;
        else if (Counter > 0)
            Counter--;

        if (Counter < TotalChkBx)
            cbHeader.checked = false;
        else if (Counter == TotalChkBx)
            cbHeader.checked = true;
        document.getElementById('<%=hdCounter.ClientID %>').value = Counter.toString();
    }

    function pop(obj) {
        if (window.confirm(obj)) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
