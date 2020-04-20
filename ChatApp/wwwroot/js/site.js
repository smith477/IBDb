// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


var username = document.getElementById("Username");
var password = document.getElementById("Password");

var loginButton = document.getElementById("LoginButton");

$(loginButton).click(function () {
    $.ajax({
        url: "/api/user/login",
        type: "POST",
        data: {
            "username": username.value,
            "password": password.value
        },
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            console.log(response);
        }
    })
})

