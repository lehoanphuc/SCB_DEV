<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSSchedulePushNotification_Widget" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:HiddenField ID="hdScheduleType" runat="server"/>
    <div class="subheader">
        <h1 class="subheader-title">
            <asp:Label ID="lblHeader" runat="server"></asp:Label>
        </h1>
    </div>
    <div class="loading">
        <asp:UpdateProgress ID="UpdateProgress" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>
                <img src="Images/tenor.gif" style="width: 32px; height: 32px;" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div id="divError">
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    </div>
        <asp:Panel ID="pnSchedule" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.thongtinlich%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.notificationname %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtScheduleName" CssClass="form-control" MaxLength="200" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.loaithongbao %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlTransferType" CssClass="form-control select2 infinity" Width="100%" runat="server">
                                                    <asp:ListItem Value="NEWS">News and alert</asp:ListItem>
                                                    <asp:ListItem Value="PROMOS">Promotions</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.loailich %></label>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:RadioButtonList ID="radSchedule" CssClass="custom-control" Width="100%" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="radSchedule_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="Button7" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="return Validate();" OnClick="Button7_Click" />
                                <asp:Button ID="btnBack" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="btnBack_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="lblTemplate"  visible="false" class="th" >
    <div style="float: left">
    </div>
    <div style="text-align: right; float: right;">
        <%--<a href="TemplateDownload/UploadPhonePush.xlsx"><%= Resources.labels.downloadTemplatePush %></a>--%>
    </div>
