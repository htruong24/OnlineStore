PrizeManagement.prototype = new BaseGrid();
PrizeManagement.prototype.constructor = new PrizeManagement();

function PrizeManagement() {
    
    this.PrizeMethodValue = {
        1: 'もれなく',
        2: '抽選'
    };
    
    this.formEdit = {
        formContent: '.edit-form',
        formUpload: '#formUpload',
        formUpdatePrize: '#formUpdatePrize',
        lblPrizeId: '#lblPrizeId',
        txtPrizeName: '#txtPrizeName',
        selPrizeMethod: '#selPrizeMethod',
        txtPrizePoint: '#txtPrizePoint',
        txtPrizeDescription: '#txtPrizeDescription',
        txtEntryStartTime: '#txtEntryStartTime',
        txtEntryEndTime: '#txtEntryEndTime',
        imgPrizeImage: '#imgPrizeImage',
        btnUpdate: '#btnUpdate',
        btnUploadFile: '#btnUploadFile',
        fileInput: '#fileInput',
    };
    
    this.prizeModel = {
        PrizeId: -1,
        PrizeName: '',
        PrizeDescription: '',
        PrizeMethod: -1,
        PrizePoints: -1,
        PrizeImg: '',
        EntryStartTime: null,
        EntryEndTime: null
    };
}

PrizeManagement.prototype.bindEvents = function () {
    var $this = this;
    $(this.formEdit.txtPrizePoint).numeric(false);
    $(this.formEdit.formContent).modal('hide');

    $(this.formEdit.txtEntryStartTime).datetimepicker({
        dateFormat: 'yy/mm/dd',
        timeFormat: 'hh:mm:ss TT',
        onClose: function (dateText, inst) {
            var endDate = $($this.formEdit.txtEntryEndTime);
            
            if (endDate.val() != '') {
                var testStartDate = new Date(dateText);
                var testEndDate = new Date(endDate.val());
                if (testStartDate > testEndDate)
                    endDate.val(dateText);
            }
            else {
                endDate.val(dateText);
            }
        },
        onSelect: function (selectedDateTime) {
            var start = $(this).datetimepicker('getDate');
            $($this.formEdit.txtEntryEndTime).datetimepicker('option', 'minDate', new Date(start.getTime()));
        }
    });
    
    $(this.formEdit.txtEntryEndTime).datetimepicker({
        dateFormat: 'yy/mm/dd',
        timeFormat: 'hh:mm:ss TT',
        onClose: function (dateText, inst) {
            var startDate = $($this.formEdit.txtEntryStartTime);
            
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
            $($this.formEdit.txtEntryStartTime).datetimepicker('option', 'maxDate', new Date(end.getTime()));
        }
    });

    $(this.formEdit.btnUploadFile).click(function(e) {
        $($this.formEdit.formUpload).submit();
    });
    
    $(this.formEdit.formUpload).ajaxForm({
        iframe: true,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSubmit: function () { },
        success: function (result) {
            if (result.ErrorCode == "0") {
                $($this.formEdit.imgPrizeImage).attr("src", result.Result);
                var file = $("#fileInput").clone();
                $("#fileInput").replaceWith(file);
            }
            else {
                jAlert(result.ErrorMessage, Resources.Notice);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            jAlert(errorThrown, Resources.Message_CannotUploadFile);
        }
    });

    $(this.formEdit.btnUpdate).click(function (e) {

        if (!$this.isValidateForm()) return false;

        $('#dialog').html(Resources.PrizeManagement_UpdateMessage);
        $("#dialog").dialog({
            resizable: false,
            height: 'auto',
            title: Resources.PrizeManagement_UpdatePrize,
            modal: true,
            buttons: {
                "OK": function () {
                    $this.bindEditModel();
                    $.ajax({
                        type: "POST",
                        contentType: 'application/json',
                        url: '/PrizeManagement/UpdatePrize',
                        cache: false,
                        data: JSON.stringify($this.prizeModel),
                        success: function (data) {
                            if (data.ErrorCode == "0") {
                                jAlert("Update successfully!", 'Success', function () {
                                    //window.location.href = $($this.btnUpdate).attr("data-url");
                                    $this.updateGridRow(data.Result);
                                    $this.cleanEditForm();

                                    $($this.formEdit.formContent).modal('hide');
                                });
                            } else {
                                jAlert(data.ErrorMessage, 'Notice');
                            }
                        },
                        error: function (jqXHR, error, errorThrown) {
                            jAlert(error, 'Notice');
                        }
                    });

                    $(this).dialog("close");
                },
                "キャンセル": function () {
                    $(this).dialog("close");
                }
            }
        });
    });
};

PrizeManagement.prototype.setGridData = function() {
    var $this = this;
    
    this.GridId = '#gridPrize';
    this.PagerId = '#gridPrizePager';
    this.SortName = 'PrizeID';
    this.SortOrder = 'desc';
    this.Url = '/PrizeManagement/GetPrizes';
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


    this.ColNames = [Resources.PrizeManagement_PrizeId,
      Resources.PrizeManagement_PrizeName,
      Resources.PrizeManagement_PrizeMethod,
      Resources.PrizeManagement_RequiredNumberPoint,
      Resources.PrizeManagement_PrizeDescription,
      Resources.PrizeManagement_EntryStartTime,
      Resources.PrizeManagement_EntryEndTime,
      Resources.PrizeManagement_PrizeImage,
      "",
      Resources.PrizeManagement_PrizeMethod
    ];

    //this.ColNames = ["Prize Id", "Prize Name", "Method", "Required number point", "Prize Description",
    //                "Entry Start Time", "Entry End Time", "Prize Image", "",
    //                "PrizeMethod"
    //];
    
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
            //formatter: 'date', formatoptions: {
            //    srcformat: 'Y-m-d H:i:s',
            //    newformat: 'Y-m-d H:i:s'
            //}
        },
        {
            name: 'EntryEndTime', index: 'EntryEndTime', width: 130, align: 'center',
            formatter: this.formatDateTime,
            unformat: this.unFormatDateTime
            //formatter: 'date', formatoptions: {
            //    srcformat: 'Y-m-d H:i:s',
            //    newformat: 'Y-m-d H:i:s'
            //}
        },
        {
            name: 'PrizeImg', index: 'PrizeImg', width: 100,
            align: 'center', formatter: this.formatImage, unformat: this.unFormatImage
        },
        {
            name: 'Action', index: 'Action', width: 30, sortable: false,
            align: 'center', formatter: this.formatEditLink
        },
        { name: 'PrizeMethod', index: 'PrizeMethod', hidden: true }
    ];

    this.LoadComplete = function (data) {
        $this.applyClassToHeader('RequiredNumberPoint');
        $this.bindEditRow();
    };
};

