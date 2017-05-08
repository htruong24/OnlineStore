UserManagementEdit.prototype.constructor = UserManagementEdit;      // Otherwise instances of Cat would have a constructor of Mammal 

function UserManagementEdit() {
    this.BtnViewDelete = "#btnViewDelete";
    this.BtnViewWithdrawal = "#btnViewWithdrawal";
    this.BtnEdit = "#btnEdit";

    this.BtnEditDelete = "#btnEditDelete";
    this.BtnEditWithdrawal = "#btnEditWithdrawal";
    this.BtnUpdate = "#btnUpdate";

    this.ViewPart = "#ViewPart";
    this.EditPart = "#EditPart";

    //View
    this.ViewNickname = "#ViewNickname";
    this.ViewGender = "#ViewGender";
    this.ViewDateOfBirth = "#ViewDateOfBirth";
    this.ViewPlaceOfBirth = "#ViewPlaceOfBirth";
    this.ViewFavoriteSport = "#ViewFavoriteSport";
    this.ViewFavoriteTeam = "#ViewFavoriteTeam";
    this.ViewProfileImg = "#ViewProfileImg";

    //Edit
    this.EditNickname = "#EditNickname";
    this.EditGender = "#EditGender";
    this.EditDateOfBirth = "#EditDateOfBirth";
    this.EditPlaceOfBirth = "#EditPlaceOfBirth";
    this.EditFavoriteSport = "#EditFavoriteSport";
    this.EditFavoriteTeam = "#EditFavoriteTeam";
    this.EditProfileImg = "#EditProfileImg";
    
    this.MemberId = -1;

    this.MemberRequestModel = {
        PointID: -1,
        MemberID: -1,
        FundsPoint: -1,
        PossessionPoint: -1,
        PayOffPoints: -1,
    };

    //Point Details

    this.PointRequestModel = {
        MemberId: -1,
        Nickname: '',
        Gender: -1,
        DateOfBirth: '',
        PlaceOfBirth: -1,
        FavoriteSport: '',
        FavoriteTeam: ''
    };
}

//Member Details

UserManagementEdit.prototype.getPrefectureMappingModelList = function () {
    var $this = this;
    $.ajax({
        type: "POST",
        contentType: 'application/json',
        url: '/Prefecture/GetPrefectureMappingModelList',
        async: false,
        cache: false,
        success: function (data) {
            if (data != null) {
                if (data.ErrorCode == "0") {
                    if (data.Result != null && data.Result.length > 0) {
                        var slPlaceOfBirthId = $($this.EditPlaceOfBirth);
                        slPlaceOfBirthId.empty();
                        slPlaceOfBirthId.append('<option id="PlaceOfBirthItem" name="PlaceOfBirthItem" value="" ></option>');
                        for (var i = 0; i < data.Result.length; i++) {
                            var item = data.Result[i];
                            slPlaceOfBirthId.append('<option id="PlaceOfBirthItem" name="PlaceOfBirthItem" value="' + item.PrefecturesID + '" >' + item.PrefecturesName + '</option>');
                        }
                    }
                } else {
                    jAlert(data.ErrorMessage, Resources.Notice);
                }
            }
        },
        error: function (jqXHR, error, errorThrown) { }
    });
};

UserManagementEdit.prototype.getFavoriteTeamMappingModelList = function () {
    var $this = this;
    var postData = common.parseArray($(this.EditFavoriteSport).val());
    $.ajax({
        type: "POST",
        url: '/Team/GetFavoriteTeamMappingModelList',
        async: false,
        cache: false,
        data: { sportIds: postData },
        success: function (data) {
            if (data != null) {
                if (data.ErrorCode == "0") {
                    if (data.Result != null) {
                        var slFavoriteTeamId = $($this.EditFavoriteTeam);
                        slFavoriteTeamId.empty();
                        if (data.Result.length > 0) {
                            for (var i = 0; i < data.Result.length; i++) {
                                var item = data.Result[i];
                                slFavoriteTeamId.append('<option id="FavoriteTeamItem" name="FavoriteTeamItem" value="' + item.TeamId + '_' + item.SportId + '" >' + item.TeamName + '</option>');
                            }
                        }
                    }
                } else {
                    jAlert(data.ErrorMessage, Resources.Notice);
                }
            }
        },
        error: function (jqXHR, error, errorThrown) { }
    });
};

