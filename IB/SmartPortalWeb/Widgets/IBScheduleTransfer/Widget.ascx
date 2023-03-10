<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBScheduleTransfer_Widget" %>



<style>
    .al {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
    }

    input[type=radio] {
        margin-right: 5px;
    }

    table.radio-button tr td {
        padding: 5px;
    }

    .lightblue {
        background: lightblue;
    }
</style>
<script src="JS/mask.js" type="text/javascript"></script>
<script src="JS/docso.js" type="text/javascript"></script>
<link href="CSS/bootstrap-timepicker.min.css" rel="stylesheet" />
<script src="JS/bootstrap-timepicker.min.js"></script>
<script src="JS/jquery.inputmask.js"></script>
<script src="JS/jquery.inputmask.date.extensions.js"></script>
<script src="JS/jquery.inputmask.extensions.js"></script>
<script src="Widgets/IBScheduleTransfer/JS/common.js"></script>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<div class="al">
    <span><%=Resources.labels.datlichchuyenkhoan %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divError">
            <asp:Label runat="server" ID="lblTextError" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <!--First-->
        <asp:Panel ID="pnSchedule" runat="server">
            <figure>
                <%--<legend class="handle"><%=Resources.labels.thongtindatlich %>
        </legend>--%>
                <div class="divcontent display-label">
                    <div class="item1">
                        <div class="handle">
                            <label><%= Resources.labels.thongtinnguoitratien %></label>
                        </div>

                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-4 col-xs-12 right bold"><%= Resources.labels.tenlichgioihan50kytu %></label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtScheduleName" autocomplete="off" CssClass="form-control" onkeypress="return this.value.length<=50" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-4 col-xs-12 right bold"><%= Resources.labels.loaichuyenkhoan %> *</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlTransferType" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-4 col-xs-12 right bold"><%= Resources.labels.kieulich %> *</label>
                                <div class="col-sm-8 radio">
                                    <asp:RadioButtonList ID="radSchedule" RepeatLayout="Flow" runat="server" CssClass="radio-button" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="O" Text="<%$ Resources:labels, motlan %>"></asp:ListItem>
                                        <asp:ListItem Value="D" Text="<%$ Resources:labels, hangngay %>"></asp:ListItem>
                                        <asp:ListItem Value="W" Text="<%$ Resources:labels, hangtuan %>"></asp:ListItem>
                                        <asp:ListItem Value="M" Text="<%$ Resources:labels, hangthang %>"></asp:ListItem>

                                    </asp:RadioButtonList>
                                </div>
                            </div>

                        </div>
                        <div class="button-group">
                            <asp:Button ID="Button7" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, tieptuc %>'
                                OnClick="Button7_Click" OnClientClick="return Validate10();" />

                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </figure>

        </asp:Panel>
        <!--end-->
        <!--Daily-->
        <asp:Panel ID="pnDaily" runat="server">
            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietlich %> : <span class="bold"><%= Resources.labels.hangngay %></span></legend>--%>
                <div class="divcontent display-label">
                    <div class="handle">
                        <label><%=Resources.labels.chitietlich %> : <span class=""><%= Resources.labels.hangngay %></span></label>
                    </div>

                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right"><%= Resources.labels.thoidiem %></label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtScheduleTimeDaily" autocomplete="off" CssClass="timepicker form-control " Width="100%" runat="server" data-inputmask='"mask": "99:99:99"' data-mask></asp:TextBox>
                               
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right"><%= Resources.labels.ngayhieuluc %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtFromD" autocomplete="off" runat="server" CssClass="dateselect form-control" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right">Expired date*</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtToD" autocomplete="off" runat="server" CssClass="dateselect form-control" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                            </div>
                        </div>


                        <div class="clearfix"></div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="Button12" runat="server" OnClick="Button12_Click" CssClass="btn  btn-warning"
                            Text='<%$ Resources:labels, quaylai %>' />
                        <asp:Button ID="Button8" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, tieptuc %>' OnClientClick="return ValidateDaily();"
                            OnClick="Button8_Click" />
                        <div class="clearfix"></div>
                    </div>
                </div>


            </figure>
        </asp:Panel>
        <!--end-->
        <!--Weekly-->
        <asp:Panel ID="pnWeekly" runat="server">
            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietlich %> : <span class="bold"><%= Resources.labels.hangtuan %></span></legend>--%>
                <div class="divcontent display-label">
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.chitietlich %> : <span class=""><%= Resources.labels.hangtuan %></span></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.timeschedule %></label>
                                <div class="col-sm-4 col-xs-7">
                                    <asp:TextBox ID="txtScheduleTimeWeekly" autocomplete="off" CssClass="form-control timepicker" runat="server" data-inputmask='"mask": "99:99:99"' data-mask></asp:TextBox>

                                  
                                </div>
                              </div>
                            <div class="row form-group">
                                <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %> *</label>
                                <div class="col-sm-4 col-xs-7">
                                    <asp:TextBox ID="txtFromW" runat="server" autocomplete="off" CssClass="dateselect form-control" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %> *</label>
                                <div class="col-sm-4 col-xs-7">
                                    <asp:TextBox ID="txtToW" runat="server" autocomplete="off" CssClass="dateselect form-control" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngaysegui %> *</label>
                                <div class="col-sm-4 col-xs-7">

                                    <asp:DropDownList ID="ddlWeekyDayNo" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="2" Text="<%$ Resources:labels, thuhai %>"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="<%$ Resources:labels, thuba %>"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="<%$ Resources:labels, thutu %>"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="<%$ Resources:labels, thunam %>"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="<%$ Resources:labels, thusau %>"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="<%$ Resources:labels, thubay %>"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="<%$ Resources:labels, chunhat %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                </div>
                </div>
                    <div class="button-group">
                        <asp:Button ID="Button13" runat="server" OnClick="Button13_Click" CssClass="btn btn-warning"
                            Text="<%$ Resources:labels, quaylai %>" />
                        <asp:Button ID="Button9" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, tieptuc %>" OnClientClick="return ValidateWeekly();"
                            OnClick="Button9_Click" />
                        <div class="clearfix"></div>
                    </div>
                </div>


            </figure>

        </asp:Panel>
        <!--end-->
        <!--Monthly-->
        <asp:Panel ID="pnMonthly" runat="server">

            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietlich %> : <span class="bold"><%= Resources.labels.hangthang %></span></legend>--%>
                <div class="divcontent display-label">
                    <div class="handle">
                        <label><%=Resources.labels.chitietlich %> : <span class=""><%= Resources.labels.hangthang %></span></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.thoidiem %></label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtScheduleTimeMonthly" autocomplete="off" CssClass="form-control timepicker" runat="server" data-inputmask='"mask": "99:99:99"' data-mask></asp:TextBox>
                               
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtFromM" runat="server" autocomplete="off" CssClass="dateselect" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtToM" runat="server" autocomplete="off" CssClass="dateselect" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngaysegui %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:DropDownList ID="ddlMonthlyDayNo" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%-- <tr>
                    <td class="tibtd">
                        <asp:Label ID="Label26" runat="server" Text="<%$ Resources:labels, thoidiem %>" Visible="False"></asp:Label>
                        <asp:Label ID="Label31" runat="server" Text="*" Visible="False"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlhourM" runat="server" Visible="False" Width="40px">
                        </asp:DropDownList>
                        <asp:Label ID="Label69" runat="server" Text="<%$ Resources:labels, gio %>" Visible="False"></asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="ddlminuteM" runat="server" Visible="False" Width="40px">
                        </asp:DropDownList>
                        <asp:Label ID="Label70" runat="server" Text="<%$ Resources:labels, phut %>" Visible="False"></asp:Label>
                    </td>
                </tr>--%>

                        <div class="clearfix"></div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="Button14" runat="server" OnClick="Button14_Click" CssClass="btn btn-warning"
                            Text='<%$ Resources:labels, quaylai %>' />
                        <asp:Button ID="bt_Review" runat="server" CssClass="btn btn-success" Text='<%$ Resources:labels, xemchitiet %>' Visible="false" />
                        <asp:Button ID="Button10" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, tieptuc %>' OnClientClick="return ValidateMonthly();"
                            OnClick="Button10_Click" />
                        <div class="clearfix"></div>
                    </div>

                </div>

                <asp:Panel ID="pn_reviewMonth" runat="server" Visible="false">
                    <div style="text-align: center"><%=Resources.labels.xemchitietngaythuchien %> </div>
                    <div>
                        <asp:Literal ID="ltr_reviewMonth" runat="server"></asp:Literal>
                    </div>
                </asp:Panel>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--Onetime-->
        <asp:Panel ID="pnOnetime" runat="server">

            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietlich %>  : <span class="bold"><%= Resources.labels.motlan %></span></legend>--%>
                <div class="divcontent display-label">
                    <div class="handle">
                        <label><%=Resources.labels.chitietlich %> : <span class=""><%= Resources.labels.motlan %></span></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.timeschedule %></label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtScheduleTimeOnetime" CssClass="form-control timepicker" autocomplete="off" runat="server" data-inputmask='"mask": "99:99:99"' data-mask></asp:TextBox>

                               
                            </div>

                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngaythuchien %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtDateO" autocomplete="off" CssClass="dateselect" runat="server" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="Button15" runat="server" CssClass="btn btn-warning" OnClick="Button15_Click"
                            Text='<%$ Resources:labels, quaylai %>' />
                        <asp:Button ID="Button11" runat="server" CssClass="btn btn-primary" Text='<%$ Resources:labels, tieptuc %>' OnClientClick="return ValidateOnetime();"
                            OnClick="Button11_Click" />
                        <div class="clearfix"></div>
                    </div>
                </div>


            </figure>
        </asp:Panel>
        <!--end-->
        <!--Transfer In Bank-->
        <asp:Panel ID="pnTIB" runat="server">
            <figure>
                <%-- <legend class="handle"><%=Resources.labels.chitietgiaodich %> </legend>--%>
                <div class="divcontent display-label">
                    <div class="handle">
                        <label><%=Resources.labels.thongtinnguoitratien %></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.debitaccount %></label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:DropDownList ID="ddlSenderAccount" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSenderAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="handle">
                        <label><%=Resources.labels.thongtinnguoinhantien %></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.thongtinnguoinhantien %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:DropDownList ID="ddlNguoiThuHuongTIB" runat="server"
                                    OnSelectedIndexChanged="ddlNguoiThuHuongTIB_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text='<%$ Resources:labels, khac %>' Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.taikhoanbaoco %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtRecieverAccount" runat="server" type="number"></asp:TextBox>
                                &nbsp;<asp:Label ID="lblreceiverCurrency" Visible="false" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="handle">
                        <label><%=Resources.labels.noidungchuyenkhoan %></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.sotien %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" MaxLength="15" CssClass="amount"></asp:TextBox>
                                <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                <asp:Label ID="lblText" runat="server" Font-Size="7pt" Font-Bold="True"
                                    Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.mieutagioihan100kytu %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtDesc" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);" runat="server" Height="50px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="button-group" style="text-align: center;">
                        <asp:Button ID="Button17" runat="server" CssClass="btn btn-warning" OnClick="Button17_Click"
                            Text='<%$ Resources:labels, quaylai %>' />
                        <asp:Button ID="btnTIBNext" runat="server" CssClass="btn btn-primary" OnClientClick="return validate();" Text='<%$ Resources:labels, tieptuc %>'
                            OnClick="btnTIBNext_Click" />
                        <div class="clearfix"></div>

                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--Transfer BAC-->
        <asp:Panel ID="pnBAC" runat="server">
            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %> </legend>--%>
                <div class="divcontent display-label">
                    <div class="handle">
                        <label><%=Resources.labels.thongtinnguoitratien %></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.debitaccount %></label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:DropDownList ID="ddlSenderAcc" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSenderAcc_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="handle">
                        <label><%=Resources.labels.thongtinnguoinhantien %></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.taikhoanbaoco %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:DropDownList ID="ddlNguoiThuHuong" runat="server">
                                </asp:DropDownList>
                                &nbsp;<asp:Label ID="lblreceiverCCYIDBAC" Visible="false" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="handle">
                        <label><%=Resources.labels.noidungchuyenkhoan %></label>
                    </div>
                    <div class="content_table">
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.sotien %> *</label>
                            <div class="col-sm-4 col-xs-7 ">
                                <asp:TextBox ID="txtAmount1" runat="server" MaxLength="15" CssClass="amount"></asp:TextBox>
                                <asp:Label ID="lblCCYIDBAC" runat="server"></asp:Label>
                                <asp:Label ID="lblText1" runat="server" Font-Size="7pt" Font-Bold="True"
                                    Font-Italic="True" ForeColor="#0066FF" Width="200px"></asp:Label>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.mieutagioihan100kytu %> *</label>
                            <div class="col-sm-4 col-xs-7">
                                <asp:TextBox ID="txtDesc1" onkeyup="ValidateLimit(this,200);" onkeyDown="ValidateLimit(this,200);" onpaste="ValidateLimit(this,200);" runat="server" Height="80px" TextMode="MultiLine" Width="300px"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="Button18" runat="server" CssClass="btn btn-warning" OnClick="Button17_Click"
                            Text='<%$ Resources:labels, quaylai %>' />
                        <asp:Button ID="Button16" runat="server" CssClass="btn btn-primary" OnClientClick="return validate1();" Text='<%$ Resources:labels, tieptuc %>'
                            OnClick="btnTIBNext_Click" />
                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>

        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <!--end-->
        <!--Transfer TOB-->
        <asp:Panel ID="pnTOB" runat="server">
            <figure>
                <div class="divcontent display-label">
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.thongtindatlich %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.debitaccount %></label>&nbsp;*
                    <div class="col-sm-4 col-xs-7">
                        <asp:DropDownList ID="ddlSenderAccountTOB" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSenderAccountTOB_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;<asp:HiddenField ID="txtChu" runat="server" />
                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.hotennguoitratien  %></label>&nbsp;*
                    <div class="col-sm-4 col-xs-7">
                        <asp:TextBox ID="txtSenderName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <asp:Label ID="Label1" runat="server" Text='<%$ Resources:labels, thongtinnguoinhantien   %>'></asp:Label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.nguoithuhuong  %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:DropDownList ID="ddlNguoiThuHuongTOB" runat="server" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlNguoiThuHuongTOB_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.hinhthucnhan   %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:RadioButton ID="radCMND" runat="server" Text="<%$ Resources:labels, quasocmnd %>"
                                            GroupName="AccNoTo" /><%--onclick="enableCMND();"--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <div class="content_table">
                                        <div class="row form-group">
                                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.socmnd    %></label>
                                            <div class="col-sm-4 col-xs-7">
                                                <asp:TextBox ID="txtCMND" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.ngaycap     %></label>
                                            <div class="col-sm-4 col-xs-7">
                                                <asp:TextBox ID="txtissuedate" autocomplete="off" runat="server" CssClass="dateselect form-control" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.noicap     %></label>
                                            <div class="col-sm-4 col-xs-7">
                                                <asp:TextBox ID="txtissueplace" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-sm-4 col-xs-5 right bold"></label>
                                            <div class="col-sm-4 col-xs-7">
                                                <asp:RadioButton ID="radAcctNo" runat="server" Checked="true"
                                                    GroupName="AccNoTo" Text="<%$ Resources:labels, quasotaikhoan %>" />
                                            </div>
                                        </div>
                                        <div class="row form-group">
                                            <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.taikhoanbaoco     %></label>
                                            <div class="col-sm-4 col-xs-7">
                                                <asp:TextBox ID="txtReceiverAccount" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.hotennguoinhantien    %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:TextBox ID="txtReceiverName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.diachinguoinhantien     %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:TextBox ID="txtReceiverAdd" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.tinhthanh      %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.nganhang       %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:DropDownList ID="ddlBankRecieve" runat="server" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlBankRecieve_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlNation" runat="server" Visible="False">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.chinhanhphonggiaodich %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:DropDownList ID="ddlChildBank" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="txtBranchDesc" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.noidungchuyenkhoan %></label>
                                    <div class="col-sm-4 col-xs-7">
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.sotien %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:TextBox ID="txtAmount2" runat="server" MaxLength="15" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        &nbsp;<asp:Label ID="lblcurrentcyTOB" runat="server"></asp:Label>
                                        &nbsp;<asp:Label ID="lblText2" runat="server" Font-Bold="True" Font-Italic="True"
                                            Font-Size="7pt" ForeColor="#0066FF" Width="200px"></asp:Label>
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-4 col-xs-5 right bold"><%= Resources.labels.mieutagioihan100kytu  %></label>
                                    <div class="col-sm-4 col-xs-7">
                                        <asp:TextBox ID="txtDesc2" onkeypress="return this.value.length<=100" runat="server" autocomplete="off" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="btnNextTOB" runat="server" CssClass="btn btn-primary"
                            OnClientClick="return validateTOB();" Text='<%$ Resources:labels, tieptuc %>' OnClick="btnNextTOB_Click" />
                        &nbsp;
                      <asp:Button ID="Button19" CssClass="btn btn-warning" runat="server" OnClick="Button17_Click"
                          Text='<%$ Resources:labels, quaylai %>' />

                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>
        </asp:Panel>
        <!--end-->
        <!--confirm-->
        <asp:Panel ID="pnConfirm" runat="server">
            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %> </legend>--%>
                <div class="divcontent display-label">
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.thongtindatlich %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.tenlich %></label>

                                <asp:Label ID="lblConfirmScheduleName" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>

                            </div>
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.loaichuyenkhoan %></label>

                                <asp:Label ID="lblConfirmTransferType" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>

                            </div>
                            <asp:Panel runat="server" ID="pnConfirmDaily">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.hangngay %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %></label>
                                    <asp:Label ID="lbfromcfD" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %></label>
                                    <asp:Label ID="lbtocfD" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.thoidiem %></label>
                                    <asp:Label ID="lbcftimeD" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatdaily" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnConfirmWeekly">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.hangtuan %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %></label>
                                    <asp:Label ID="lbfromcfW" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %></label>
                                    <asp:Label ID="lbtocfW" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.thoidiem %></label>
                                    <asp:Label ID="lbfctimeW" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayxuly %></label>
                                    <asp:Label ID="lblConfirmWeekyDayNo" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>

                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatweekly" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnConfirmMonthly">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.hangthang %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %></label>
                                    <asp:Label ID="lbfromcfM" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %></label>
                                    <asp:Label ID="lbtocfM" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.thoidiem %></label>
                                    <asp:Label ID="lbcftimeM" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayxuly %></label>
                                    <asp:Label ID="lblConfirmMonthDayNo" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>

                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatmonthly" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div>
                                    <div class="header-title">
                                        <label class="bold"><%= Resources.labels.chitietthuchien %></label>
                                    </div>

                                    <asp:Table ID="tbdetailsmonth" border="1" class="table style1 footable" CellSpacing="0" CellPadding="5" runat="server">

                                        <asp:TableRow>

                                            <asp:TableHeaderCell>Year</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Content</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Jan</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Feb</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">March</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">April</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">May</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">June</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">July</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">August</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Sept</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Oct</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Nov</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Dec</asp:TableHeaderCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnConfirmOnetime">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.motlan %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngaythuchien %></label>
                                    <asp:Label ID="lbDateO" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatonetime" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.thongtinnguoitratien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.hotennguoitratien %></label>
                                <asp:Label ID="lbsender" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.debitaccount %></label>
                                <asp:Label ID="lbaccsent" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.thongtinnguoinhantien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.hotennguoinhantien %></label>
                                <asp:Label ID="lbreceiver" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                            </div>
                            <asp:Panel runat="server" ID="pnTaiKhoanBaoCo">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.taikhoanbaoco %></label>
                                    <asp:Label ID="lbaccreceive" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnConfirmCMND" runat="server">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.socmnd %></label>
                                    <asp:Label ID="lblLicense" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngaycap %></label>
                                    <asp:Label ID="lblIssueDate" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.noicap %></label>
                                    <asp:Label ID="lblIssuePlace" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnBank" runat="server">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.diachinguoinhantien %></label>
                                    <asp:Label ID="lblConfirmReceiverAdd" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.chinhanhphonggiaodich %></label>
                                    <asp:Label ID="lblConfirmChildBank" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.noidungchuyenkhoan %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.sotien %></label>
                                <div class="col-sm-6 col-xs-7">
                                    <asp:Label ID="lbamount" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lbccid" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </div>
                            </div>
                            <asp:Panel ID="pnConFirmFee" runat="server" CssClass="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.sotienphi %></label>
                                <div class="col-sm-6 col-xs-7">
                                    <asp:Label ID="lblPhiAmount" runat="server" Text="0"></asp:Label>
                                    &nbsp;<asp:Label ID="lblFeeCCYID" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="row form-group ">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.nguoitraphi %></label>
                                <label class="col-sm-6 col-xs-7"><%= Resources.labels.nguoichuyen %></label>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.noidungthanhtoan %></label>
                                <asp:Label ID="lbdesc" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="button-group">
                        <asp:Button ID="Button3" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="Button3_Click" />

                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, tieptuc %>" OnClick="Button2_Click" />

                        <div class="clearfix"></div>
                    </div>
                </div>
            </figure>

        </asp:Panel>
        <!--end-->
        <!--token-->
        <asp:Panel ID="pnOTP" runat="server">
            <div class="handle">
                <label class="bold"><%=Resources.labels.xacthucgiaodich %></label>
            </div>
            <div class="content display-label">
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <label class="bold"><%=Resources.labels.loaixacthuc %></label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:DropDownList ID="ddlLoaiXacThuc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoaiXacThuc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-5 col-sm-4 left">
                        <asp:Panel ID="pnSendOTP" runat="server">
                            <asp:Button ID="btnSendOTP" runat="server" CssClass="btn btn-primary btnSendOTP" OnClick="btnSendOTP_Click" Text="<%$ Resources:labels, resend %>" />
                            <div class="countdown hidden">
                                <span><%=Resources.labels.OTPcodeexpiresin %></span>&nbsp;<span class="countdown_time"></span>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-4 right">
                        <label class="bold"><%=Resources.labels.maxacthuc %></label>
                    </div>
                    <div class="col-xs-7 col-sm-4 line30">
                        <asp:TextBox ID="txtOTP" runat="server" Width="100%" AutoCompleteType="None"></asp:TextBox>
                    </div>
                </div>
                <div class="row" style="text-align:center; color:#7A58BF">
                    <asp:Label ID="lbnotice" runat="server" class="bold" Visible="false">If you do not see the email in your inbox, please check your “junk mail” folder or “spam” folder.</asp:Label>
                </div>
                <div class="button-group">
                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-warning" Text="<%$ Resources:labels, quaylai %>" OnClick="Button4_Click" />
                    <asp:Button ID="Button5" CssClass="btn btn-primary" runat="server" OnClick="Button5_Click" Text='<%$ Resources:labels, next %>' />
                </div>
            </div>
        </asp:Panel>
        <!--end-->
        <!--sao ke-->
        <asp:Panel ID="pnResultTransaction" runat="server">
            <figure>
                <%--<legend class="handle"><%=Resources.labels.chitietgiaodich %> </legend>--%>
                <div class="divcontent display-label">
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.thongtindatlich %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.tenlich %></label>
                                <asp:Label ID="lblEndSchedule" runat="server" CssClass="col-sm-6 col-xs-7" />
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.loaichuyenkhoan %></label>
                                <asp:Label ID="lblEndTransferType" runat="server" CssClass="col-sm-6 col-xs-7" />
                            </div>

                            <asp:Panel ID="pnResultDaily" runat="server">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.hangngay %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %></label>
                                    <asp:Label ID="lbrsfromD" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %></label>
                                    <asp:Label ID="lbrstoD" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.thoidiem %></label>
                                    <asp:Label ID="lbrstimeD" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatdaily2" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnResultWeekly">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.hangtuan %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %></label>
                                    <asp:Label ID="lbrsfromW" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %></label>
                                    <asp:Label ID="lbrstoW" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.thoidiem %></label>
                                    <asp:Label ID="lbrstimeW" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayxuly %></label>
                                    <asp:Label ID="lblEndWeekyDayNo" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>

                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatweekly2" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnResultMonthly">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.hangthang %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhieuluc %></label>
                                    <asp:Label ID="lbrsfromM" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayhethieuluc %></label>
                                    <asp:Label ID="lbrstoM" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.thoidiem %></label>
                                    <asp:Label ID="lbrstimeM" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngayxuly %></label>
                                    <asp:Label ID="lblEndMonthlyDayNo" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>

                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatmonthly2" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div>
                                    <div class="header-title">
                                        <label class="bold"><%= Resources.labels.chitietthuchien %></label>
                                    </div>
                                    <asp:Table ID="tbdetailsmonth2" border="1" class="table style1 footable" CellSpacing="0" CellPadding="5" runat="server">

                                        <asp:TableRow>

                                            <asp:TableHeaderCell>Year</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Content</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Jan</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Feb</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">March</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">April</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">May</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">June</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">July</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">August</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Sept</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Oct</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Nov</asp:TableHeaderCell>
                                            <asp:TableHeaderCell data-breakpoints="xs">Dec</asp:TableHeaderCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnResultOnetime">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.kieulich %></label>
                                    <label class="col-sm-6 col-xs-7"><%= Resources.labels.motlan %></label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngaythuchien %></label>
                                    <asp:Label ID="lbrsDateO" runat="server" CssClass="col-sm-6 col-xs-7" Text="08/03/2010"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.solanlap %></label>
                                    <asp:Label ID="lbrepeatonetime2" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.thongtinnguoitratien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.hotennguoitratien %></label>
                                <asp:Label ID="lbrssender" runat="server" CssClass="col-sm-6 col-xs-7" />
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.debitaccount %></label>
                                <asp:Label ID="lbrssenderacc" runat="server" CssClass="col-sm-6 col-xs-7" />
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.thongtinnguoinhantien %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.hotennguoinhantien %></label>
                                <asp:Label ID="lbrsreceiver" runat="server" CssClass="col-sm-6 col-xs-7" />
                            </div>
                            <%--               <div class="row form-group">
                        <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.debitaccount %></label>
                        <asp:Label ID="Label2" runat="server" CssClass="col-sm-6 col-xs-7" />
                    </div>--%>
                            <asp:Panel ID="pnTaiKhoanBaoCoRS" runat="server" CssClass="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.taikhoanbaoco %></label>
                                <asp:Label ID="lbrsreceiveracc" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>

                            </asp:Panel>
                            <asp:Panel ID="pnConfirmCMNDRS" runat="server">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.socmnd %></label>
                                    <asp:Label ID="lblLicenseRS" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.ngaycap %></label>
                                    <asp:Label ID="lblIssueDateRS" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.noicap %></label>
                                    <asp:Label ID="lblIssuePlaceRS" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnBankRS" runat="server">
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.diachinguoinhantien %></label>
                                    <asp:Label ID="lblConfirmReceiverAddRS" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                                <div class="row form-group">
                                    <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.chinhanhphonggiaodich %></label>
                                    <asp:Label ID="lblConfirmChildBankRS" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="item">
                        <div class="handle">
                            <label><%=Resources.labels.noidungchuyenkhoan %></label>
                        </div>
                        <div class="content_table">
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.sotien %></label>
                                <div class="col-sm-6 col-xs-7">
                                    <asp:Label ID="lbrsamount" runat="server" />
                                    &nbsp;<asp:Label ID="lbrsccyid" runat="server" Text="<%$ Resources:labels, lak %>"></asp:Label>
                                </div>
                            </div>
                            <asp:Panel ID="pnConFirmFeeRS" runat="server" CssClass="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.sotienphi %></label>
                                <div class="col-sm-6 col-xs-7">
                                    <asp:Label ID="lblPhiAmountRS" runat="server" Text="0"></asp:Label>
                                    &nbsp;<asp:Label ID="lblFeeCCYIDRS" runat="server"></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.nguoitraphi %></label>
                                <label class="col-sm-6 col-xs-7"><%= Resources.labels.nguoichuyen %></label>
                            </div>
                            <div class="row form-group">
                                <label class="col-sm-6 col-xs-5 right bold"><%= Resources.labels.noidungthanhtoan %></label>
                                <asp:Label ID="lbrsdesc" runat="server" CssClass="col-sm-6 col-xs-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="button-group" style="text-align: center">
                        <asp:Button ID="Button6" runat="server" CssClass="btn btn-success" Text='<%$ Resources:labels, lammoi %>'
                            OnClick="Button6_Click" />

                    </div>
                </div>
            </figure>

        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<!--end-->
