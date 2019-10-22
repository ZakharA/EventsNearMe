$("#createEventForm").validate({
    rules: {
        "Event.Name": {
            required: true,
            minLength: 5
        },
        "CoverImage": {
            required: true
        },
        "Event.Location.Address": {
            required: true,
        }
    },
    errorPlacement: function (error, element) {
        if (element.is(":radio"))
            error.appendTo(element.parent().next().next());
        else if (element.is(":checkbox"))
            error.appendTo(element.next());
        else
            error.appendTo(element.parent().next());
    },
})

$.validator.setDefaults({
    ignore: [],
});

$("#Event_IsFree").click(function () {
    var priceInput = $("#Event_Price");
    if (this.checked) {
        priceInput.val(0);
        priceInput.prop("disabled", true);
    } else {
        priceInput.prop("disabled", false);
    }
});