// Example using jQuery
$(document).ready(function () {
    let typingTimer;
    const doneTypingInterval = 500; // milliseconds

    $('#searchInput').on('keyup', function () {
        clearTimeout(typingTimer);
        if ($('#searchInput').val()) {
            typingTimer = setTimeout(performSearch, doneTypingInterval);
        }
    });

    function performSearch() {
        const searchTerm = $('#searchInput').val();
        $.ajax({
            url: '/api/search', // Your API endpoint
            method: 'GET',
            data: { query: searchTerm },
            success: function (data) {
                // Update your UI with the new results
                // e.g., populate a dropdown, update a list
            },
            error: function (error) {
                console.error('Error fetching search results:', error);
            }
        });
    }
});