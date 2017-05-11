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
  //  $('#' + container).empty().append(loadingContent);
    $.ajax({
        type: "GET",
        url: rootPath + action,
        data: parameters,
        success: function (data) {
            $("#" + container).html(data);
       //     $("#loadingContent").remove();
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
          //  $("#loadingContent").remove();
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