$(document).ready(function () {
    let typingTimer;
    const doneTypingInterval = 500; // milliseconds

    $('#EditSubstance_Chemical_ChemicalName').on('keyup', function () {
        clearTimeout(typingTimer);
        if ($('#EditSubstance_Chemical_ChemicalName').val()) {
            typingTimer = setTimeout(performSearch, doneTypingInterval);
        }
    });

    function performSearch() {
        const searchTerm = $('#EditSubstance_Chemical_ChemicalName').val();
        $.ajax({
            url: '/api/chem',
            method: 'GET',
            type: 'json',
            data: { query: searchTerm },
            success: function (data) {
                $('#EditSubstance_Chemical_ChemicalName').val(data.ChemicalName[0]);
                $('#EditSubstance_Chemical_CASNumber').val(data.CASNumber[0]);
                $('#EditSubstance_Chemical_CommonName').val(data.CommonName[0]);
            },
            error: function (error) {
                console.error('Error fetching search results:', error);
            }
        });
    }
});