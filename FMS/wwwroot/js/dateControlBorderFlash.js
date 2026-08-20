document.addEventListener('DOMContentLoaded', () => {
    const reportingPeriodType = document.getElementById('Item_ReportingPeriodType');
    const reportingPeriodStart = document.getElementById('Item_ReportingPeriodStart');
    const reportingPeriodEnd = document.getElementById('Item_ReportingPeriodEnd');

    if (!reportingPeriodType || !reportingPeriodStart || !reportingPeriodEnd) return;

    reportingPeriodType.addEventListener('change', () => {
        const today = new Date();
        const currentYear = today.getFullYear();
        const currentMonth = today.getMonth(); // 0-indexed

        let startDate, endDate;

        switch (reportingPeriodType.value) {
            case '1': // Monthly
                // Last month
                startDate = new Date(currentYear, currentMonth - 1, 1);
                endDate = new Date(currentYear, currentMonth, 0);
                break;

            case '2': // First Quarter
                startDate = new Date(currentYear - (currentMonth < 3 ? 1 : 0), 0, 1);
                endDate = new Date(currentYear - (currentMonth < 3 ? 1 : 0), 3, 0);
                break;

            case '3': // Second Quarter
                startDate = new Date(currentYear - (currentMonth < 6 ? 1 : 0), 3, 1);
                endDate = new Date(currentYear - (currentMonth < 6 ? 1 : 0), 6, 0);
                break;

            case '4': // Third Quarter
                startDate = new Date(currentYear - (currentMonth < 9 ? 1 : 0), 6, 1);
                endDate = new Date(currentYear - (currentMonth < 9 ? 1 : 0), 9, 0);
                break;

            case '5': // Fourth Quarter
                startDate = new Date(currentYear - 1, 9, 1);
                endDate = new Date(currentYear, 0, 0);
                break;

            case '6': // First Semiannual
                startDate = new Date(currentYear - (currentMonth < 6 ? 1 : 0), 0, 1);
                endDate = new Date(currentYear - (currentMonth < 6 ? 1 : 0), 6, 0);
                break;

            case '7': // Second Semiannual
                startDate = new Date(currentYear - 1, 6, 1);
                endDate = new Date(currentYear, 0, 0);
                break;

            case '8': // Annual
                startDate = new Date(currentYear - 1, 0, 1);
                endDate = new Date(currentYear, 0, 0);
                break;

            default:
                return;
        }

        if (startDate && endDate) {
            reportingPeriodStart.value = FormatDate(startDate);
            reportingPeriodEnd.value = FormatDate(endDate);
            reportingPeriodStart.classList.remove('flashOutline');
            reportingPeriodEnd.classList.remove('flashOutline');
            // `offsetWidth` is only referenced here to trigger a layout reflow, so that adding back the class restarts the CSS animation.
            // See: https://css-tricks.com/restart-css-animation/#aa-update-another-javascript-method-to-restart-a-css-animation
            reportingPeriodStart.offsetWidth; //NOSONAR triggers reflow
            reportingPeriodEnd.offsetWidth; //NOSONAR triggers reflow
            reportingPeriodStart.classList.add('flashOutline');
            reportingPeriodEnd.classList.add('flashOutline');
        }
    });
});

function FormatDate(date) {
    return [
        date.getFullYear(),
        String(date.getMonth() + 1).padStart(2, '0'),
        String(date.getDate()).padStart(2, '0'),
    ].join('-');
}
