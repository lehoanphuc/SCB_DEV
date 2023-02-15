<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="rptContractList_Widget" %>
<style type="text/css">
    #divSearch {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 5px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }

    #divToolbar {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 0px 0px 0px 0px;
        padding: 5px 5px 5px 5px;
    }

    #divLetter {
        background-color: #F8F8F8;
        border: solid 1px #B9BFC1;
        margin: 10px 5px 5px 5px;
        padding: 5px 5px 5px 5px;
    }

    #divResult {
        margin: 20px 5px 5px 5px;
        padding: 0px 0px 0px 0px;
    }

    .gvHeader {
        text-align: left;
    }

    #divCustHeader {
        width: 100%;
        font-weight: bold;
        padding: 0px 5px 0px 5px;
    }

    #divError {
        width: 100%;
        height: 10px;
        text-align: center;
        padding: 5px 5px 5px 5px;
    }

    .hightlight {
        background-color: #EAFAFF;
        color: #003366;
    }

    .nohightlight {
        background-color: White;
    }

    .style6 {
        width: 265px;
    }
</style>
<link href="widgets/SEMSContractList/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/css.css" rel="stylesheet" type="text/css" />


<script src="widgets/SEMSContractList/JS/jscal2.js" type="text/javascript"></script>
<script src="widgets/SEMSContractList/JS/lang/en.js" type="text/javascript"></script>

<link href="widgets/SEMSContractList/CSS/jscal2.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/border-radius.css" rel="stylesheet" type="text/css" />
<link href="widgets/SEMSContractList/CSS/steel/steel.css" rel="stylesheet" type="text/css" />
<br />
<div id="divCustHeader">
    <img alt="" src="widgets/rptAllReport/Images/report.png" style="width: 40px; height: 32px; margin-bottom: 10px;" align="middle" />
    <%=Resources.labels.thamsobaocao %>
</div>
<div style="padding-top: 50px; padding-left: 100px;">
    <asp:Panel runat="server" DefaultButton="btnViewReport">
        <table width="800px" style="border: solid 1px #DFDFDF; background-color: #DFDFDF" cellpadding="10px">
            <tr>
                <td class="style6">
                    <asp:Label ID="Label0" runat="server" Text="<%$ Resources:labels, loaihopdong %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="rptusertype" runat="server">
                        <asp:ListItem Value="PCO" Text="<%$ Resources:labels, canhan %>"></asp:ListItem>
                        <asp:ListItem Value="CCO" Text="<%$ Resources:labels, doanhnghiep %>"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td>
                    <asp:Button ID="btnViewReport" runat="server" Text="<%$ Resources:labels, timkiem %>"
                        OnClick="btnViewReport_Click" />
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:labels, trangthai %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="rptstatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:labels, ngayhieuluc %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="rptcreatedate" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="style6">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:labels, ngayhethan %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="rptenddate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:labels, chinhanh %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="rptbranchid" runat="server"
                        OnSelectedIndexChanged="rptbranchid_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:HiddenField ID="rptimper" runat="server" />
                    <asp:HiddenField ID="rptbranchname" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<script type="text/javascript">
    //<![CDATA[

    var cal = Calendar.setup({
        onSelect: function (cal) { cal.hide() }
    });
    cal.manageFields("<%=rptcreatedate.ClientID %>", "<%=rptcreatedate.ClientID %>", "%d/%m/%Y");
    cal.manageFields("<%=rptenddate.ClientID %>", "<%=rptenddate.ClientID %>", "%d/%m/%Y");
    //]]></script>
