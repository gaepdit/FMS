$(document).ready(function formEdit() {
    $("#Facility_FileLabel").on("input", function () {
        if ($(this).val().trim() === "" && $("#Facility_FacilityTypeId option:selected").text().trim() !== "RN (Release Notification)") {
            $("#FileIdHelpBlock").removeClass("d-none");
        } else {
            $("#FileIdHelpBlock").addClass("d-none");
        }
    });

    $('.table-hover tbody tr').click(function () {
        $(this).find('input[type=radio]').prop('checked', true);
        $(this).parent().find('tr').removeClass('table-primary');
        $(this).addClass('table-primary');
    });

    $("#Facility_FacilityTypeId").on("change", function () {
        if ($("#Facility_FacilityTypeId option:selected").text().trim() === "RN (Release Notification)" && $("#Facility_FacilityStatusId option:selected").text().trim() != "COMPLAINT") {
            $("#RNBlock").removeClass("d-none");
            $("#FacilityNumberHelpBlock").removeClass("d-none");
            $("#Facility_FacilityNumber").prop('value', '')
            $("#Facility_FacilityNumber").attr('readonly', true)
        } else {
            $("#RNBlock").addClass("d-none");
            $("#FacilityNumberHelpBlock").addClass("d-none");
            $("#Facility_FacilityNumber").attr('readonly', false)
        };
        if ($("#Facility_FacilityNumber").text().trim() === "") {
            switch ($("#Facility_FacilityTypeId option:selected").text().trim()) {
                case "RN (Release Notification)":
                    if ($("#Facility_FacilityStatusId option:selected").text().trim() === "COMPLAINT") {
                        $("#Facility_FacilityNumber").attr('placeholder', 'RNdddddd')
                    }
                    else {
                        $("#Facility_FacilityNumber").attr('placeholder', 'RNdddd')
                    };
                    break;
                case "GEN (RCRA generator)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'ddddd');
                    break;
                case "NPL (NPL)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'GAxdddddddd');
                    break;
                case "DOD (DOD RCRA non-generator)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'GAdddddddddd');
                    break;
                case "TSDCA (TSD/CA RCRA non-generator)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'GAddddddddd');
                    break;
                case "FUDS (FUDS)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'FUDddddddddd');
                    break;
                case "VRP (VRP)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'VRPdddddddddd');
                    break;
                case "HSI (HSI)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'ddddd');
                    break;
                case "BROWN (Brownfield)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'BRFddddddd');
                    break;
            };
        };
    });
    $("#Facility_CountyId").on("change", function () {
        if ($("#Facility_FileLabel").text().trim() === "") {
            var selectElement = document.querySelector("#Facility_CountyId");
            var countyNumber = selectElement.value;
            var placeHolderFileLabel = countyNumber + '-####';
            $("#Facility_FileLabel").attr('placeholder', placeHolderFileLabel);
        };
    });
});
