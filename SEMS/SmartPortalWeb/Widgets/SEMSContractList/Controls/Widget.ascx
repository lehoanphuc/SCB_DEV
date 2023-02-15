<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_SEMSContractList_Controls_Widget" %>
<script src="widgets/SEMSContractList/JS/ajax.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/tab-view.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/IBTransactionHistory1/JS/lang/en.js" type="text/javascript"></script>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/common.js"> </script>
<%--<script type="text/javascript" src="widgets/SEMSCustomerList/js/subModal.js"> </script>--%>
<script type="text/javascript" src="widgets/SEMSCustomerList/js/commonjs.js"> </script>
<%@ Register Src="~/Controls/GirdViewPaging/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>
<%@ Import Namespace="SmartPortal.Constant" %>
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
<asp:HiddenField ID="hfuserType" Value="0201" runat="server" />
<asp:HiddenField ID="hfSubUserType" Value="0201" runat="server" />
<asp:HiddenField ID="hfTransactionAlter" Value="0201" runat="server" />
<asp:HiddenField ID="hfSubUserTypeOld" Value="0201" runat="server" />

<asp:HiddenField ID="hfsms" Value="False" runat="server" />
<asp:HiddenField ID="hfline" Value="False" runat="server" />
<asp:HiddenField ID="hfwhatsapp" Value="False" runat="server" />
<asp:HiddenField ID="hftele" Value="False" runat="server" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script>
    $(document).ready(function () {
        $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
            localStorage.setItem('activeTab', $(e.target).attr('href'));
        });
        var activeTab;
        if (localStorage.getItem('activeTab')) {
            activeTab = localStorage.getItem('activeTab');
        }
        else activeTab = '#tab_1';

        if (activeTab == "#tab_2_1") {
            $('#divButtonList').css('visibility', 'hidden')
            $('#divButtonList2').css('visibility', 'visible')
        }
        else {
            $('#divButtonList').css('visibility', 'visible')
            $('#divButtonList2').css('visibility', 'hidden')
        }

        if (activeTab) {
            $('#tabs a[href="' + activeTab + '"]').tab('show');
        }
    });
    function hiddenSave() {
        $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
            var tab = $(e.target).attr('href')
            if (tab == "#tab_2_1") {
                $('#divButtonList').css('visibility', 'hidden')
                $('#divButtonList2').css('visibility', 'visible')
            }
            else {
                $('#divButtonList').css('visibility', 'visible')
                $('#divButtonList2').css('visibility', 'hidden')
            }
        });
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <script>
            function pageLoad(sender, args) {
                $(document).ready(function () {
                    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                        localStorage.setItem('activeTab', $(e.target).attr('href'));
                    });
                    var activeTab = '#tab_1';;
                    //if (localStorage.getItem('activeTab')) {GenderGender
                    //    activeTab = localStorage.getItem('activeTab');
                    //}
                    //else activeTab = '#tab_1';

                    if (activeTab == "#tab_2_1") {
                        $('#divButtonList').css('visibility', 'hidden')
                        $('#divButtonList2').css('visibility', 'visible')
                    }
                    else {
                        $('#divButtonList').css('visibility', 'visible')
                        $('#divButtonList2').css('visibility', 'hidden')
                    }

                    if (activeTab) {
                        $('#tabs a[href="' + activeTab + '"]').tab('show');
                    }
                });
                function hiddenSave() {
                    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                        var tab = $(e.target).attr('href')
                        if (tab == "#tab_2_1") {
                            $('#divButtonList').css('visibility', 'hidden')
                            $('#divButtonList2').css('visibility', 'visible')
                        }
                        else {
                            $('#divButtonList').css('visibility', 'visible')
                            $('#divButtonList2').css('visibility', 'hidden')
                        }
                    });
                }
            }
        </script>
        <div class="subheader">
            <h1 class="subheader-title">
                <%=Resources.labels.thongtinhopdong %>
            </h1>
        </div>
        <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"><%=Resources.labels.image %></h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body">
                        <asp:Image ID="Image1" runat="server" Style="max-width: 1000px; max-height: 1000px; display: block; margin: 0 auto;"
                            class="img-responsive" OnLoad="Show_viewfile" />
                    </div>
                </div>
            </div>
        </div>
        <div id="myModal1" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"><%=Resources.labels.image %></h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                    </div>
                    <div class="modal-body">
                        <asp:Image ID="Image2" runat="server" Style="max-width: 1000px; max-height: 1000px; display: block; margin: 0 auto;"
                            class="img-responsive" OnLoad="Show_viewfile1" />
                    </div>
                </div>
            </div>
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
        <div class="row" runat="server" id="divAccount">
            <div class="col-sm-12">
                <div id="tabs" class="nav-tabs-custom" onclick="hiddenSave();">
                    <ul class="nav nav-tabs">
                        <li class="active" id="liTabDetailContract" runat="server"><a href="#tab_1" data-toggle="tab"><%=Resources.labels.chitiethopdong %></a></li>
                        <li id="liTabDetailCustomer" runat="server"><a href="#tab_2" data-toggle="tab"><%=Resources.labels.chitietkhachhang %></a></li>
                        <%--<li id="liKYC" runat="server"><a href="#tab_2_1" data-toggle="tab"><%=Resources.labels.kycinformation %></a></li>--%>
                        <li id="liTabWorkingAcc" runat="server"><a href="#tab_3" data-toggle="tab"><%=Resources.labels.taikhoansudung %></a></li>
                        <li id="liTabWorkingCard" runat="server"><a href="#tab_4" data-toggle="tab"><%=Resources.labels.workingcard %></a></li>
                    </ul>
                    <asp:HiddenField ID="hdfactivetab" runat="server" />
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                            <div class="panel-container divToolbar">
                                <div class="panel-content form-horizontal p-b-0">
                                    <asp:Panel ID="pnAdd" runat="server">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.mahopdong %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtcontractno" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.kieunguoidung %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlUserType" CssClass="form-control select2" Width="100%" runat="server" OnSelectedIndexChanged="ddlUserType_OnSelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.tenkhachhang %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtcustname" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.subusertype %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlSubUserType" Width="100%" OnSelectedIndexChanged="ddlSubUserType_OnSelectedIndexChanged" CssClass="form-control select2" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.nguoitao %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtusercreate" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.loaihinhsanpham %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlProductType" CssClass="form-control select2" Width="100%" runat="server" OnSelectedIndexChanged="ddlProductType_OnSelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblProductType" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.ngaymo %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtopendate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.ngayhethan %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtenddate" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.nguoiduyet %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtuserapprove" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.trangthai %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.nguoisuadoi %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtuserlastmodify" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.ngaysuadoi %></label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtmodifydate" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6" style="display:none;">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.contractaLevel %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlContractLevel" CssClass="form-control select2" runat="server" Width="100%" Enabled="false">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblContractLevel" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label"><%=Resources.labels.chinhanh %></label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlBranch" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" runat="server" id="divMerchantCategory" visible="false">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Merchant category</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlMerchantCategory" CssClass="form-control select2" Width="100%" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblMerchantCategory" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                </div>

                                             <%-- <div class="col-sm-6" runat="server" id="divScanQR">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">QR Code</label>
                                                    <div class="col-sm-8">
                                                        <asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, print %> " />
                                                        <div class="wcontent">
                                                            <div class="wabsute">
                                                                <asp:Image Visible="false" runat="server" ID="ImageQR" Style="max-width: 200px; max-height: 200px; width: 100%" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            </div>
                                        </div>
                                        <div class="row" style="display:none;">
                                            <div class="col-sm-12" runat="server">
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label"><%=Resources.labels.transactionalert %></label>
                                                    <div class="col-sm-10">
                                                        <div class="row">
                                                            <div class="form-group custom-control">
                                                                <div class="col-sm-3">
                                                                    <asp:CheckBox ID="cbSMS" runat="server" Text="<%$ Resources:labels, sms %>" />
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <asp:CheckBox ID="cbWAPP" runat="server" Text="<%$ Resources:labels, whatsapp %>" />
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <asp:CheckBox ID="cbLINE" runat="server" Text="<%$ Resources:labels, line %>" />
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <asp:CheckBox ID="cbTELE" runat="server" Text="<%$ Resources:labels, telegram %>" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group custom-control">
                                                    <asp:CheckBox ID="chkRenew" runat="server" Checked="True" Text="<%$ Resources:labels, autorenewlabel %>" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group  custom-control">
                                                    <asp:CheckBox ID="cbIsReceiver" runat="server" Text="<%$ Resources:labels, chichophepchuyenkhoantrongdanhsachnguoithuhuong %>" Visible="false" />
                                                </div>
                                            </div>

                                            <div class="col-sm-6"></div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_2">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnLinkBank" runat="server">
                                        <div class="divToolbar">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label class="col-sm-4 control-label">Customer code</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtCustomerCode" placeholder="Customer Code" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <asp:Button ID="btnLinkBank" runat="server" CssClass="btn btn-primary" Text="Link Bank" OnClick="btnLinkBank_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <div class="panel-container divToolbar">
                                            <div class="panel-content form-horizontal p-b-0">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Customer code</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtCustCode" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.loaikhachhang %></label>
                                                                <asp:Label runat="server" ID="lblCustType" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlCustType" CssClass="form-control select2 infinity" runat="server" Enabled="false" Width="100%"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label">Customer ID</label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtcustID" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.tendaydu %></label>
                                                                <asp:Label runat="server" ID="lblFullname" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtFullNameCust" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.tenviettat %></label>
                                                                <asp:Label runat="server" ID="lblShortName" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtShortName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.chinhanh %></label>
                                                                <asp:Label runat="server" ID="lblbranchcust" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlBranchCust" CssClass="form-control select2" runat="server" Enabled="false" Width="100%"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label "><%=Resources.labels.region %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control select2" Width="100%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label "><%=Resources.labels.Townshipname %></label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlTownship" runat="server" CssClass="form-control select2" Width="100%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.birthday %></label>
                                                                <asp:Label runat="server" ID="lblbirth" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtBirth" CssClass="form-control datetimepicker" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <%--<label class="col-sm-4 control-label"><%=Resources.labels.gender %></label>
                                                                <asp:Label runat="server" ID="lblGender" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlGender" CssClass="form-control  select2 infinity" runat="server" Enabled="false" Width="100%"></asp:DropDownList>
                                                                </div>--%>
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.email %></label>
                                                                <asp:Label runat="server" ID="lblEmail" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.phone %></label>
                                                                <asp:Label runat="server" ID="lblMobi" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtMobi" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.ngaycap %></label>
                                                                <asp:Label runat="server" ID="lblReleasedate" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtReleasedate" CssClass="form-control datetimepicker" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.hochieuchungminh %></label>
                                                                <asp:Label runat="server" ID="lblPassportCmdn" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtPassportCmdn" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.quocgia %></label>
                                                                <asp:Label runat="server" ID="lblddlNation" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlNation" CssClass="form-control  select2 infinity" runat="server" Enabled="false" Width="100%"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.diachithuongtru %></label>
                                                                <asp:Label runat="server" ID="lblResidentAddress" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtResidentAddress" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                 <label class="col-sm-4 control-label"><%=Resources.labels.diachitamtru %></label>
                                                                <asp:Label runat="server" ID="lblTempStay" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtTempStay" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6 hidden">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.diachilamviec %></label>
                                                                <asp:Label runat="server" ID="lblAddressOffice" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtAddressOffice" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 hidden">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.dienthoaicoquan %></label>
                                                                <asp:Label runat="server" ID="lblOfficePhone" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtOfficePhone" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label"><%=Resources.labels.noicap %></label>
                                                                <asp:Label runat="server" ID="lblRelease" Visible="false" />
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtRelease" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                               
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnLinkBank" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="tab_2_1">
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">

                                    <asp:Panel ScrollBars="Auto" runat="server" GroupingText="KYC Information">
                                        <%--<div style="overflow-x:hidden; overflow-y:scroll;">--%>
                                        <asp:Repeater runat="server" ID="Repeater1" OnItemCommand="rptData_ItemCommand">
                                            <HeaderTemplate>
                                                <div class="pane">
                                                    <div class="table-responsive">
                                                        <table class="table table-hover footable c_list">
                                                            <thead class="thead-light repeater-table">
                                                                <tr>

                                                                    <th class="title-repeater"><%=Resources.labels.RequestNo%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.PaperNumber%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.KycLevel%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.PhoneNumber%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.status%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.DateCreate%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.CreatedBy%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.ApprovedBy%></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="tr-boder"><%#Eval("RequestID") %></td>
                                                    <td class="tr-boder"><%#Eval("PaperNO") %></td>
                                                    <td class="tr-boder"><%#Eval("PaperType") %></td>
                                                    <td class="tr-boder"><%#Eval("PHONE") %></td>
                                                    <td class="tr-boder"><%#Eval("StatusCaption")%></td>
                                                    <td class="tr-boder"><%#Eval("DateCreateFormat") %></td>
                                                    <td class="tr-boder"><%#Eval("UserCreated")%></td>
                                                    <td class="tr-boder"><%#Eval("UserApproved") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                        </table>
                        </div> </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <asp:HiddenField runat="server" ID="HiddenField1" />
                                        <%--</div>--%>
                                    </asp:Panel>

                                    <asp:UpdatePanel ID="UpdatePanelGridView" UpdateMode="Always" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <uc1:GridViewPaging ID="GridViewPaging" Visible="false" runat="server"></uc1:GridViewPaging>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="GridViewPaging" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                    <asp:Panel ScrollBars="Auto" runat="server" GroupingText="Document Information">
                                        <%--<div style="overflow-x:scroll; overflow-y:scroll;">--%>
                                        <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_OnItemDataBound" OnItemCommand="rptData_ItemCommand">
                                            <HeaderTemplate>
                                                <div class="pane">
                                                    <div class="table-responsive">
                                                        <table class="table table-hover footable c_list">
                                                            <thead class="thead-light repeater-table">
                                                                <tr>
                                                                    <th class="title-repeater"><%=Resources.labels.DocumentCode%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.DocumentName%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.UploadDate%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.CreatedBy%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.status%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.edit%></th>
                                                                    <th class="title-repeater"><%=Resources.labels.Download%></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="tr-boder"><%#Eval("DocumentCode") %></td>
                                                    <td class="tr-boder"><%#Eval("DocumentName") %></td>
                                                    <td class="tr-boder"><%#Eval("DateCreated") %></td>
                                                    <td class="tr-boder"><%#Eval("UserCreated")%></td>
                                                    <td class="tr-boder">
                                                        <%#Eval("Status") %>
                                                        <label id="lbStatusDocument" visible="false" runat="server"><%#Eval("ValueStatus") %></label>
                                                        <label id="lbisNew" visible="false" runat="server"><%#Eval("isNew") %></label>
                                                    </td>
                                                    <td class="tr-boder item-center">
                                                        <%--<asp:LinkButton ID="lbtnViewFile" runat="server" class="btn btn-info" CommandArgument='<%#Eval("FILE") %>' CommandName='<%#IPC.ACTIONPAGE.DETAILS %>'> <%=Resources.labels.view%>
                                                </asp:LinkButton>--%>
                                                        <asp:UpdatePanel ID="updatepanel" runat="server">
                                                            <ContentTemplate>
                                                                <%--<uc1:PreviewImage ID="PreviewImage" runat="server"></uc1:PreviewImage>--%>
                                                                <a id="btnShowPopup" data-toggle="modal" data-target="#Modal<%#Eval("No") %>">
                                                                    <asp:Image ID="ImageView" Style="max-width: 150px" runat="server" src='<%# "data:image/jpg;base64," + Eval("FILE") %>' data-toggle="tooltip" title="Show image" />
                                                                </a>
                                                                <asp:Panel ID="pannelModal" runat="server" DefaultButton="btnOK">
                                                                    <!-- The Modal -->
                                                                    <div class="modal" id="Modal<%#Eval("No") %>">
                                                                        <div class="modal-dialog">
                                                                            <div class="modal-content">

                                                                                <!-- Modal Header -->
                                                                                <div class="modal-header">
                                                                                    <h4 class="modal-title" style="text-align: left!important">Edit Document</h4>
                                                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                                </div>

                                                                                <!-- Modal body -->
                                                                                <div class="divlog" style="color: red">
                                                                                    <label id="lblErrorPopup" runat="server"></label>
                                                                                </div>
                                                                                <div class="modal-body">
                                                                                    <div class="panel-container">
                                                                                        <div class="panel-content form-horizontal p-b-0">
                                                                                            <asp:TextBox ID="txtNo" Visible="false" Text='<%#Eval("No") %>' runat="server"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtDocname" Visible="false" Text='<%#Eval("DocumentName") %>' runat="server"></asp:TextBox>
                                                                                            <asp:HiddenField ID="hdfDocumentType" runat="server" Value='<%#Eval("DocumentType") %>' />
                                                                                            <div class="form-group">
                                                                                                <label class="control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                                                <asp:TextBox ID="txtDocumentName" MaxLength="250" Text='<%#Eval("DocumentName") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="form-group">
                                                                                                <label class="control-label required" style="float: left"><%=Resources.labels.DocumentType%></label>
                                                                                                <asp:DropDownList ID="ddlDocumentType" MaxLength="250" runat="server" Style="height: auto;" CssClass="form-control">
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                            <div class="view-image" style="text-align: center; width: 100%; height: auto">
                                                                                                <asp:Image ID="ImageDocument" runat="server" src='<%# "data:image/jpg;base64," + Eval("FILE") %>' Style="width: 100%; max-width: 500px; height: auto;" Height="400" Width="500" />
                                                                                            </div>
                                                                                            <div class="button" style="text-align: right; padding-bottom: 10px">
                                                                                                <asp:FileUpload ID="fileUpdate" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp" Width="348px" Height="27px" />
                                                                                                <asp:UpdatePanel ID="UpdatePanelUpdate" UpdateMode="Always" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:Button ID="btnImportUpdate" type="button" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, ImportFile %>" autopostback="false" OnClick="btnImportFileUpdate_Click" />
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:PostBackTrigger ControlID="btnImportUpdate" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <!-- Modal footer -->
                                                                                <div class="modal-footer" style="text-align: center!important">
                                                                                    <asp:Button runat="server" class="btn btn-primary" data-check="itemBranch111" ID="btnOK" OnClick="btnOK_Click" Text='<%$Resources:labels,ok %>' />
                                                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal"><%=Resources.labels.cancel %></button>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnOK" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td class="tr-boder item-center">
                                                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                                            <ContentTemplate>
                                                                <asp:LinkButton runat="server" class="btn btn-info" BackColor="DodgerBlue" ID="txtdown_file1" CommandArgument='<%#Eval("DocumentCode") + "---" + Eval("File") %>' CommandName='<%#IPC.ACTIONPAGE.EXPORT %>'><i class="fa fa-download"></i> Download</asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="txtdown_file1" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </table>
                                            </div> </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <%--</div>--%>
                                        <div style="text-align: right; padding-bottom: 10px">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnDowloadAll" type="button" CssClass="btn btn-secondary" runat="server" Text="<%$ Resources:labels, DownloadAllFile %>" autopostback="false" OnClick="btnDownloadAll_Click" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnDowloadAll" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <asp:Panel runat="server" ID="pnImportNewDocument" GroupingText="Import New Document" Visible="false">
                                            <div class="panel-container">
                                                <div class="panel-content form-horizontal p-b-0">
                                                    <asp:Panel runat="server">
                                                        <div class="row">
                                                            <div class="col-sm-3" runat="server" id="divNRIC">
                                                                <div class="form-group custom-control">
                                                                    <asp:RadioButton ID="radNewNRIC" runat="server" GroupName="GETINFO"
                                                                        Text="NRIC" Checked="true" onclick="accordion();" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2" runat="server" id="divPassport">
                                                                <div class="form-group custom-control">
                                                                    <asp:RadioButton ID="radPassport" runat="server" GroupName="GETINFO"
                                                                        Text="Passport" onclick="accordion();" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2" runat="server" id="divLicense">
                                                                <div class="form-group custom-control">
                                                                    <asp:RadioButton ID="radLicense" runat="server" GroupName="GETINFO"
                                                                        Text="License" onclick="accordion();" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2" runat="server">
                                                                <div class="form-group custom-control">
                                                                    <asp:RadioButton ID="radAgreement" runat="server" GroupName="GETINFO"
                                                                        Text="Agreement Contract" onclick="accordion();" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2" runat="server">
                                                                <div class="form-group custom-control">
                                                                    <asp:RadioButton ID="radBusiness" runat="server" GroupName="GETINFO"
                                                                        Text="Business Document" onclick="accordion();" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="PnPassport" runat="server">
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label required">Passport</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlContryPassport" CssClass="form-control select2 infinity" runat="server" Width="100%" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtPassPortNo" CssClass="form-control" placeholder="Passport No" MaxLength="20" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="ddlDatetimePassport" Width="100%" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnNewNRIC" runat="server">
                                                        <div class="row">
                                                            <div class="NRIC">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtDocumentNameImport" placeholder="Document name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.PaperNumber%></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtPaperNumberImport" placeholder="NRC number" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.IssueDate%></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtIssueDateImport" onchange="onchage_issuedateimport();" MaxLength="250" CssClass="form-control datetimepicker" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentType%></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlDocumentTypeImport" onChange="onchage(this);" MaxLength="250" Width="100%" runat="server" CssClass="form-control select2">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnLicense" runat="server">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtLicenseName" Text="License" placeholder="License name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-4 control-label required">License Number</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtLicenseNumber" CssClass="form-control" onchange="onchage_papernumimport();" placeholder="License number" runat="server" MaxLength="20"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnAgreement" runat="server">
                                                        <div class="row">
                                                            <div class="Agreement">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtAgreementNameImport" Text="Agreement Contract" placeholder="Agreement name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnBusiness" runat="server">
                                                        <div class="row">
                                                            <div class="Business">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label required" style="float: left"><%=Resources.labels.DocumentName%></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtBusinessNameImport" Text="Business Document" placeholder="Business name" MaxLength="250" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:FileUpload ID="documentUpload" runat="server" accept=".PNG, .png, .jpg, .JPG, .JPEG, .jpeg, .BMP, .bmp" Width="348px" Height="27px" />
                                                    <div class="button" style="text-align: right; padding-bottom: 10px">
                                                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnImport" type="button" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, ImportFile %>" autopostback="false" OnClick="btnImportFile_Click" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnImport" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:HiddenField runat="server" ID="hdCLMS_SCO_SCO_PRODUCT" />
                                        <%--</div>--%>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_3">
                            <asp:Panel ID="pnToolbar" runat="server">
                                <div class="divToolbar">
                                    &nbsp;<asp:Button ID="btnAddUser" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddNew_Click" />
                                    &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDelete_Click" />
                                </div>
                            </asp:Panel>
                            <div class="divResult">
                                <asp:GridView ID="gvCustomerList" runat="server" BackColor="White" CssClass="table table-hover"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                    OnRowDataBound="gvCustomerList_RowDataBound"                                    
                                     PageSize="30">
                                    <RowStyle ForeColor="#000000" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbxSelect" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="USERID" Visible="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpUID" runat="server" Visible="false"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, fullname %>" SortExpression="FULLNAME">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpUserFullName" runat="server"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, phone %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, kieunguoidung %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserType" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, account %>" >
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAccountQR" runat="server" CssClass="form-control"  AutoPostBack="True"
                  onselectedindexchanged="ddlAccountQR_SelectedIndexChanged" >                         
                                                </asp:DropDownList>
                                             <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" width="100%" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QR" Visible="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpQR" CssClass="btn btn-primary" CommandName="GenQR" runat="server">[hpQR]</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpEdit" CssClass="btn btn-primary" runat="server">[hpEdit]</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpDelete" CssClass="btn btn-secondary" runat="server">[hpDelete]</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gvFooterStyle" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                    <SelectedRowStyle />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="tab-pane" id="tab_4">
                            <asp:Panel ID="pnCard" runat="server">
                                <div class="divToolbar">
                                    &nbsp;<asp:Button ID="btnAddCard" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, themmoi %>" OnClick="btnAddCard_Click" />
                                    &nbsp;<asp:Button ID="btnDeleteCard" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, delete %>" OnClick="btnDeleteCard_Click" />
                                </div>
                            </asp:Panel>
                            <div class="divResult">
                                <asp:GridView ID="gvCard" runat="server" BackColor="White" CssClass="table table-hover"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                    Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                    OnRowDataBound="gvCard_RowDataBound" PageSize="15">
                                    <RowStyle ForeColor="#000000" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbxSelect" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="USERID" Visible="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpUID" runat="server" Visible="false"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, makhachhang %>" SortExpression="FULLNAME">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpHolderCIF" runat="server"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, tendaydu %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFullName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, numberofcard %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoCard" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Link type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, trangthai %>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, edit %> ">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpEdit" runat="server">[hpEdit]</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:labels, delete %>">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpDelete" runat="server">[hpDelete]</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gvFooterStyle" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                    <SelectedRowStyle />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnregister" runat="server" Visible="false">
            <div class="row" runat="server" id="div1">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-hdr">
                            <h2>Register information</h2>
                        </div>
                        <div class="tab-content">
                            <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Account name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtaccname" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Preferred Login Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtloginame" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">House No</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txthouse" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Unit</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtunit" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Village</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtvillage" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">District</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtdistrict" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Province</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtprovince" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Phone</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtphoneno" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Paper Type</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPapertype" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Papaer ID</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPaperID" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Issue Date</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtissuedate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Expiry Date</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtexpirydate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="divResult">
                                <asp:GridView ID="gvlistacc" runat="server" BackColor="White" CssClass="table table-hover"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                    OnRowDataBound="gvlistacc_RowDataBound" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="30">
                                    <RowStyle ForeColor="#000000" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Account number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaccnumber" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Currency">
                                            <ItemTemplate>
                                                <asp:Label ID="lblccyid" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gvHeader" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gvFooterStyle" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="pager" />
                                    <SelectedRowStyle />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="row" runat="server" id="divReject">
            <div class="col-sm-12">
                <div class="panel">
                    <div class="panel-hdr">
                        <h2>Reject Reason </h2>
                    </div>
                    <div class="tab-content">
                        <div class="tab-pane active">
                            <div class="panel-container">
                                <div class="panel-content form-horizontal p-b-0">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Reason name</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control select2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2"></div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Description</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" onkeyup="ValidateLimit(this,'250');" onkeyDown="ValidateLimit(this,'250');" onpaste="ValidateLimit(this,'250');" onChange="ValidateLimit(this,'250');" onmousedown="ValidateLimit(this,'250');"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2"></div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="margin-top: 10px; text-align: center;" id="divButtonList">
            &nbsp;<asp:Button ID="btSave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:labels, save %>" OnClick="btSave_Click" OnClientClick="return validate();" />
            &nbsp;<asp:Button ID="btnSync" Visible="False" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, dongbotucorebank %>" OnClick="btnSync_OnClick" />
            &nbsp;<asp:Button ID="Button1" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="Button1_Click" />

        </div>
        <div style="margin-top: 10px; text-align: center;" id="divButtonList2">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    &nbsp;<asp:Button ID="btSaveDocument" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:labels, SaveDocument %>" OnClick="btSaveDoc_Click" />
                    &nbsp;<asp:Button ID="Button2" runat="server" CssClass="btn btn-secondary" Text="<%$ Resources:labels, back %>" OnClick="Button1_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btSaveDocument" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script>
    function SelectCbx(obj) {
        var count = document.getElementById('aspnetForm').elements.length;
        var elements = document.getElementById('aspnetForm').elements;
        if (obj.checked) {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].disabled != true && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                    elements[i].checked = true;
                    //elements[i].parentNode.parentNode.className = "hightlight";
                }
            }

        } else {
            for (i = 0; i < count; i++) {
                if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                    elements[i].checked = false;
                    //elements[i].parentNode.parentNode.className = "nohightlight";
                }
            }
        }
    }

    function HighLightCBX(obj, obj1) {
        //var obj2=document.getElementById(obj1);
        if (obj1.checked) {
            document.getElementById(obj).className = "hightlight";
        }
        else {
            document.getElementById(obj).className = "nohightlight";
        }
    }

    function checkColor(obj, obj1) {
        var obj2 = document.getElementById(obj);
        if (obj2.checked) {
            obj1.className = "hightlight";
        }
        else {
            obj1.className = "nohightlight";
        }
    }
