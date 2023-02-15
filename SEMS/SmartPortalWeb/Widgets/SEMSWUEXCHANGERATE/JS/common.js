function validateEmpty(id, msg) {
    if (document.getElementById(id).value == "") {
        window.alert(msg);
        return false;
    }
    else {
        return true;
    }
}
function validateMoney(id, msg) {
    if (document.getElementById(id).value == "0" || document.getElementById(id).value == "") {
        window.alert(msg);
        return false;
    }
    else {
        return true;
    }
}
function validateFormTo(id, id2, msg) {

    if (parseFloat(document.getElementById(id2).value.replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "")) < parseFloat(document.getElementById(id).value.replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", "").replace(",", ""))) {

        window.alert(msg);
        return false;
    }
    else {
        return true;
    }
}