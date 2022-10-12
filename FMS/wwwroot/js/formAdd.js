$(document).ready(function formAdd() {
    $('.table-hover tbody tr').click(function() {
        $(this).find('input[type=radio]').prop('checked', true);
        $(this).parent().find('tr').removeClass('table-primary');
        $(this).addClass('table-primary');
    })
});
