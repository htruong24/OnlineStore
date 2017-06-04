// Load View Content
function fnList(func, action) {
    if (action === "" || $('#searchContainer').length === 0) {
        fnLoadContent("view-container", func, { func: func });
    }
}

function fnLoad(action) {
    fnLoadContent("view-container", action, "");
}

function fnLoadContent(container, action, parameters) {
    //$('#' + container).empty().load(rootPath + action, parameters, function (response, status, XMLHttpRequest) {
    //    if (status === "Error") {
    //        toastr.error("Oops! Something went wrong.");
    //        $("#loadingContent").remove();
    //    }
    //});
    $('#' + container).empty().append(loadingContent);
    $.ajax({
        type: "GET",
        url: rootPath + action,
        data: parameters,
        success: function (data) {
            $("#" + container).html(data);
           $("#loadingContent").remove();
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
            $("#loadingContent").remove();
        }
    });
}

// CRUD
function fnCreate(model, controller) {
    var ctl = $("#hdControllerName").val();
    if (controller != undefined && controller !== "") {
        ctl = controller;
    }
    var action = ctl + "/Create";
    fnLoadContent("view-container", action, {});
}

function fnEdit(id, mode, controller) {
    if (controller == undefined || controller === "") {
        var param = fnGetHashParameter('action');
        if (param !== '')
            controller = param.split('/')[0];
        else
            controller = $("#hdControllerName").val();
    }
    if (mode == null | mode === '')
        mode = "Edit";
    if (id === "" || id === undefined)
        id = $("#hdId").val();
    var action = controller + "/" + "Create?id=" + encodeURI(id) + "&mode=" + mode;
    fnLoadContent("view-container", action, { id: encodeURI(id) });

}

function fnDetails(id, controller) {
    var ctrl = controller;
    if (controller == null || controller === "" || controller === "undefined") {
        ctrl = $("#hdControllerName").val();
        if (ctrl === "undefined" | ctrl == null) {
            var param = fnGetHashParameter("function");
            if (param === "" || param === "undefined") {
                toastr.warning("URL could not be found!");
                return;
            }
            else
                ctrl = param.split('/')[0];
        }
    }
    if (id.split(" ").length > 1) {
        var arr = id.split(" ");
        var temp = "";
        for (var i = 0; i < arr.length; i++) {
            temp = temp + arr[i] + "%20";
        }
        id = temp;
    }

    var action = ctrl + "/" + "Details?id=" + id;
    fnLoadContent("view-container", action, { id: encodeURI(id) });
}

function fnDelete(id, action) {
    var actionResult = "Delete";
    if (action != undefined) {
        actionResult = action;
    }

    bootbox.confirm({
        title: "Xóa",
        message: "Bạn có chắc muốn xóa dữ liệu này không?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Không'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Có'
            }
        },
        callback: function (result) {
            if (result) {
                var param = fnGetHashParameter('action');
                var ctrl = '';
                if (param != '' || param != null)
                    ctrl = param.split('/')[0];
                else
                    ctrl = $("#hdControllerName").val();
                if (id === "" || id === 'undefined')
                    id = $("#hdId").val();

                $.ajax({
                    type: "GET",
                    url: rootPath + ctrl + "/" + actionResult,
                    data: { id: encodeURI(id) },
                    success: function (data) {
                        if (data.Result) {
                            toastr.success("Xóa bản ghi thành công!");
                            fnLoadSearchData();
                        }
                        else {
                            toastr.error(data.ErrorMessage);
                        }
                        $("#" + container).html(data);
                    },
                    error: function (jqXhr, error, errorThrown) {
                        toastr.error(error);
                    }
                });
            }
        }
    });
}

function fnBack() {
    parent.history.back();
}

// Search
function fnSearch() {
    var postData = $("#frmSearch").serializeArray();
    fnLoadContent("search-container", "Admin/" + $("#hdControllerName").val() + "/" + "_List", postData);
}

// Damage

//function fnSearch(callbackFunc) {

