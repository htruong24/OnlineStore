NotificationManagement.prototype.constructor = NotificationManagement;      // Otherwise instances of Cat would have a constructor of Mammal 

function NotificationManagement() {
    this.BtnUploadFile = "#btnUploadFile";
    this.FrmFileUpload = "#frmFileUpload";
    this.FileUpload = "#fileUpload";
    this.LinkErrorCsv = "#linkErrorCsv";
    this.CsvFile = "#csvFile";
    this.SubFile = "#subFile";
}

NotificationManagement.prototype.bindEvents = function () {
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

NotificationManagement.prototype.uploadFile = function () {
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
                noticeInfo.reloadGrid();
            }
            else if (result.ErrorCode == "1") {
                $($this.LinkErrorCsv).removeClass("hide");
                noticeInfo.reloadGrid();
            } else {
                jAlert(result.ErrorMessage, Resources.Notice);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            jAlert(errorThrown, Resources.Message_CannotUploadFile);
        }
    });
};


NoticeInfo.prototype = new BaseGrid();
NoticeInfo.prototype.constructor = NoticeInfo;

function NoticeInfo() {
}

NoticeInfo.prototype.setGridData = function () {
    var $this = this;
    this.GridId = '#gridNoticeInfo';
    this.PagerId = '#gridNoticeInfoPager';
    this.SortName = 'No';
    this.SortOrder = 'asc';
    this.Url = '/NotificationManagement/GetNoticeInfo';
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
      Resources.NotificationManagement_MailDelivCondID,
      Resources.NotificationManagement_Text,
      Resources.NotificationManagement_StartDate,
      Resources.NotificationManagement_EndDate,
      Resources.NotificationManagement_Url
    ];
    this.ColModel = [
        { name: 'No', index: 'No' },
        { name: 'Uid', key: true },
        { name: 'MailDelivCondID', index: 'MailDelivCondID' },
        { name: 'Title', index: 'Title' },
        { name: 'DeliveryTime', index: 'DeliveryTime' },
        { name: 'NoticeDisplayEndTime', index: 'NoticeDisplayEndTime' },
        { name: 'TransitionsURL', index: 'TransitionsURL' }
    ];
};



