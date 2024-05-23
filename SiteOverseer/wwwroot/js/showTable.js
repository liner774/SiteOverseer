document.addEventListener('DOMContentLoaded', function () {
    const showTableButtons = document.querySelectorAll('.showTableButton');

    showTableButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            $('#tableModal').modal('show');
        });
    });
});
