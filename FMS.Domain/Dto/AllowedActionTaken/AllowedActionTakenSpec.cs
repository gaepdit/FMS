using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class AllowedActionTakenSpec
    {
        public AllowedActionTakenSpec() { }

        public AllowedActionTakenSpec(AllowedActionTaken allowedActionTaken)
        {
            Id = allowedActionTaken.Id;
            Active = allowedActionTaken.Active;
            StartDateRequired = allowedActionTaken.StartDateRequired;
            DueDateRequired = allowedActionTaken.DueDateRequired;
            CompletionDateRequired = allowedActionTaken.CompletionDateRequired;
            EventTypeId = allowedActionTaken.EventType.Id;
            EventTypeName = allowedActionTaken.EventType.Name;
            EventTypeActive = allowedActionTaken.EventType.Active;
            ActionTakenId = allowedActionTaken.ActionTaken.Id;
            ActionTakenName = allowedActionTaken.ActionTaken.Name;
            ActionTakenActive = allowedActionTaken.ActionTaken.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Start Date Required")]
        public bool StartDateRequired { get; set; }

        [Display(Name = "Due Date Required")]
        public bool DueDateRequired { get; set; }

        [Display(Name = "Completion Date Required")]
        public bool CompletionDateRequired { get; set; }

        public Guid EventTypeId { get; set; }

        public string EventTypeName { get; set; }

        public bool EventTypeActive { get; set; }

        public Guid ActionTakenId { get; set; }

        public string ActionTakenName { get; set; } 

        public bool ActionTakenActive { get; set; }
    }
}
