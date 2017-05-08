NewsManagement.prototype = new BaseGrid();
NewsManagement.prototype.constructor = NewsManagement;

function NewsManagement() {
    
}

NewsManagement.prototype.setGridData = function() {
    var $this = this;
    this.GridId = '#gridNews';
    this.PagerId = '#gridNewsPager';
    this.SortName = 'DeliveryDate';
    this.SortOrder = 'desc';
    this.Url = '/NewsManagement/GetBriefNews';
    this.DataType = 'json';
    this.SerializeGridData = function(postData) {
        return JSON.stringify(postData);
    };
    this.AjaxGridOptions = { contentType: "application/json" };

    this.PostData = {};

    this.MType = 'POST';
    this.AutoWidth = true;
    this.ShrinkToFit = true;
    this.Height = 570;

    this.RowNum = 25;
    this.ViewRecords = true;
    this.PgButtons = true;
    this.PgInput = true;
    this.CmTemplate = { search: false };

    this.ColNames = ["NewsItemId",
        Resources.NewsManagement_DeliveryDate,
        Resources.NewsManagement_DeliveryTime,
        Resources.NewsManagement_Headline,
        Resources.NewsManagement_SubHeadline,
        ""
    ];
    this.ColModel = [
        { name: 'NewsItemId', hidden: true, key: true },
        {
            name: 'DeliveryDate',
            index: 'DeliveryDate',
            width: 130,
            fixed: true,
            align: 'center',
            formatter: this.formatDate
        },
        {
            name: 'DeliveryTime',
            index: 'DeliveryDate',
            width: 130,
            fixed: true,
            align: 'center',
            formatter: this.formatTime
        },
        { name: 'Headline', index: 'Headline', formatter: this.formatLink },
        { name: 'SubHeadline', index: 'SubHeadline', formatter: this.formatLink },
        {
            name: 'NewsItemId', sortable: false, align: 'center', width: 60, fixed: true,
            formatter: this.formatEditLink
        }
    ];
};

NewsManagement.prototype.formatDate = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined)
        return common.convertDateYYYYMMDD(cellvalue);
    return "";
};

NewsManagement.prototype.formatTime = function (cellvalue, options, rowObject) {
    if (rowObject && rowObject.DeliveryDate != undefined)
        return common.convertTimeHH24MI(rowObject.DeliveryDate);
    return "";
};

NewsManagement.prototype.formatLink = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined) {
        if (rowObject && rowObject.NewsItemId != undefined)
            return '<a href="/NewsManagement/Edit/' + rowObject.NewsItemId + '">' + cellvalue + '</a>';
        else return cellvalue;
    }
    return "";
};

NewsManagement.prototype.formatEditLink = function (cellvalue, options, rowObject) {
    if (cellvalue != null && cellvalue != undefined) {
        if (rowObject && rowObject.NewsItemId != undefined)
            return '<a href="/NewsManagement/Edit/' + rowObject.NewsItemId  + '"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></a>';
        else return cellvalue;
    }
    return "";
};