var common = new Common();
var ui;

function Common() {}

Common.prototype.loadPageContent = function (id, action, parameter) {
    ui.showPageLoading();
    $.ajax({
        type: "GET",
        url: appPath + action,
        data: parameter,
        success: function (data) {
            $("#" + id).html(data);
            $(".date-picker").each(function () {
                $(this).datepicker({
                    dateFormat: 'dd-mm-yy'
                });
            });
            ui.hidePageLoading();
        },
        error: function (jqXHR, error, errorThrown) {
            toaster.error(error);
            ui.hidePageLoading();
        }
    });

   
}

// Get week of year from date
Common.prototype.getWeekOfYear = function (d) {
    var day = d.getDay();
    if (day == 0) day = 7;
    d.setDate(d.getDate() + (4 - day));
    var year = d.getFullYear();
    var ZBDoCY = Math.floor((d.getTime() - new Date(year, 0, 1, -6)) / 86400000);
    return 1 + Math.floor(ZBDoCY / 7);
};

// Get week of year from date
Common.prototype.getTotalWeeksOfMonth = function (year, month) {
    if(month == 1)
        return 5;
    var firstOfMonth = new Date(year, month - 1, 1);
    var firstOfDay = firstOfMonth.getDay() == 1 ? 1 : 0;
    var lastOfMonth = new Date(year, month, 0);
    return common.getWeekOfYear(lastOfMonth) - common.getWeekOfYear(firstOfMonth) + firstOfDay;
};

// Get file name from Path
Common.prototype.getFilePath = function (val) {
    return val.replace(/^.*(\\|\/|\:)/, '');
};

// Get current date number of date added
Common.prototype.getDate = function (val) {
    var d = new Date();
    var currentDate = new Date(d.getFullYear(), d.getMonth(), d.getDate());
    currentDate.setDate(currentDate.getDate() + val);
    return currentDate;
};

// Convert multiple chosen to text
Common.prototype.parseChosen = function (val) {
    var arr = [];
    $(val + " option:selected").each(function () {
        arr.push($(this).text());
    });
    return arr.join(', ');
};

// Convert Array to text
Common.prototype.parseArray = function (val) {
    if (val != null) {
        return val.toString();
    }
    return '';
};

// Convert Gender to Japanese Text
Common.prototype.parseGender = function (val) {
    switch (val)
    {
        case 1:
            return "Nam";
        case 2:
            return "Nữ";
    }
    return "";
};

// Convert Date to dd/MM/YYYY
Common.prototype.convertDate = function (val) {
    if (val == null || val == undefined || val == "")
        return "";
    var dt = this.parseDate(val);
    
    var month = dt.getMonth() + 1;
    var date = dt.getDate();
    var year = dt.getFullYear();

    var formatMonth = '' + month;
    var formatDate = '' + date;

    if (month > 0 && month < 10)
        formatMonth = '0' + month;
    
    if (date > 0 && date < 10)
        formatDate = '0' + date;

    return formatDate + "/" + formatMonth + "/" + year;
};

// Convert Date to HH24:Mi
Common.prototype.convertTimeHH24MI = function (val) {
    if (val == null || val == undefined || val == "")
        return "";
    
    var dt = this.parseDate(val);

    var hour = dt.getHours();
    var minutes = dt.getMinutes();
    var seconds = dt.getSeconds();

    var formatHour;
    var formatMinutes;
    var formatSeconds;

    var amOrPm = "";
    if (hour >= 12) {
        amOrPm = "PM";
    }
    else {
        amOrPm = "AM";
    }
    
    if (hour >= 0 && hour < 10)
        formatHour = '0' + hour;
    else formatHour = '' + hour;

    if (minutes >= 0 && minutes < 10)
        formatMinutes = '0' + minutes;
    else formatMinutes = '' + minutes;

    if (seconds >= 0 && seconds < 10)
        formatSeconds = '0' + seconds;
    else formatSeconds = '' + seconds;

    return formatHour + ":" + formatMinutes + " " + amOrPm;
};

