$(document).ready(function () {
    // Change page size
    $(".ui-pg-selbox").val($("#pgSize").val());
    $(".ui-pg-selbox").change(function () {
        $("#hdPageSize").val($(this).val());
        fnSearch();
    });

    // Backward
    $("#first_grid-pager").click(function () {
        $('#PageSize').val($("#pgSize").val());
        $("#hdCurrentPage").val($("#txtCurrentPage").val());
        var currentPage = $("#txtCurrentPage").val();
        if (currentPage > 1)
            $("#hdCurrentPage").val("1");
        else {
            return;
        }
        fnSearch();
    });

    // Forward
    $("#last_grid-pager").click(function () {
        var currentPage = parseInt($("#txtCurrentPage").val());
        var totalPages = $("#totalPages").val();
        if (currentPage < totalPages) {
            $("#hdCurrentPage").val(totalPages);
        }
        else {
            return;
        }
        fnSearch();
    });

    // Previous
    $("#prev_grid-pager").click(function () {
        var currentPage = $('#txtCurrentPage').val();
        if (currentPage > 1)
            $("#hdCurrentPage").val(currentPage - 1);
        else {
            return;
        }
        fnSearch();
    });

    // Next
    $("#next_grid-pager").click(function () {
        var currentPage = parseInt($('#txtCurrentPage').val());
        var totalPages = $("#totalPages").val();
        if (currentPage < totalPages) {
            currentPage = currentPage + 1;
            $("#hdCurrentPage").val(currentPage);
        }
        else {
            return;
        }
        fnSearch();
    });

    // Change page number
    $('.ui-pg-input').change(function () {
        var currentPage = $(".ui-pg-input").val();
        if (currentPage < 1) {
            $("#hdCurrentPage").val(1);
        } else {
            $("#hdCurrentPage").val(currentPage);
        }
        fnSearch();
    });

    // Reload grid
    $("#refresh_grid-table").click(function () {
        fnSearch();
    });
});