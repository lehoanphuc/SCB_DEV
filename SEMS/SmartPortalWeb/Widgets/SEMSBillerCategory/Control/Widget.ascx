<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSBillerCategory_Control_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
<asp:Panel ID="UpdatePanel1" runat="server">
    <div class="subheader">
        <h1 class="subheader-title">
            <asp:Label runat="server" ID="lbTitle" />
        </h1>
    </div>
    <div id="divError">
        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-container">
                    <div class="panel-content form-horizontal p-b-0">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="row">
                                <div class="col-sm-6 col-xs-12 ">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12  control-label required"><%= Resources.labels.catid %></label>
                                        <div class="col-sm-8 col-xs-12 ">
                                            <asp:TextBox ID="txtCatID" CssClass="form-control" MaxLength="20" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12 ">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12  control-label required"><%= Resources.labels.catName %></label>
                                        <div class="col-sm-8 col-xs-12 ">
                                            <asp:TextBox ID="txtCatName" CssClass="form-control" MaxLength="150" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-xs-12 ">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12  control-label"><%=Resources.labels.localcatname %></label>
                                        <div class="col-sm-8 col-xs-12 ">
                                            <asp:TextBox ID="txtCatNameLocal" CssClass="form-control" MaxLength="200" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12 ">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12  control-label required"><%=Resources.labels.catShortName %></label>
                                        <div class="col-sm-8 col-xs-12 ">
                                            <asp:TextBox ID="txtCatShortName" CssClass="form-control"
                                                MaxLength="100" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div> 
                            <div class="row">   
                                <div class="col-sm-6 col-xs-12 ">
                                    <div class="form-group">
                                        <label class="col-sm-4 col-xs-12  control-label required"><%=Resources.labels.status %></label>
                                        <div class="col-sm-8 col-xs-12 ">
                                            <asp:DropDownList Width="100%" ID="ddlStatus" CssClass="form-control select2 infinity" runat="server">
                                                <asp:ListItem Value="A" Text="<%$ Resources:labels, active %>" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="I" Text="<%$ Resources:labels, inactive %>"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-12 ">
                                    <div class="form-group">
                                        <asp:Label runat="server" class="col-sm-4 col-xs-12 control-label"
                                            ID="lbPath" Text="<%$ Resources:labels, linkURL %>">   
                                        </asp:Label>
                                        <div class="col-sm-8 col-xs-12 ">
                                            <asp:TextBox runat="server" ID="txtLinkImage" CssClass="form-control"  />
                                        </div>
                                    </div>
                                </div> 
                            </div>
                            <div class="row">   
                                <div class="col-sm-6 col-xs-12 ">
                                    <div class="form-group">
                                        <asp:Label runat="server" class="col-sm-4 col-xs-12 control-label"
                                                   ID="lbImg" Text="<%$ Resources:labels, txtImg %>" Visible="false">   
                                        </asp:Label>
                                        <div class="col-sm-8 col-xs-12">
                                            <div class="form-group">
                                                <asp:Image ID="Image1" CssClass="img-rounded" Visible="false" runat="server" Height="130" Width="160" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClientClick="return checkValidation();"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" CssClass="btn btn-secondary" runat="server"
                            Text="<%$ Resources:labels, Clear %>" OnClick="btnClear_Click" />
                        <asp:Button ID="btnbackIndex" CssClass="btn btn-secondary" runat="server"
                            Text="<%$Resources:labels, quaylai %>" OnClick="btnbackIndex_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">  
    function checkValidation() {
        var re = /\s/ig;
        if (re.test(document.getElementById('<%= txtCatID.ClientID %>').value.trim())) {
            window.alert('<%= Resources.labels.checkWhiteSpace_catID %>');
            return false;
        }

        if (!validateEmpty('<%= txtCatID.ClientID %>', '<%= Resources.labels.validCatID %>')) {
            document.getElementById('<%= txtCatID.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtCatName.ClientID %>', '<%= Resources.labels.validCatName %>')) {
            document.getElementById('<%= txtCatName.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%= txtCatShortName.ClientID %>', '<%= Resources.labels.validCatShortName %>')) {
            document.getElementById('<%= txtCatShortName.ClientID %>').focus();
            return false;
        }
        <%--if (document.getElementById('<%= cbxURI.ClientID %>').checked == true) {
            if (!validateEmpty('<%= txtLinkImage.ClientID %>','<%= Resources.labels.txtLinkImagerequired %>')) {
                document.getElementById('<%= txtLinkImage.ClientID %>').focus();
                return false;
            }
        }--%>
        return true;
    } 

  <%--  var maxFileSize = '<%= System.Configuration.ConfigurationManager.AppSettings["imageuploadec"].ToString() %>';
    var validFiles = ["png", "jpg"];
    function CheckExt(obj) {
        var source = obj.value;
        var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
        for (var i = 0; i < validFiles.length; i++) {
            if (validFiles[i] == ext)
                break;
        }
        if (i >= validFiles.length) {
            obj.value = "";
            alert("That's not a valid image, please load an image with an extention of one of the following:\n\n" + validFiles.join(", "));
        }
        console.log(maxFileSize)
        console.log(obj.files[0].size)
        if (obj.files[0].size > maxFileSize) {
            obj.value = "";
            alert('File too big! Please choose file below 4MB');
        }
    }--%>
</script>
