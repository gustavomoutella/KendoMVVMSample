$(function () {
    $("#GetToken").click(function () {
        $.ajax({
            type: 'POST',
            url: '/Login',
            data: JSON.stringify({ "Username": $("#login").val(), "Password": $("#pass").val() }),
            contentType: "application/json"
        }).done(function (data, statusText, xhdr) {
            //$("#token").text(data);
            console.log(data);
            location.href = '/Home/About';
        }).fail(function (xhdr, statusText, errorText) {
            //$("#token").text(errorText);
            console.log(errorText);
        });
    });

    $("#UseToken").click(function () {
        $.ajax({
            method: 'GET',
            url: '/Home/About',
            beforeSend: function (xhdr) {
                xhdr.setRequestHeader(
                    "Authorization", "Bearer " + $("#token").text());
            }
        }).done(function (data, statusText, xhdr) {
            $("#result").text(JSON.stringify(data));
        }).fail(function (xhdr, statusText, errorText) {
            $("#result").text(JSON.stringify(xhdr));
        });
    });
});