PrizeManagement.prototype.bindEditForm = function (rowData) {
    $(this.formEdit.lblPrizeId).text(rowData.PrizeId);
    $(this.formEdit.txtPrizeName).val(rowData.PrizeName);
    $(this.formEdit.txtPrizeDescription).val(rowData.PrizeDescription);
    $(this.formEdit.txtPrizePoint).val(rowData.PrizePoints);
    
    $("option[value='" + rowData.PrizeMethod + "']", this.formEdit.selPrizeMethod).prop('selected', true);
    
    $(this.formEdit.txtEntryStartTime).val(rowData.EntryStartTime);
    $(this.formEdit.txtEntryEndTime).val(rowData.EntryEndTime);
    $(this.formEdit.imgPrizeImage).attr('src', rowData.PrizeImg);
};

PrizeManagement.prototype.bindEditModel = function () {
    this.prizeModel.PrizeId = common.parseInt($(this.formEdit.lblPrizeId).text());
    this.prizeModel.PrizeName = $(this.formEdit.txtPrizeName).val();
    this.prizeModel.PrizeDescription = $(this.formEdit.txtPrizeDescription).val();
    this.prizeModel.PrizePoints = $(this.formEdit.txtPrizePoint).val();
    this.prizeModel.PrizeMethod = common.parseInt($("option:selected", this.formEdit.selPrizeMethod).val());
    this.prizeModel.PrizeImg = $(this.formEdit.imgPrizeImage).attr('src');
    this.prizeModel.EntryStartTime = $(this.formEdit.txtEntryStartTime).val();
    this.prizeModel.EntryEndTime = $(this.formEdit.txtEntryEndTime).val();
};

PrizeManagement.prototype.cleanEditForm = function () {
    $(this.formEdit.lblPrizeId).text('');
    $(this.formEdit.txtPrizeName).val('');
    $(this.formEdit.txtPrizeDescription).val('');
    $(this.formEdit.txtPrizePoint).val('');

    $("option[value='1']", this.formEdit.selPrizeMethod).prop('selected', true);
    
    $(this.formEdit.txtEntryStartTime).val('');
    $(this.formEdit.txtEntryEndTime).val('');
    $(this.formEdit.imgPrizeImage).attr('src', '');
};

PrizeManagement.prototype.updateGridRow = function (prizeModel) {
    var rowid = prizeModel.PrizeId;

    var startDate = prizeModel.EntryStartTime;
    var endDate = prizeModel.EntryEndTime;

    this.Grid.setCell(rowid, 'PrizeName', prizeModel.PrizeName);
    this.Grid.setCell(rowid, 'PrizePoints', prizeModel.PrizePoints);
    this.Grid.setCell(rowid, 'PrizeDescription', prizeModel.PrizeDescription);
    this.Grid.setCell(rowid, 'PrizeMethod', prizeModel.PrizeMethod);
    this.Grid.setCell(rowid, 'EntryStartTime', startDate);
    this.Grid.setCell(rowid, 'EntryEndTime', endDate);
    this.Grid.setCell(rowid, 'PrizeImg', prizeModel.PrizeImg);
};

