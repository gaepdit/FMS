// Populate the ActionType dropdown when an EventType selection is made.
function setUpActionTypeDropdown(eventElementId, actionElementId, placeholder) {

    const eventSelect = document.getElementById(eventElementId);

    eventSelect.addEventListener("change", () => {
        const actionSelect = document.getElementById(actionElementId);
        actionSelect.innerHTML = `<option value="">${placeholder}</option>`;
        actionSelect.disabled = false;
        if (eventSelect.value === '') return;

        axios.get(`/api/${eventSelect.value}`)
            .then(function (response) {
                const data = response.data;
                if (data == null || data.length === 0) return;

                let opt;
                for (const item of data) {
                    opt = document.createElement('option');
                    opt.text = item.text;
                    opt.value = item.value;
                    actionSelect.add(opt);
                }
            })
            .catch(function errorHandler(error) {
                actionSelect.innerHTML = '<option value="">Error</option>';
                if (error instanceof Error && typeof rg4js === "function") {
                    rg4js('send', { error: error, tags: ['handled_promise_rejection'] });
                }
            });
    });
}