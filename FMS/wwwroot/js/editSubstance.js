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
                $('#EditSubstance_Chemical_ChemicalName').value = data[0].ChemicalName;
                $('#EditSubstance_Chemical_CASNumber').value = data[0].CASNumber;
                $('#EditSubstance_Chemical_CommonName').value = data[0].CommonName;
            },
            error: function (error) {
                console.error('Error fetching search results:', error);
            }
        });
    }
});