</asp:Panel>
        <asp:Panel ID="pnCFInfoLich" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.thongtinlich%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-2 col-xs-12"><%=Resources.labels.notificationname %></div>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:Label ID="lblCFTenLich" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-2 col-xs-12"><%=Resources.labels.loaithongbao %></div>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:Label ID="lblCFLoaiThongBao" runat="server"></asp:Label>
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

        <asp:Panel ID="pnDaily" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.hangngay%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromD" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToD" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.thoidiem %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourD" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteD" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="Button8" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="return ValidateDaily();" OnClick="Button8_Click" />
                                <asp:Button ID="Button12" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button12_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnWeekly" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.hangtuan%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromW" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group"> 
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToW" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.thoidiem %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourW" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteW" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.chonthu %></label>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:CheckBoxList ID="cblThu" runat="server" CssClass="custom-control" Width="100%" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="<%$ Resources:labels, chunhat %>"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="<%$ Resources:labels, thuhai %>"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="<%$ Resources:labels, thuba %>"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="<%$ Resources:labels, thutu %>"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="<%$ Resources:labels, thunam %>"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="<%$ Resources:labels, thusau %>"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="<%$ Resources:labels, thubay %>"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="Button9" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="return ValidateWeekly();" OnClick="Button9_Click" />
                                <asp:Button ID="Button13" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button13_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnMonthly" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.hangthang%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tungay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtFromM" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.denngay %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtToM" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.thoidiem %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlhourM" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.gio %></label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlminuteM" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-3 control-label"><%=Resources.labels.phut %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.chonngay %></label>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:CheckBoxList ID="cblThuM" runat="server" CssClass="custom-control" Width="100%" RepeatDirection="Horizontal" RepeatColumns="16">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="Button10" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="return ValidateMonthly();" OnClick="Button10_Click" />
                                <asp:Button ID="Button14" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button14_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnOnetime" runat="server">
             <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.motlan%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <label class="col-sm-2 col-xs-12 control-label required"><%=Resources.labels.thoidiem %></label>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <div class="col-sm-6 col-xs-12">
                                                <asp:TextBox ID="txtDateO" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 col-xs-12">
                                                <asp:DropDownList ID="ddlHour" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-1 control-label"><%=Resources.labels.gio %></label>
                                            <div class="col-sm-2 col-xs-12">
                                                <asp:DropDownList ID="ddlMinute" CssClass="form-control select2" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="col-sm-1 control-label"><%=Resources.labels.phut %></label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-xs-12"></div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="Button11" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="return ValidateOnetime();" OnClick="Button11_Click" />
                                <asp:Button ID="Button15" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button15_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnTIB" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.pushnotificationinfo%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row" id="trPNID" visible="false" runat="server">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-labe required"><%=Resources.labels.notificationid %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtPNID" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tieude %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtTitle" CssClass="form-control" MaxLength="65" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.bodycontent %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtBody" CssClass="form-control2" TextMode="MultiLine" onkeyup="ValidateLimit(this,'240');" onkeyDown="ValidateLimit(this,'240');" onpaste="ValidateLimit(this,'240');" onChange="ValidateLimit(this,'240');" onmousedown="ValidateLimit(this,'240');" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <asp:Label ID="Label1" runat="server" ForeColor="#CC0000" Text="<%$ Resources:labels, noidungnhapkhongcokytudacbiet %>"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.noidungthongbao %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:TextBox ID="txtContent" CssClass="form-control2" TextMode="MultiLine" onkeyup="ValidateLimit(this,'240');" onkeyDown="ValidateLimit(this,'240');" onpaste="ValidateLimit(this,'240');" onChange="ValidateLimit(this,'240');" onmousedown="ValidateLimit(this,'240');" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <asp:Label ID="lblChuY" runat="server" ForeColor="#CC0000" Text="<%$ Resources:labels, noidungnhapkhongcokytudacbiet %>"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.tuychongui %></label>
                                            <div class="col-sm-8 col-xs-12">
                                                <asp:DropDownList ID="ddlSendType" CssClass="form-control select2 infinity" Width="100%" runat="server" OnSelectedIndexChanged="ddlSendType_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                    </div>
                                </div>
                                <div class="row" id="trSend" runat="server" visible="false">
                                    <div id="divUserNoti" runat="server">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.userid %></label>
                                                <div class="col-sm-8 col-xs-12">
                                                    <asp:TextBox ID="txtUser" CssClass="form-control" TextMode="MultiLine" runat="server" Visible="false" onkeyup="ValidateLimit(this,'3830');" onkeyDown="ValidateLimit(this,'3830');" onpaste="ValidateLimit(this,'3830');" onChange="ValidateLimit(this,'3830');" onmousedown="ValidateLimit(this,'3830');"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="Label11" runat="server" Text="(Seperate by ',')" ForeColor="#CC0000" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                   <div class="row" id="trSendExecl" runat="server" visible="false">
                                    <div id="div2" runat="server">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label required"><%=Resources.labels.import %></label>
                                                <div class="col-sm-6 col-xs-12">
                                                    <asp:FileUpload ID="FUUserID" AllowMultiple="false" runat="server" ClientIDMode="Static" accept=".xls,.xlsx" />
                                                </div>
                                                 <div class="col-sm-2 col-xs-12">
                                          <%--<asp:Button ID="btnUploadFile" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels,upload %>"  OnClick="btnUploadFile_Click" />--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblUpload" runat="server" Text="(Import xls,xlsx File Only)" ForeColor="#CC0000" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="btnTIBNext" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, next %>" OnClientClick="return ValidateNext();" OnClick="btnTIBNext_Click" />
                                <asp:Button ID="Button17" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button17_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnConfirm" runat="server">
        <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.chitietgiaodich%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-12 col-xs-12 control-label bold"><%=Resources.labels.thongtindatlich %></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.loailich %></label>
                                            <asp:Label ID="lbScheduleName" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12"></div>
                                </div>
                                <asp:Panel ID="pnConfirmDaily" runat="server">
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tungay %></label>
                                                <asp:Label ID="lbfromcfD" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.denngay %></label>
                                                <asp:Label ID="lbtocfD" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.thoidiem %></label>
                                                <asp:Label ID="lbcftimeD" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12"></div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnConfirmWeekly" runat="server">
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.kieulich %></label>
                                                <label class="col-sm-8 col-xs-12 control-label" runat="server"><%=Resources.labels.weekly %></label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tungay %></label>
                                                <asp:Label ID="lbfromcfW" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.denngay %></label>
                                                <asp:Label ID="lbtocfW" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.thoidiem %></label>
                                                <asp:Label ID="lbfctimeW" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-2 col-xs-12 control-label"><%=Resources.labels.thusegui %></label>
                                                <div class="col-sm-10 col-xs-12">
                                                    <asp:CheckBoxList CssClass="custom-control" ID="cblcfW" Width="100%" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="<%$ Resources:labels, chunhat %>"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="<%$ Resources:labels, thuhai %>"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="<%$ Resources:labels, thuba %>"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="<%$ Resources:labels, thutu %>"></asp:ListItem>
                                                        <asp:ListItem Value="5" Text="<%$ Resources:labels, thunam %>"></asp:ListItem>
                                                        <asp:ListItem Value="6" Text="<%$ Resources:labels, thusau %>"></asp:ListItem>
                                                        <asp:ListItem Value="7" Text="<%$ Resources:labels, thubay %>"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnConfirmMonthly" runat="server">
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.kieulich %></label>
                                                <label class="col-sm-8 col-xs-12 control-label" runat="server"><%=Resources.labels.monthly %></label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.tungay %></label>
                                                <asp:Label ID="lbfromcfM" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.denngay %></label>
                                                <asp:Label ID="lbtocfM" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.thoidiem %></label>
                                                <asp:Label ID="lbcftimeM" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-2 col-xs-12 control-label"><%=Resources.labels.chonngay %></label>
                                                <div class="col-sm-10 col-xs-12">
                                                    <asp:CheckBoxList ID="cblcfM" CssClass="custom-control" Width="100%" runat="server" RepeatDirection="Horizontal" RepeatColumns="16">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnConfirmOnetime" runat="server">
                                    <div class="row">
                                        <div class="col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label class="col-sm-4 col-xs-12 control-label"><%=Resources.labels.thoidiem %></label>
                                                <asp:Label ID="lbDateO" CssClass="col-sm-8 col-xs-12 control-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-xs-12"></div>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-12 col-xs-12 control-label bold"><%=Resources.labels.pushnotificationinfo %></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label"><%=Resources.labels.tieude %></label>
                                            <asp:Label ID="lblTitle" CssClass="col-sm-10 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label"><%=Resources.labels.bodycontent %></label>
                                            <asp:Label ID="lblBodyContent" CssClass="col-sm-10 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label"><%=Resources.labels.noidungthongbao %></label>
                                            <asp:Label ID="lblCFContent" CssClass="col-sm-10 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 col-xs-12 control-label"><%=Resources.labels.tuychongui %></label>
                                            <asp:Label ID="lblCFSendType" CssClass="col-sm-10 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="trSendOptionCF" Visible="False" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" CssClass="col-sm-2 col-xs-12 control-label" runat="server"></asp:Label>
                                            <asp:Label ID="lblSendOption" CssClass="col-sm-10 col-xs-12 control-label" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="trSendOption" runat="server">
                                    <div class="col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" CssClass="col-sm-2 col-xs-12 control-label" runat="server"></asp:Label>
                                            <div class="col-sm-10 col-xs-12">
                                                <asp:Label ID="lblUserCF" runat="server" Visible="false"  Style="word-wrap: normal; word-break: break-all;"  ></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, save %>" OnClick="Button5_Click" />
                                <asp:Button ID="Button3" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, back %>" OnClick="Button3_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnCFketqua" runat="server">
            <div class="row">
                <div class="col-sm-12 col-xs-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>
                                <%=Resources.labels.ketquadatlich%>
                            </h2>
                        </div>
                        <div class="panel-container">
                            <div class="panel-content form-horizontal p-b-0">
                                <div class="row" style="color: red; text-align: center; font-weight: bold;">
                                    <div class="form-group">
                                        <asp:Label ID="lblketquadatlich" runat="server" Text="<%$ Resources:labels, datlichthanhcong %>"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-content div-btn rounded-bottom border-faded border-left-0 border-right-0 border-bottom-0 text-muted">
                                <asp:Button ID="Button18" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, thoat %>" OnClick="Button18_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <%--<asp:PostBackTrigger ControlID="btnUploadFile" />--%>
    </Triggers>
