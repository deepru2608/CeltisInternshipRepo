$(document).ready(function () {
    $('#dtBasicExample').DataTable({
        "pagingType": "first_last_numbers"
    });
});


window.ShowAlert = (title, message, status) => {
    Swal.fire(title, message, status)
};