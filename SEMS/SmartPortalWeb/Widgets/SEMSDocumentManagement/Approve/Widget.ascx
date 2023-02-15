<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSDocumentManagement_Controls_Widget" %>


<asp:ScriptManager runat="server" ID="ScriptManager1">
</asp:ScriptManager>
<style type="text/css">
    @media (min-width:1080px) {
        .modal-dialog {
            width: 1080px;
            height: 1080px;
            margin: 30px auto;
            text-align: center;
        }

        .modal-content {
            -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
            box-shadow: 0 5px 15px rgba(0,0,0,.5)
        }

        .modal-sm {
            width: 300px
        }
    }

    @media (min-width:1080px) {
        .modal-lg {
            width: 1080px;
            height: 1080px;
            text-align: center;
        }
    }
</style>
<br />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
          <div class="loading">
            <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                <ProgressTemplate>
                    <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"><%=Resources.labels.image %></h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body" style="padding: 5px;">
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="view-image" style="text-align: center; width: 100%; height: auto">
                                    <asp:Image ID="myImage" runat="server" Style="width: 100%; max-width: 500px; height: auto;" Height="400" Width="500" />
                                </div>
                                <div id="caption" runat="server" style="text-align: center;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr" style="padding-right: 15px">
                        <h2>
                            <%=Resources.labels.DocumentManagementApproveOrReject%>
                        </h2>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, search %>" OnClick="btnSearch_Click" />
                    </div>
                    <div class="panel-container">
                        <div class="panel-content form-horizontal">
                            <asp:Panel ID="pnFocus" runat="server">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PhoneNumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.fullname %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.custtype %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCustomerType" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PaperType %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlPaperType" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.PaperNumber %></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPaperNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label required"><%=Resources.labels.DocumentType %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlDocumentType" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label required"><%=Resources.labels.status %></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label required"><%=Resources.labels.tenfile %></label>
                                            <div class="col-sm-8">
                                                <%--<button type="button" class="btn btn-secondary" data-toggle="modal" data-target="#myModal">File Attached</button>--%>
                                                <asp:Image ID="Image1" runat="server" class="img-responsive" data-toggle="modal" data-target="#myModal" OnLoad="Show_viewfile" Style="width: 100%; max-height:300px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                            <asp:Button ID="btApprove" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, approve %>" OnClick="btnApprove_click" />
                            <asp:Button ID="btReject" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, reject %>" OnClick="btnReject_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, cancel %>" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>




