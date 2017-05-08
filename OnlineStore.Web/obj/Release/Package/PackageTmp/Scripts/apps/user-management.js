UserManagement.prototype = new BaseGrid();        // Here's where the inheritance occurs 
UserManagement.prototype.constructor = UserManagement;      // Otherwise instances of Cat would have a constructor of Mammal 

function UserManagement() {
    this.ChkFavoriteSportId = "#chkFavoriteSport";
    this.BtnSearchMain = "#btnSearchMain";
    this.BtnSearchOptional = "#btnSearchOptional";
    this.FrmSearchMember = "#frmSearchMember";
    this.FrmEditMember = "#frmEditMember";

    this.MemberValueId = "#DisplayMemberId";
    this.NicknameValueId = "#Nickname";
    this.FavoriteSportValueId = 'input[name="FavoriteSport"]:checked';
    
    this.SearchMemberViewModel = {
        DisplayMemberId: '',
        Nickname: '',
        FavoriteSport: ''
    };
}

UserManagement.prototype.setGridData = function () {
    var $this = this;
    this.GridId = '#gridUserManagement';
    this.PagerId = '#gridUserManagementPager';
    this.SortName = 'DisplayMemberId';
    this.SortOrder = 'desc';
    this.Url = '/UserManagement/GetMembers';
    this.DataType = 'json';

    this.SerializeGridData = function (postData) {
        return JSON.stringify(postData);
    };
    this.AjaxGridOptions = { contentType: "application/json" };

    this.PostData = {
        searchRequest: this.SearchMemberViewModel
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
    

    this.ColNames = [Resources.UserManagement_Uid,
       Resources.UserManagement_Nickname,
       Resources.UserManagement_Gender,
       Resources.UserManagement_Birthday,
       Resources.UserManagement_PlaceOfBirth, 
       Resources.UserManagement_FavoriteSport,
       Resources.UserManagement_Photo,
       ""
    ];
    this.ColModel = [
                    { name: 'DisplayMemberId', index: 'DisplayMemberId', key: true },
                    {
                        name: 'Nickname', index: 'Nickname', width: 180
                    },
                    {
                        name: 'Gender', index: 'Gender',
                        formatter: function (cellvalue, options, rowObject) {
                            return common.parseGender(cellvalue);
                        }
                    },
                    {
                        name: 'DateOfBirth', index: 'DateOfBirth',
                        formatter: function (cellvalue, options, rowObject) {
                            return common.convertDateYYYYMMDD(cellvalue);
                        }
                    },
                    { name: 'ViewPlaceOfBirth', index: 'ViewPlaceOfBirth', sortable: false },
                    {
                        name: 'FavoriteSport', index: 'FavoriteSport', sortable: false,
                        formatter: function (cellvalue, options, rowObject) {
                            var arr = [];
                            for (var i = 0; i < cellvalue.length; i++) {
                                var item = cellvalue[i];
                                arr.push(item.SportName);
                            }
                            return arr.join(', ');
                        }
                    },
                    {
                        name: 'Photo', index: 'Photo', sortable: false,
                        formatter: function (cellvalue, options, rowObject) {
                            return "<img src='" + cellvalue + "' alt='' />";
                        }
                    },
                    {
                        name: 'MemberId', index: 'MemberId', sortable: false,
                        formatter: function (cellvalue, options, rowObject) {
                            var url = "/UserManagement/Details/" + cellvalue;
                            return '<a href="' + url + '"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a>';
                        }
                    }
    ];

    this.LoadComplete = function (data) {

    };
};

UserManagement.prototype.bindEvents = function () {
    var $this = this;
    $(this.BtnSearchMain).click(function () {
        $this.search();
    });

    $(this.BtnSearchOptional).click(function () {
        $this.search();
    });

    $(this.MemberValueId).keyup(function (event) {
        if (event.keyCode == 13) {
            $this.search();
        }
    });

    $(this.NicknameValueId).keyup(function (event) {
        if (event.keyCode == 13) {
            $this.search();
        }
    });
};

UserManagement.prototype.search = function () {
    var memberId = $(this.MemberValueId).val();
    var nickname = $(this.NicknameValueId).val();
    var favoriteSport = $(this.FavoriteSportValueId).map(function () {
        return this.value;
    }).get().toString();

    this.SearchMemberViewModel.DisplayMemberId = memberId;
    this.SearchMemberViewModel.Nickname = nickname;
    this.SearchMemberViewModel.FavoriteSport = favoriteSport;

    this.Grid.jqGrid('setGridParam', {
        page: 1,
        postData: {
            searchRequest: this.SearchMemberViewModel
        }
    });
    this.reloadGrid();
};

UserManagement.prototype.getFavoriteSportMappingModelList = function () {
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
                        var chkFavoriteSportId = $($this.ChkFavoriteSportId);
                        chkFavoriteSportId.empty();
                        for (var i = 0; i < data.Result.length; i++) {
                            var item = data.Result[i];
                            //if (i % 2 == 0)
                            //    chkFavoriteSportId.append('<br/>');
                            chkFavoriteSportId.append('<label class="col-md-2"><input id="FavoriteSport" name="FavoriteSport" type="checkbox" value="' + item.SportsID + '" />' + item.SportsName + '</label>');
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




