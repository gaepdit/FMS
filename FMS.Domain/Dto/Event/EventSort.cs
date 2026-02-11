namespace FMS.Domain.Dto
{
    public enum EventSort
    {
        EventType,
        EventTypeDesc,
        ActionTaken,
        ActionTakenDesc,
        StartDate,
        StartDateDesc,
        DueDate,
        DueDateDesc,
        CompletionDate,
        CompletionDateDesc,
        ComplianceOfficer,
        ComplianceOfficerDesc,
        EventAmount,
        EventAmountDesc,
        EventContractor,
        EventContractorDesc
    }

    public enum EventReportSort
    {
        EventPending,
        EventActivityCompleted,
        EventCompletedOutstanding,
        EventCompleted,
        EventCompliance,
        EventNoActionTaken
    }
}