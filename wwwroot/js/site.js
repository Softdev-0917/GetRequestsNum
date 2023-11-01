window.setTimeout(function () {
    $(".alert").fadeTo(100, 0).slideUp(100, function () {
        $(this).remove();
    });
}, 1000);



showPopUp = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#formModal .modal-body").html(res);
            $("#formModal .modal-title").html(title);
            $("#formModal").modal('show');
        }
    })
};

