$('#clicker').click(function(){
    $.ajax({
        method: "GET",
        url: "/click"
    })
    .done(function(response){
        $("#score").html(response);
    });
})