// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('.viewDialog').click(function (e) {
        $.ajaxSetup({ cache: false });
        e.preventDefault();
        var $data;
        $data = $(this).parent('form').serialize();
        var value = this.value;
        console.log(window.location.origin);
        $.ajax({
            url: $(this).parent('form').attr('action'),
            type: 'get',
            data: $data,
            success: function (result) {
                $.get(window.location.origin + '/user/' + value + '?' + $data, function (data) {
                    $('#dialogContent').html(data);
                    $('#modDialog').modal('show');
                });
            }
        });
        window.preventAction = false;
    });
});
$(function () {
    $('#selectAll').on('click', function () {
        if (this.checked) {
            $('.checkbox').each(function () {
                this.checked = true;
            });
        } else {
            $('.checkbox').each(function () {
                this.checked = false;
            });
        }
    });

    $('.checkbox').on('click', function () {
        if ($('.checkbox:checked').length == $('.checkbox').length) {
            $('#selectAll').prop('checked', true);
        } else {
            $('#selectAll').prop('checked', false);
        }
    });
});