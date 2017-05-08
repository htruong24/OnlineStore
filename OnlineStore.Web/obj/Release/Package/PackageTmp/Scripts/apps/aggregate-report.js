function AggregateReport() {
    this.monthlyReport = new MonthlyReport();
    this.weeklyReport = new WeeklyReport();
    this.dailyReport = new DailyReport();
}

AggregateReport.prototype.bindEvents = function() {
    this.monthlyReport.bindEvents();
    this.weeklyReport.bindEvents();
    this.dailyReport.bindEvents();
};


function MonthlyReport() {
    this.form = {
        txtMonthlyFromDate: '#txtMonthlyFromDate',
        txtMonthlyToDate: '#txtMonthlyToDate',
        btnMonthlyExtractCsv: '#btnMonthlyExtractCsv'
    };
}

MonthlyReport.prototype.bindEvents = function () {
    var $this = this;
    $(this.form.txtMonthlyFromDate).datepicker(
        {
            dateFormat: "yy/mm",
            changeMonth: true,
            changeYear: true,
            maxDate: '0y 0m',
            showButtonPanel: true,
            onClose: function (dateText, inst) {
                function isDonePressed() {
                    return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
                }
                if (isDonePressed()) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1)).trigger('change');
                    $(this).focusout(); //Added to remove focus from datepicker input box on selecting date
                } else {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
                
                var selDate = $(this).val();
                if (selDate.length > 0) {
                    var start = new Date(selDate);
                    $($this.form.txtMonthlyToDate).datepicker('option', 'minDate', start);
                }
                var endDate = $($this.form.txtMonthlyToDate);
                if (endDate.val() != '') {
                    var testStartDate = new Date(dateText);
                    var testEndDate = new Date(endDate.val());
                    if (Date.parse(testStartDate) > Date.parse(testEndDate))
                        endDate.datepicker('setDate', new Date(dateText));
                }
                else {
                    endDate.datepicker('setDate', new Date(dateText));
                }
            },
            beforeShow: function (dateText, inst) {
                $(".ui-datepicker-calendar").hide();
                if (dateText.length > 0) {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
            }
        }).bind("focus blur", function () {
            $(".ui-datepicker-calendar").addClass('hide');
        });

    $(this.form.txtMonthlyToDate).datepicker(
        {
            dateFormat: "yy/mm",
            changeMonth: true,
            changeYear: true,
            maxDate: '0y 0m',
            showButtonPanel: true,
            onClose: function (dateText, inst) {
                function isDonePressed() {
                    return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
                }
                if (isDonePressed()) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1)).trigger('change');
                    $(this).focusout(); //Added to remove focus from datepicker input box on selecting date
                } else {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
            },
            beforeShow: function (dateText, inst) {
                $(".ui-datepicker-calendar").hide();
                if (dateText.length > 0) {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
            }
        }).bind("focus blur", function () {
            $(".ui-datepicker-calendar").addClass('hide');
        });
    
    $(this.form.txtMonthlyFromDate).datepicker('setDate', new Date());
    $(this.form.txtMonthlyToDate).datepicker('option', 'minDate', new Date());
    $(this.form.txtMonthlyToDate).datepicker('setDate', new Date());
    

    $(this.form.btnMonthlyExtractCsv).click(function () {
        $this.exportCsv();
    });

};

MonthlyReport.prototype.exportCsv = function () {
    var $this = this;
    var fromMonth = $($this.form.txtMonthlyFromDate).val();
    var toMonth = $($this.form.txtMonthlyToDate).val();
    
    var searchData = {
        fromMonth: fromMonth,
        toMonth: toMonth
    };
    
    $.fileDownload('/AggregateReport/ExportMonthlyCsv', {
        data: searchData,
        httpMethod: "POST",
        prepareCallback: function (url) {
            ui.showPageLoading();
        },
        successCallback: function (url) {
            ui.hidePageLoading();
        },
        failCallback: function (responseHtml, url) {
            jAlert(Resources.Message_FileNotFound, Resources.Error, function () {
                ui.hidePageLoading();
            });
        }
    });
};


