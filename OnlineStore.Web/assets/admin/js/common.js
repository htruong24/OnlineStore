function fnSearch(func) {

    if (func) {
        //   $("#frmSearch").submit();

        $('#frmSearch').ajaxForm(function () {
            alert("Thank you for your comment!");
        });

        var postData = $("#frmSearch").serializeArray();
        $.ajax(
        {
            url: $("#frmSearch").attr("action"),
            type: "POST",
            data: postData,
            success: function(data) {
                func();
            },
            error: function (xhr, textStatus, error) {
                toastr.error(error);
            }
        });
    }
    $("#frmSearch").submit();
}