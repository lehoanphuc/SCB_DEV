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
    .swal2-container.swal2-shown {
	background-color: rgba(0, 0, 0, 0.2);
}

.swal2-container.swal2-fade {
	-webkit-transition: background-color 0.1s;
	transition: background-color 0.1s;
}

.swal2-container.swal2-center {
	-webkit-box-align: center;
	-ms-flex-align: center;
	align-items: center;
}

.swal2-container {
	display: -webkit-box;
	display: -ms-flexbox;
	display: flex;
	position: fixed;
	z-index: 1060;
	top: 0;
	right: 0;
	bottom: 0;
	left: 0;
	-webkit-box-orient: horizontal;
	-webkit-box-direction: normal;
	-ms-flex-direction: row;
	flex-direction: row;
	-webkit-box-align: center;
	-ms-flex-align: center;
	align-items: center;
	-webkit-box-pack: center;
	-ms-flex-pack: center;
	justify-content: center;
	padding: 0.625em;
	overflow-x: hidden;
	background-color: transparent;
	-webkit-overflow-scrolling: touch;
}

	.swal2-container:not(.swal2-top):not(.swal2-top-start):not(.swal2-top-end):not(.swal2-top-left):not(.swal2-top-right):not(.swal2-center-start):not(.swal2-center-end):not(.swal2-center-left):not(.swal2-center-right):not(.swal2-bottom):not(.swal2-bottom-start):not(.swal2-bottom-end):not(.swal2-bottom-left):not(.swal2-bottom-right):not(.swal2-grow-fullscreen) > .swal2-modal {
		margin: auto;
	}

.swal2-show {
	-webkit-animation: swal2-show 0.3s;
	animation: swal2-show 0.3s;
}

.swal2-popup {
	display: none;
	position: relative;
	-webkit-box-sizing: border-box;
	box-sizing: border-box;
	-webkit-box-orient: vertical;
	-webkit-box-direction: normal;
	-ms-flex-direction: column;
	flex-direction: column;
	-webkit-box-pack: center;
	-ms-flex-pack: center;
	justify-content: center;
	width: 30em;
	max-width: 100%;
	padding: 1.25em;
	border: none;
	border-radius: 0.3125em;
	background: #fff;
	font-family: inherit;
	font-size: 1rem;
}

.swal2-header {
	display: -webkit-box;
	display: -ms-flexbox;
	display: flex;
	-webkit-box-orient: vertical;
	-webkit-box-direction: normal;
	-ms-flex-direction: column;
	flex-direction: column;
	-webkit-box-align: center;
	-ms-flex-align: center;
	align-items: center;
}

.swal2-icon.swal2-error {
	border-color: #fd3995;
}

.swal2-animate-error-icon {
	-webkit-animation: swal2-animate-error-icon 0.5s;
	animation: swal2-animate-error-icon 0.5s;
}

.swal2-icon {
	position: relative;
	-webkit-box-sizing: content-box;
	box-sizing: content-box;
	-webkit-box-pack: center;
	-ms-flex-pack: center;
	justify-content: center;
	width: 5em;
	height: 5em;
	margin: 1.25em auto 1.875em;
	zoom: normal;
	border: .25em solid transparent;
	border-radius: 50%;
	font-family: inherit;
	line-height: 5em;
	cursor: default;
	-webkit-user-select: none;
	-moz-user-select: none;
	-ms-user-select: none;
	user-select: none;
}

	.swal2-icon.swal2-error .swal2-x-mark {
		position: relative;
		-webkit-box-flex: 1;
		-ms-flex-positive: 1;
		flex-grow: 1;
	}

.swal2-animate-error-icon .swal2-x-mark {
	-webkit-animation: swal2-animate-error-x-mark 0.5s;
	animation: swal2-animate-error-x-mark 0.5s;
}

.swal2-icon.swal2-error [class^='swal2-x-mark-line'][class$='left'] {
	left: 1.0625em;
	-webkit-transform: rotate(45deg);
	transform: rotate(45deg);
}

.swal2-icon.swal2-error [class^='swal2-x-mark-line'][class$='right'] {
	right: 1em;
	-webkit-transform: rotate(-45deg);
	transform: rotate(-45deg);
}

.swal2-icon.swal2-error [class^='swal2-x-mark-line'] {
	display: block;
	position: absolute;
	top: 2.3125em;
	width: 2.9375em;
	height: .3125em;
	border-radius: .125em;
	background-color: #fd3995;
}

[class^='swal2'] {
	-webkit-tap-highlight-color: transparent;
}

.swal2-title {
	position: relative;
	max-width: 100%;
	margin: 0 0 1em;
	padding: 0;
	color: #595959 !important;
	font-size: 1.0625em;
	font-weight: 600;
	text-align: center;
	text-transform: none;
	word-wrap: break-word;
}

.swal2-title {
	font-weight: 500 !important;
}

.swal2-content {
	z-index: 1;
	-webkit-box-pack: center;
	-ms-flex-pack: center;
	justify-content: center;
	margin: 0;
	padding: 0;
	color: #909090;
	font-size: 0.875em;
	font-weight: 300;
	line-height: normal;
	text-align: center;
	word-wrap: break-word;
}

.swal2-styled, .swal2-content {
	font-weight: 400 !important;
}

.swal2-actions {
	z-index: 1;
	-ms-flex-wrap: wrap;
	flex-wrap: wrap;
	-webkit-box-align: center;
	-ms-flex-align: center;
	align-items: center;
	-webkit-box-pack: center;
	-ms-flex-pack: center;
	justify-content: center;
	width: 100%;
	margin: 1.25em auto 0;
}

	.swal2-actions:not(.swal2-loading) .swal2-styled:active {
		background-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0, 0, 0, 0.2)), to(rgba(0, 0, 0, 0.2)));
		background-image: linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2));
	}

	.swal2-actions:not(.swal2-loading) .swal2-styled:hover {
		background-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0, 0, 0, 0.1)), to(rgba(0, 0, 0, 0.1)));
		background-image: linear-gradient(rgba(0, 0, 0, 0.1), rgba(0, 0, 0, 0.1));
		text-decoration: none;
	}

.swal2-styled.swal2-confirm {
	border: 0;
	border-radius: 0.25em;
	background: initial;
	background-color: #886ab5;
	color: #fff;
	font-size: 0.875em;
}

.swal2-styled:not([disabled]) {
	cursor: pointer;
}

.swal2-styled {
	margin: .3125em;
	padding: .625em 2em;
	-webkit-box-shadow: none;
	box-shadow: none;
	font-weight: 500;
}

.swal2-styled {
	margin: .3125em;
	padding: .625em 2em;
	-webkit-box-shadow: none;
	box-shadow: none;
	font-weight: 500;
}

</style>
<div class="swal2-container swal2-center swal2-fade swal2-shown" style="overflow-y: auto;">
    <div aria-labelledby="swal2-title" class="swal2-popup swal2-modal swal2-show" style="display: flex;">
        <div class="swal2-header">
            <div class="swal2-icon swal2-info" style="display: flex;"></div>
            <h2 class="swal2-title">
                <asp:Label ID="lblErrorDesc" Text="<%$ Resources:labels, accessdenied %>" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="swal2-content">
            <div id="swal2-content" style="display: block;">
            </div>
        </div>
        <div class="swal2-actions" style="display: flex;">
            <a class="btn btn-primary" style="display: inline-block;" href="javascript:history.go(-1);"><%=Resources.labels.back %></a>
        </div>
    </div>
</div>