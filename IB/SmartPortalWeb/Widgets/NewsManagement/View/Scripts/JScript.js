

function SelectCbx(obj) {
    var count = document.getElementById('aspnetForm').elements.length;
    var elements = document.getElementById('aspnetForm').elements;
    if (obj.checked) {
        for (i = 0; i < count; i++) {
            if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                elements[i].checked = true;
                elements[i].parentNode.parentNode.className = "hightlight";
            }
        }

    } else {
        for (i = 0; i < count; i++) {
            if (elements[i].type == 'checkbox' && elements[i].id != 'ctl00_ContentPlaceHolder_Content_ctl00_ctl00_ctl00_gvCustomerView_ctl01_cbxSelectAll') {
                elements[i].checked = false;
                elements[i].parentNode.parentNode.className = "nohightlight";
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

