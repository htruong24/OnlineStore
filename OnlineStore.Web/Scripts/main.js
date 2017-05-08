var _ignoreHashChange = false;
var _currentModule = "";

$(document).ready(function () {
    $(window).hashchange(function () {
        var module = fnGetHashParameter("module");
        var func = fnGetHashParameter("function");
        var action = fnGetHashParameter("action");

        if (action !== "") {
            fnLoadSubMenu(module, func);
        }
        else if (func !== "" & action === "") {
            fnLoadSubMenu(module, func);
            fnList(func, action);
        }
        else if (module !== "") {
            fnLoadSubMenuList(module, func);
        }

        if (_ignoreHashChange === false) {
            if (action !== "")
                fnLoad(action);
        }

        _ignoreHashChange = false;
    });

    $(window).hashchange();
});

// Load View Content

function fnList(func, action) {
    if (action === "" || $('#searchContainer').length === 0) {
        LoadViewContent("mainContainer", func, { func: func });
    }
}

function fnLoad(action) {
    LoadViewContent("mainContainer", action, "");
}

function LoadViewContent(container, action, parameters) {
    // ui.showPageLoading();
    $('#' + container).empty().append(loadingContent);
    $.ajax({
        type: "GET",
        url: appPath + action,
        data: parameters,
        success: function (data) {
            $("#" + container).html(data);
            $("#loadingContent").remove();
          //  ui.hidePageLoading();
        },
        error: function (xhr, textStatus, error) {
            toastr.error(error);
            $("#loadingContent").remove();
          //  ui.hidePageLoading();
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
    fnChangeHash(null, action);
    //LoadViewContent("mainContainer", action, {});
    
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
     fnChangeHash(null, action);
    //LoadViewContent("mainContainer", action, { id: encodeURI(id) });
    
}

function fnDetails(id, controller) {
    var ctrl = controller;
      if (controller ==  null || controller === "" || controller === "undefined") {
          ctrl = $("#hdControllerName").val();
        if (ctrl === "undefined" | ctrl ==  null) {
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
    fnChangeHash(null, action);
    //LoadViewContent("mainContainer", action, { id: encodeURI(id) });
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
                    url: appPath + ctrl + "/" + actionResult,
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

function fnClear() {
    $(window).hashchange();
}

function fnBack() {
    parent.history.back();
}

// Load Search Data

function fnLoadSearchData() {
    var postData = $("#frmSearch").serializeArray();
    LoadViewContent('searchContainer', $("#hdControllerName").val() + "/" + $("#hdControllerName").val() + "List", postData);
}

// Hash (Ajax History)

function fnGetHashParameter(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "";
    if (name == 'action') {
        regexS = "[\\#&]" + name + "=([^#]*)";
    } else {
        regexS = "[\\#&]" + name + "=([^&#]*)";
    }
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.hash);
    if (results == null)
        return "";
    else
        return results[1];
}

function fnChangeHash(module, action, funcId) {
    if (module == null)
        module = fnGetHashParameter('module');

    var func = fnGetHashParameter('function');
    funcId = fnGetHashParameter('funcId');
    location.hash = 'module=' + module + '&funcId=' + funcId + "&function=" + func + "&action=" + action;
}

function fnChangehashAfterSave(data) {
    if (data.Result) {
        var param = fnGetHashParameter('action');
        var controller;
        if (param !== "" || param != null)
            controller = param.split("/")[0];
        else
            controller = $("#hdControllerName").val();

        var action = controller;

        fnChangeHash(null, action);
        toastr.info("Lưu thành công!");
    }
    else {
        toastr.error(data.ErrorMessage);
    }
}

// Menu

function fnChangeModule(moduleId) {
    $("#bodyContent").empty();
    location.hash = "module=" + moduleId;
}

function fnLoadSubMenu(moduleId, func) {
    if (func === "" || $("#subMenu").html() === "") {
        LoadViewContent("subMenu", "Menu/SubMenu", { moduleId: moduleId });
        if (moduleId != null) {
            $("#" + _currentModule).removeClass();
            $("#" + moduleId).addClass("active");
            _currentModule = moduleId;
        }
    }
}

function fnLoadSubMenuList(moduleId, func) {
    if (func === "" || $("#subMenu").length === 0) {
        LoadViewContent("bodyContent", "Menu/SubMenuList", { moduleId: moduleId });
        if (moduleId != null) {
            $("#" + _currentModule).removeClass();
            $("#" + moduleId).addClass("active");
            _currentModule = moduleId;
        }
    }
}

function fnChangeMenu(funcId, func, action) {
    if (action == null || action === '') {
        var module = fnGetHashParameter("module");
        location.hash = "module=" + module + "&funcId=" + funcId + "&function=" + func;
        if (funcId !== '') {
            if (funcId === "ST011")
                funcId = "INVEN";
            $("#bodyContent").find(".active").removeClass("active");
            $("#" + funcId).addClass("active");
        }
    }
}