//    fnLoadContent('searchContainer', $("#hdControllerName").val() + "/" + $("#hdControllerName").val() + "List", postData);

//    var postData = $("#frmSearch").serializeArray();
//    $.ajax(
//    {
//        url: $("#frmSearch").attr("action"),
//        type: "POST",
//        data: postData,
//        success: function (data) {
//            $("#search-container").html(data);
//            if (callbackFunc) {
//                callbackFunc();
//            }
//        },
//        error: function (xhr, textStatus, error) {
//            toastr.error(error);
//        }
//    });
//}

//Pop-up functions

function fnSearchPopup() {
    var postData = $("#frmSearch").serializeArray();
    fnLoadContent("search-container", "Admin/" + $("#hdControllerName").val() + "/" + "_List_Popup", postData);
}

// CONTROLLER: Photos

function fnCreatePhoto() {
    $("#sub-modal-table #modal-title").text("Thêm ảnh mới");
    var url = "/Admin/Photos/_Create_Popup";
    $.get(url, function (data) {
        $("#sub-modal-table .modal-body").html(data);
    });
    $('#sub-modal-table').modal('show');
}

function fnCreateMultiplePhotos() {
    $("#sub-modal-table #modal-title").text("Thêm nhiều ảnh mới");
    var url = "/Admin/Photos/_Create_Multiple_Popup";
    $.get(url, function (data) {
        $("#sub-modal-table .modal-body").html(data);
    });
    $('#sub-modal-table').modal('show');
}

function fnEditPhoto(id) {
    $("#sub-modal-table #modal-title").text("Cập nhật thông tin ảnh");
    var url = "/Admin/Photos/_Edit_Popup";
    $.ajax({
        type: "GET",
        url: url,
        data: { id : id },
        success: function (data) {
            $("#sub-modal-table .modal-body").html(data);
            $('#sub-modal-table').modal('show');
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
        }
    });
    //var url = "/Admin/Photos/_Edit_Popup";
    //$.get(url, function (data) {
    //    $("#sub-modal-table .modal-body").html(data);
    //});
    //$('#sub-modal-table').modal('show');
}

function fnDetailPhoto(id) {
    $("#sub-modal-table #modal-title").text("Chi tiết ảnh");
    var url = "/Admin/Photos/_Details_Popup";
    $.ajax({
        type: "GET",
        url: url,
        data: { id: id },
        success: function (data) {
            $("#sub-modal-table .modal-body").html(data);
            $('#sub-modal-table').modal('show');
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
        }
    });
}

function fnDeletePhoto(id) {
    $("#sub-modal-table #modal-title").text("Xóa ảnh");
    var url = "/Admin/Photos/_Delete_Popup";
    $.ajax({
        type: "GET",
        url: url,
        data: { id: id },
        success: function (data) {
            $("#sub-modal-table .modal-body").html(data);
            $('#sub-modal-table').modal('show');
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
        }
    });
}

function fnSelectProductPhotos(productId) {
    $("#modal-table #modal-title").text("Chọn ảnh đại diện cho sản phẩm");
    var url = "/Admin/Photos/_Index_Popup";
    $.get(url, function (data) {
        $("#modal-table .modal-body").html(data);
    });
    $('#modal-table').modal('show');
}

function fnRemovePhoto(photoId) {
    $.ajax(
    {
        url: "/Photos/RemoveTemporaryPhoto",
        type: "POST",
        data: { photoId: photoId },
        success: function (data) {
            $("#selected-photos").html(data);
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
        }
    });
}

function fnSelectPhoto(photoId) {
    $.ajax(
    {
        url: "/Photos/AddTemporaryPhoto",
        type: "POST",
        data: { photoId: photoId },
        success: function (data) {
            if (data.ErrorCode && data.ErrorCode !== "0")
            {
                toastr.warning(data.ErrorMessage);
            }
            else
            {
                $("#selected-photos").html(data);
            }
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error + ": " + xhr.responseText);
        }
    });
}