PrizeManagement.prototype.formatImage = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined) {
        var arr = [];
        arr.push('<img class="img-thumbnail" src="' + cellvalue + '" alt="' + cellvalue + '"/>');
        return arr.join('');
    }
    return "";
};

PrizeManagement.prototype.formatDateTime = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined)
        return "<span value='" + cellvalue + "' >" + common.convertDateTimeYYYYMMDDHH24MI(cellvalue, true) + "</span>";
    return "<span></span>";
};

PrizeManagement.prototype.unFormatDateTime = function (cellvalue, options, cell) {
    var $span = $('span', cell);
    var dateValue = common.convertDateTimeYYYYMMDDHH24MI($span.attr("value"), true);
    return dateValue;
};

PrizeManagement.prototype.unFormatImage = function (cellvalue, options, cell) {
    var $img = $('img', cell);
    return $img.attr('src');
};

PrizeManagement.prototype.formatEditLink = function (cellvalue, options, rowObject) {
    if (rowObject && rowObject.PrizeId != undefined) {
        var arr = [];
        arr.push('<a href="javascript:void(0);"><span class="glyphicon glyphicon-pencil edit-item" style="padding-right:5px" aria-hidden="true" rowid="' + rowObject.PrizeId + '"></span></a>');
        arr.push('<a href="javascript:void(0);"><span class="glyphicon glyphicon glyphicon-floppy-saved save-item hide" style="padding-right:5px" aria-hidden="true" rowid="' + rowObject.PrizeId + '"></span></a>');
        arr.push('<a href="javascript:void(0);"><span class="glyphicon glyphicon-refresh cancel-item hide" aria-hidden="true" rowid="' + rowObject.PrizeId + '"></span></a>');
        return arr.join('');
    } else return "";
};

PrizeManagement.prototype.bindEditRow = function () {
    var $this = this;
    var editparameters = {
        "keys": true,
        "oneditfunc": function (rowid) { },
        "successfunc": function (serverresponse) {
            var data = $.parseJSON(serverresponse.responseText);

            if (data != null) {
                if (data.ErrorCode == "0") {
                    return true;
                } else {
                    jAlert(data.ErrorMessage, 'Notice');
                    return false;
                }
            }
            return false;
        },
        "url": '/PrizeManagement/UpdatePrize',
        "extraparam": { },
        "aftersavefunc": function (rowId) {
        },
        "errorfunc": function () {
            
        },
        "afterrestorefunc": function (rowId) {
            var cellAction = $this.Grid.find("tr.jqgrow[id=" + rowId + "]").find("td[aria-describedby*='PrizeId']");
            $("span.save-item", cellAction).addClass("hide");
            $("span.cancel-item", cellAction).addClass("hide");
            $("span.edit-item", cellAction).removeClass("hide");
        },
        "restoreAfterError": true,
        "mtype": "POST"
    };

    $(this.GridId).on('click', 'span.edit-item', function (e) {
        var rowid = $(this).attr("rowid");
        var rowData = $this.Grid.getRowData(rowid);

        //$this.Grid.editRow(rowid, editparameters);
        
        //var td = $(this).closest("td");
        //$("span.save-item", td).removeClass("hide");
        //$("span.cancel-item", td).removeClass("hide");
        //$("span.edit-item", td).addClass("hide");
        
        //$($this.formEdit.formContent).dialog('open');

        $this.bindEditForm(rowData);
        $($this.formEdit.formContent).modal('show');
    });

    $(this.GridId).on('click', 'span.cancel-item', function (e) {
        var rowid = $(this).attr("rowid");
        $this.Grid.restoreRow(rowid, editparameters);
    });

    $(this.GridId).on('click', 'span.save-item', function (e) {
        var rowid = $(this).attr("rowid");
        $this.Grid.saveRow(rowid, editparameters);
    });
};

PrizeManagement.prototype.isValidateForm = function () {
    var startTime = $(this.formEdit.txtEntryStartTime);
    var endTime = $(this.formEdit.txtEntryEndTime);
    
    var isValidEntryStartTime = ui.isValidDateTime(startTime.val(), "yy/mm/dd", "hh:mm:ss TT");
    var isValidEntryEndTime = ui.isValidDateTime(endTime.val(), "yy/mm/dd", "hh:mm:ss TT");

    if (!isValidEntryStartTime) {
        jAlert("Invalid Entry Start Time!", "Notice", function() {
            startTime.focus();
        });
        return false;
    }
    
    if (!isValidEntryEndTime) {
        jAlert("Invalid Entry End Time!", "Notice", function() {
            endTime.focus();
        });
        return false;
    }

    return true;
};