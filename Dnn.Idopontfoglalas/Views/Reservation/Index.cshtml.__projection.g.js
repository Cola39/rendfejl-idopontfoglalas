/* BEGIN EXTERNAL SOURCE */

/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */

  var sf = $.ServicesFramework('/************************/');
    var serviceUrl = sf.getServiceRoot('Dnn.Bce.Dnn.Idopontfoglalas');

    $(document).on('click', '.btn-danger[data-id]', function () {
        console.log('deleteForm submitted');
        var id = $(this).data('id');
        console.log('Reservation ID:', id);


        if (!confirm('Biztosan t�rl�d ezt a foglal�st?')) {
            alert('A foglal�s t�rl�se megszak�tva.');
            return;
        };

        var updateData = {
            IsActive: false
        };
        var url = serviceUrl + 'ReservationApiController/DeleteReservation?id=' + id

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updateData),
            success: function (response) {
                alert('Foglal�s sikeresen lemondva!');
                window.location.href = 'http://www.dnndev.me/';
            },
            error: function (xhr, status, error) {
                alert('URCE */
/* BEGIN EXTERNAL SOURCE */

    var moduleId = /************************/;

/* END EXTERNAL SOURCE */
******/');
    var serviceUrl = sf.getServiceRoot('Dnn.Bce.Dnn.Idopontfoglalas');

    $(document).on('click', '.btn-danger[data-id]', function () {
        console.log('deleteForm submitted');
        var id = $(this).data('id');
        console.log('Reservation ID:', id);


        if (!confirm('Biztosan t�rl�d ezt a foglal�st?')) {
            alert('A foglal�s t�rl�se megszak�tva.');
            return;
        };

        var updateData = {
            IsActive: false
        };
        var url = serviceUrl + 'ReservationApiController/DeleteReservation?id=' + id

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updateData),
            success: function (response) {
                alert('Foglal�s sikeresen lemondva!');
                window.location.href = 'http://www.dnndev.me/';
            },
            error: function (xhr, status, error) {
                alert('Hiba t�rt�nt: ' + error);
            }
        });
    });

/* END EXTERNAL SOURCE */
******/sf.getServiceRoot('Dnn.Bce.Dnn.Idopontfoglalas');

    $('#deleteForm').submit(function (event) {
        event.preventDefault();
        console.log('deleteForm submitted');
        id = $(this).find('input[name="id"]').val();

        if (!confirm('Biztosan t�rl�d ezt a foglal�st?')) {
            alert('A foglal�s t�rl�se megszak�tva.');
            return;
        };

        var updateData = {
            IsActive: false
        };
        var url = serviceUrl + 'ReservationApiController/DeleteReservation?id=' + id

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updateData),
            success: function (response) {
                alert('Foglal�s sikeresen lemondva!');
                window.location.href = 'http://www.dnndev.me/';
            },
            error: function (xhr, status, error) {
                alert('Hiba t�rt�nt: ' + error);
            }
        });
    });

/* END EXTERNAL SOURCE */