UserManagementEdit.prototype.getFavoriteSportMappingModelList = function () {
    var $this = this;
    $.ajax({
        type: "POST",
        contentType: 'application/json',
        url: '/Sport/GetFavoriteSportMappingModelList',
        async: false,
        cache: false,
        success: function (data) {
            if (data != null) {
                if (data.ErrorCode == "0") {
                    if (data.Result != null && data.Result.length > 0) {
                        var slFavoriteSportId = $($this.EditFavoriteSport);
                        slFavoriteSportId.empty();
                        for (var i = 0; i < data.Result.length; i++) {
                            var item = data.Result[i];
                            slFavoriteSportId.append('<option id="FavoriteSportItem" name="FavoriteSportItem" value="' + item.SportsID + '" >' + item.SportsName + '</option>');
                        }
                    }
                } else {
                    jAlert(data.ErrorMessage, Resources.Notice);
                }
            }
        },
        error: function (jqXHR, error, errorThrown) { }
    });
};

UserManagementEdit.prototype.initEditPart = function () {
    var $this = this;

    $("select").chosen({ width: "inherit", allow_single_deselect: true });

    $(this.EditDateOfBirth).datepicker({ dateFormat: 'yy-mm-dd' });

    $(this.EditGender).val($(this.EditGender).attr("value")).trigger("chosen:updated");
    $(this.EditPlaceOfBirth).val($(this.EditPlaceOfBirth).attr("value")).trigger("chosen:updated");
    $(this.EditFavoriteSport).val($(this.EditFavoriteSport).attr("value").split(',')).trigger("chosen:updated");

    $this.getFavoriteTeamMappingModelList();

    $(this.EditFavoriteTeam).val($(this.EditFavoriteTeam).attr("value").split(',')).trigger("chosen:updated");

    $(this.ViewPart).removeClass("hide");
    $(this.EditPart).addClass("hide");
};

UserManagementEdit.prototype.bindEvents = function () {
    var $this = this;

    $(this.BtnViewDelete).click(function () {
        $this.deleteMemberProfileImage();
    });

    $(this.BtnEditDelete).click(function () {
        $this.deleteMemberProfileImage();
    });

    $(this.BtnViewWithdrawal).click(function () {
        $this.withdrawal();
    });
    
    $(this.BtnEditWithdrawal).click(function () {
        $this.withdrawal();
    });

    $(this.BtnEdit).click(function () {
        $($this.ViewPart).addClass("hide");
        $($this.EditPart).removeClass("hide");
    });

    $(this.BtnUpdate).click(function() {
        if ($this.checkFormError()) {
            $('#dialog').html(Resources.UserManagement_UpdateMessage);
            $("#dialog").dialog({
                resizable: false,
                height:'auto',
                title: Resources.UserManagement_Update,
                modal: true,
                buttons: {
                    "OK": function() {

                        var memberId = $this.MemberId;
                        var nickname = $($this.EditNickname).val();
                        var dateOfBirth = $($this.EditDateOfBirth).val();
                        var gender = common.parseInt($($this.EditGender).val());
                        var editPlaceOfBirth = common.parseInt($($this.EditPlaceOfBirth).val());
                        var editFavoriteSport = common.parseArray($($this.EditFavoriteSport).val());
                        var editFavoriteTeam = common.parseArray($($this.EditFavoriteTeam).val());

                        $this.MemberRequestModel.MemberID = memberId;
                        $this.MemberRequestModel.Nickname = nickname;
                        $this.MemberRequestModel.DateOfBirth = dateOfBirth;
                        $this.MemberRequestModel.Gender = gender;
                        $this.MemberRequestModel.PlaceOfBirth = editPlaceOfBirth;
                        $this.MemberRequestModel.FavoriteSport = editFavoriteSport;
                        $this.MemberRequestModel.FavoriteTeam = editFavoriteTeam;

                        var postData = $this.MemberRequestModel;
                        $.ajax({
                            type: "POST",
                            url: '/UserManagement/Create',
                            async: false,
                            cache: false,
                            data: postData,
                            success: function(data) {
                                if (data.ErrorCode == "0") {
                                    // update
                                    var nickname = $($this.EditNickname).val();
                                    var dateOfBirth = $($this.EditDateOfBirth).val();
                                    var viewGender = $($this.EditGender + " option:selected").text();
                                    var viewPlaceOfBirth = $($this.EditPlaceOfBirth + " option:selected").text();
                                    var viewFavoriteSport = common.parseChosen($this.EditFavoriteSport);
                                    var viewFavoriteTeam = common.parseChosen($this.EditFavoriteTeam);

                                    $($this.ViewNickname).text(nickname);
                                    $($this.ViewGender).text(viewGender);
                                    $($this.ViewDateOfBirth).text(dateOfBirth);
                                    $($this.ViewPlaceOfBirth).text(viewPlaceOfBirth);
                                    $($this.ViewFavoriteSport).text(viewFavoriteSport);
                                    $($this.ViewFavoriteTeam).text(viewFavoriteTeam);

                                    $($this.EditPart).addClass("hide");
                                    $($this.ViewPart).removeClass("hide");
                                } else {
                                    jAlert(data.ErrorMessage, Resources.Notice);
                                }
                            },
                            error: function(jqXHR, error, errorThrown) {
                                jAlert(error, Resources.Notice);
                            }
                        });

                        $(this).dialog("close");
                    },
                    "キャンセル": function () {
                        $(this).dialog("close");
                    }
                }
            });
        } 
    });
    
    $(this.EditFavoriteSport).change(function () {
        var favoriteTeam = $($this.EditFavoriteTeam).val();
        $this.getFavoriteTeamMappingModelList();
        $($this.EditFavoriteTeam).trigger("chosen:updated");
        $($this.EditFavoriteTeam).val(favoriteTeam).trigger("chosen:updated");
    });
    
    $(this.EditFavoriteTeam).change(function () {
        
    });
};

