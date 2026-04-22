$(document).ready(function COMultiSelect() {
    $('#Spec_ComplianceOfficerId').select2({
        placeholder: "",
        closeOnSelect: false,
        width: '100%',
        templateResult: function (state) {
            if (!state.id) return state.text;
            return $('<span style="display:flex; align-items:center;"><input type="checkbox" class="me-2"/> ' + state.text + '</span>');
        },
        templateSelection: function (state) {
            return state.text;
        },
        escapeMarkup: function (markup) { return markup; }
    });

    $('#Spec_ComplianceOfficerId').on('select2:select select2:unselect', function () {
        var selected = $(this).val() || [];
        $('#Spec_ComplianceOfficerId option').each(function () {
            $(this).prop('selected', selected.includes($(this).val()));
        });
    });
});
