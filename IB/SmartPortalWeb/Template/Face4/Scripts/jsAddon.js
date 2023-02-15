function queryString(key) {
    return queryStringValueArray()[key];
}
function queryStringKeyArray(){
    var qrStr = window.location.search;
    var spQrStr = qrStr.substring(1);
    var arrQrStrKeys = new Array();
    var arr = spQrStr.split('&');
    for (var i = 0; i < arr.length; i++) {
        var index = arr[i].indexOf('=');
        var key = arr[i].substring(0, index);
        arrQrStrKeys[i] = key;
    }
    return arrQrStrKeys;
}
function queryStringValueArray() {
    var qrStr = window.location.search;
    var spQrStr = qrStr.substring(1);
    var arrQrStr = new Array();
    var arr = spQrStr.split('&');
    for (var i = 0; i < arr.length; i++) {
        var index = arr[i].indexOf('=');
        var key = arr[i].substring(0, index);
        var val = arr[i].substring(index + 1);
        arrQrStr[key] = val;
    }
    document.getElementById("").getAttribute("id");
   
    return arrQrStr;
}
function getHeight() {
    var myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        myHeight = window.innerHeight;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        myHeight = document.documentElement.clientHeight;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        myHeight = document.body.clientHeight;
    }
    return myHeight - 15;
}
function setBottomDiv() {
    try{
        document.getElementsByName("bottomDiv")[0].top = getHeight() - 100;
    }catch(e){
    }

}