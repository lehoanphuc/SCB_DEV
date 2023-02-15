function swalError(msgContent) {
    Swal.fire({
        icon: 'error',
        text: msgContent
    });
}

function swalWarning(msgContent) {
    Swal.fire({
        icon: 'warning',
        text: msgContent
    });
}

function swalWarningFocus(obj, msgContent) {
    Swal.fire({
        icon: 'warning',
        text: msgContent,
        onAfterClose: () => {
            setTimeout(() => obj.focus(), 110);
        }
    });
}

function swalSuccess(msgContent) {
    Swal.fire({
        icon: 'success',
        text: msgContent
    });
}

function sweetAlertConfirm(btnDelete) {
    if (btnDelete.dataset.confirmed) {
        btnDelete.dataset.confirmed = false;
        return true;
    } else {
        event.preventDefault();
        Swal.fire({
            title: 'Are you sure to delete?',
            text: "You won't be able to revert this!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            confirmButtonColor: '#337ab7',
            cancelButtonColor: '#d9534f',
        })
        .then((result) => {
            if (result.isConfirmed) {
                btnDelete.dataset.confirmed = true;
                btnDelete.click();
            }
        });
    }
}



function validateEmpty(id, msg) {
    var obj = document.getElementById(id);
    if (obj.value.trim() == "") {
        swalWarningFocus(obj, msg);
        return false;
    }
    else {
        return true;
    }
}

function validateMoney(id, msg) {
    var obj = document.getElementById(id);
    if (obj.value == "0" || obj.value == "") {
        swalWarningFocus(obj, msg);
        return false;
    }
    else {
        return true;
    }
}



function validateSameAccount(id1, id2, msg) {
    if (document.getElementById(id1).value.trim() == document.getElementById(id2).value.trim()) {
        swalWarning(msg);
        return false;
    }
    else {
        return true;
    }
}
function validateSameValue(id1, id2, msg) {
    if (document.getElementById(id1).value.trim() == document.getElementById(id2).value.trim()) {
        return true;
    }
    else {
        swalWarning(msg);
        return false;
    }
}
window.mobileAndTabletcheck = function () {
    var check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino|android|ipad|playbook|silk/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true; })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
};
function ValidateLimit(obj, maxchar) {
    if (this.id) obj = this;
    replaceSQLChar(obj);
    var remaningChar = maxchar - obj.value.length;

    if (remaningChar <= 0) {
        obj.value = obj.value.substring(maxchar, 0);
    }
}
function replaceSQLChar(obj) {
    if (obj.value.length > 0) {
        obj.value = obj.value.replace(/'|!|#|\$|%|\^|&|\*|;|xp_|script|iframe|delete|drop|exec|execute|--|<|>/g, "");
    }
}
var check = function (string) {
    var specialChars = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`-="
    for (i = 0; i < specialChars.length; i++) {
        if (string.indexOf(specialChars[i]) > -1) {
            return true
        }
    }
    return false;
}
function IsDateGreater(DateValue1, DateValue2, msg) {
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
    if (DaysDiff > 0)
        return true;
    else
        swalWarning(msg);
    return false;
}
function validateFormTo(id, id2, msg) {

    var obj = document.getElementById(id2);
    if (document.getElementById(id2).value.replace(/\$|\,/g, '') - document.getElementById(id).value.replace(/\$|\,/g, '') < 0) {
        swalWarningFocus(obj, msg);
        return false;
    }
    else {
        return true;
    }
}
function countdownOTP(number) {
    //clearInterval(interval);
    var countdownTimer = {
        init: function () {
            this.render();
        },
        render: function () {
            var totalTime = number * 1,
                display = this.$time;
            this.startTimer(totalTime, display);
            $('.countdown_time').removeClass('countdown__time--red').removeClass('countdown__time--orange');
            $('.countdown').removeClass('hidden');
            $('.btnSendOTP').addClass('hidden');
        },
        startTimer: function (duration) {
            var timer = duration, minutes, seconds;
            var interval = setInterval(function () {
                minutes = parseInt(timer / 60, 10);
                seconds = parseInt(timer % 60, 10);
                minutes = minutes < 10 ? '0' + minutes : minutes;
                seconds = seconds < 10 ? '0' + seconds : seconds;
                $('.countdown_time').html(minutes + ':' + seconds);
                if (--timer < 0) {
                    clearInterval(interval);
                };
                if (timer < 20) {
                    $('.countdown_time').addClass('countdown__time--orange');
                };
                if (timer < 5) {
                    $('.countdown_time').addClass('countdown__time--red');
                };
                if (timer == 0) {
                    $('.countdown').addClass('hidden');
                    $('.btnSendOTP').removeClass('hidden');
                    clearInterval(interval);
                }
            }, 1000);
        },
    };
    countdownTimer.init();
}

function resetOTP() {
    $('.countdown').addClass('hidden');
    $('.btnSendOTP').removeClass('hidden');
}


function validateyearold(id, msg, year) {
    var obj = document.getElementById(id);
    var date = new Date();
    var flag = true;
    var dateNow = parseInt(date.getDate());
    var monthNow = parseInt(date.getMonth()+1);
    var yearNow = parseInt(date.getFullYear() - year);
    var parts = document.getElementById(id).value.split('/');
    var dateOld = parseInt(parts[0]);
    var monthOld = parseInt(parts[1]);
    var yearOld = parseInt(parts[2]);
    if (yearNow< yearOld) {
        swalWarningFocus(obj, msg);
        return false;
    }
    if (monthNow < monthOld) {
        swalWarningFocus(obj, msg);
        return false;
    }
    if (dateNow < dateOld) {
        swalWarningFocus(obj, msg);
        return false;
    }
    return true;
}