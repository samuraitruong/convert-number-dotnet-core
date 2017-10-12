// We can also using Ajax.BeginForm and using MVC built-in feature to do ajax submit without any jquery/client site code
// However it is not really good practice since the page content reload ussually bigger than actuall data reload
// Nowaday, also client site is using SPA such as Angular/react/Vue and  and it so much easier to do content refresh with those framework

$(document).ready(function () {
    // This is not nice to use jquery, to populate data like this. 
    // I would not using any other library for this simple app.
    // I can demo this simple ap with react/angular or normal javascript string template like handlebars.... Lets me know if you want to see that
    
    $('#form').submit(function (ev) {
        ev.preventDefault();
        // validation error , just return MVC client site will display error message
        if (!$(this).valid()) return false;
        $('.loader').show();
        var name = $('#Name').val();
        var number = $('#Salary').val();
        console.log(name, number)
        $.ajax({
            url: '/api/v1/converter',
            type: "POST",
            data: JSON.stringify({ InputNumber: number }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('.loader').hide();
                $('#display').html('<div class="alert alert-success">Hello <strong>' + name + '</strong><br/><br/>Your payment are :' + number + '('+data.read+')</div>')
            },
            error: function () {
                $('.loader').hide();

                $('#display').html('<div class="alert alert-danger">Error occured!!!</div>')
            }
        });

        return false;
    });
});