Common.prototype.convertDateTimeYYYYMMDDHH24MI = function (val, showSecond) {
    var dt = this.parseDate(val);

    var month = dt.getMonth() + 1;
    var date = dt.getDate();
    var year = dt.getFullYear();
    var hour = dt.getHours();
    var minutes = dt.getMinutes();
    var seconds = dt.getSeconds();

    var formatMonth;
    var formatDate;
    var formatHour;
    var formatMinutes;
    var formatSeconds;

    var amOrPm = "";
    if (hour >= 12) {
        amOrPm = "PM";
    }
    else {
        amOrPm = "AM";
    }
    if (hour >= 13) hour -= 12;
    if (hour == 0) hour = 12;

    if (month > 0 && month < 10)
        formatMonth = '0' + month;
    else formatMonth = '' + month;

    if (date > 0 && date < 10)
        formatDate = '0' + date;
    else formatDate = '' + date;

    if (hour >= 0 && hour < 10)
        formatHour = '0' + hour;
    else formatHour = '' + hour;

    if (minutes >= 0 && minutes < 10)
        formatMinutes = '0' + minutes;
    else formatMinutes = '' + minutes;

    if (seconds >= 0 && seconds < 10)
        formatSeconds = '0' + seconds;
    else formatSeconds = '' + seconds;

    if (showSecond == undefined || showSecond == true)
        return year + "/" + formatMonth + "/" + formatDate + " " + formatHour + ":" + formatMinutes + ":" + formatSeconds + " " + amOrPm;

    return year + "/" + formatMonth + "/" + formatDate + " " + formatHour + ":" + formatMinutes + " " + amOrPm;
};

// Convert Datetime to MM/dd/YYYY hh:mm:ss AM/PM
Common.prototype.convertDateTime = function (val, showSecond) {
    var dt = this.parseDate(val);
    
    var month = dt.getMonth() + 1;
    var date = dt.getDate();
    var year = dt.getFullYear();
    var hour = dt.getHours();
    var minutes = dt.getMinutes();
    var seconds = dt.getSeconds();

    var formatMonth;
    var formatDate;
    var formatHour;
    var formatMinutes;
    var formatSeconds;

    var amOrPm = "";
    if (hour >= 12) {
        amOrPm = "PM";
    }
    else {
        amOrPm = "AM";
    }
    if (hour >= 13) hour -= 12;
    if (hour == 0) hour = 12;

    if (month > 0 && month < 10)
        formatMonth = '0' + month;
    else formatMonth = '' + month;

    if (date > 0 && date < 10)
        formatDate = '0' + date;
    else formatDate = '' + date;
    
    if (hour >= 0 && hour < 10)
        formatHour = '0' + hour;
    else formatHour = '' + hour;

    if (minutes >= 0 && minutes < 10)
        formatMinutes = '0' + minutes;
    else formatMinutes = '' + minutes;
    
    if (seconds >= 0 && seconds < 10)
        formatSeconds = '0' + seconds;
    else formatSeconds = '' + seconds;

    if (showSecond == undefined || showSecond == true)
        return formatMonth + "/" + formatDate + "/" + year + " " + formatHour + ":" + formatMinutes + ":" + formatSeconds + " " + amOrPm;
    
    return formatMonth + "/" + formatDate + "/" + year + " " + formatHour + ":" + formatMinutes + " " + amOrPm;
};

// Convert Datetime to yyyy-MM-dd hh:mm:ss
Common.prototype.convertYYYMMDDHHMMSSDateTime = function (val, showSecond) {
    var dt = this.parseDate(val);

    var month = dt.getMonth() + 1;
    var date = dt.getDate();
    var year = dt.getFullYear();
    var hour = dt.getHours();
    var minutes = dt.getMinutes();
    var seconds = dt.getSeconds();

    var formatMonth;
    var formatDate;
    var formatHour;
    var formatMinutes;
    var formatSeconds;
   
    //if (hour >= 13) hour -= 12;
    //if (hour == 0) hour = 12;

    if (month > 0 && month < 10)
        formatMonth = '0' + month;
    else formatMonth = '' + month;

    if (date > 0 && date < 10)
        formatDate = '0' + date;
    else formatDate = '' + date;

    if (hour >= 0 && hour < 10)
        formatHour = '0' + hour;
    else formatHour = '' + hour;

    if (minutes >= 0 && minutes < 10)
        formatMinutes = '0' + minutes;
    else formatMinutes = '' + minutes;

    if (seconds >= 0 && seconds < 10)
        formatSeconds = '0' + seconds;
    else formatSeconds = '' + seconds;

    if (showSecond == undefined || showSecond == true)
        return year + "-" + formatMonth + "-" + formatDate + " " + formatHour + ":" + formatMinutes + ":" + formatSeconds;
    return year + "-" + formatMonth + "-" + formatDate + " " + formatHour + ":" + formatMinutes + ":" + formatSeconds;
};

