$(document).ready(function () {

    LoadSorting();

    $("th.sortable").click(function (evt) {
        var sortfield = $(evt.target).data("sortfield");
        if ($("#hdSortField").val() === sortfield) {
            if ($("#hdSortDirection").val() === "ascending") {
                $("#hdSortDirection").val("descending");
            }
            else {
                $("#hdSortDirection").val("ascending");
                $(this).parent().removeClass("sorting");
            }
        }
        else {
            $("#hdSortField").val(sortfield);
            $("#hdSortDirection").val("ascending");
        }
        fnSearch(LoadSorting);
    });
});

function LoadSorting() {
    var sortField = $("#hdSortField").val();
    var sortDirection = $("#hdSortDirection").val();

    $("th.sortable").each(function () {
        if ($(this).data("sortfield") === sortField) {
            if (sortDirection === "ascending") {
                $(this).removeClass("sorting");
                $(this).addClass("sorting_desc");
            }
            else {
                $(this).removeClass("sorting");
                $(this).addClass("sorting_asc");
            }
        } else {
            if ($(this).hasClass("sorting_desc")) {
                $(this).removeClass("sorting_desc");
                $(this).addClass("sorting");
            }
            else if ($(this).hasClass("sorting_asc")) {
                $(this).removeClass("sorting_asc");
                $(this).addClass("sorting");
            }
        }
    });
}