function WeeklyReport() {
    this.form = {
        txtWeeklyFromDate: '#txtWeeklyFromDate',
        txtWeeklyToDate: '#txtWeeklyToDate',
        slFromWeek: '#slFromWeek',
        slToWeek: '#slToWeek',
        btnWeeklyExtractCsv: '#btnWeeklyExtractCsv'
    };
}

WeeklyReport.prototype.bindEvents = function () {
    var $this = this;

    $(this.form.slFromWeek).change(function() {
        $this.bindToWeek();
    });

    $(this.form.slFromWeek).empty();
    $(this.form.slToWeek).empty();

    var currentDate = new Date();
    var count = common.getTotalWeeksOfMonth(currentDate.getFullYear(), currentDate.getMonth() + 1);
    for (var i = 1; i <= count; i++) {
        $(this.form.slFromWeek).append("<option value='" + i + "'>週" + i + "</option>");
        $(this.form.slToWeek).append("<option value='" + i + "'>週" + i + "</option>");
    }

    $(this.form.txtWeeklyFromDate).datepicker(
        {
            dateFormat: "yy/mm",
            changeMonth: true,
            changeYear: true,
            maxDate: '0y 0m',
            showButtonPanel: true,
            onClose: function (dateText, inst) {
                function isDonePressed() {
                    return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
                }
                if (isDonePressed()) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1)).trigger('change');
                    $(this).focusout(); //Added to remove focus from datepicker input box on selecting date
                } else {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
                
                var selDate = $(this).val();
                if (selDate.length > 0) {
                    var start = new Date(selDate);
                    $($this.form.txtWeeklyToDate).datepicker('option', 'minDate', start);
                }
                var endDate = $($this.form.txtWeeklyToDate);
                if (endDate.val() != '') {
                    var testStartDate = new Date(dateText);
                    var testEndDate = new Date(endDate.val());
                    if (Date.parse(testStartDate) > Date.parse(testEndDate))
                        endDate.datepicker('setDate', new Date(dateText));
                }
                else {
                    endDate.datepicker('setDate', new Date(dateText));
                }
                $this.bindToWeek();
            },
            beforeShow: function (dateText, inst) {
                $(".ui-datepicker-calendar").hide();
                if (dateText.length > 0) {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
            }
        }).bind("focus blur", function () {
            $(".ui-datepicker-calendar").addClass('hide');
        });

    $(this.form.txtWeeklyToDate).datepicker(
        {
            dateFormat: "yy/mm",
            changeMonth: true,
            changeYear: true,
            maxDate: '0y 0m',
            showButtonPanel: true,
            onClose: function (dateText, inst) {
                function isDonePressed() {
                    return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
                }
                if (isDonePressed()) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).datepicker('setDate', new Date(year, month, 1)).trigger('change');
                    $(this).focusout(); //Added to remove focus from datepicker input box on selecting date
                } else {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
                $this.bindToWeek();
            },
            beforeShow: function (dateText, inst) {
                $(".ui-datepicker-calendar").hide();
                if (dateText.length > 0) {
                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
                    $(this).datepicker('setDate', new Date(dateText));
                }
            }
        }).bind("focus blur", function () {
            $(".ui-datepicker-calendar").addClass('hide');
        });
    
    $(this.form.txtWeeklyFromDate).datepicker('setDate', new Date());
    $(this.form.txtWeeklyToDate).datepicker('option', 'minDate', new Date());
    $(this.form.txtWeeklyToDate).datepicker('setDate', new Date());

    $(this.form.btnWeeklyExtractCsv).click(function () {
        $this.exportCsv();
    });
};

