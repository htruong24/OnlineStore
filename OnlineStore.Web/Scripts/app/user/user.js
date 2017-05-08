$(document).ready(function () {
    LoadSorting();

    $(".header").click(function (evt) {
        var sortfield = $(evt.target).data("sortfield");
        if ($("#hdSortField").val() == sortfield) {
            if ($("#hdSortDirection").val() == "ascending") {
                $("#hdSortDirection").val("descending");
                $(this).parent().attr("class", "sorting_desc");

                console.log($("#hdSortDirection").val());
            }
            else {
                $("#hdSortDirection").val("ascending");
                $(this).parent().removeClass("sorting");
                $(this).parent().attr("class", "sorting_asc");
            }
        }
        else {
            $("#hdSortField").val(sortfield);
            $("#hdSortDirection").val("ascending");
        }
        evt.preventDefault();
        //$("form").submit();
           fnLoadSearchData();
           LoadSorting();

        // CRUD

        $(".date-time").each(function () {
            $(this).datepicker(
            {
                dateFormat: "dd-mm-yy"
            });
        });

        $("#btnSumit-User").click(function () {
            if ($("#formUser").valid()) {
                var date = $.datepicker.parseDate("dd-mm-yy", $("#txtDateOfBirth").val());
                $("#hdDateOfBirth").val($.datepicker.formatDate("mm/dd/yy", date));
                var postData = $("#formUser").serializeArray();
                $.ajax(
                {
                    url: appPath + "User/Create",
                    type: "POST",
                    data: postData,
                    success: function (data) {
                        fnChangehashAfterSave(data);
                    },
                    error: function (xhr, textStatus, error) {
                        toastr.error(error);
                    }
                });
            }
        });

        $("input[type='text'].required ").attr("placeholder", $("#val-required").val());
    });

    $(".pager").click(function (evt) {
        var pageindex = $(evt.target).data("pageindex");
        $("#CurrentPageIndex").val(pageindex);
        evt.preventDefault();
        //$("form").submit();
        fnLoadSearchData();
        LoadSorting();
    });
});

function LoadSorting() {
    var sortField = $("#hdSortField").val();
    var sortDirection = $("#hdSortDirection").val();

    // Remove All Sorting
    $(".header").parent().attr("class", "sorting");
    $(".header").each(function () {
        if ($(this).data("sortfield") == sortField) {
            if (sortDirection == "ascending") {
                $(this).parent().attr("class", "sorting_desc");
            }
            else {
                $(this).parent().attr("class", "sorting_asc");
            }
        }
    });
}