// Parse to client Date from iso format
Common.prototype.parseDate = function (val) {
    var parts = val.match(/(\d+)/g);
    
    //if (parts[3] > 0) {
    //    return new Date(parts[0], parts[1] - 1, parts[2], parts[3] - 1, parts[4], parts[5]); //     months are 0-based
    //}
    return new Date(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]); //     months are 0-based
};

Common.prototype.parseInt = function (val) {
    if (isNaN(parseInt(val)) == false)
        return parseInt(val);
    console.log("parseInt: Cannot convert " + val + " to integer.");
    return -1;
};

Common.prototype.trimString = function(val) {
    if (!val)
        return "";
    while (val.charCodeAt(0) <= 32) {
        val = val.substr(1);
    }

    while (val.charCodeAt(val.length - 1) <= 32) {
        val = val.substr(0, val.length - 1);
    }
    return val;
};

Common.prototype.leftString = function(val, n) {
    if (n <= 0)
        return "";
    else if (n > String(val).length)
        return val;
    else
        return String(val).substring(0, n);
};

Common.prototype.rightString = function(val, n) {
    if (n <= 0)
        return "";
    else if (n > String(val).length)
        return val;
    else {
        var iLen = String(val).length;
        return String(val).substring(iLen - n, iLen);
    }
};

Common.prototype.stringFormat = function (val, args) {
    var i;
    if (args instanceof Array) {
        for (i = 0; i < args.length; i++) {
            val = val.replace(new RegExp('\\{' + i + '\\}', 'gm'), args[i]);
        }
        return val;
    }
    
    for (i = 0; i < arguments.length - 1; i++) {
        val = val.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i + 1]);
    }
    return val;

    //for (var i = 0; i < args.length; i++) {
    //    var reg = new RegExp("\\{" + i + "\\}", "gm");
    //    val = val.replace(reg, args[i]);
    //}
    //return val;
};

Common.prototype.parseBoolean = function (val) {
    if (typeof(val) == "boolean") {
        return val;
    } else {
        switch (val.toLowerCase()) {
        case "true":
            return true;
        case "false":
            return false;
        default:
            throw new Error("parseBoolean: Cannot convert string to boolean.");
        }
    }
};

Common.prototype.uniqueArray = function (arr) {
    return $.grep(arr, function (el, index) {
        return index == $.inArray(el, arr);
    });
};

Common.prototype.removeArray = function (arr, from, to) {
    if (to == null || to == undefined)
        to = arr.length;
    var rest = arr.slice((to || from) + 1 || arr.length);
    arr.length = from < 0 ? arr.length + from : from;
    return arr.push.apply(arr, rest);
};

Common.prototype.findItemArray = function (arr, key, value) {
    var index = -1;
    for (var i = 0, il = arr.length; i < il; i++) {
        if (arr[i][key] == value) {
            index = i;
            break;
        }
    }
    if (index == -1)
        return null;
    return arr[index];
};

Common.prototype.removeItemArray = function (arr, item) {
    for (var il = arr.length, i = il - 1; i >= 0; i--) {
        var temp = arr[i];
        if (temp == item) {
            this.removeArray(arr, i, i);
            break;
        }
    }
};

Common.prototype.moveItemArray = function(arr, from, to) {
    if (to === from) return;

    var target = arr[from];
    var increment = to < from ? -1 : 1;

    for (var k = from; k != to; k += increment) {
        arr[k] = arr[k + increment];
    }
    arr[to] = target;
};

Common.prototype.cloneArray = function (arr) {
    if (arr == null || arr == undefined) return null;
    return arr.slice(0);
};

Common.prototype.getParams = function () {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
};

Common.prototype.decodeHTML = function(val) {
    var swapCodes = new Array("&#44;", "&#39;", "&quot;", "&amp;", "&lt;", "&gt;");
    var swapStrings = new Array("`", "'", "\"", "&", "<", ">");

    for (var i = 0; i < swapCodes.length; i++) {
        var swapper = new RegExp(swapCodes[i], "g");
        val = val.replace(swapper, swapStrings[i]);
    }
    return val.trim();
};

Common.prototype.getErrorMessage = function(xhr, exception) {
    if (xhr.status === 0) {
        return 'Not connected.\nPlease verify your network connection.';
    }
    else if (xhr.status == 404) {
        return 'The requested page not found. [404]';
    }
    else if (xhr.status == 500) {
        return 'Internal Server Error [500].';
    }
    else if (exception === 'parsererror') {
        return 'Requested JSON parse failed.';
    }
    else if (exception === 'timeout') {
        return 'Time out error.';
    }
    else if (exception === 'abort') {
        return 'Ajax request aborted.';
    }
    else {
        var error = $.parseJSON(xhr.responseText);
        //alert("Message: " + r.Message);
        //alert("StackTrace: " + r.StackTrace);
        //alert("ExceptionType: " + r.ExceptionType);
        return 'Uncaught Error.\n' + error.Message;
    }
};

