function RemoveWidget(id) {
    if (confirm('Are you sure you want to remove the widget?')) {
        sendRequestRemoveWidgetInPage(id);
        document.location.reload();
    }
}
function rem() {
    alert('123');
}