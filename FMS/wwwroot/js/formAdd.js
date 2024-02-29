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
        };
        if ($("#Facility_FacilityNumber").text().trim() === "") {
            switch ($("#Facility_FacilityTypeId option:selected").text().trim()) {
                case "RN (Release Notification)":
                    $("#Facility_FacilityNumber").attr('placeholder', 'RNdddd');
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
