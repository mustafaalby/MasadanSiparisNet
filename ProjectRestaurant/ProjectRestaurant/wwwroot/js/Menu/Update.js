//open update modal screen when clicked update button
$("[id^=menuContentUpdate-]").click(function () {
    var menuContentId = $(this).attr("id").split("-")[1];
    $.ajax({
        url: '/Menu/Update?id=' + menuContentId,
        dataType: 'html',
        success: function (data) {
            $("#modalTitle").html("Menü İçeriği Güncelle")
            $("#modalBody").html(data);
        }
    });
});

// update current menu content when update button clicked
$("#updateMenuContent").click(function () {
    $.ajax({
        url: '/Menu/Update',
        type: 'POST',
        dataType: 'json',
        data: {
            "MenuId": $("#menuId").val(),
            "ProductName": $("#productName").val(),
            "Price": $("#price").val(),
            "Quantity": $("#quantity").val(),
            "Description": $("#description").val(),
            "ProductTypeId": $("#productTypeId").val(),
        },
        statusCode: {
            200: function () {
                $("#modalTitle").html("Menü İçeriği Güncelle")
                $("#modalBody").html("<strong>Ürün başarılı bir şekilde güncellendi.</strong>")
            }
        }
    });
});