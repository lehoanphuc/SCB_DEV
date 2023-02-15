<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSDocumentManagement_Widget" %>
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
        <div id="divSearch">
            <div class="subheader">
                <h1 class="subheader-title">
                    <%=Resources.labels.DocumentSearch%>
                </h1>
            </div>
            <div class="panel-container form-horizontal p-b-0">
                <div class="search_box">
                    <div class="row">
                        <div class="container">
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
                                </div>
                                <div class="col-sm-1"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="wrap-collabsible">
                    <div class="SearchAdvance">
                        <div class="panel-container">
                            <span>
                                <em class="fa fa-angle-down"></em>
                                <%--<span id="collapsible" data-target="#SearchAdvence"  data-toggle="collapse">--%>
                                <span id="toggleDetail" data-toggle="collapse" href="#SearchAdvence" role="button" aria-expanded="false" aria-controls="collapseExample">
                                    <label for="collapsible" class="lbl-toggle"><%=Resources.labels.AdvanceSearch %></label>
                                    <span style="display: block;" class="downarrowclass"></span>
                                </span>
                                <div id="SearchAdvence" class="collapse in">
                                    <div class="panel-content form-horizontal p-b-0">
                                        <div class="row">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.PhoneNumber %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.DateCreate %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtCreatedDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.PaperNumber %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtPaperNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.LastModifiedDate %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtLastModifiedDate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.fullname %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" Style="width: 100%;" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.custtype %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlCustomerType" CssClass="form-control select2" Style="width: 100%;" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                        <asp:Button ID="btAdvanceSearch" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, search %>" OnClick="btnAdvanceSearch_click" />
                                        <asp:Button ID="btBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_click" />
                                    </div>
                                </div>
                        </div>
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
                                            <th class="title-repeater"><%=Resources.labels.PhoneNumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.customercode%></th>
                                            <th class="title-repeater"><%=Resources.labels.fullname%></th>
                                            <th class="title-repeater"><%=Resources.labels.custtype%></th>
                                            <th class="title-repeater"><%=Resources.labels.PaperNumber%></th>
                                            <th class="title-repeater"><%=Resources.labels.DocumentType%></th>
                                            <th class="title-repeater"><%=Resources.labels.DateCreate%></th>
                                            <th class="title-repeater"><%=Resources.labels.ngayduyet%></th>
                                            <th class="title-repeater"><%=Resources.labels.LastModifiedDate%></th>
                                            <th class="title-repeater"><%=Resources.labels.status%></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="tr-boder">
                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("DocumentID")%>' CommandName='<%#IPC.ACTIONPAGE.APPROVE%>' OnClientClick="Loading();"><%#Eval("Phone") %></asp:LinkButton>
                            </td>
                            <td class="tr-boder"><%#Eval("CUSTCODE") %></td>
                            <td class="tr-boder"><%#Eval("FULLNAME") %></td>
                            <td class="tr-boder"><%#Eval("ContractType") %></td>
                            <td class="tr-boder"><%#Eval("PAPERNO") %></td>
                            <td class="tr-boder"><%#Eval("DOCUMENT_TYPE") %></td>
                            <td class="tr-boder"><%#Eval("DATECREATED")%></td>
                            <td class="tr-boder"><%#Eval("DATEAPPROVED") %></td>
                            <td class="tr-boder"><%#Eval("LASTMODIFIED") %></td>
                            <td class="tr-boder"><%#Eval("STATUS") %></td>
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