<%--            <asp:Button ID="Button1" runat="server" Text="In kết quả" 
                onclick="Button1_Click" />--%>
<script  type="text/javascript">
    function Validate10() {
        if (!validateEmpty('<%=txtScheduleName.ClientID %>', '<%=Resources.labels.bancannhaptencholich %>')) {
             document.getElementById('<%=txtScheduleName.ClientID %>').focus();
             return false;
         }
     }
</script>
<script type="text/javascript">
    Onload();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        Onload();
    }

    function Onload() {
        $("[data-mask]").inputmask();
        $(".timepicker").timepicker({
            showInputs: false,
            minuteStep: 1,
            showMeridian: false,
            showSeconds: true
        });
        var date = new Date();
        date.setDate(date.getDate() + 1)
        $('.dateselect').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            language: 'en',
            todayBtn: "linked",
            startDate: date
        });
    }

    function replaceAll(str, from, to) {
        var idx = str.indexOf(from);


        while (idx > -1) {
            str = str.replace(from, to);
            idx = str.indexOf(from);
        }

        return str;
    }

    function ntt(sNumber, idDisplay, event) {

        executeComma(sNumber, event);

        if (document.getElementById(sNumber).value == "") {
            document.getElementById(idDisplay).innerHTML = "";
            return;
        }

        document.getElementById(idDisplay).innerHTML = "(" + number2text(replaceAll(document.getElementById(sNumber).value, ",", ""),'<%=System.Globalization.CultureInfo.CurrentCulture.ToString()%>') + ")";

    }
   
    function ValidateDaily() {
        if (validateEmpty('<%=txtFromD.ClientID %>','<%=Resources.labels.bannhapngaybatdau %>')) {
            if (validateEmpty('<%=txtToD.ClientID %>','<%=Resources.labels.bannhapngayketthuc %>')) {
                if (IsDateGreater('<%=txtToD.ClientID %>','<%=txtFromD.ClientID %>','<%=Resources.labels.ngayketthucphailonhonngaybatdau %>')) {

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
                if (IsDateGreater('<%=txtToW.ClientID %>','<%=txtFromW.ClientID %>','<%=Resources.labels.ngayketthucphailonhonngaybatdau %>')) {


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
                if (IsDateGreater('<%=txtToM.ClientID %>','<%=txtFromM.ClientID %>','<%=Resources.labels.ngayketthucphailonhonngaybatdau %>')) {

                    if (IsDateGreaterDay('<%=txtToM.ClientID %>','<%=txtFromM.ClientID %>', 30,'<%=Resources.labels.thoigianhieulucitnhatlabamuoingay %>')) {
                    }
                    else {
                        document.getElementById('<%=txtFromM.ClientID %>').focus();
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
        if (document.getElementById("<%= ddlMonthlyDayNo.ClientID %>").value >= 29) {
            if (confirm('<%=Resources.labels.alertendofmonthschedule %>') == true) {

            }
            else {
                return false;
            }
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
    function validate() {
        if (validateEmpty('<%=txtRecieverAccount.ClientID %>','<%=Resources.labels.bancannhaptaikhoannguoinhan %>')) {
            if (validateMoney('<%=txtAmount.ClientID %>','<%=Resources.labels.bancannhapsotien %>')) {
                if (validateEmpty('<%=txtDesc.ClientID %>','<%=Resources.labels.bancannhapnoidung %>')) {

                }
                else {
                    document.getElementById('<%=txtDesc.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtAmount.ClientID %>').focus();
                return false;
            }

        }
        else {
            document.getElementById('<%=txtRecieverAccount.ClientID %>').focus();
            return false;
        }

    }
    function validate1() {
        if (validateMoney('<%=txtAmount1.ClientID %>','<%=Resources.labels.bancannhapsotien %>')) {
            if (validateEmpty('<%=txtDesc1.ClientID %>','<%=Resources.labels.bancannhapnoidung %>')) {

            }
            else {
                document.getElementById('<%=txtDesc1.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtAmount1.ClientID %>').focus();
            return false;
        }

    }



    function IsDateGreaterDay(DateValue1, DateValue2, day, aler) {

        var DaysDiff;
        DateValue1 = document.getElementById(DateValue1).value;
        DateValue2 = document.getElementById(DateValue2).value;

        var dt1 = DateValue1.substring(0, 2);
        var mon1 = DateValue1.substring(3, 5);
        var yr1 = DateValue1.substring(6, 10);
        var dt2 = DateValue2.substring(0, 2);
        var mon2 = DateValue2.substring(3, 5);
        var yr2 = DateValue2.substring(6, 10);
        DateValue1 = mon1 + "/" + dt1 + "/" + yr1;
        DateValue2 = mon2 + "/" + dt2 + "/" + yr2;

        Date1 = new Date(DateValue1);
        Date2 = new Date(DateValue2);
        DaysDiff = Math.floor((Date1.getTime() - Date2.getTime()) / (1000 * 60 * 60 * 24));


        if (DaysDiff >= day)
            return true;
        else
            window.alert(aler);
        return false;
    }
    function validateTOB() {

        if (document.getElementById("<%=radCMND.ClientID %>").checked == true) {

            if (validateEmpty('<%=txtCMND.ClientID %>','<%=Resources.labels.socmndcuataikhoandenkhongrong %>')) {
                if (IsNumeric('<%=txtCMND.ClientID %>','<%=Resources.labels.socmndcuataikhoandenkhongdungdinhdangso %>')) {
                    if (validateEmpty('<%=txtissuedate.ClientID %>','<%=Resources.labels.ngaycapkhongrong %>')) {
                        if (validateEmpty('<%=txtissueplace.ClientID %>','<%=Resources.labels.noicapkhongrong %>')) {
                        }
                        else {
                            document.getElementById('<%=txtissueplace.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtissuedate.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtCMND.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtCMND.ClientID %>').focus();
                return false;
            }
        }
        if (document.getElementById("<%=radAcctNo.ClientID %>").checked == true) {
            if (validateEmpty('<%=txtReceiverAccount.ClientID %>','<%=Resources.labels.sotaikhoandenkhongrong %>')) {

            }
            else {
                document.getElementById('<%=txtReceiverAccount.ClientID %>').focus();
                return false;
            }
        }


        if (validateMoney('<%=txtAmount2.ClientID %>','<%=Resources.labels.bancannhapsotien %>')) {
            if (validateEmpty('<%=txtDesc2.ClientID %>','<%=Resources.labels.bancannhapnoidung %>')) {
                if (validateEmpty('<%=txtReceiverName.ClientID %>','<%=Resources.labels.bancannhaptennguoinhan %>')) {
                    if (validateEmpty('<%=txtReceiverAdd.ClientID %>','<%=Resources.labels.bancannhapdiachinguoinhan %>')) {
                        if (validateEmpty('<%=txtSenderName.ClientID %>','<%=Resources.labels.bancannhaptennguoigui %>')) {
                        }
                        else {
                            document.getElementById('<%=txtSenderName.ClientID %>').focus();
                            return false;
                        }
                    }
                    else {
                        document.getElementById('<%=txtReceiverAdd.ClientID %>').focus();
                        return false;
                    }
                }
                else {
                    document.getElementById('<%=txtReceiverName.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtDesc2.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtAmount2.ClientID %>').focus();
            return false;
        }

    }
</script>
