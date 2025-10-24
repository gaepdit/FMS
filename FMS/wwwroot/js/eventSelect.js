// Populate the EventType dropdown when an EventType selection is made.
function setUpEventTypeDropdown(actionElementId, eventElementId, placeholder) {

    const actionSelect = document.getElementById(actionElementId);

    actionSelect.addEventListener("change", () => {
        const eventSelect = document.getElementById(eventElementId);
        if (eventSelect.innerHTML === '') {
            eventSelect.innerHTML = `<option value="">${placeholder}</option>`
        } else{
            return;
        };
        eventSelect.disabled = false;
        if (actionSelect.value === '') return;

        axios.get(`/api/${actionSelect.value}`)
            .then(function (response) {
                const data = response.data;
                if (data == null || data.length === 0) return;

                let opt;
                for (const item of data) {
                    opt = document.createElement('option');
                    opt.text = item.text;
                    opt.value = item.value;
                    eventSelect.add(opt);
                }
            })
            .catch(function errorHandler(error) {
                eventSelect.innerHTML = '<option value="">Error</option>';
                if (error instanceof Error && typeof rg4js === "function") {
                    rg4js('send', { error: error, tags: ['handled_promise_rejection'] });
                }
            });
    });
}