//Tao ID tu tang
Common.prototype.RandomId = function() {
    var now = new Date();
    var id = now.getTime();
    return id++;
};

/*
// Remove duplicate param in array
Array.prototype.unique = function () {
    return this.filter(
        function(a) { return !this[a] ? this[a] = true : false; }, {}
    );
};

Array.prototype.findById = function (id) {
    var index = -1;

    for (var i = 0, il = this.length; i < il; i++) {
        if (this[i].Id == id) {
            index = i;
            break;
        }
    }
    if (index == -1)
        return null;
    return this[index];
};

Array.prototype.findByVal = function (val) {
    var index = -1;
    for (var i = 0, il = this.length; i < il; i++) {
        if (this[i] == val) {
            index = i;
            break;
        }
    }
    if (index == -1)
        return null;
    return this[index];
};

Array.prototype.insert = function (element, where) {
    this.splice(where, 0, element);
};

Array.prototype.remove = function (from, to) {
    if (to == null || to == undefined)
        to = this.length;
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

Array.prototype.removeByValue = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) {
            this.splice(i, 1);
            break;
        }
    }
};

Array.prototype.removeByItem = function (item) {
    for (var il = this.length, i = il - 1; i >= 0; i--) {
        var temp = this[i];
        if (temp == item) {
            this.remove(i, i);
            break;
        }
    }
};
*/

function BaseGrid() {
    this.Grid = null;
    this.GridId = '';
    this.PagerId = '';

    // rowNum, Sets how many records we want to view in the grid. This parameter is passed to the url 
    // for use by the server routine retrieving the data
    // Number of rows per page
    this.RowNum = 20;

    // rowList, an array to construct a select box element in the pager in which we can change the number 
    // of the visible rows. When changed during the execution, this parameter replaces the rowNum parameter 
    // that is passed to the url
    this.RowList = new Array(20, 30, 40, 60, 80);

    // sortname, sets the initial sorting column. Can be a name or number. This parameter is added to the 
    // url for use by the server routine
    // Initial sorting column
    this.SortName = '';

    // Specifying either 'asc' or 'desc' for this property will set the initial sort order. (Default : asc)
    // Initial sorting direction
    this.SortOrder = 'asc';

    // viewrecords, Defines whether we want to display the number of total records from the query in the 
    // pager bar
    // Enable to display total records count
    this.ViewRecords = false;
    this.AltRows = false;

    this.GridView = true;
    this.ForceFit = false;
    this.ShrinkToFit = false;
    this.AutoWidth = false;

    this.CmTemplate = null;
    
    //Creates dynamic scrolling grids. When enabled, the pager elements are disabled and we can use the vertical scrollbar to load data. 
    //When set to true the grid will always hold all the items from the start through to the latest point ever visited. 
    this.Scroll = false;
    this.ScrollRows = false;
    this.Width = 'auto';// The width of the grid in px 
    this.Height = 'auto';// The height of the grid in px 
    //this.LoadUi = 'disable';
    this.LoadOnce = false;

    // Display column row number
    this.RowNumbers = false;
    this.RowNumWidth = 40;

    this.Url = '';
    this.DataType = '';
    this.MType = 'GET';

    this.PgInput = false;
    this.PgButtons = false;
    this.OnPaging = null;
    this.AutoSearch = false;
    this.MultiSelect = false;

    // Sets the caption for the grid. If this parameter is not set the Caption layer will be not visible
    //caption: '<font size="3" face="verdana">Servo Status</font><br/><font color=green><u>E</u></font>dit; <font color=green><u>S</u></font>ave; <font color=green><u>C</u></font>ancel editing</font>'
    this.Caption = '';

    this.EmptyRecords = 'レコードが表示されないように', // 'No records to display...'
    this.ColNames = [];
    this.ColModel = [];

    this.JsonReader = {
        root: "rows",
        page: "page",
        total: "total",
        records: "records",
        repeatitems: false,
        userdata: "userdata"
    };

    this.PostData = {};
    this.OnSelectRow = null;
    this.OnCellSelect = null;
    this.OnSelectCell = null;
    this.BeforeRequest = null;
    this.BeforeProcessing = null;
    this.GridComplete = null;
    this.LoadComplete = null;
    this.OndblClickRow = null;
    this.AfterInsertRow = null;
    this.OnSelectAll = null;

    // Use in edit cell, default : false
    this.CellEdit = false;
    this.CellSubmit = 'remote';
    this.CellUrl = '';
    this.FormatCell = null;
    this.BeforeSelectRow = null;
    this.AfterEditCell = null;
    this.BeforeSubmitCell = null;
    this.AfterSubmitCell = null;
    this.AfterSaveCell = null;
    this.ErrorCell = null;

    // Edit Url Submit to Server
    this.EditUrl = '';

    //Grouping - Default is not enable
    this.Grouping = false;
    this.GroupingView = {};

    // Post JSON object mode
    this.SerializeGridData = null;
    this.SerializeCellData = null;
    this.SerializeRowData = null;
    this.AjaxCellOptions = null;
    this.AjaxGridOptions = null;
    this.AjaxRowOptions = null;

    /*
	onSelectRow: function (id) 
	{
		if (id && id !== lastSel)
		{
			jQuery('#jqServoStatus').restoreRow(lastSel);	// restore last grid row
			jQuery('#jqServoStatus').editRow(id,true);		// show form elements for the row selected to enable updates
			lastSel = id;												// save current row ID so that when focus is gone it can be restored
		} 		
	},
	*/
    //editurl: base_url + "index/update",
    // imgpath, the path to the images needed for the grid.  The path should not end with '/'
    // imgpath: 'themes/basic/images',
}

