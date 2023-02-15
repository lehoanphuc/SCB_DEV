function ConfirmAlert(msg, ismulti = false) {
    if (ismulti) {
    }
    return confirm(msg);
}
$('[data-close]').click(function () {
    var popup = $(this).data('close');
    $('#' + popup).modal('hide');
})

function closeModal(id) {
    $('.modal-backdrop').first().remove();
    $('body').css('overflow', 'auto');
    $('#' + id).modal('hide');
}

function openModal(id) {
    $('#' + id).modal('show');
}

function setActiveTab(element, idtab = 'tabs') {
    var tabName = $('#' + element).val();
    $('#' + idtab + ' a[href="#' + tabName + '"]').tab('show');
    //$('#' + idtab + ' a').click(function () {

    //    var a= $('#' + element).val();
    //    $('#' + element).val($(this).attr("href").replace("#", ""));

    //});
}

function onReady() {
    $('.footable').footable();
    $(".select2").select2();

    //$("[data-mask]").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });

    var width = $(window).width();
    if (width > 768) {
        $(".dateselect").attr("readonly", false);
    }
    else {
        $(".dateselect").attr("readonly", true);
    }

    $('.dateselect').datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy',
        language: 'en',
        todayBtn: "linked",
        allowInputToggle: false,
        ignoreReadonly: true,
        endDate: new Date()
    }).on("changeDate", function (e) {

        if ($(this).attr('data-level') === 0) {
            var from = $(this).val();
            var start = process(from);
            $(".dateselect[data-level='1'][data-name='" + $(this).attr('data-name') + "']").datepicker({
                startDate: start,
                endDate: new Date()
            })
        }

        function process(date) {
            var parts = date.split("/");
            return new Date(parts[2], parts[1] - 1, parts[0]);
        }
    });

    $('.dateselect1').datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy',
        language: 'en',
        todayBtn: "linked",
        allowInputToggle: false,
        ignoreReadonly: true
    });


    //$('input[data-idpopup]').click(function () {
    //    var elType = $(this).data("elcurrent");
    //    var arg = $(this).data("feature");
    //    var valCurrent = $('#' + elType).val();
    //    if (arg == valCurrent) {
    //        var popup = $(this).data("idpopup");
    //        $('#' + popup).modal('show');
    //        return false;
    //    }
    //});

    // check lai cac value da dc check truoc khi postback
    $(".keepvalue").each(function () {
        //if ($(this).attr("name") == 'itemctl00_ctl15_Widget1_txtCOUNTRY_ID258_LOS_Country') {
        //    var a = CheckItem(this);
        //    $(this).prop('checked', CheckItem(this));
        //}
        $(this).prop('checked', CheckItem(this));
    });
    $('[data-close]').click(function () {
        var popup = $(this).data('close');
        $('#' + popup).modal('hide');
    })

    $('[data-check]').click(function () {
        var name = $(this).data('check');
        var display = $('input[name=' + name + '][data-display]:checkbox').data('display');
        setTextPopup(name, display);
        var popup = $(this).data('popup');
        $('#' + popup).modal('hide');
    });

    $('[name=integer-default]').maskNumber({ integer: true });
    $('[data-format=integer-default]').maskNumber({ integer: true });

    //$('[name=currency-default]').maskNumber();
    //$('[name=currency-data-attributes]').maskNumber();
    //$('[name=currency-configuration]').maskNumber({ decimal: '_', thousands: '*' });
    //$('[name=integer-data-attribute]').maskNumber({ integer: true });
    //$('[name=integer-configuration]').maskNumber({ integer: true, thousands: '_' });



}

function CheckEmailFormat(emailField) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test(emailField.value) == false) {
        $(".email-valid").addClass("border-color-red");
        return false;
    }
    else {
        $(".email-valid").removeClass("border-color-red");
        return true;
    }

    return true;

}

function CheckNull(Field) {

    if (!Field.value) {
        Field.style.borderColor = "red";
        return false;
    }
    else {
        Field.style.borderColor = "";
        return true;
    }

    return true;

}

$(document).ready(function () {
    $(".email-place").attr("placeholder", "example@gmail.com");
    $(".datetime-place").attr("placeholder", "DD/MM/YYY");
});

function ConfigRatio(el) {
    var name = $(el).attr('name');
    var ctValue = $('input[name=' + name + '][data-control]');
    var ivalue = $(ctValue).data("control");
    var idisplay = $(ctValue).data("display");
    $('#' + ivalue).val('#' + $(el).val());
    $('#' + idisplay).val($(el).data("text"));
    //setTextPopup(name, $(checkboxAll).data('display'));
}

function ConfigRatioPlus(el) {
    var name = $(el).attr('name');
    var ctValue = $('input[name=' + name + '][data-control]');
    var ivalue = $(ctValue).data("control");
    var idisplay = $(ctValue).data("display");
    var idname = $(ctValue).data("name");
    $('#' + ivalue).val('#' + $(el).val());
    $('#' + idisplay).val($(el).data("text"));
    $('#' + idname).val($(el).data("name"));
    //setTextPopup(name, $(checkboxAll).data('display'));
}

