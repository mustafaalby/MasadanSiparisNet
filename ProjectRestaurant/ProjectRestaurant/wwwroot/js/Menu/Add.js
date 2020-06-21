$("#addMenuContent").click(function () {
    $.ajax({
        url: '/Menu/Add',
        dataType: 'html',
        success: function (data) {
            $("#modalTitle").html("Menü İçeriği Ekle")
            $("#modalBody").html(data);
        }
    });
});

$("#saveMenuContent").click(function () {
    $.ajax({
        url: '/Menu/Add',
        type: 'POST',
        dataType: 'json',
        data: {
            "ProductName": $("#productName").val(),
            "Price": $("#price").val(),
            "Quantity": $("#quantity").val(),
            "Description": $("#description").val(),
            "ProductTypeId": $("#productTypeId").val(),
        },
        statusCode: {
            200: function () {
                $("#modalTitle").html("Menü İçeriği Ekle")
                $("#modalBody").html("<strong>Ürün başarılı bir şekilde eklendi.</strong>")
            }
        }
    });
});