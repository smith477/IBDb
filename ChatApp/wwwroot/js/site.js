// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


var username = document.getElementById("Username");
var password = document.getElementById("Password");

var loginButton = document.getElementById("LoginButton");

$(loginButton).click(function () {
    var payload = {
        displayname: username.value,
        password: password.value
    }
    $.ajax({
        url: "http://localhost:5000/api/user/login",
        type: "POST",
        data: JSON.stringify(payload),
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
           console.log(response);
        },
        error: function (error) {
            console.log(error);
        }
    })
})

