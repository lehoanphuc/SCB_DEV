<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_IBScheduleExecTrans_Widget" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<style type="text/css">
    .style1 {
        width: 100%;
        background-color: #EAEDD8;
    }

    .tibtd {
    }

    .tibtdh {
        background-color: #009CD4;
        font-weight: bold;
    }

    .style2 {
        width: 100%;
    }

    .al {
        font-weight: bold;
        padding-left: 5px;
        padding-top: 10px;
        padding-bottom: 10px;
    }
</style>

<div class="al">
    <span><%=Resources.labels.ngaythuchiengiaodichcualich %></span><br />
    <img style="margin-top: 5px;" src="Images/WidgetImage/underline.png" />
</div>
<asp:ScriptManager runat="server">
</asp:ScriptManager>

<div style="text-align: center; color: Red;">
    <asp:Label runat="server" Font-Bold="true" ID="lblTextError"></asp:Label>
</div>

<!--First-->
<asp:Panel ID="pnSchedule" runat="server">
    <figure>
        <legend class="handle"><%=Resources.labels.thongtintimkiem %></legend>
        <div class="content display-label">
            <div class="row form-group">
                <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.ngayhieuluc %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:TextBox ID="txtFromW" CssClass="dateselect" runat="server"></asp:TextBox>
                </div>
                <label class="col-sm-2 col-xs-5 bold textright-mb"><%= Resources.labels.ngayhethieuluc %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:TextBox ID="txtToW" CssClass="dateselect" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row form-group">
                <label class="col-sm-2 col-xs-5 bold"><%= Resources.labels.loaichuyenkhoan %></label>
                <div class="col-sm-4 col-xs-7">
                    <asp:DropDownList ID="ddlTransferType" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-2"></div>
                <div class="hidden-sm hidden-md hidden-lg col-xs-5"></div>
                <div class="col-sm-4 col-xs-7">
                    <asp:Button ID="btn_Search" CssClass="btn btn-primary" runat="server" Text='<%$ Resources:labels, search %>'
                        OnClientClick="return Validate();" OnClick="btn_Search_Click" />
                </div>
            </div>

            <div class="clearfix"></div>
        </div>


    </figure>


</asp:Panel>
<br />

<asp:Panel ID="pnDayPilot" runat="server">
    <div style="padding-left: 4px; height: 400px; overflow: auto;">

        <DayPilot:DayPilotCalendar ID="DayPilotCalendar" runat="server"
            BusinessBeginsHour="8" Days="7" HourHeight="300"
            EventClickHandling="PostBack" FreetimeClickHandling="PostBack"
            ShowToolTip="False" ColumnMarginRight="1" StartDate="" Width="100%"
            EventClickJavaScript="" FreeTimeClickJavaScript="" Font-Size="5px"
            DayFontSize="10pt" BusinessEndsHour="9" EventFontSize="7pt"
            HourFontSize="10pt" HourWidth="5"
            OnEventClick="DayPilotCalendar_EventClick" />

    </div>

</asp:Panel>
<br />
<script type="text/javascript">
    Onload();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        Onload();
    }

    function Onload() {
        $('.dateselect').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            language: 'en',
            todayBtn: "linked"
        });
    }
    

    function Validate() {
        if (validateEmpty('<%=txtFromW.ClientID %>','<%=Resources.labels.bancannhapngaybatdau %>','<%=lblTextError.ClientID%>')) {
            if (validateEmpty('<%=txtToW.ClientID %>', '<%=Resources.labels.bancannhapngayketthuc %>', '<%=lblTextError.ClientID%>')) {
                if (IsDateGreater('<%=txtToW.ClientID %>','<%=txtFromW.ClientID %>','<%=Resources.labels.ngayketthucphailonhonngaybatdau %>')) {

                    if (IsDateGreaterDay('<%=txtToW.ClientID %>','<%=txtFromW.ClientID %>', 7,'<%=Resources.labels.thoigianhieuluctoida7ngay %>')) {
                    }
                    else {
                        document.getElementById('<%=txtFromW.ClientID %>').focus();
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


        if (DaysDiff <= day)
            return true;
        else
            window.alert(aler);
        return false;
    }



</script>