BaseGrid.prototype.getGrid = function () {
    var $this = this;
    try {
        $(this.GridId).jqGrid({
            url: this.Url,
            mtype: this.MType,
            datatype: this.DataType,
            cellEdit: this.CellEdit,                            // use in edit cell
            cellsubmit: this.CellSubmit,                        // use in edit cell
            editurl: this.EditUrl,
            cellurl: this.CellUrl,                              // use in edit cell
            formatCell: this.FormatCell,
            emptyrecords: this.EmptyRecords,
            beforeSelectRow: this.BeforeSelectRow,                  // use in edit cell
            afterEditCell: this.AfterEditCell,                  // use in edit cell
            beforeSubmitCell: this.BeforeSubmitCell,            // use in edit cell
            afterSubmitCell: this.AfterSubmitCell,              // use in edit cell
            afterSaveCell: this.AfterSaveCell,                  // use in edit cell
            errorCell: this.ErrorCell,                           // use in edit cell
            ondblClickRow: this.OndblClickRow,
            colNames: this.ColNames,
            colModel: this.ColModel,
            cmTemplate: this.CmTemplate,
            serializeGridData: this.SerializeGridData,
            serializeCellData: this.SerializeCellData,
            serializeRowData: this.SerializeRowData,
            ajaxGridOptions: this.AjaxGridOptions,
            ajaxCellOptions: this.AjaxCellOptions,//{ contentType: "application/json; charset=utf-8", dataType: "json" },
            ajaxRowOptions: this.AjaxRowOptions,
            pager: this.PagerId,
            width: this.Width,
            height: this.Height,
            loadui: this.LoadUi,
            prmNames: {
                rows: "rows",
                page: "page"
            },
            jsonReader: this.JsonReader,
            rowNum: this.RowNum,
            pginput: this.PgInput,
            pgbuttons: this.PgButtons,
            autosearch: this.AutoSearch,
            multiselect: this.MultiSelect,
            viewrecords: this.ViewRecords,
            gridview: this.GridView,
            sortname: this.SortName,
            sortorder: this.SortOrder,
            caption: this.Caption,
            shrinkToFit: this.ShrinkToFit,
            autowidth: this.AutoWidth,
            forceFit: this.ForceFit,
            scroll: this.Scroll,
            scrollrows: this.ScrollRows,
            postData: this.PostData,
            onPaging: this.OnPaging,
            onSelectRow: this.OnSelectRow,
            onCellSelect: this.OnCellSelect,
            onSelectCell: this.OnSelectCell,
            onSelectAll: this.OnSelectAll,
            beforeRequest: function () {
                //setTimeout(function() { 
                    //ui.showLoading($($this.GridId).closest(".ui-jqgrid")); 
                //}, 500);
                
                if ($this.BeforeRequest != undefined && $this.BeforeRequest != null && typeof ($this.BeforeRequest) == 'function') {
                    $this.BeforeRequest();
                }
            },
            beforeProcessing: function (data, status, xhr) {
                //setTimeout(function () {
                    //ui.hideLoading($($this.GridId).closest(".ui-jqgrid"));
                //}, 500);
                
                if ($this.BeforeProcessing != undefined && $this.BeforeProcessing != null && typeof ($this.BeforeProcessing) == 'function') {
                    $this.BeforeProcessing(data, status, xhr);
                }
            },
            gridComplete: function () {
                //ui.hideLoading($($this.GridId).closest(".ui-jqgrid"));
                if ($this.GridComplete != undefined && $this.GridComplete != null && typeof ($this.GridComplete) == 'function') {
                    $this.GridComplete();
                }
            },
            loadComplete: this.LoadComplete,
            afterInsertRow: this.AfterInsertRow, //  will don't run if grouping enable
            grouping: this.Grouping,
            groupingView: this.GroupingView,
            loadonce: this.LoadOnce
        });

        this.Grid = $(this.GridId);

        //this.Grid.jqGrid('navGrid', this.PagerId,
        //    {
        //        add: false,
        //        edit: true,
        //        del: false,
        //        search: false,
        //        shrinkToFit: false
        //    },
        //    {
        //        ajaxEditOptions: { contentType: "application/json" },
        //    }, // default settings for edit
        //    {}, // add
        //    {}, // delete
        //    {
        //        closeOnEscape: true,
        //        closeAfterSearch: true
        //    },
        //    //search
        //    {}
        //);

    }
    catch (ex) {
        throw ex;
    }
};

