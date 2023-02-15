<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSREGIONFEE_Controls_Widget" %>
<%@ Register Src="~/Controls/SearchTextBox/LoadBranch.ascx" TagPrefix="uc1" TagName="LoadBranch" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="subheader">
            <h1 class="subheader-title">
                <asp:Label ID="lblTitleBranch" runat="server"></asp:Label>
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
            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>
                            <%=Resources.labels.accountdefinitioninformation%>
                        </h2>
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal p-b-0">
                            <asp:Panel ID="pnRegion" runat="server">
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required" ><%=Resources.labels.accountnumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox type="Text" ID="txtAccNumber"  CssClass="form-control" runat="server"  MaxLength="50" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                       <div class="form-group">
                                            <label class="col-sm-4 control-label required" ><%=Resources.labels.Baseaccountnumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox type="Text" ID="txtBacno"  CssClass="form-control" runat="server"  MaxLength="50" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">  
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.accountname%></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAccName" MaxLength="500"  CssClass="form-control2" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                         <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.shortaccountname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtShortName"  MaxLength="500" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--  --%>
                                 <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                       <div class="form-group">
                                             <label class="col-sm-4 control-label required"><%=Resources.labels.branchcode %></label>
                                            <div class="col-sm-8">
                                                <uc1:LoadBranch ID="txtBranch" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                          <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.currencyid %></label>
                                            <div class="col-sm-8">
                                               <asp:DropDownList ID="ddlCCYID" CssClass="form-control select2 infinity" runat="server">
                                                        </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                       <div class="form-group">
                                             <label class="col-sm-4 control-label"><%=Resources.labels.accountclassifinication %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlAccClS" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                         <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.balanceSide %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlBalanceSide" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                       <div class="form-group">
                                          <label class="col-sm-4 control-label"><%=Resources.labels.postingside %></label>
                                            <div class="col-sm-8">
                                                 <asp:DropDownList ID="ddlPostingSide" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                       <div class="form-group">
                                             <label class="col-sm-4 control-label"><%=Resources.labels.accountgroup %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlAccgroup" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                         <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8">
                                                 <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                         <div class="form-group">
                                           <label class="col-sm-4 control-label"><%=Resources.labels.AccountLevel %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlAccountLevel" CssClass="form-control select2" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAccountLevel_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2%">
                                    <div class="col-sm-5">
                                         <div class="form-group">
                                            <label class="col-sm-4 control-label"><%=Resources.labels.ParentAccount %></label>
                                            <div class="col-sm-8">
                                                 <asp:DropDownList ID="ddlParentID" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-5">
                                         <div class="form-group" style="display:none">
                                             <label class="col-sm-4 control-label"><%=Resources.labels.englishName %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtEnglishName" MaxLength="250" CssClass="form-control " runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                              
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btsave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return validate()" OnClick="btsave_Click" />
                            <asp:Button ID="btClear" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, Clear %>" OnClick="btClear_Click" />
                            <asp:Button ID="btback" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btback_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">

    function validateEmpty(id, msg) {
        if (document.getElementById(id).value == "0" || document.getElementById(id).value == "") {
            window.alert(msg);
            return false;
        }
        else {
            return true;
        }
    }
</script>