UserManagementEdit.prototype.deleteMemberProfileImage = function () {
    var $this = this;
    $('#dialog').html(Resources.NewsManagement_UpdateNewsMessage);
    $("#dialog").dialog({
        resizable: false,
        title: Resources.UserManagement_DeleteImage,
        height: 'auto',
        modal: true,
        buttons: {
            "OK": function () {
                var postData = $this.MemberId;
                $.ajax({
                    type: "POST",
                    url: '/UserManagement/DeleteMemberProfileImage',
                    async: false,
                    cache: false,
                    data: { memberId: postData },
                    success: function (data) {
                        if (data.ErrorCode == "0") {
                            // update
                            $($this.ViewProfileImg).attr("src", "blank");
                            $($this.EditProfileImg).attr("src", "blank");
                        } else {
                            jAlert(data.ErrorMessage, Resources.Notice);
                        }
                    },
                    error: function (jqXHR, error, errorThrown) { }
                });
                
                $(this).dialog("close");
            },
            "キャンセル": function () {
                $(this).dialog("close");
            }
        }
    });
};

UserManagementEdit.prototype.withdrawal = function () {
    var $this = this;
    
    $('#dialog').html(Resources.UserManagement_WidthdrawalMessage);
    $("#dialog").dialog({
        resizable: false,
        title: Resources.UserManagement_Withdrawal,
        height: 'auto',
        modal: true,
        buttons: {
            "OK": function () {

                var postData = $this.MemberId;
                $.ajax({
                    type: "POST",
                    url: '/UserManagement/Withdrawal',
                    async: false,
                    cache: false,
                    data: { memberId: postData },
                    success: function (data) {
                        if (data.ErrorCode == "0") {

                        } else {
                            jAlert(data.ErrorMessage, Resources.Notice);
                        }
                    },
                    error: function (jqXHR, error, errorThrown) { }
                });

                $(this).dialog("close");
            },
            "キャンセル": function () {
                $(this).dialog("close");
            }
        }
    });



   
};

PossessionPoint.prototype = new BaseGrid();
PossessionPoint.prototype.constructor = PossessionPoint; 

function PossessionPoint() {
    this.MemberId = -1;
}