BaseGrid.prototype.reloadGrid = function () {
    $(this.GridId).trigger('reloadGrid');
};

BaseGrid.prototype.sortGrid = function (sortName, sortOrder, reload) {
    this.Grid.jqGrid("setGridParam", { sortorder: sortOrder });
    this.Grid.jqGrid("sortGrid", sortName, reload);
};

BaseGrid.prototype.getCellIndexByName = function (cellName) {
    var cm = $(this.GridId).jqGrid('getGridParam', 'colModel');
    var i, l;
    for (i = 0, l = cm.length; i < l; i += 1) {
        if (cm[i].name === cellName) {
            return i; // return the index
        }
    }
    return -1;
};

BaseGrid.prototype.getCellNameByIndex = function (cellIndex) {
    var cm = this.Grid.jqGrid("getGridParam", "colModel");
    var colNameAttr = cm[cellIndex];
    if (colNameAttr != null)
        return colNameAttr.name;
    return "";
};

BaseGrid.prototype.mergeCell = function (cellName) {
    var ids = this.Grid.getDataIDs();

    var length = ids.length;
    for (var i = 0; i < length; i++) {
        var before = this.Grid.jqGrid('getRowData', ids[i]);
        var rowSpanCount = 1;
        for (var j = i + 1; j <= length; j++) {
            var end = this.Grid.jqGrid('getRowData', ids[j]);
            if (before[cellName] == end[cellName]) {
                rowSpanCount ++;
                this.Grid.setCell(ids[j], cellName, '', {
                    display: 'none'
                });
            } else {
                rowSpanCount = 1;
                break;
            }
            if (rowSpanCount > 1)
                this.Grid.find("tr.jqgrow[id=" + ids[i] + "]").find("td[aria-describedby*='" + cellName + "']").attr("rowspan", rowSpanCount);
        }
    }
};

BaseGrid.prototype.hideLoopValueInRow = function (cellName) {
    var rows = this.Grid.find("tr").not(".jqgfirstrow");
    var length = rows.length;

    for (var i = 0; i < length; i++) {
        var beforeRowId = rows[i].id;
        var before = this.Grid.jqGrid('getRowData', beforeRowId);
        if ($(rows[i]).is("[id*='ghead']")) {
            continue;
        }
        
        for (var j = i + 1; j < length; j++) {
            if ($(rows[j]).is("[id*='ghead']")) break;

            var afterRowId = rows[j].id;
            var end = this.Grid.jqGrid('getRowData', afterRowId);
            if (before[cellName] == end[cellName])
            {
                this.Grid.find("tr.jqgrow[id=" + afterRowId + "]").find("td[aria-describedby*='" + cellName + "']").children().hide();
            }
            else break;
        }
    }
};

BaseGrid.prototype.hideCellValueInRow = function (cellName) {
    var rows = this.Grid.find("tr").not(".jqgfirstrow, .jqfoot");
    var length = rows.length;

    for (var i = 0; i < length; i++) {
        var rowId = rows[i].id;
        
        if ($(rows[i]).is("[id*='ghead']")) {
            continue;
        }
        this.Grid.find("tr.jqgrow[id=" + rowId + "]").find("td[aria-describedby*='" + cellName + "']").children().hide();
    }
};

