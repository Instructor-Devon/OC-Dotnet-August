// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    $.ajax({
        url: "/users",
    }).done(function(res) {
        console.log(res);
        $("#table-body").html(res);
    });
})

$("#user-form").submit(function(event) {
    event.preventDefault();
    let form_data = $(this).serialize();
    console.log(form_data);
    $.ajax({
        url: "/create",
        method: "POST",
        data: form_data
    }).done(function(res) {
        $("#table-body").html(res);
    })
    $(this)[0].reset();
});

