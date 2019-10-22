$(document).ready(function () {

    $('#stars li').on('mouseover', function () {
        var onStar = parseInt($(this).data('value'), 10);

        $(this).parent().children('li.star').each(function (e) {
            if (e < onStar) {
                $(this).addClass('hover');
            }
            else {
                $(this).removeClass('hover');
            }
        });

    }).on('mouseout', function () {
        $(this).parent().children('li.star').each(function (e) {
            $(this).removeClass('hover');
        });
    });


    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently selected
        var stars = $(this).parent().children('li.star');
        var ratedBookingId = $(this).closest("td").prop("id");

        for (i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }

        for (i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }

        $.ajax({
            url: '/Bookings/Rate',
            type: 'POST',
            contentType: 'application/json;utf-8',
            datatype: 'json',
            data: JSON.stringify({
                bookingId: ratedBookingId,
                rating: onStar
            }),
        }).done(function (data) {
            if (data.fail === true) {
                $.notify("Already rated", "warn");
            } else {
                $.notify("Success", "success");
            }
            
        });

    });


});


function responseMessage(msg) {
    $('.success-box').fadeIn(200);
    $('.success-box div.text-message').html("<span>" + msg + "</span>");
}