BaseGrid.prototype.applyClassToHeader = function (cellName) {
    var gridId = this.GridId.replace("#", "");
    var divs = $("thead", this.Grid.hdiv).closest('div');

    var table = $("table[aria-labelledby*='" + gridId + "']", divs);
    var trHead = $("thead", table);
    var colModel = this.Grid.getGridParam("colModel");

    for (var iCol = 0; iCol < colModel.length; iCol++) {
        var col = colModel[iCol];
        if (col.name == cellName && col.classes) {
            var headDiv = jQuery("th:eq(" + iCol + ") div", trHead);
            headDiv.addClass(col.classes);
        }
    }
};

BaseGrid.prototype.displayEmptyText = function() {
    var emptyText = this.Grid.getGridParam('emptyrecords');
    $('tbody', this.GridId).html('<tr><td><div class="ui-jgrid-empty">' + emptyText + '</div></td></tr>');
};

BaseGrid.prototype.getVisibleColnames = function () {
    var arr = [];
    var colNames = this.Grid.getGridParam('colNames');
    var colModels = this.Grid.getGridParam('colModel');
    
    for (var i = 0; i < colModels.length; i++) {
        var colModel = colModels[i];
        if (colModel && (colModel.hidedlg == undefined || colModel.hidedlg == false)) {
            if (colModel.hidden == false) {
                var colName = colNames[i];
                arr.push(colName);
            }
        }
    }
    return arr;
};

BaseGrid.prototype.getHiddenColnames = function () {
    var arr = [];
    var colNames = this.Grid.getGridParam('colNames');
    var colModels = this.Grid.getGridParam('colModel');

    for (var i = 0; i < colModels.length; i++) {
        var colModel = colModels[i];
        if (colModel && colModel.hidedlg == true || colModel.hidedlg == undefined) {
            if (colModel.hidden && colModel.classes != 'hidden') {
                var colName = colNames[i];
                arr.push(colName);
            }
        }
    }
    return arr;
};

BaseGrid.prototype.getVisibleColumns = function () {
    var arr = [];
    var colNames = this.Grid.getGridParam('colNames');
    var colModels = this.Grid.getGridParam('colModel');

    for (var i = 0; i < colModels.length; i++) {
        var colModel = colModels[i];
        if (colModel && (colModel.hidedlg == undefined || colModel.hidedlg == false)) {
            if (colModel.hidden == false) {
                var item = {
                    ColName: colNames[i],
                    ColModel: colModels[i]
                };
                arr.push(item);
            }
        }
    }
    return arr;
};

BaseGrid.prototype.getHiddenColumns = function () {
    var arr = [];
    var colNames = this.Grid.getGridParam('colNames');
    var colModels = this.Grid.getGridParam('colModel');

    for (var i = 0; i < colModels.length; i++) {
        var colModel = colModels[i];
        if (colModel && colModel.hidedlg == true || colModel.hidedlg == undefined) {
            if (colModel.hidden && colModel.classes != 'hidden') {
                var item = {
                    ColName: colNames[i],
                    ColModel: colModels[i]
                };
                arr.push(item);
            }
        }
    }
    return arr;
};

BaseGrid.prototype.getFullColumns = function () {
    var arr = [];
    var colNames = this.ColNames;
    var colModels = this.ColModel;

    for (var i = 0; i < colModels.length; i++) {
        var item = {
            ColName: colNames[i],
            CellName: colModels[i].name,
            ColModel: colModels[i]
        };
        arr.push(item);
    }
    return arr;
};

BaseGrid.prototype.getColumnWidthFixed = function () {
    var colModels = this.Grid.getGridParam('colModel');
    var width = 0;

    for (var i = 0; i < colModels.length; i++) {
        var colModel = colModels[i];
        if (colModel.hidden == false && colModel.fixed) {
            width += colModel.width;
        }
    }

    return width;
};

BaseGrid.prototype.getColumnWidthNotFixed = function () {
    var colModels = this.Grid.getGridParam('colModel');
    var width = 0;

    for (var i = 0; i < colModels.length; i++) {
        var colModel = colModels[i];
        if (colModel.hidden == false) {
            width += colModel.width;
        }
    }

    return width + 60;
};


function BaseUi() {
    this.LoadingContent = $('#pageLoading');
}

