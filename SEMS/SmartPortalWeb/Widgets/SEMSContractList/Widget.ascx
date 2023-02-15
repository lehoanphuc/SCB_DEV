<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractList_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Register Src="../../Controls/LetterSearch/LetterSearch.ascx" TagName="LetterSearch" TagPrefix="uc1" %>
<script src="widgets/SEMSContractList/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/lang/en.js" type="text/javascript"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.thongtinhopdong %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="divSearch" runat="server" DefaultButton="btnSearch">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.mahopdong %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtContractCode" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" CssClass="col-sm-4 control-label" runat="server"><%=Resources.labels.phone %></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server"></asp:TextBox>
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
                                            <label class="col-sm-4 control-label"><%=Resources.labels.cmndgpkd %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtlicenseid" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.socifcorebanking %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtcustcode" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ngaymo %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtOpenDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ngayhethan %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtEndDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <asp:Label ID="Label112" CssClass="col-sm-4 control-label" runat="server"><%=Resources.labels.nguoimo %></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtopenPer" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-sm-5">
                                        <div class="form-group">
                                            <%--<asp:Label ID="Label1123" CssClass="col-sm-4 control-label" runat="server" Text="<%$ Resources:labels,subusertype  %>"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control select2 infinity">
                                                </asp:DropDownList>
                                            </div>--%>
                                            <asp:Label ID="Label3" CssClass="col-sm-4 control-label" runat="server" Text="<%$ Resources:labels,loaihinhsanpham  %>"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlproducttype" runat="server" CssClass="form-control select2 infinity">
                                                    </asp:DropDownList>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.tenkhachhang %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtCustName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" CssClass="col-sm-4 control-label" runat="server" Text="<%$ Resources:labels,trangthai %>"></asp:Label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2 infinity">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <%--<div id ="divsearchProduct" runat="server" visible="false">--%>
                                                
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divToolbar">
            &nbsp;<asp:Button ID="btnDelete" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, deletecontract %>" OnClick="btnDelete_Click" />
            &nbsp;<asp:Button ID="btnBlock" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, khoahopdong %>" OnClick="btnBlock_Click" />
            &nbsp;<asp:Button ID="Button3" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, exporttofile %>" Visible="false" />
        </div>
        <div class="row" runat="server" id="divresult">
            <div class="col-sm-12">
                <div id="divResult" class="table-responsive;">
                    <asp:Literal ID="litError" runat="server"></asp:Literal>
                    <asp:GridView ID="gvcontractList" runat="server" BackColor="White" CssClass="table table-hover white-space" 
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                        Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvContractList_RowCommand"
                        OnRowDataBound="gvcontractList_RowDataBound" PageSize="15" 
                        OnPageIndexChanging="gvcontractList_PageIndexChanging"
                        OnSorting="gvcontractList_Sorting" AllowSorting="True">
                        <RowStyle ForeColor="#000000" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, mahopdong %>" SortExpression="CONTRACTNO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="hpcontractCode" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("ContractNo")%>'>[hpDetails]</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, phone %>" SortExpression="PHONE">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, khachhang %>" SortExpression="FULLNAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblcustName" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paper Number" SortExpression="LICENSEID">
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
                            <asp:TemplateField HeaderText="<%$ Resources:labels, subusertype %>" SortExpression="USERTYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblContractType" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, corptype %>" SortExpression="CONTRACTTYPE" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblcorpType" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, socifcorebanking %>" SortExpression="CUSTCODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblcustcode" runat="server"></asp:Label>
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
                            <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                                <ItemTemplate>
                                    <asp:LinkButton ID="hpEdit" runat="server" CssClass="btn btn-primary" CommandName='<%#IPC.ACTIONPAGE.EDIT %>' CommandArgument='<%#Eval("ContractNo")%>'>[hpEdit]</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="hpDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' CommandArgument='<%#Eval("ContractNo")%>'>[hpDelete]</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:labels, xemlai %>">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpReview" runat="server" CssClass="btn btn-secondary">[hpReview]</asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle />
                    </asp:GridView>
                    <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
                    <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
                    <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
                </div>
            </div>
        </div>
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
        var count = document.getElementById('<%=gvcontractList.ClientID %>').rows.length;
        var elements = document.getElementById('<%=gvcontractList.ClientID %>').rows;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvcontractList_ctl01_cbxSelectAll') {
                    elements[i].cells[0].children[0].checked = true;
                    Counter++;
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl20_gvcontractList_ctl01_cbxSelectAll') {
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

        var grid = document.getElementById('<%= gvcontractList.ClientID %>');
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

</script>
<style>
    .white-space td{
        white-space:nowrap;
    }
    .white-space th {
        padding:10px 4px !important;
    }
</style>