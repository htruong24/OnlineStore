PointManagement.prototype.constructor = PointManagement;      // Otherwise instances of Cat would have a constructor of Mammal 

function PointManagement() {
    this.BtnUploadFile = "#btnUploadFile";
    this.FrmFileUpload = "#frmFileUpload";
    this.FileUpload = "#fileUpload";
    this.LinkErrorCsv = "#linkErrorCsv";
    this.CsvFile = "#csvFile";
    this.SubFile = "#subFile";
}

PointManagement.prototype.bindEvents = function () {
    var $this = this;
    $this.uploadFile();

    $(this.BtnUploadFile).click(function () {
        $($this.LinkErrorCsv).addClass("hide");
        $($this.FrmFileUpload).submit();
    });
    
    $(this.CsvFile).change(function () {
        $($this.SubFile).val($(this).val());
        if ($(this).val() != "") {
            $($this.SubFile).val(common.getFilePath($(this).val()));
        }
    });
    
    $(this.SubFile).change(function () {
        $($this.CsvFile).val($(this).val());
    });
};

PointManagement.prototype.uploadFile = function () {
    var $this = this;
    $($this.FrmFileUpload).ajaxForm({
        iframe: true,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSubmit: function() {
            if ($($this.SubFile).val() == "") {
                jAlert(Resources.Message_NoFileSelect, Resources.Notice);
                return false;
            } else if ($($this.SubFile).val().toLowerCase().indexOf(".csv") < 0) {
                jAlert(Resources.Message_NotCsvFile, Resources.Notice);
                return false;
            }
            return true;
        },
        success: function (result) {
            $($this.SubFile).val("");
            $($this.CsvFile).val("");
            if (result.ErrorCode == "0") {
                uploadPoint.reloadGrid();
            }
            else if (result.ErrorCode == "1") {
                $($this.LinkErrorCsv).removeClass("hide");
                uploadPoint.reloadGrid();
            } else {
                jAlert(result.ErrorMessage, Resources.Notice);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            jAlert(errorThrown, Resources.Message_CannotUploadFile);
        }
    });
};


UploadPoint.prototype = new BaseGrid();
UploadPoint.prototype.constructor = UploadPoint;

function UploadPoint() {
}

UploadPoint.prototype.setGridData = function () {
    var $this = this;
    this.GridId = '#gridUploadPoint';
    this.PagerId = '#gridUploadPointPager';
    this.SortName = 'Uid';
    this.SortOrder = 'asc';
    this.Url = '/PointManagement/GetUploadPoint';
    this.DataType = 'json';
    this.SerializeGridData = function (postData) {
        return JSON.stringify(postData);
    };
    this.AjaxGridOptions = { contentType: "application/json" };

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

    this.ColNames = ["",
      Resources.UserManagement_Uid,
      Resources.PointManagement_PointType,
      Resources.PointManagement_TargetYear,
      Resources.PointManagement_TargetMonth,
      Resources.PointManagement_TargetWeek,
      Resources.PointManagement_GivingDate,
      Resources.PointManagement_CampaignId,
      Resources.PointManagement_NumberOfPoint
    ];

    this.ColModel = [
        { name: 'No', index: 'No' },
        { name: 'Uid', key: true },
        { name: 'PointType', index: 'PointType' },
        { name: 'TargetYear', index: 'TargetYear' },
        { name: 'TargetMonth', index: 'TargetMonth' },
        { name: 'TargetWeek', index: 'TargetWeek' },
        { name: 'GivingDate', index: 'GivingDate' },
        { name: 'CampaignId', index: 'CampaignId' },
        { name: 'NumberOfPoints', index: 'NumberOfPoints' }
    ];
};


CampaignInfo.prototype = new BaseGrid();
CampaignInfo.prototype.constructor = CampaignInfo;

function CampaignInfo() {
    
    this.CampaignInfoRequestModel = {
        CampaignId: -1,
        CampaignTitle: '',
        CampaignClass: -1,
        Condition: -1,
        Points: -1,
        GiveTime: '',
        CampaignStartTime: '',
        CampaignEndTime: ''
    };
}

CampaignInfo.prototype.setGridData = function () {
    var $this = this;
    this.GridId = '#gridCampaignInfo';
    this.PagerId = '#gridCampaignInfoPager';
    this.SortName = 'CampaignTitle';
    this.SortOrder = 'desc';
    this.Url = '/PointManagement/GetCampaignInfos';
    this.DataType = 'json';

    this.SerializeRowData = function(postData) {
        return JSON.stringify(postData);
    };

    this.SerializeGridData = function(postData) {
        return JSON.stringify(postData);
    };

    this.AjaxGridOptions = { contentType: "application/json" };

    this.AjaxRowOptions = { contentType: "application/json" };

    this.PostData = { };

    this.MType = 'POST';
    this.Height = 200;
    this.ShrinkToFit = true;
    this.AutoWidth = true;
    this.RowNum = 10;
    this.ViewRecords = true;
    this.PgButtons = true;
    this.PgInput = true;
    this.CmTemplate = { search: false };
    
    this.ColNames = [Resources.PointManagement_CampaignTitle,
      Resources.PointManagement_CampaignClass,
      Resources.PointManagement_CampaignCondition,
      Resources.PointManagement_CampaignPoint,
      Resources.PointManagement_CampaignGiveTime,
      Resources.PointManagement_CampaignStartTime,
      Resources.PointManagement_CampaignEndTime,
      ""
    ];

    this.ColModel = [
        { name: 'CampaignTitle', index: 'CampaignTitle', editrules: { custom: true, custom_func: ui.ValidateRequired }, editable: true },
        { name: 'CampaignClass', index: 'CampaignClass', editrules: { custom: true, custom_func: ui.ValidateRequired }, editable: true, edittype: "select", formatter: 'select', editoptions: { value: "1:チュートリアル;2:連続ログイン;3:SNSシェア;4:友達紹介" } },
        { name: 'Condition', index: 'Condition', editrules: { custom: true, custom_func: ui.ValidateInt }, editable: true },
        { name: 'Points', index: 'Points', editable: true, editrules: { custom: true, custom_func: ui.ValidateInt } },
        {
            name: 'GiveTime',
            index: 'GiveTime',
            formatter: 'date',
            formatoptions: { srcformat: 'ISO8601Long', newformat: 'Y-m-d H:i:s' },
            editable: true,
            editoptions: {
                dataInit: function (element) {
                    $(element).datetimepicker({
                        dateFormat: 'yy-mm-dd',
                        timeFormat: 'HH:mm:ss',
                        //controlType: 'select',
                        minDate: common.getDate(1),
                        stepHour: 1,
                        stepMinute: 1,
                        stepSecond: 1
                    }, $.datepicker.regional["ja"]);
                }
            },
            editrules: {
                custom: true, custom_func: function (value, colName) {
                    return ui.ValidateDateTime("", value, colName, colName, "", "", false, true, false);
                }
            }
        },
        {
            name: 'CampaignStartTime',
            index: 'CampaignStartTime',
            editable: true,
            formatter: 'date',
            formatoptions: { srcformat: 'ISO8601Long', newformat: 'Y-m-d H:i:s' },
            editoptions: {
                dataInit: function (element) {
                    $(element).datetimepicker({
                        dateFormat: 'yy-mm-dd',
                        timeFormat: 'HH:mm:ss',
                        stepHour: 1,
                        stepMinute: 1,
                        stepSecond: 1
                    }, $.datepicker.regional["ja"]);
                }
            },
            editrules: {
                custom: true, custom_func: function (value, colName) {
                    return ui.ValidateDateTime("", value, colName, colName, "", "", true, false, false);
                }
            }
        },
        {
            name: 'CampaignEndTime',
            index: 'CampaignEndTime',
            editable: true,
            formatter: 'date',
            formatoptions: { srcformat: 'ISO8601Long', newformat: 'Y-m-d H:i:s' },
            editoptions: {
                dataInit: function (element) {
                    $(element).datetimepicker({
                        dateFormat: 'yy-mm-dd',
                        timeFormat: 'HH:mm:ss',
                        minDate: common.getDate(1),
                        stepHour: 1,
                        stepMinute: 1,
                        stepSecond: 1
                    }, $.datepicker.regional["ja"]);
                }
            },
            editrules: {
                custom: true, custom_func: function (value, colName) {
                    var rowId = $this.Grid.jqGrid('getGridParam', 'selrow');
                    return ui.ValidateDateTime(rowId, value, colName, colName, "CampaignStartTime", "CampaignStartTime", true, true, true);
                }
            }
        },
        {
            name: 'CampaignId',
            index: 'CampaignId',
            sortable: false,
            key: true,
            formatter: function(cellvalue, options, rowObject) {
                return '<a href="javascript:void(0);"><span class="glyphicon glyphicon-pencil edit-item" style="padding-right:5px" aria-hidden="true" rowid="' + cellvalue + '"></span></a>'
                    + '<a href="javascript:void(0);"><span class="glyphicon glyphicon glyphicon-floppy-saved save-item hide" style="padding-right:5px" aria-hidden="true" rowid="' + cellvalue + '"></span></a>'
                    + '<a href="javascript:void(0);"><span class="glyphicon glyphicon-refresh cancel-item hide" aria-hidden="true" rowid="' + cellvalue + '"></span></a>';
            }
        }
    ];

    this.LoadComplete = function (data) {
        $this.bindEditRow();
      

    };
};

CampaignInfo.prototype.bindEditRow = function () {
    var $this = this;
    var editparameters = {
        "keys": true,
        "oneditfunc": function (rowid) {
        },
        "successfunc": function(serverresponse) {
            var data = $.parseJSON(serverresponse.responseText);
            if (data != null) {
                if (data.ErrorCode == "0") {
                    return true;
                } else {
                    jAlert(data.ErrorMessage, Resources.Notice);
                    return false;
                }
            }
            return false;
        },
        "url": '/PointManagement/UpdateCampaignInfo',
        "extraparam": {
            MemberID: $this.MemberId
        },
        "aftersavefunc": function(rowId) {
            var cellAction = $this.Grid.find("tr.jqgrow[id=" + rowId + "]").find("td[aria-describedby*='CampaignId']");
            $("span.save-item", cellAction).addClass("hide");
            $("span.cancel-item", cellAction).addClass("hide");
            $("span.edit-item", cellAction).removeClass("hide");
        },
        "errorfunc": function() {
            alert("errorfunc");
        },
        "afterrestorefunc": function(rowId) {
            var cellAction = $this.Grid.find("tr.jqgrow[id=" + rowId + "]").find("td[aria-describedby*='CampaignId']");
            $("span.save-item", cellAction).addClass("hide");
            $("span.cancel-item", cellAction).addClass("hide");
            $("span.edit-item", cellAction).removeClass("hide");
        },
        "restoreAfterError": true,
        "mtype": "POST"
    };

    $(this.GridId).on('click', 'span.edit-item', function(e) {
        var rowid = $(this).attr("rowid");
        var rowData = $this.Grid.getRowData(rowid);
        $this.Grid.editRow(rowid, editparameters);
        $(this).closest("td").find("span.save-item").removeClass("hide");
        $(this).closest("td").find("span.cancel-item").removeClass("hide");
        $(this).closest("td").find("span.edit-item").addClass("hide");
    });

    $(this.GridId).on('click', 'span.cancel-item', function(e) {
        var rowid = $(this).attr("rowid");
        $this.Grid.restoreRow(rowid, editparameters);
    });

    $(this.GridId).on('click', 'span.save-item', function (e) {
        var rowid = $(this).attr("rowid");
        $this.Grid.saveRow(rowid, editparameters);
    });
};

CampaignInfo.prototype.formatDateTime = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined && cellvalue != "")
        return "<span value='" + cellvalue + "' >" + common.convertYYYMMDDHHMMSSDateTime(cellvalue, true) + "</span>";
    return "";
};

CampaignInfo.prototype.unFormatDateTime = function (cellvalue, options, cell) {
    var $span = $('span', cell);
    return $span.attr("value");
};
