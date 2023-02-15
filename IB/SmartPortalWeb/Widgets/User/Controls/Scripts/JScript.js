function aa() {
    //validate username
    if (document.getElementById('<%=txtUserName.ClientID%>').value == '') {
        alert('<%=Resources.labels.usernamerequire %>');
        return false;
    }
    else {
        return true;
    }
    //validate password
    if (document.getElementById('<%=txtPassword.ClientID%>').value == '') {
        alert('<%=Resources.labels.passwordrequire %>');
        return false;
    }
    else {
        return true;
    }
    //validate retype password
    if (document.getElementById('<%=txtRePassword.ClientID%>').value == '') {
        alert('<%=Resources.labels.retypepasswordrequire %>');
        return false;
    }
    else {
        return true;
    }
    //validate firstname
    if (document.getElementById('<%=txtFirstName.ClientID%>').value == '') {
        alert('<%=Resources.labels.firstnamerequire %>');
        return false;
    }
    else {
        return true;
    }
}