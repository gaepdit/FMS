// Don't submit empty form fields
$(document).ready(function formSearch() {
    $('#SearchButton').click(function DisableEmptyInputs() {
        $('input').each(function () {
            const $input = $(this);
            if ($input.val() === '')
                $input.attr('disabled', 'disabled');
        });
        $('select').each(function () {
            const $input = $(this);
            if ($input.val() === '')
                $input.attr('disabled', 'disabled');
        });
        return true;
    });
    $("#Spec_FacilityTypeId").on("change", function () {
        if ($("#Spec_FacilityTypeId option:selected").text().trim() === "RN (Release Notification)") {
            $("#RNBlock").removeClass("d-none");
        } else {
            $("#RNBlock").addClass("d-none");
            $("#ShowPendingCB").prop("checked",false)
        }
    });
});