BaseUi.prototype.showPageLoading = function () {
    var newContent = this.LoadingContent.clone();
    $(newContent).attr("id", common.RandomId());
    $.blockUI({
        showOverlay: true,
        fadeIn: 0,
        fadeOut: 0,
        message: $(newContent),
        css: {
            //top: '50%',
            //left: '50%',
            padding: 0,
            margin: 0,
            textAlign: 'center',
            border: 'none',
            backgroundColor: 'transparent',
            cursor: 'wait'
        }
    });
};

BaseUi.prototype.showLoading = function (selector) {
    var newContent = this.LoadingContent.clone();
    $(newContent).attr("id", common.RandomId());
    $(selector).block({
        showOverlay: true,
        fadeIn: 0,
        fadeOut: 0,
        message: $(newContent),
        centerX: false,
        centerY: false,
        css: {
            top: '45%',
            left: '45%',
            padding: 0,
            margin: 0,
            textAlign: 'center',
            border: 'none',
            backgroundColor: 'transparent',
            cursor: 'wait'
        }
    });
};

BaseUi.prototype.hideLoading = function (selector) {
    $(selector).unblock();
};

BaseUi.prototype.hidePageLoading = function () {
    $.unblockUI();
};

BaseUi.prototype.loadCss = function (startcss, callback) {
    ensure({
        css: startcss
    }, function () {
        if (typeof (callback) === 'function') callback.call();
    });
};

BaseUi.prototype.loadScript = function (startjs, callback) {
    ensure({
        js: startjs
    }, function () {
        callback.call();
    });
};

BaseUi.prototype.getResources = function (js, css, callback) {
    ensure({
        js: js,
        css: css
    }, function () {
        if (typeof (callback) === 'function') callback.call();
    });
};

BaseUi.prototype.addStylesheet = function (fileName) {
    var head = $("head");
    var linkElement = "<link rel='stylesheet' href='" + fileName + "' type='text/css'>";
    head.prepend(linkElement);
};

BaseUi.prototype.addLastStylesheet = function (fileName) {
    var head = $("head");
    var last = head.find("link[rel='stylesheet']:last");
    var linkElement = "<link rel='stylesheet' href='" + fileName + "' type='text/css'>";
    if (last.length) {
        last.after(linkElement);
    }
    else {
        head.append(linkElement);
    }
};

// Check if value is Integer and greater than 0
BaseUi.prototype.ValidateInt = function (val, colname) {
    if (isNaN(val) == false && val.trim() != "") {
        if (parseInt(val) > 0) {
            return [true, ""];
        } else {
            return [false, "<b>" + colname + "</b>" + ": " + Resources.Validate_NumberGreaterThan + " 0"];
        }
    } else {
        return [false, "<b>" + colname + "</b>" + ": " + Resources.Validate_NumberGreaterThan + " 0"];
    }
};

BaseUi.prototype.ValidateRequired = function (val, colname) {
    if (val.trim() != "") {
        return [true, ""];
    } else {
        return [false, "<b>" + colname + "</b>" + ": " + Resources.Validate_RequireValue];
    }
};

BaseUi.prototype.ValidateDateTime = function(rowId, val, colName, colText, childName, childText, isRequired, isGreaterThanCurrentDate, isGreaterDate) {
    if (isRequired) {
        if (val.trim() == "") {
            return [false, "<b>" + colText + "</b>" + ": " + Resources.Validate_RequireValue];
        }
    }
    if (val.trim() != "") {
        var datetime = Date.parse(val);
        if (isNaN(datetime)) {
            return [false, "<b>" + colText + "</b>" + ": " + Resources.Validate_NotCorrectDatetimeFormat];
        } else {
            if (isGreaterThanCurrentDate) {
                var nextDate = Date.parse(common.getDate(1));
                if (datetime < nextDate)
                    return [false, "<b>" + colText + "</b>" + ": " + Resources.Validate_DateMustGreaterThanCurrentDate];
                else {
                    if (isGreaterDate) {
                        var parentDate = $("#" + rowId + "_" + colName).val();
                        var childDate = $("#" + rowId + "_" + childName).val();
                        if (Date.parse(parentDate) < Date.parse(childDate)) {
                            return [false, "<b>" + colText + "</b>" + ": " + Resources.Validate_DateMustGreaterThan + childText + " !"];
                        } else {
                            return [true, ""];
                        }
                    } else
                        return [true, ""];
                }
            } else {
                return [true, ""];
            }
        }
    } else {
        return [true, ""];
    }
};

BaseUi.prototype.isValidDateTime = function (val, formatDate, formatTime) {
    try {
        $.datepicker.parseDateTime(formatDate, formatTime, val, null, {
            timeFormat: formatTime
        });
    } catch(e) {
        return false;
    }
    return true;
};