</script>

<script type="text/javascript">
    initTabs('dhtmlgoodies_tabView1', Array('<%=Resources.labels.chitiethopdong %>', '<%=Resources.labels.chitietkhachhang %>', '<%=Resources.labels.taikhoansudung %>', '<%=Resources.labels.workingcard %>'), 0, '100%', 320, Array(false, false, false, false));


    function validate() {
        if (validateEmpty('<%=txtopendate.ClientID %>','<%=Resources.labels.bannhapngayhieuluc %>')) {
            if (validateEmpty('<%=txtenddate.ClientID %>','<%=Resources.labels.bannhapngayhethan %>')) {
                if (IsDateGreater('<%=txtenddate.ClientID %>','<%=txtopendate.ClientID %>','<%=Resources.labels.ngayhethanphailonhonngayhieuluc %>')) {

                }
                else {
                    document.getElementById('<%=txtopendate.ClientID %>').focus();
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtenddate.ClientID %>').focus();
                return false;
            }
        }
        else {
            document.getElementById('<%=txtopendate.ClientID %>').focus();
            return false;
        }

    }

</script>
<script type="text/javascript">
    if (CheckNRIC('<%=showTabNRIC()%>') == true) {
        LoadHide("block", "none", "none", "none", "none");
    }
    if (CheckLicense('<%=showTabLicense()%>') == true) {
        LoadHide("none", "block", "none", "none", "none");
    }
    if (CheckPassport('<%=showTabPassport()%>') == true) {
        LoadHide("none", "none", "block", "none", "none");
    }
    if (CheckAgreement(1) == true) {
        LoadHide("none", "none", "none", "block", "none");
    }
    if (CheckBusiness(1) == true) {
        LoadHide("none", "none", "none", "none", "block");
    }
    function accordion() {
        debugger;
        if (CheckNRIC('<%=showTabNRIC()%>') == true) {
            LoadHide("block", "none", "none", "none", "none");
        }
        if (CheckLicense('<%=showTabLicense()%>') == true) {
            LoadHide("none", "block", "none", "none", "none");
        }
        if (CheckPassport('<%=showTabPassport()%>') == true) {
            LoadHide("none", "none", "block", "none", "none");
        }
        if (CheckAgreement(1) == true) {
            LoadHide("none", "none", "none", "block", "none");
        }
        if (CheckBusiness(1) == true) {
            LoadHide("none", "none", "none", "none", "block");
        }
    }
    function LoadHide(nric, license, passport, agreement, business) {
        debugger;
        document.getElementById("<%=PnPassport.ClientID %>").style.display = "none";
        document.getElementById("<%=pnLicense.ClientID %>").style.display = "none";
        document.getElementById("<%=pnNewNRIC.ClientID %>").style.display = "none";
        document.getElementById("<%=pnAgreement.ClientID %>").style.display = "none";
        document.getElementById("<%=pnBusiness.ClientID %>").style.display = "none";
        if ('<%=showTabNRIC()%>' == 'True') {
            document.getElementById("<%=pnNewNRIC.ClientID %>").style.display = nric;
        }
        if ('<%=showTabLicense()%>' == 'True') {
            document.getElementById("<%=pnLicense.ClientID %>").style.display = license;
        }
        if ('<%=showTabPassport()%>' == 'True') {
            document.getElementById("<%=PnPassport.ClientID %>").style.display = passport;
        }
        document.getElementById("<%=pnAgreement.ClientID %>").style.display = agreement;
        document.getElementById("<%=pnBusiness.ClientID %>").style.display = business;
    }
    function CheckPassport(id) {
        debugger;
        if (id == 'True') {
            return document.getElementById("<%=radPassport.ClientID %>").checked;
        }
        else return false;
    }
    function CheckAgreement(id) {
        debugger;
        if (id == 1) {
            return document.getElementById("<%=radAgreement.ClientID %>").checked;
        }
        else return false;
    }
    function CheckBusiness(id) {
        debugger;
        if (id == 1) {
            return document.getElementById("<%=radBusiness.ClientID %>").checked;
        }
        else return false;
    }
    function CheckLicense(id) {
        if (id == 'True') {
            return document.getElementById("<%=radLicense.ClientID %>").checked;
        }
        else return false;
    }
    function CheckNRIC(id) {
        if (id == 'True') {
            return document.getElementById("<%=radNewNRIC.ClientID %>").checked;
        }
        else return false;
    } 
</script>
<script>
    function onchage(ob) {
        var textbox = document.getElementById('<%=txtDocumentNameImport.ClientID %>');
        textbox.value = ob.options[ob.selectedIndex].text;
    }
</script>
