<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSReversalApprove_Widget" %>
<%@ Import Namespace="SmartPortal.Constant" %>
<%@ Register Src="/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>

<%@ Import Namespace="SmartPortal.Constant" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="divSearch"  style="padding-left: 2%">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.ART%>
                </h1>
            </div>
            <br />
            <br />
            <div class="panel-container form-horizontal p-b-0" style="margin-left: 2%">
                <div class="row">
                    <div class="col-sm-5">
                        <div class="form-group">
                            <label class="col-sm-5 control-label"><%=Resources.labels.ReversalRequestID %></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtrequestID" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6" >
                        <div class="form-group">
                            <label class="col-sm-5 control-label"><%=Resources.labels.Transid %></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtTranid" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" >
                    <div class="col-sm-5" >
                        <div class="form-group">
                            <label class="col-sm-5 control-label"><%=Resources.labels.tungay %></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtFrom" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label class="col-sm-5 control-label"><%=Resources.labels.denngay %></label>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtTo" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" >
                    <div class="col-sm-5">
                        <div class="form-group">
                            <label class="col-sm-5 control-label"><%=Resources.labels.status %></label>
                            <div class="col-sm-7">
                                <asp:DropDownList ID="ddStatus" CssClass="form-control select2 infinity" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-11" style="text-align: center; margin:1%">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click" />
                    </div>
                </div>
            </div>
        </div>
        <panel id="pnResult" runat="server" visible="false">
        <%-- Table View --%>
        <div id="divResult">
            <asp:Panel runat="server" ScrollBars="Horizontal">
                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                    <HeaderTemplate>
                        <div class="pane">
                            <div class="table-responsive">
                                <table class="table table-hover footable c_list">
                                    <thead class="thead-light repeater-table">
                                        <tr>
                                            <th class="title-repeater"><%=Resources.labels.ReversalRequestID%></th>
                                            <th class="title-repeater"><%=Resources.labels.Transid%></th>
                                            <th class="title-repeater"><%=Resources.labels.maker%></th>
                                            <th class="title-repeater"><%=Resources.labels.ngaygio%></th>
                                            <th class="title-repeater"><%=Resources.labels.ReasonforReversal%></th>
                                            <th class="title-repeater"><%=Resources.labels.status%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tr-boder">
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("RRID") + "+" + Eval("TxrefRR")%>' CommandName='<%#IPC.ACTIONPAGE.DETAILS%>' OnClientClick="Loading();"><%#Eval("RRCode") %></asp:LinkButton>
                            </td>
                            <td class="tr-boder"><%#Eval("TxrefRR") %></td>
                            <td class="tr-boder"><%#Eval("Maker") %></td>
                            <td class="tr-boder"><%#Eval("DateRequest") %></td>
                            <td class="tr-boder"><%#Eval("ReasonName") %></td>
                            <td class="tr-boder"><%#Eval("Caption") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                           </div> </div>
                    </FooterTemplate>
                </asp:Repeater>
                <uc1:GridViewPaging runat="server" ID="GridViewPaging" />
            </asp:Panel>
        </div>
            </panel>
    </ContentTemplate>
</asp:UpdatePanel>
