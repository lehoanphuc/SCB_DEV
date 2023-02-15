<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSResetPassByUser_Detail_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.laylaimatkhaupinchonguoidung %>
            </h1>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.Requestbyuser %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaihinhdichvu %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtService" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.userid %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtUserID" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tendaydu %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtName" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">NRC </label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtNRIC" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ngaysinh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDOB" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.email %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtEmail" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.phone %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtPhone" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.datecreated %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDateCreated" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtStatus" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.authentype %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="lbltyperes" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group hidden">
                                            <label class="col-sm-4 col-xs-12 control-label">7 Last Account No digits</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtActNo" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.Userinformationinsystem %>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <div style="float: left;" class="col-sm-12 col-xs-12">
                                    <div class="col-xs-12 col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaihinhdichvu %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtService_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.userid %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtUserID_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tendaydu %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtName_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">NRC</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtNRIC_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.ngaysinh %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDOB_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.email %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtEmail_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.phone %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtPhone_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.datecreated %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtDateCreated_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtStatus_sys" Enabled="false" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  hidden">
                                            <div class="col-sm-12 col-xs-12">
                                                <asp:Label runat="server" ID="lblAlertAct" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <%--   <div class="col-sm-12">
                <div class="form-group">
                    <asp:GridView ID="gvAccountList" runat="server" BackColor="White" CssClass="table table-hover" OnPageIndexChanging="gvAccountList_PageIndexChanging"
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                        Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="gvAccountList_RowDataBound">
                        <RowStyle ForeColor="#000000" />
                        <Columns>
                            <asp:BoundField HeaderText="<%$ Resources:labels, sotaikhoan %>" DataField="ACCOUNTNO" />
                            <asp:TemplateField HeaderText="<%$ Resources:labels, loaitaikhoan %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblacctype" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="<%$ Resources:labels, tiente %>" DataField="CCYID" />
                        </Columns>
                        <FooterStyle CssClass="gvFooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                        <SelectedRowStyle />
                        <HeaderStyle CssClass="gvHeader" />
                    </asp:GridView>

                </div>
            </div>--%>
            <div class="divimage">
                <asp:Repeater runat="server" ID="rptImage" OnItemDataBound="rptImage_ItemDataBound">
                    <ItemTemplate>
                        <asp:Image runat="server" ID="img" />
                    </ItemTemplate>
                </asp:Repeater>
                <div style="clear: both"></div>
            </div>
        </div>
        </div>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>
<div class="row">
    <div class="col-sm-12">
        <div class="panel">
            <div class="panel-hdr">
                <h2>
                    <%=Resources.labels.chidinhguithongbao %>
                </h2>
            </div>
            <div class="panel-container">
                <div class="panel-content form-horizontal p-b-0 pd0 ">
                    <div class="col-sm-12 col-xs-12 ">
                        <div class="form-group alg-center panel-hdr">
                            <asp:Label CssClass="col-sm-3 col-xs-12 col-sm-offset-2 control-label required" ID="lblsendinfo" runat="server" Visible="true" Text="<%$ Resources:labels, sendcontractinfor %>"></asp:Label>
                            <div class="col-sm-4  col-xs-12">
                                <asp:DropDownList ID="ddlSendinfo" CssClass="form-control select2" Visible="true" runat="server" Width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-4 col-xs-12">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 ">
                        <div class="alg-item">
                            <asp:HiddenField runat="server" ID="txtusername" />
                            &nbsp;
    <asp:Button ID="btnApprove" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, duyet %>" OnClick="btnApprove_Click" OnClientClick="Loading(); return ConfirmApprove();" />
                            &nbsp;
    <asp:Button ID="btnReject" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, khongduyet %>" OnClick="btnReject_Click" OnClientClick="Loading(); return ConfirmReject();" />
                            &nbsp;
                      
 <asp:Button ID="btback" OnClick="btnBack_Click" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
<style>
    .divHeaderStyle {
        margin-bottom: 15px;
    }

    .pd0 {
        padding: 0;
    }

    .alg-item {
        height: 50px;
        align-items: center;
        display: flex;
        justify-content: center;
    }

    .alg-center {
        height: 50px;
        display: flex;
        justify-content: center;
        align-items: center;
        margin: 0;
    }
</style>
<script>
    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }
    function ConfirmApprove() {
        if (document.getElementById('<%=lbltyperes.ClientID %>').innerHTML  == 'PINCODE') {
            return confirm('<%=Resources.labels.approveresetpincode %>');
        } else {
            return confirm('<%=Resources.labels.approveresetpass %>');
        }
    }
    function ConfirmReject() {
        if (document.getElementById('<%=lbltyperes.ClientID %>').innerHTML  == 'PINCODE') {
            return confirm('<%=Resources.labels.rejectresetpincode %>');
        } else {
            return confirm('<%=Resources.labels.rejectresetpass %>');
        }

    }
</script>
