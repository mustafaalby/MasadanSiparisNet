//refresh page when modal closed
$('#modalTarget').on('hidden.bs.modal', function () {
    location.reload();
})