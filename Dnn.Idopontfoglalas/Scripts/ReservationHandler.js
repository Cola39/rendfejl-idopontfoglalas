$(document).ready(function () {
    var sf = $.ServicesFramework(moduleId);
    var serviceUrl = sf.getServiceRoot('Dnn.Bce.Dnn.Idopontfoglalas');

    $('#reservationForm').submit(function (event) {
        event.preventDefault();

        var reservationData = {
            Name: $("#Name").val(),
            Email: $("#Email").val(),
            Phone: $("#Phone").val(),
            Time: $("#Time").val(),
            Quantity: parseInt($("#Quantity").val())
        };
        var url = serviceUrl + "ReservationApiController/CreateReservation";

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(reservationData),
            success: function (response) {
                alert('Foglalás sikeresen létrehozva!');
                window.location.href = 'http://www.dnndev.me/';
            },
            error: function (xhr, status, error) {
                alert('Hiba történt: ' + error);
            }
        });




        $('#deleteForm').submit(function (event) {
            event.preventDefault();
            console.log('deleteForm submitted');
            var id = $(this).find('input[name="id"]').val();

            if (!confirm('Biztosan törlöd ezt a foglalást?')) {
                alert('A foglalás törlése megszakítva.');
                return;
            }

            var updateData = {
                IsActive: false
            };
            var url = serviceUrl + 'ReservationApiController/DeleteReservation?id=' + id;

            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(updateData),
                success: function (response) {
                    alert('Foglalás sikeresen lemondva!');
                    window.location.href = 'http://www.dnndev.me/';
                },
                error: function (xhr, status, error) {
                    alert('Hiba történt: ' + error);
                }
            });
        });
    });
});