</asp:UpdatePanel>
<script src="/JS/Common.js"></script>
<script type="text/javascript">

    function Validate() {
        if (!validateEmpty('<%=txtScheduleName.ClientID %>', '<%=Resources.labels.bannhaptencholich %>')) {
            document.getElementById('<%=txtScheduleName.ClientID %>').focus();
            return false;
        }
        return true;
    }

    function ValidateNext() {
        <%--if (!validateEmpty('<%=txtScheduleName.ClientID %>', '<%=Resources.labels.bannhaptencholich %>')) {
            document.getElementById('<%=txtScheduleName.ClientID %>').focus();
            return false;
        }--%>
        if (document.getElementById('<%=hdScheduleType.ClientID %>').value != "") {
            if (document.getElementById('<%=hdScheduleType.ClientID %>').value == 'DAILY') {
                if (validateEmpty('<%=txtFromD.ClientID %>','<%=Resources.labels.bannhapngaybatdau %>')) {
                    if (validateEmpty('<%=txtToD.ClientID %>','<%=Resources.labels.bannhapngayketthuc %>')) {
                        if (IsDateGreater('<%=txtToD.ClientID %>','<%=txtFromD.ClientID %>','<%=Resources.labels.ngayketthuclonhonngaybatdau %>')) {

                        }
                        else {
                            document.getElementById('<%=txtFromD.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtToD.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtFromD.ClientID %>').focus();
                    return false;
                }
            } else if (document.getElementById('<%=hdScheduleType.ClientID %>').value == 'WEEKLY') {
                if (validateEmpty('<%=txtFromW.ClientID %>','<%=Resources.labels.bannhapngaybatdau %>')) {
                    if (validateEmpty('<%=txtToW.ClientID %>','<%=Resources.labels.bannhapngayketthuc %>')) {
                        if (IsDateGreater('<%=txtToW.ClientID %>','<%=txtFromW.ClientID %>','<%=Resources.labels.ngayketthuclonhonngaybatdau %>')) {
                            if (ValidateCheckBoxList("<%= cblThu.ClientID %>")) {
                            }
                            else {
                                window.alert('<%=Resources.labels.banphaichonitnhatmotthutrongtuan %>');
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtFromW.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtToW.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtFromW.ClientID %>').focus();
                    return false;
                }
            } else if (document.getElementById('<%=hdScheduleType.ClientID %>').value == 'MONTHLY') {
                if (validateEmpty('<%=txtFromM.ClientID %>','<%=Resources.labels.bannhapngaybatdau %>')) {
                    if (validateEmpty('<%=txtToM.ClientID %>','<%=Resources.labels.bannhapngayketthuc %>')) {
                        if (IsDateGreater('<%=txtToM.ClientID %>','<%=txtFromM.ClientID %>','<%=Resources.labels.ngayketthuclonhonngaybatdau %>')) {
                            if (ValidateCheckBoxList("<%= cblThuM.ClientID %>")) {

                            }
                            else {
                                window.alert('<%=Resources.labels.banphaichonitnhatmotngaytrongthang %>');
                                return false;
                            }
                        }
                        else {
                            document.getElementById('<%=txtFromM.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtToM.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtFromM.ClientID %>').focus();
                    return false;
                }
            } else {
                if (validateEmpty('<%=txtDateO.ClientID %>','<%=Resources.labels.bannhapngaythuchien %>')) {

                }
                else {
                    document.getElementById('<%=txtDateO.ClientID %>').focus();
                    return false;
                }
            }
        }
        if (!validateEmpty('<%=txtTitle.ClientID %>', '<%=Resources.labels.youmustinputtitle %>')) {
            document.getElementById('<%=txtTitle.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtBody.ClientID %>', '<%=Resources.labels.youmustinputbodycontent %>')) {
            document.getElementById('<%=txtBody.ClientID %>').focus();
            return false;
        }
        if (!validateEmpty('<%=txtContent.ClientID %>', '<%=Resources.labels.youmustinputnotificationcontent %>')) {
            document.getElementById('<%=txtContent.ClientID %>').focus();
            return false;
        }
        if (document.getElementById('<%=ddlSendType.ClientID %>').value.trim() == 'USER') {
            if (document.getElementById('<%=txtUser.ClientID %>').value.trim() == "") {
                window.alert('<%=Resources.labels.youmustinputuserid %>');
                document.getElementById('<%=txtUser.ClientID %>').focus();
                return false;
            }
        }
        return true;
    }

    function ValidateDaily() {
        if (validateEmpty('<%=txtFromD.ClientID %>','<%=Resources.labels.bannhapngaybatdau %>')) {
            if (validateEmpty('<%=txtToD.ClientID %>','<%=Resources.labels.bannhapngayketthuc %>')) {
                if (DateGreater('<%=txtToD.ClientID %>','<%=txtFromD.ClientID %>', '1','<%=Resources.labels.ToDateMustBeGreaterThanFromDate1days %>')) {

                }
                else {
                    document.getElementById('<%=txtFromD.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtToD.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtFromD.ClientID %>').focus();
            return false;
        }
    }

    function ValidateWeekly() {
        if (validateEmpty('<%=txtFromW.ClientID %>','<%=Resources.labels.bannhapngaybatdau %>')) {
            if (validateEmpty('<%=txtToW.ClientID %>','<%=Resources.labels.bannhapngayketthuc %>')) {
                if (DateGreater('<%=txtToW.ClientID %>','<%=txtFromW.ClientID %>', '7','<%=Resources.labels.ToDateMustBeGreaterThanFromDate7days %>')) {
                    if (ValidateCheckBoxList("<%= cblThu.ClientID %>")) {
                    }
                    else {
                        window.alert('<%=Resources.labels.banphaichonitnhatmotthutrongtuan %>');
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtFromW.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtToW.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtFromW.ClientID %>').focus();
            return false;
        }
    }

    function ValidateCheckBoxList(obj) {
        var checkBoxList = document.getElementById(obj);
        var checkBoxes = checkBoxList.getElementsByTagName("input");
        var isvalid = false;

        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].checked) {
                isvalid = true;
                return isvalid;
            }
        }
        return isvalid;
    }

    function ValidateMonthly() {
        if (validateEmpty('<%=txtFromM.ClientID %>','<%=Resources.labels.bannhapngaybatdau %>')) {
            if (validateEmpty('<%=txtToM.ClientID %>','<%=Resources.labels.bannhapngayketthuc %>')) {
                if (DateGreater('<%=txtToM.ClientID %>','<%=txtFromM.ClientID %>', '30','<%=Resources.labels.ToDateMustBeGreaterThanFromDate30days %>')) {
                    if (ValidateCheckBoxList("<%= cblThuM.ClientID %>")) {

                    }
                    else {
                        window.alert('<%=Resources.labels.banphaichonitnhatmotngaytrongthang %>');
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtFromM.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtToM.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtFromM.ClientID %>').focus();
            return false;
        }
    }
    function ValidateOnetime() {
        if (validateEmpty('<%=txtDateO.ClientID %>','<%=Resources.labels.bannhapngaythuchien %>')) {

        }
        else {
            document.getElementById('<%=txtDateO.ClientID %>').focus();
            return false;
        }
    }

</script>