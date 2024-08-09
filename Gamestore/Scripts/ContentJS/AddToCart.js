function addToCart(title) {
    $.ajax({
        type: "POST",
        url: "Jeuxvideo.aspx/AddToCart",
        data: JSON.stringify({ title: title }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response.d);
            location.reload();
        },
        error: function (response) {

        }
    });
}