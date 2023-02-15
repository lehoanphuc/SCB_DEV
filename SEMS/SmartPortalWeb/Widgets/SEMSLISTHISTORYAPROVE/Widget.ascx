<%@ control language="C#" autoeventwireup="true" codefile="Widget.ascx.cs" inherits="Widgets_SEMSLISTHISTORYAPROVE_Widget" %>
<%@ register tagprefix="uc1" tagname="GridViewPaging" src="~/Controls/GirdViewPaging/GridViewPaging.ascx" %>
<%@ import namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="subheader">
    <h1 class="subheader-title">
        <%=Resources.labels.listWaitingTransaction %>
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
                                                <asp:TextBox ID="txtTranID" CssClass="form-control" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12" style="display: none;">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.sogiaodichcore %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtTranRef" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loaigiaodich %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlTransactionType" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="display: none;">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.debitaccount %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtAccno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12" style="display: none;">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.taikhoanbaoco %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCreditAcct" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="display: none;">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.contractno %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtcontractno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.customername %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtcustname" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="display: none;">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.customercodecore %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtcustcode" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.socmnd %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtCMND" CssClass="form-control" runat="server"></asp:TextBox>
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
                                <div class="row" style="display:none;"">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.trangthai %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2 infinity" Width="100%" runat="server">
<%--                                                                  <asp:ListItem Value="ALL" Selected Text="<%$ Resources:labels, tatca %>"></asp:ListItem>--%>
                                   <%--                 <asp:ListItem Value="0" Text="<%$ Resources:labels, dangxuly %>"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="<%$ Resources:labels, hoanthanh %>"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="<%$ Resources:labels, loi %>"></asp:ListItem>--%>
                                                    <asp:ListItem Value="3" Selected Text="<%$ Resources:labels, choduyet %>"></asp:ListItem>
                                                    <%--                <asp:ListItem Value="4" Text="<%$ Resources:labels, huy %>"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="<%$ Resources:labels, conpending %>"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="<%$ Resources:labels, thanhtoanthatbai %>"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row" style="display: none;">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label">Schedule</label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:CheckBox ID="cbIsSchedule" runat="server"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
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
            <div id="pnbutton" runat="server">
                <asp:Button ID="btnExport" CssClass="btn btn-secondary" runat="server" Text='<%$ Resources:labels, exporttofile %>' OnClick="bt_export_Click" />
                <asp:Button ID="btnRollback" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, rollback %>"  OnClick="btnRollback_Click" />

            </div>
        </div>

<div id="divResult" runat="server" class="table-responsive">
    <asp:Literal runat="server" ID="ltrError"></asp:Literal>
    <asp:GridView ID="gvLTWA" CssClass="table table-hover" runat="server" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
        CellPadding="5" Width="100%" OnRowDataBound="gvLTWA_RowDataBound" OnRowCommand="gvLTWA_RowCommand" PageSize="15">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbxSelect" runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <HeaderStyle CssClass="gvHeader" />
             </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, sogiaodich%>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbTranID" runat="server" CommandName='<%#IPC.ACTIONPAGE.DETAILS %>' CommandArgument='<%#Eval("IPCTRANSID")%>' OnClientClick="Loading();"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, ngaygiogiaodich %>'>
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, loaigiaodich %>'>
                <ItemTemplate>
                    <asp:Label ID="lblTrantype" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, debitaccount %>'>
                <ItemTemplate>
                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, sotien %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, phi %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblFee" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, totalAmount %>' ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, loaitien %>'>
                <ItemTemplate>
                    <asp:Label ID="lblCCYID" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
         <%--   <asp:TemplateField HeaderText='<%$ Resources:labels, errordesc %>'>
                <ItemTemplate>
                    <asp:Label ID="lblErrDesc" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText='<%$ Resources:labels, mota %>'>
                <ItemTemplate>
                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, malo %>' Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblBatchRef" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, sogiaodichcore %>' Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblRefCore" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, customercodecore %>' Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblcustcodecore" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:labels, trangthai %>'>
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:labels, trangthaiduyet %>" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblResult" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
     <asp:HiddenField ID="hdCounter" Value="0" runat="server" />
      <asp:HiddenField ID="hdPageSize" Value="15" runat="server" />
    <uc1:gridviewpaging runat="server" id="GridViewPaging" />
</div>
<asp:Label ID="lblheaderFile" runat="server" Text="<%$ Resources:labels, titlelistapproved %>" Visible="false" />
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
    function SelectCbx(obj) {
        Counter = 0;
        var hdf = document.getElementById("<%= hdCounter.ClientID %>");
          TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);
          var count = document.getElementById('<%=gvLTWA.ClientID %>').rows.length;
          var elements = document.getElementById('<%=gvLTWA.ClientID %>').rows;
          if (obj.checked) {
              for (i = 0; i < count; i++) {
                  if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl19_gvLTWA_ctl01_cbxSelectAll') {
                      elements[i].cells[0].children[0].checked = true;
                      Counter++;
                  }
              }

          } else {
              for (i = 0; i < count; i++) {
                  if (elements[i].cells[0].children[0].type == 'checkbox' && elements[i].cells[0].children[0].id != 'ctl00_ctl19_gvLTWA_ctl01_cbxSelectAll') {
                      elements[i].cells[0].children[0].checked = false;
                      if (Counter > 0)
                          Counter--;
                  }
              }
          }
          hdf.value = Counter.toString();
    }
    function ChildClick(CheckBox) {
        Counter = parseInt(document.getElementById('<%=hdCounter.ClientID %>').value);
         TotalChkBx = parseInt(document.getElementById('<%=hdPageSize.ClientID %>').value);

         var grid = document.getElementById('<%= gvLTWA.ClientID %>');
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
