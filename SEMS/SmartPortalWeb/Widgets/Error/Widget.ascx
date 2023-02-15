<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_Error_Widget" %>
<link href="Widgets/Error/CSS/StyleSheet.css" rel="stylesheet" />

<div class="swal2-container swal2-center swal2-fade swal2-shown" style="overflow-y: auto;">
    <div aria-labelledby="swal2-title" class="swal2-popup swal2-modal swal2-show" style="display: flex;">
        <div class="swal2-header">
            <div class="swal2-icon swal2-error swal2-animate-error-icon" style="display: flex;">
                <span class="swal2-x-mark"><span class="swal2-x-mark-line-left"></span>
                    <span class="swal2-x-mark-line-right"></span></span>
            </div>
            <h2 class="swal2-title">
                <asp:Label ID="lblErrorCode" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="swal2-content">
            <div id="swal2-content" style="display: block;">
                <asp:Label ID="lblErrorDesc" runat="server"></asp:Label>
            </div>
        </div>
        <div class="swal2-actions" style="display: flex;">
            <a class="swal2-confirm swal2-styled" style="display: inline-block; border-left-color: rgb(136, 106, 181); border-right-color: rgb(136, 106, 181);" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>
        </div>
    </div>
</div>
