PrizeManagementExtract.prototype = new BaseGrid();
PrizeManagementExtract.prototype.constructor = new PrizeManagementExtract();

function PrizeManagementExtract() {
    this.formExtract = {        
        txtFromDate: '#txtFromDate',
        txtToDate: '#txtToDate',
        btnExtractCsv: '#btnExtractCsv'
    };

    this.ExtractUrl = '/PrizeManagementExtract/ExportCsv';
}

PrizeManagementExtract.prototype.bindEvents = function() {
    var $this = this;

    $(this.formExtract.txtFromDate).datetimepicker({
        dateFormat: 'yy/mm/dd',
        timeFormat: 'HH:mm',
        minTime: '00:00',
        maxTime: '00:00',
        maxDate: '0y 0m',
        showHour: false,
        showMinute: false,
        onClose: function(dateText, inst) {
            var endDate = $($this.formExtract.txtToDate);
            var testStartDate;
            if (endDate.val() != '') {
                testStartDate = new Date(dateText);
                var testEndDate = new Date(endDate.val());
                if (testStartDate > testEndDate) {
                    testStartDate.setHours(23, 59);
                    endDate.datepicker('setDate', testStartDate);
                }
            } else {
                if (dateText != "") {
                    testStartDate = new Date(dateText);
                    testStartDate.setHours(23, 59);
                     endDate.datepicker('setDate', testStartDate);
                } else {
                    endDate.val(dateText);
                }
                
            }
        },
        onSelect: function(selectedDateTime) {
            var start = $(this).datetimepicker('getDate');
            $($this.formExtract.txtToDate).datetimepicker('option', 'minDate', new Date(start.getTime()));
        }
    });
    
    $(this.formExtract.txtToDate).datetimepicker({
        dateFormat: 'yy/mm/dd',
        timeFormat: 'HH:mm',
        minTime: '23:59',
        maxTime: '23:59',
        showHour: false,
        showMinute: false,
        onClose: function (dateText, inst) {
            var startDate = $($this.formExtract.txtFromDate);

            if (startDate.val() != '') {
                var testStartDate = new Date(startDate.val());
                var testEndDate = new Date(dateText);
                if (testStartDate > testEndDate)
                    startDate.val(dateText);
            }
            else {
                startDate.val(dateText);
            }
        },
        onSelect: function (selectedDateTime) {
            var end = $(this).datetimepicker('getDate');
            $($this.formExtract.txtFromDate).datetimepicker('option', 'maxDate', new Date(end.getTime()));
        }
    });

    $(this.formExtract.btnExtractCsv).click(function (e) {
        $this.extractCsv();
    });
};

PrizeManagementExtract.prototype.setGridData = function () {
    var $this = this;

    this.GridId = '#gridPrizeExtract';
    this.PagerId = '#gridPrizeExtractPager';
    this.SortName = 'PrizeID';
    this.SortOrder = 'desc';
    this.Url = '/PrizeManagementExtract/GetPrizes';
    this.DataType = 'json';

    this.SerializeRowData = function (postData) {
        return JSON.stringify(postData);
    };

    this.SerializeGridData = function (postData) {
        return JSON.stringify(postData);
    };

    this.AjaxGridOptions = { contentType: "application/json" };

    this.AjaxRowOptions = { contentType: "application/json" };

    this.PostData = {};

    this.MType = 'POST';
    this.AutoWidth = true;
    this.ShrinkToFit = true;
    this.Height = 570;

    this.RowNum = 10;
    this.ViewRecords = true;
    this.PgButtons = true;
    this.PgInput = true;
    this.CmTemplate = { search: false };

    this.ColNames = ["Prize Id", "Prize Name", "Method", "Required number point", "Prize Description",
                    "Entry Start Time", "Entry End Time", "Prize Image", "",
                    "PrizeMethod", "PrizePoints"
    ];

    this.ColModel = [
        { name: 'PrizeId', index: 'PrizeId', key: true, width: 60, align: 'center' },
        { name: 'PrizeName', index: 'PrizeName', width: 130, editable: true },
        {
            name: 'PrizeMethodName', index: 'PrizeMethod', width: 80,
            editable: true, edittype: "select",
            editrules: { required: true },
            editoptions: { value: this.PrizeMethodValue },
        },
        { name: 'PrizePoints', index: 'PrizePoints', classes: 'text-nowrap' }, // ??
        {
            name: 'PrizeDescription', index: 'PrizeDescription',
            editable: true, edittype: "textarea", editoptions: { rows: "4", cols: "15" }
        },
        {
            name: 'EntryStartTime', index: 'EntryStartTime', width: 130, align: 'center',
            formatter: this.formatDateTime,
            unformat: this.unFormatDateTime
        },
        {
            name: 'EntryEndTime', index: 'EntryEndTime', width: 130, align: 'center',
            formatter: this.formatDateTime,
            unformat: this.unFormatDateTime
        },
        {
            name: 'PrizeImg', index: 'PrizeImg', width: 100,
            align: 'center', formatter: this.formatImage, unformat: this.unFormatImage
        },
        {
            name: 'Action', index: 'Action', width: 30, sortable: false,
            align: 'center', formatter: this.formatEditLink
        },
        { name: 'PrizeMethod', index: 'PrizeMethod', hidden: true },
        { name: 'PrizePoints', index: 'PrizePoints', hidden: true }
    ];

    this.LoadComplete = function (data) {
        $this.applyClassToHeader('RequiredNumberPoint');
        $this.bindEditRow();
    };
};

PrizeManagementExtract.prototype.formatDateTime = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined)
        return "<span value='" + cellvalue + "' >" + common.convertDateTimeYYYYMMDDHH24MI(cellvalue, true) + "</span>";
    return "<span></span>";
};

PrizeManagementExtract.prototype.unFormatDateTime = function (cellvalue, options, cell) {
    var $span = $('span', cell);
    var dateValue = common.convertDateTimeYYYYMMDDHH24MI($span.attr("value"), true);
    return dateValue;
};

PrizeManagementExtract.prototype.extractCsv = function () {
    var fromDate = $(this.formExtract.txtFromDate).datetimepicker('getDate');
    var toDate = $(this.formExtract.txtToDate).datetimepicker('getDate');
    if (toDate != null)
        toDate.setSeconds(59);

    var searchData = {
        fromDate: fromDate,
        toDate: toDate
    };
    
    ui.showPageLoading();
    $.ajax({
        type: "POST",
        contentType: 'application/json',
        url: this.ExtractUrl,
        data: JSON.stringify(searchData),
        success: function (data) {
            if (data != null) {
                if (data.ErrorCode == "0") {
                    
                } else {
                    jAlert(data.ErrorMessage, Resources.Notice);
                }
            }
            ui.hidePageLoading();
        },
        error: function(jqXHR, error, errorThrown) {
            jAlert(errorThrown, Resources.Error, function () {
                ui.hidePageLoading();
            });
        }
    });
};

PrizeManagementExtract.prototype.downloadCsv = function () {
    $.fileDownload(this.DownloadUrl, {
        data: {},
        httpMethod: "POST",
        prepareCallback: function(url) {
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