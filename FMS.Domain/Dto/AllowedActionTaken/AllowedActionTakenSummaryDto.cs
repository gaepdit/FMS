using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class AllowedActionTakenSummaryDto
    {
        public AllowedActionTakenSummaryDto(AllowedActionTaken allowedActionTaken)
        {
            Id = allowedActionTaken.Id;
            Active = allowedActionTaken.Active;
            EventType = allowedActionTaken.EventType;
            ActionTaken = allowedActionTaken.ActionTaken;
            StartDateRequired = allowedActionTaken.StartDateRequired;
            DueDateRequired = allowedActionTaken.DueDateRequired;
            CompletionDateRequired = allowedActionTaken.CompletionDateRequired;
        }

        public Guid Id { get; }

        public bool Active { get; }

        public bool StartDateRequired { get; }

        public bool DueDateRequired { get; }

        public bool CompletionDateRequired { get; }

        public EventType EventType { get; }

        public ActionTaken ActionTaken { get; }
    }
}
