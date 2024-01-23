$(document).ready(function formAdd() {
    $('.table-hover tbody tr').click(function () {
        $(this).find('input[type=radio]').prop('checked', true);
        $(this).parent().find('tr').removeClass('table-primary');
        $(this).addClass('table-primary');
    });

    $("#Facility_FacilityTypeId").on("change", function () {
        if ($("#Facility_FacilityTypeId option:selected").text().trim() === "RN (Release Notification)") {
            $("#RNBlock").removeClass("d-none");
        } else {
            $("#RNBlock").addClass("d-none");
        }
    });
});
