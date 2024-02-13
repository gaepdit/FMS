$(document).ready(function formEdit() {
    $("#Facility_FileLabel").on("input", function () {
        if ($(this).val().trim() === "" && $("#Facility_FacilityTypeId option:selected").text().trim() !== "RN (Release Notification)") {
            $("#FileIdHelpBlock").removeClass("d-none");
        } else {
            $("#FileIdHelpBlock").addClass("d-none");
        }
    });

    $("#Facility_FacilityTypeId").on("change", function () {
        if ($("#Facility_FacilityTypeId option:selected").text().trim() === "RN (Release Notification)") {
            $("#RNBlock").removeClass("d-none");
        } else {
            $("#RNBlock").addClass("d-none");
        }
    });
});
