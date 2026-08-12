// Populate the Compliance Officer dropdown when an OrgUnit selection is made.
function setUpComplianceOfficerDropdown(OrgUnitElementId, coElementId, placeholder) {

    const orgUnitSelect = document.getElementById(OrgUnitElementId);

    orgUnitSelect.addEventListener("change", () => {

        const coSelect = document.getElementById(coElementId);
        if (coSelect.value !== '') return;
        coSelect.innerHTML = `<option value="">${placeholder}</option>`;

        axios.get(`/api/compliance-officers/${orgUnitSelect.value}`)
            .then(function (response) {
                const data = response.data;
                if (data == null || data.length === 0) return;

                let opt;
                for (const item of data) {
                    opt = document.createElement('option');
                    opt.text = item.text;
                    opt.value = item.value;
                    coSelect.add(opt);
                }
            })
            .catch(function errorHandler(error) {
                coSelect.innerHTML = '<option value="">Error</option>';
            });
    });
}