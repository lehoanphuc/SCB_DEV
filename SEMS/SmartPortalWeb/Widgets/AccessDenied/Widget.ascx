<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Widget.ascx.cs" Inherits="Widgets_AccessDenied_Widget" %>
<style type="text/css">
    .swal2-icon.swal2-info {
        border-color: #82c4f8;
        color: #2196F3;
    }
    .swal2-icon.swal2-info::before {
        content: 'i';
    }
    .swal2-icon::before {
        display: -webkit-box;
        display: -ms-flexbox;
        display: flex;
        -webkit-box-align: center;
        -ms-flex-align: center;
        align-items: center;
        height: 92%;
        font-size: 3.75em;
    }
    .footer {
        display: none;
    }
</style>
<link href="Widgets/Error/CSS/StyleSheet.css" rel="stylesheet" />
<div class="swal2-container swal2-center swal2-fade swal2-shown" style="overflow-y: auto;">
    <div aria-labelledby="swal2-title" class="swal2-popup swal2-modal swal2-show" style="display: flex;">
        <div class="swal2-header">
            <div class="swal2-icon swal2-info swal2-animate-info-icon" style="display: flex;"></div>
            <h2 class="swal2-title">
                <asp:Label ID="lblErrorDesc" Text="<%$ Resources:labels, accessdenied %>" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="swal2-content">
            <div id="swal2-content" style="display: block;">
            </div>
        </div>
        <div class="swal2-actions" style="display: flex;">
            <a class="swal2-confirm swal2-styled" style="display: inline-block; border-left-color: rgb(136, 106, 181); border-right-color: rgb(136, 106, 181);" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>
        </div>
    </div>
</div>