using FMS.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventSummaryDto
    {
        public EventSummaryDto(Event eventEntity)
        {
            Id = eventEntity.Id;
            Active = eventEntity.Active;
            FacilityId = eventEntity.FacilityId;
            ParentId = eventEntity.ParentId;
            EventType = eventEntity.EventType;
            ActionTaken = eventEntity.ActionTaken;
            StartDate = eventEntity.StartDate;
            DueDate = eventEntity.DueDate;
            CompletionDate = eventEntity.CompletionDate;
            ComplianceOfficer = eventEntity.ComplianceOfficer;
            EventAmount = eventEntity.EventAmount;
            EventContractor = eventEntity.EventContractor;
            Comment = eventEntity.Comment;
        }

        public Guid Id { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
        [DisplayName("Event Type")]
        public EventType EventType { get; set; }

        [Required]
        [DisplayName("Action Taken")]
        public ActionTaken ActionTaken { get; set; }

        [DisplayName("Start Date")]
        public DateOnly? StartDate { get; set; }

        [DisplayName("Due Date")]
        public DateOnly? DueDate { get; set; }

        [DisplayName("Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [Required]
        [DisplayName("Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [DisplayName("Event Amount")]
        [DataType(DataType.Currency)]
        public decimal? EventAmount { get; set; }

        [DisplayName("Contractor")]
        public EventContractor EventContractor { get; set; }

        [DisplayName("Comment")]
        public string Comment { get; set; }
    }
}

               