// Populate the associate dropdown when an office selection is made.
function setUpStaffDropdown(officeElementId, staffElementId, forAssignment, placeholder) {
    let apiPath;

    if (forAssignment === true) {
        apiPath = "staff-for-assignment";
    } else {
        apiPath = "all-staff";
    }

    const officeSelect = document.getElementById(officeElementId);

    officeSelect.addEventListener("change", () => {
        const staffSelect = document.getElementById(staffElementId);
        staffSelect.innerHTML = `<option value="">${placeholder}</option>`;
        staffSelect.disabled = true;
        if (officeSelect.value === '') return;

        axios.get(`/api/offices/${officeSelect.value}/${apiPath}`)
            .then(function (response) {
                const data = response.data;
                if (data == null || data.length === 0) return;

                const onlyUnassignedCheckbox = document.getElementById("Spec_OnlyUnassigned");
                staffSelect.disabled = onlyUnassignedCheckbox ? onlyUnassignedCheckbox.checked : false;

                let opt;
                for (const item of data) {
                    opt = document.createElement('option');
                    opt.text = item.name;
                    opt.value = item.id;
                    staffSelect.add(opt);
                }
            })
            .catch(function errorHandler(error) {
                staffSelect.innerHTML = '<option value="">Error</option>';
                if (error instanceof Error && typeof rg4js === "function") {
                    rg4js('send', { error: error, tags: ['handled_promise_rejection'] });
                }
            });
    });
}