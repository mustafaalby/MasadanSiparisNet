$("[id^=menuContentDelete-]").click(function () {
    var menuContentId = $(this).attr("id").split("-")[1];
    $.ajax({
        url: '/Menu/Delete?id=' + menuContentId,
        statusCode: {
            200: function () {
                $("#modalTitle").html("Sil")
                $("#modalBody").html("<strong>Ürün silindi.</strong>")
            }
        }
    });
});