function CheckboxAll(el) {
    var idlist = $(el).data("control");
    var idlistName = $(el).data("display");
    var name = $(el).attr("name");

    $('input[name=' + name + ']:checkbox').not(el).prop('checked', el.checked);
    var lst = '';
    if ($(el).is(":checked")) {
        var lstCheck = $('input[name=' + name + ']:checkbox');
        $.each(lstCheck, function (index, item) {
            lst += '#' + $(item).val();
        });
    }
    $('#' + idlist).val(lst);
    //setTextPopup(name, idlistName);
}

function onlyDotsAndNumbers(txt, event, len) {
    if (txt.value.length > len - 1) {
        return false;
    }
    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function FormatPercent(txt, event, len) {

    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode === 46) {
        if (txt.value.indexOf(".") < 100)
            return true;
        else
            return false;
    }
    if (txt.value.length === 0) {
          txt.value = txt.value + '0';
    }

    if (txt.value.indexOf(".") > 0) {
        var txtlen = txt.value.length;
        var dotpos = txt.value.indexOf(".");
        //Change the number here to allow more decimal points than 2
        if ((txtlen - dotpos) > 4)
            return false;
    }
    if (txt.value.length > 0) {
        if (!txt.value.includes('.'))
            txt.value = txt.value + '.';
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function FormatPercent2(txt, event, len) {

    if (txt.value.length > len) {
        return false;
    }
    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode === 46) {
        if (txt.value.indexOf(".") < 1)
            return true;
        else
            return false;
    }

    if (txt.value.indexOf(".") > 0) {
        var txtlen = txt.value.length;
        var dotpos = txt.value.indexOf(".");
        //Change the number here to allow more decimal points than 2
        if ((txtlen - dotpos) > 2)
            return false;
    }
    if (txt.value.length > 2) {
        if (!txt.value.includes('.'))
            txt.value = txt.value + '.';
    }
    
    if (txt.value.length > 2) {
        if (!txt.value.includes('.')) {
            if (txt.value > 100 || txt.value < 0) {
                txt.value = 100;
                return false;
            }
        } 
        else {
            var res = txt.value.split(".", 1);
            if (res >= 100) {
                txt.value = "100.00";
                alert("Maximum percent is 100 %");
                return false;
            }
            if (res < 0) {
                txt.value = "0.00";
                return false;
            }
        }   
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function ConfirmDelete(msg, ismulti = false) {
    if (ismulti) {
        //Statement
    }
    return confirm(msg);
}

function CheckDelete(el) {
    //var name = $(el).attr('name');
    //var checkboxAll = $('input[name=' + name + '][data-control]');
    //var idlst = $(checkboxAll).data("control");
    //var lst = $('#' + idlst).val();
    //if (lst = null) {
    //    alert("You must select item");
    //}
    var name = $(el).attr('name');
    var checkboxAll = $('input[name=' + name + '][data-control]');
    var idlst = $(checkboxAll).data("control");
    var lst = $('#' + idlst).val();
    if ($(el).is(":checked")) {
        lst += '#' + $(el).val();
        if (ischeckall(name)) {
            $(checkboxAll).prop('checked', el.checked);
        }
    }
    else {
        lst = lst.replace('#' + $(el).val(), '');
        $(checkboxAll).prop('checked', el.checked);
    }
}

function CheckItem(el) {
    var name = $(el).attr('name');
    var checkboxAll = $('input[name=' + name + '][data-control]');
    var idlst = $(checkboxAll).data("control");
    var lst = $('#' + idlst).val();
    return lst.includes('#' + $(el).val());
}

function ConfigCheckbox(el) {
    var name = $(el).attr('name');
    var checkboxAll = $('input[name=' + name + '][data-control]');
    var idlst = $(checkboxAll).data("control");
    var lst = $('#' + idlst).val();
    if ($(el).is(":checked")) {
        lst += '#' + $(el).val();
        if (ischeckall(name)) {
            $(checkboxAll).prop('checked', el.checked);
        }
    }
    else {
        lst = lst.replace('#' + $(el).val(), '');
        $(checkboxAll).prop('checked', el.checked);
    }
    $('#' + idlst).val(lst);
    //setTextPopup(name, $(checkboxAll).data('display'));
}
function ischeckall(name) {
    var lstCheck = $('input[name=' + name + '].check:checkbox:not(:checked)');
    return lstCheck === null || lstCheck.length === 0;
}

if ($('#divToolbar').html().trim() == "") {
    $('#divToolbar').addClass("hidden");
}

var prm = Sys.WebForms.PageRequestManager.getInstance();
if (prm != null) {
    prm.add_endRequest(function(sender, e) {
        if (sender._postBackSettings.panelsToUpdate != null) {
            if ($('#divToolbar').html().trim() == "") {
                $('#divToolbar').addClass("hidden");
            }
        }
    });
};