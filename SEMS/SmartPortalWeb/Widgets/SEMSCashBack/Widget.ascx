<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSCashBack_Widget" %>
<%@ Register TagPrefix="uc1" TagName="GridViewPaging" Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.cashbacklist %>
    </h1>
</div>
<div id="divError">
    <asp:Label ID="lblError" runat="server"></asp:Label>
</div>

<div class="row">
    <div class="col-sm-12 col-xs-12">
        <div class="panel">
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0">
                    <asp:Panel ID="pnSearch" runat="server" DefaultButton="btnSearch">
                        <div class="row">
                            <div class="col-sm-10 col-xs-12">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 col-xs-12 control-label"><%=Resources.labels.sogiaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtTranID" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.usercreated %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtUserCreated" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">To Phone</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                       <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.trangthai %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="ALL" Text="<%$ Resources:labels, tatca %>"></asp:ListItem>
                                                    <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                                                    <asp:ListItem Value="Incompleted" Text="Incompleted"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-sm-2 col-xs-12">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, timkiem %>" OnClick="btnSearch_Click" OnClientClick="return Validate();" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divToolbar">
    <asp:Button ID="btnUploadFile" CssClass="btn btn-secondary" runat="server" Text="Upload File" OnClick="bt_Upload_Click" />
</div>
<div id="divResult">
    <asp:Literal runat="server" ID="ltrError"></asp:Literal>
    <asp:GridView ID="gvLTWA" CssClass="table table-hover" runat="server" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
        CellPadding="5" Width="100%" OnRowDataBound="gvLTWA_RowDataBound" OnRowCommand="gvLTWA_OnRowCommand" PageSize="15">
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbTranID" CssClass="cslinkbutton" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("TRANID")%>' OnClientClick="Loading();"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, ngaygiogiaodich %>'>
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From Phone" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblFrmPhone" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From Full Name" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblFrmFullName" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Phone">
                <ItemTemplate>
                    <asp:Label ID="lblToPhone" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Full Name">
                <ItemTemplate>
                    <asp:Label ID="lblToFulName" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, sotien %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CCYID">
                <ItemTemplate>
                    <asp:Label ID="lblccyid" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="gvHeader" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User created">
                <ItemTemplate>
                    <asp:Label ID="lblUserCreated" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"  Width="30%"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, trangthai %>'>
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
</div>
<asp:Label ID="lblheaderFile" runat="server" Text="<%$ Resources:labels, nganhangphuongnamnhatkygiaodich %>" Visible="false" />
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function Validate() {
        if ((document.getElementById('<%=txtFromDate.ClientID %>').value != "") && (document.getElementById('<%=txtToDate.ClientID %>').value != "")) {
            if (!IsDateGreater('<%=txtToDate.ClientID %>','<%=txtFromDate.ClientID %>','<%=Resources.labels.ngayketthuclonhonngaybatdau %>')) {
                document.getElementById('<%=txtFromDate.ClientID %>').focus();
                return false;
            }
        }
        return true;
    }
</script>
