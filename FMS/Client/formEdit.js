import $ from 'jquery';

$(document).ready(function domReady() {
    $("#Facility_FileLabel").on("input", function () {
        if ($(this).val().trim() === "") {
            $("#FileIdHelpBlock").removeClass("d-none");
        } else {
            $("#FileIdHelpBlock").addClass("d-none");
        }
    });
});