WeeklyReport.prototype.bindToWeek = function() {
    var $this = this;

    // StartToWeek
    var startToWeek = 1;

    // From Week - To Week
    var weekFrom = common.parseInt($($this.form.slFromWeek).val());
    var weekTo = common.parseInt($($this.form.slToWeek).val());

    // From Month - To Month DateTime
    var fromWeekly = new Date($($this.form.txtWeeklyFromDate).val());
    var toWeekly = new Date($($this.form.txtWeeklyToDate).val());
    fromWeekly.setHours(0, 0, 0, 0);
    toWeekly.setHours(0, 0, 0, 0);

    // Total From - Total To
    var totalFrom = common.getTotalWeeksOfMonth(fromWeekly.getFullYear(), fromWeekly.getMonth() + 1);
    var totalTo = common.getTotalWeeksOfMonth(toWeekly.getFullYear(), toWeekly.getMonth() + 1);

    // Current Date
    var currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0);
    currentDate.setDate(1);

    if ((weekFrom == 1 && (totalFrom == 4 || totalFrom == 5)) || (weekFrom == 2 && totalFrom == 5)) {
        toWeekly = fromWeekly;
        totalTo = weekFrom + 3;
        startToWeek = weekFrom;

    } else {
        if (Date.parse(fromWeekly) == Date.parse(currentDate)) {
            toWeekly = fromWeekly;
            totalTo = totalFrom;
            startToWeek = weekFrom;
        } else {
            toWeekly.setMonth(fromWeekly.getMonth() + 1);
            totalTo = 4 - (totalFrom - weekFrom + 1);
        }
    }
    
    $(this.form.slFromWeek).empty();
    for (var i = 1; i <= totalFrom; i++) {
        $(this.form.slFromWeek).append("<option value='" + i + "'>週" + i + "</option>");
    }

    $(this.form.slToWeek).empty();
    for (var i = startToWeek; i <= totalTo; i++) {
        $(this.form.slToWeek).append("<option value='" + i + "'>週" + i + "</option>");
    }

    if ($($this.form.slFromWeek + " option[value=" + weekTo + "]").length > 0)
        $($this.form.slFromWeek).val(weekFrom);
    
    if ($($this.form.slToWeek + " option[value=" + weekTo + "]").length > 0)
        $($this.form.slToWeek).val(weekTo);
    
    $(this.form.txtWeeklyToDate).datepicker('option', 'minDate', toWeekly);
    $(this.form.txtWeeklyToDate).datepicker('option', 'maxDate', toWeekly);
    $($this.form.txtWeeklyToDate).datepicker('setDate', toWeekly);
};

