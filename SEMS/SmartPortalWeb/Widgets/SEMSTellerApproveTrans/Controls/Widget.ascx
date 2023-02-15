<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSTellerApproveTrans_Controls_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleProduct" runat="server"></asp:Label>
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
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12 col-xs-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.thongtinduyet%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnAdd" runat="server">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tensanpham %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlRole" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.giaodich %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlTrans" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tu %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtFrom" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.den %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtTo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tiente %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:CheckBox ID="cbPassLevel" runat="server" Text="<%$ Resources:labels, duyetvuotcap %>" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <asp:Label CssClass="col-sm-4 col-xs-12 control-label" Text="<%$ Resources:labels, capbacduyet %>" runat="server" Visible="false"></asp:Label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlLastLvApprove" runat="server" Width="100%" Visible="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="Loading();return validate();" OnClick="btsave_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClientClick="Loading();" OnClick="btnClear_Click" />
                            <asp:Button ID="btnback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClientClick="Loading();" OnClick="btnback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnProcessApprove" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.quitrinhduyet%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-10 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.thutuduyet %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlOrderApp" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.capbac %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlLevel" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.mota %></label>
                                                    <div class="col-sm-8 col-xs-12">
                                                        <asp:TextBox ID="txtDesc" CssClass="form-control" TextMode="MultiLine" onkeyup="ValidateLimit(this,'255');" onkeyDown="ValidateLimit(this,'255');" onpaste="ValidateLimit(this,'255');" onChange="ValidateLimit(this,'255');" onmousedown="ValidateLimit(this,'255');" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12"></div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-12 col-xs-12">
                                                <asp:Button ID="btnSaveDetails" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, add %>" OnClick="btnSaveDetails_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnGV" runat="server" Visible="False">
            <div id="divResult">
                <asp:GridView ID="gvAppTransDetailsList" CssClass="table table-hover" runat="server" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvAppTransDetailsList_RowDataBound" PageSize="15"
                    OnPageIndexChanging="gvAppTransDetailsList_PageIndexChanging"
                    OnRowDeleting="gvAppTransDetailsList_RowDeleting">
                    <RowStyle ForeColor="#000000" />
                    <Columns>
                        <asp:TemplateField HeaderText="ProcessID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbProcessID" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, thutuduyet %>">
                            <ItemTemplate>
                                <asp:Label ID="lblOder" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, capbac %>" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblLevel" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, capbac %>">
                            <ItemTemplate>
                                <asp:Label ID="lblLevelName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, mota%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-secondary" CommandName='<%#IPC.ACTIONPAGE.DELETE %>' OnClientClick="Loading(); return Confirm();">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                </asp:GridView>
                <asp:Literal ID="litPager" runat="server"></asp:Literal>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">
    function validate() {
        if (validateEmpty('<%=txtFrom.ClientID %>', '<%=Resources.labels.sotienkhoidiemkhongrong %>')) {
            if (validateEmpty('<%=txtTo.ClientID %>', '<%=Resources.labels.sotienketthuckhongrong %>')) {
                if (validateFormTo('<%=txtFrom.ClientID %>', '<%=txtTo.ClientID %>', '<%=Resources.labels.sotienketthucphailonhonsotienkhoidiem %>')) {

                }
                else {
                    document.getElementById('<%=txtFrom.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtTo.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtFrom.ClientID %>').focus();
            return false;
        }
    }

    function Loading() {
        if (document.getElementById('<%=lblError.ClientID%>').innerHTML != '') {
            document.getElementById('<%=lblError.ClientID%>').innerHTML = '';
        }
    }

    function Confirm() {
        return confirm('<%=Resources.labels.banchacchanmuonxoa %>');
    }

</script>

