User.prototype = new BaseGrid(); 
User.prototype.constructor = User;

function User() {
    this.BtnAddNewUser = "#btnAddNewUser";
}

User.prototype.bindEvents = function () {
    var $this = this;
    $(this.BtnAddNewUser).click(function () {
        common.loadPageContent('mainContent', 'User/Create', { });
    });
};

User.prototype.setGridData = function () {
    var $this = this;
    this.GridId = '#gridUser';
    this.PagerId = '#gridUserPager';
    this.SortName = 'UserId';
    this.SortOrder = 'asc';
    this.Url = '/User/GetUsers';
    this.DataType = 'json';
    this.SerializeGridData = function (postData) {
        return JSON.stringify(postData);
    };
    this.AjaxGridOptions = { contentType: "application/json" };
    this.PostData = {};
    this.MType = 'POST';
    this.Height = 200;
    this.ShrinkToFit = true;
    this.AutoWidth = true;
    this.RowNum = 10;
    this.ViewRecords = true;
    this.PgButtons = true;
    this.PgInput = true;
    this.CmTemplate = { search: false };

    this.ColNames = [
       "Mã người dùng",
       "Tên đăng nhập",
       "Họ tên",
       "Ngày sinh",
       "Giới tính",
       "Địa chỉ",
       "Email",
       "Điện thoại",
       ""
    ];

    this.ColModel = [
                    { name: 'UserId', index: 'UserId', key: true },
                    { name: 'Username', index: 'Username' },
                    { name: 'FullName', index: 'FullName' },
                    {
                        name: 'DateOfBirth', index: 'DateOfBirth',
                        formatter: function (cellvalue, options, rowObject) {
                            return common.convertDate(cellvalue);
                        }
                    },
                    {
                        name: 'Gender', index: 'Gender',
                        formatter: function (cellvalue, options, rowObject) {
                            return common.parseGender(cellvalue);
                        }
                    },
                    { name: 'Address', index: 'Address' },
                    { name: 'Email', index: 'Email' },
                    { name: 'Phone', index: 'Phone' },
                    {
                        name: 'UserId', index: 'UserId', sortable: false,
                        formatter: function (cellvalue, options, rowObject) {
                            return '<a href="javascript:void(0);"><span class="glyphicon glyphicon-pencil edit-item" style="padding-right:5px" aria-hidden="true" rowid="' + cellvalue + '"></span></a>'
                            + '<a href="javascript:void(0);"><span class="glyphicon glyphicon-pencil details-item" style="padding-right:5px" aria-hidden="true" rowid="' + cellvalue + '"></span></a>'
                        }
                    }
    ];
    this.LoadComplete = function (data) {
        $this.bindEditRow();
    };
};

User.prototype.bindEditRow = function () {
    $("span.edit-item").on('click', function (e) {
        var rowid = $(this).attr("rowid");
        common.loadPageContent('mainContent', 'User/Create', { id: rowid });
    });

    $("span.details-item").on('click', function (e) {
        var rowid = $(this).attr("rowid");
        common.loadPageContent('mainContent', 'User/Details', { id: rowid });
    });
};






