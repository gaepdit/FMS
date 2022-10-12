$(document).ready(function formEdit() {
    $("#Facility_FileLabel").on("input", function () {
        if ($(this).val().trim() === "") {
            $("#FileIdHelpBlock").removeClass("d-none");
        } else {
            $("#FileIdHelpBlock").addClass("d-none");
        }
    });
});
