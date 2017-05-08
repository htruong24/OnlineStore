$(document).ready(function () {
    ui = new BaseUi();

    $(window).bind('resize', function () {
        var width = $('#gridContainer').width();
        $('#gridList').setGridWidth(width);
    });

    $(document).ready(function () {
        var width = $('#gridContainer').width();
        $('#gridList').setGridWidth(width);
    });
});

