// Populate the ActionType dropdown when an EventType selection is made.
function setUpActionTypeDropdown(eventElementId, actionElementId, placeholder) {
    let apiPath;

    const eventSelect = document.getElementById(eventElementId);

    eventSelect.addEventListener("change", () => {
        const actionSelect = document.getElementById(actionElementId);
        actionSelect.innerHTML = `<option value="">${placeholder}</option>`;
        actionSelect.disabled = true;
        if (eventSelect.value === '') return;

        axios.get(`/api/events/${eventSelect.value}/${apiPath}`)
            .then(function (response) {
                const data = response.data;
                if (data == null || data.length === 0) return;

                const onlyUnassignedCheckbox = document.getElementById("Spec_OnlyUnassigned");
                actionSelect.disabled = onlyUnassignedCheckbox ? onlyUnassignedCheckbox.checked : false;

                let opt;
                for (const item of data) {
                    opt = document.createElement('option');
                    opt.text = item.name;
                    opt.value = item.id;
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