WeeklyReport.prototype.exportCsv = function() {
    var $this = this;
    var fromMonth = $($this.form.txtWeeklyFromDate).val();
    var toMonth = $($this.form.txtWeeklyToDate).val();
    var fromWeek = common.parseInt($($this.form.slFromWeek).val());
    var toWeek = common.parseInt($($this.form.slToWeek).val());
    
    var searchData = {
        fromMonth: fromMonth,
        toMonth: toMonth,
        fromWeek: fromWeek,
        toWeek: toWeek
    };

    $.fileDownload('/AggregateReport/ExportWeeklyCsv', {
        data: searchData,
        httpMethod: "POST",
        prepareCallback: function (url) {
            ui.showPageLoading();
        },
        successCallback: function (url) {
            ui.hidePageLoading();
        },
        failCallback: function (responseHtml, url) {
            jAlert(Resources.Message_FileNotFound, Resources.Error, function () {
                ui.hidePageLoading();
            });
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: '/AggregateReport/ExportWeeklyCsv',
    //    contentType: 'application/json',
    //    data: JSON.stringify(searchData),
    //    success: function(data) {
    //        if (data.ErrorCode == "0") {
    //            jAlert("Export Success", Resources.Notice);
    //        } else {
    //            jAlert(data.ErrorMessage, Resources.Notice);
    //        }
    //    },
    //    error: function(jqXHR, error, errorThrown) {
    //    }
    //});
};


function DailyReport() {
    this.form = {
        txtDailyFromDate: '#txtDailyFromDate',
        txtDailyToDate: '#txtDailyToDate',
        btnDailyExtractCsv: '#btnDailyExtractCsv'
    };
}

DailyReport.prototype.bindEvents = function () {
    var $this = this;
    $(this.form.txtDailyFromDate).datepicker({
        dateFormat: 'yy/mm/dd',
        maxDate: '0y 0m',
        showButtonPanel: true,
        onClose: function (dateText, inst) {
            $(this).datepicker('option', 'defaultDate', new Date(dateText));
            $(this).datepicker('setDate', new Date(dateText));

            var selDate = $(this).val();
            if (selDate.length > 0) {
                var start = new Date(selDate);
                $($this.form.txtDailyToDate).datepicker('option', 'minDate', start);
            }
            var endDate = $($this.form.txtDailyToDate);
            if (endDate.val() != '') {
                var testStartDate = new Date(dateText);
                var testEndDate = new Date(endDate.val());
                if (Date.parse(testStartDate) > Date.parse(testEndDate))
                    endDate.datepicker('setDate', new Date(dateText));
            }
            else {
                endDate.datepicker('setDate', new Date(dateText));
            }

            //Current date
            var limitDate = new Date();
            limitDate.setHours(0, 0, 0, 0);

            // From date + 30
            var fromDate = new Date(dateText);
            fromDate.setHours(0, 0, 0, 0);
            fromDate.setDate(fromDate.getDate() + 30);

            if (Date.parse(fromDate) >= Date.parse(limitDate))
                $($this.form.txtDailyToDate).datetimepicker('option', 'maxDate', limitDate);
            else
                $($this.form.txtDailyToDate).datetimepicker('option', 'maxDate', fromDate);
        },
        onSelect: function (selectedDateTime) {
            var start = $(this).datetimepicker('getDate');
            $($this.form.txtDailyToDate).datetimepicker('option', 'minDate', new Date(start.getTime()));
        }
    });
    
    $(this.form.txtDailyToDate).datepicker({
        dateFormat: 'yy/mm/dd',
        maxDate: '0y 0m',
        showButtonPanel: true,
        onClose: function (dateText, inst) {
            $(this).datepicker('option', 'defaultDate', new Date(dateText));
            $(this).datepicker('setDate', new Date(dateText));
        },
        onSelect: function (selectedDateTime) {
            //var end = $(this).datetimepicker('getDate');
            //$($this.form.txtDailyFromDate).datetimepicker('option', 'maxDate', new Date(end.getTime()));
        }
    });
    
    $(this.form.txtDailyFromDate).datepicker('setDate', new Date());
    $(this.form.txtDailyToDate).datepicker('option', 'minDate', new Date());
    $(this.form.txtDailyToDate).datepicker('setDate', new Date());
    
    $(this.form.btnDailyExtractCsv).click(function () {
        $this.exportCsv();
    });
};

DailyReport.prototype.exportCsv = function () {
    var $this = this;
    var fromDate = $($this.form.txtDailyFromDate).val();
    var toDate = $($this.form.txtDailyToDate).val();
    
    var searchData = {
        fromDate: fromDate,
        toDate: toDate
    };

    $.fileDownload('/AggregateReport/ExportDailyCsv', {
        data: searchData,
        httpMethod: "POST",
        prepareCallback: function (url) {
            ui.showPageLoading();
        },
        successCallback: function (url) {
            ui.hidePageLoading();
        },
        failCallback: function (responseHtml, url) {
            jAlert(Resources.Message_FileNotFound, Resources.Error, function () {
                ui.hidePageLoading();
            });
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: '/AggregateReport/ExportDailyCsv',
    //    contentType: 'application/json',
    //    data: JSON.stringify(searchData),
    //    success: function (data) {
    //        if (data.ErrorCode == "0") {
    //            jAlert("Export Success", Resources.Notice);
    //        } else {
    //            jAlert(data.ErrorMessage, Resources.Notice);
    //        }
    //    },
    //    error: function (jqXHR, error, errorThrown) { }
    //});
};


//Sub version


//function MonthlyReport() {
//    this.form = {
//        txtMonthlyFromDate: '#txtMonthlyFromDate',
//        txtMonthlyToDate: '#txtMonthlyToDate',
//        btnMonthlyExtractCsv: '#btnMonthlyExtractCsv'
//    };
//}

//MonthlyReport.prototype.bindEvents = function () {
//    var $this = this;
//    $(this.form.txtMonthlyFromDate).datepicker(
//        {
//            dateFormat: "yy/mm",
//            changeMonth: true,
//            changeYear: true,
//            maxDate: '0y 0m',
//            showButtonPanel: true,
//            onClose: function (dateText, inst) {
//                function isDonePressed() {
//                    return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
//                }
//                if (isDonePressed()) {
//                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
//                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
//                    $(this).datepicker('setDate', new Date(year, month, 1)).trigger('change');
//                    $(this).focusout(); //Added to remove focus from datepicker input box on selecting date
//                } else {
//                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
//                    $(this).datepicker('setDate', new Date(dateText));
//                }
//            },
//            beforeShow: function (dateText, inst) {
//                $(".ui-datepicker-calendar").hide();
//                if (dateText.length > 0) {
//                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
//                    $(this).datepicker('setDate', new Date(dateText));
//                }
//            }
//        }).bind("focus blur", function () {
//            $(".ui-datepicker-calendar").addClass('hide');
//        });

//    $(this.form.txtMonthlyToDate).datepicker(
//        {
//            dateFormat: "yy/mm",
//            changeMonth: true,
//            changeYear: true,
//            maxDate: '0y 0m',
//            showButtonPanel: true,
//            onClose: function (dateText, inst) {
//                function isDonePressed() {
//                    return ($('#ui-datepicker-div').html().indexOf('ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all ui-state-hover') > -1);
//                }
//                if (isDonePressed()) {
//                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
//                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
//                    $(this).datepicker('setDate', new Date(year, month, 1)).trigger('change');
//                    $(this).focusout(); //Added to remove focus from datepicker input box on selecting date
//                } else {
//                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
//                    $(this).datepicker('setDate', new Date(dateText));
//                }
                
//                var selDate = $(this).val();
//                if (selDate.length > 0) {
//                    var start = new Date(selDate);
//                    start.setMonth(start.getMonth() - 6);
//                    $($this.form.txtMonthlyFromDate).datepicker('option', 'minDate', start);
//                    var end = new Date(selDate);
//                    $($this.form.txtMonthlyFromDate).datepicker('option', 'maxDate', end);
//                }
                
//                var fromDate = $($this.form.txtMonthlyFromDate);
//                if (fromDate.val() == '') {
//                    fromDate.datepicker('setDate', new Date(dateText));
//                }
//            },
//            beforeShow: function (dateText, inst) {
//                $(".ui-datepicker-calendar").hide();
//                if (dateText.length > 0) {
//                    $(this).datepicker('option', 'defaultDate', new Date(dateText));
//                    $(this).datepicker('setDate', new Date(dateText));
//                }
//            }
//        }).bind("focus blur", function () {
//            $(".ui-datepicker-calendar").addClass('hide');
//        });


//    var currentDate = new Date();
//    currentDate.setMonth(currentDate.getMonth() - 6);
//    $(this.form.txtMonthlyFromDate).datepicker('option', 'minDate', currentDate);
//    $(this.form.txtMonthlyFromDate).datepicker('setDate', new Date());
//    $(this.form.txtMonthlyToDate).datepicker('setDate', new Date());


//    $(this.form.btnMonthlyExtractCsv).click(function () {
//        $this.exportCsv();
//    });

//};

//MonthlyReport.prototype.exportCsv = function () {
//    var $this = this;
//    var fromMonth = new Date($($this.form.txtMonthlyToDate).val());
//    var toMonth = new Date($($this.form.txtMonthlyToDate).val());

//    var searchData = {
//        fromMonth: fromMonth,
//        toMonth: toMonth
//    };

//    $.ajax({
//        type: "POST",
//        url: '/AggregateReport/ExportMonthlyCsv',
//        contentType: 'application/json',
//        data: JSON.stringify(searchData),
//        success: function (data) {
//            if (data.ErrorCode == "0") {
//                jAlert("Export Success", Resources.Notice);
//            } else {
//                jAlert(data.ErrorMessage, Resources.Notice);
//            }
//        },
//        error: function (jqXHR, error, errorThrown) { }
//    });
//};