PossessionPoint.prototype.setGridData = function() {
    var $this = this;
    this.GridId = '#gridPossessionPoint';
    this.PagerId = '#gridPossessionPointPager';
    this.SortName = 'GiveTargetYM';
    this.SortOrder = 'desc';
    this.Url = '/UserManagement/GetPossessionPoints';
    this.DataType = 'json';

    this.SerializeRowData = function(postData) {
        return JSON.stringify(postData);
    };

    this.SerializeGridData = function(postData) {
        return JSON.stringify(postData);
    };

    this.AjaxGridOptions = { contentType: "application/json" };

    this.AjaxRowOptions = { contentType: "application/json" };

    this.PostData = {
        searchRequest: $this.MemberId
    };

    this.MType = 'POST';
    this.Height = 200;
    this.ShrinkToFit = true;
    this.AutoWidth = true;
    this.RowNum = 10;
    this.ViewRecords = true;
    this.PgButtons = true;
    this.PgInput = true;
    this.CmTemplate = { search: false };
    
    this.ColNames = [Resources.UserManagement_GiveTargetYM,
        Resources.UserManagement_GiveTargetWeek,
        Resources.UserManagement_FundsPoint,
        Resources.UserManagement_PossessionPoint,
        Resources.UserManagement_PayOffPoint,
        "", "PayOffFlg"];
    
    this.ColModel = [
        {
            name: 'GiveTargetYM',
            index: 'GiveTargetYM',
            formatter: function(cellvalue, options, rowObject) {
                return cellvalue;
            }
        },
        { name: 'GiveTargetWeek', index: 'GiveTargetWeek' },
        { name: 'FundsPoint', index: 'FundsPoint', editrules: { custom: true, custom_func: ui.ValidateInt }, editable: true },
        { name: 'PossessionPoint', index: 'PossessionPoint', editrules: { custom: true, custom_func: ui.ValidateInt }, editable: true },
        { name: 'PayOffPoints', index: 'PayOffPoints', editrules: { custom: true, custom_func: ui.ValidateInt }, editable: true },
        {
            name: 'PointID',
            index: 'PointID',
            sortable: false,
            key: true,
            formatter: function(cellvalue, options, rowObject) {
                return '<a href="javascript:void(0);"><span class="glyphicon glyphicon-pencil edit-item" style="padding-right:5px" aria-hidden="true" rowid="' + cellvalue + '"></span></a>'
                    + '<a href="javascript:void(0);"><span class="glyphicon glyphicon glyphicon-floppy-saved save-item hide" style="padding-right:5px" aria-hidden="true" rowid="' + cellvalue + '"></span></a>'
                    + '<a href="javascript:void(0);"><span class="glyphicon glyphicon-refresh cancel-item hide" aria-hidden="true" rowid="' + cellvalue + '"></span></a>';
            }
        },
        { name: 'PayOffFlg', index: 'PayOffFlg', hidden: true }
    ];

    this.LoadComplete = function(data) {
        $this.bindEditRow();
    };
};

PossessionPoint.prototype.bindEditRow = function() {
    var $this = this;
    var editparameters = {
        "keys": true,
        "oneditfunc": function(rowid) {
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
        "url": '/UserManagement/UpdatePossessionPoint',
        "extraparam": {
            MemberID: $this.MemberId
        },
        "aftersavefunc": function(rowId) {
            var cellAction = $this.Grid.find("tr.jqgrow[id=" + rowId + "]").find("td[aria-describedby*='PointID']");
            $("span.save-item", cellAction).addClass("hide");
            $("span.cancel-item", cellAction).addClass("hide");
            $("span.edit-item", cellAction).removeClass("hide");

            historyPoint.reloadGrid();
        },
        "errorfunc": function() {
            alert("Error");
        },
        "afterrestorefunc": function(rowId) {
            var cellAction = $this.Grid.find("tr.jqgrow[id=" + rowId + "]").find("td[aria-describedby*='PointID']");
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

        var isPayOff = common.parseBoolean(rowData.PayOffFlg);
        if (!isPayOff)
            $this.Grid.setColProp("PayOffPoints", { editable: false });
        else
            $this.Grid.setColProp("PayOffPoints", { editable: true });

        $this.Grid.editRow(rowid, editparameters);

        $(this).closest("td").find("span.save-item").removeClass("hide");
        $(this).closest("td").find("span.cancel-item").removeClass("hide");
        $(this).closest("td").find("span.edit-item").addClass("hide");
    });

    $(this.GridId).on('click', 'span.cancel-item', function(e) {
        var rowid = $(this).attr("rowid");
        $this.Grid.restoreRow(rowid, editparameters);
    });

    $(this.GridId).on('click', 'span.save-item', function(e) {
        var rowid = $(this).attr("rowid");
        $this.Grid.saveRow(rowid, editparameters);
    });
};

HistoryPoint.prototype = new BaseGrid();
HistoryPoint.prototype.constructor = HistoryPoint;

function HistoryPoint() {
    this.MemberId = -1;
}

HistoryPoint.prototype.setGridData = function () {
    var $this = this;
    this.GridId = '#gridHistoryPoint';
    this.PagerId = '#gridHistoryPointPager';
    this.SortName = 'GiveTargetYM';
    this.SortOrder = 'desc';
    this.Url = '/UserManagement/GetHistoryPoints';
    this.DataType = 'json';

    this.SerializeGridData = function (postData) {
        return JSON.stringify(postData);
    };
    this.AjaxGridOptions = { contentType: "application/json" };

    this.PostData = {
        searchRequest: $this.MemberId
    };

    this.MType = 'POST';
    this.Height = 200;
    this.ShrinkToFit = true;
    this.AutoWidth = true;
    this.RowNum = 10;
    this.ViewRecords = true;
    this.PgButtons = true;
    this.PgInput = true;
    this.CmTemplate = { search: false };
    
    this.ColNames = [Resources.UserManagement_GiveTargetYM,
        Resources.UserManagement_GiveTargetWeek,
        Resources.UserManagement_FundsPoint,
        Resources.UserManagement_PossessionPoint,
        Resources.UserManagement_PayOffPoint,
        Resources.UserManagement_ExpectedPoint,
        Resources.UserManagement_Content,
        Resources.UserManagement_Detail,
        Resources.UserManagement_Modified];
    
    this.ColModel = [
        { name: 'GiveTargetYM', index: 'GiveTargetYM' },
        { name: 'GiveTargetWeek', index: 'GiveTargetWeek' },
        { name: 'FundsPoint', index: 'FundsPoint' },
        { name: 'PossessionPoint', index: 'PossessionPoint' },
        { name: 'PayOffPoints', index: 'PayOffPoints' },
        { name: 'ExpectedPoint', index: 'ExpectedPoint' },
        { name: 'Content', index: 'Content' },
        { name: 'Detail', index: 'Detail' },
        { name: 'Modified', index: 'Modified', formatter: this.formatDate, width: 200 }
    ];

    this.LoadComplete = function (data) {

    };
};

HistoryPoint.prototype.formatDate = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined)
        return common.convertYYYMMDDHHMMSSDateTime(cellvalue, true);
    return "";
};

HistoryPoint.prototype.bindEditRow = function () {
    var $this = this;
    var editparameters = {
        "keys": true,
        "oneditfunc": function (rowid) { },
        "successfunc": function (data) {
            if (data.responseJSON != null) {
                //if (data.responseJSON.ProjectContactId != null) {
                //    $this.Grid.jqGrid('addRowData', data.responseJSON.ProjectContactId, data.responseJSON, "after", 0);
                //    //Reset first row data
                //    $("tr[id='0'] input[name!='ProjectKey']", $this.GridId).val('');

                //} else {
                //    jAlert(data.responseJSON.Message, 'Cannot Save Client Contact');
                //}
            }
            return false;

        },
        "url": '/UserManagement/GetPossessionPoints/Edit',
        "extraparam": {},
        "aftersavefunc": function () {

        },
        "errorfunc": function () {

        },
        "afterrestorefunc": function () {

        },
        "restoreAfterError": false,
        "mtype": "POST"
    };

    $(".edit-item").each(function () {
        var rowid = $(this).attr("rowid");
        $(this).click(function () {
            $this.Grid.editRow(rowid, editparameters);
        });
    });
};

UserManagementEdit.prototype.checkFormError = function() {
    var $this = this;
    $(".error-message").text("");
    var errorMessage = "";

    $(".required").each(function () {
        $(this).parent().removeClass("has-error");
        if ($(this).val() == "") {
            $(this).parent().addClass("has-error");
            errorMessage += $(this).attr("name") + " is required ! <br>";
        }
    });
    
    $(".datetime").each(function () {
        $(this).parent().removeClass("has-error");
        if ($(this).val() != "") {
            var dt = Date.parse($(this).val());
            if (isNaN(dt)) {
                $(this).parent().addClass("has-error");
                errorMessage += $(this).attr("name") + Resources.Message_NotCorrectFormat + " <br>";
            }
        }
    });

    if (errorMessage != "") {
        $(".error-message").html(errorMessage);
        return false;
    } else {
        return true;
    }
};

