using FMS.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventEditDto
    {
        public EventEditDto() { }

        public EventEditDto(Event eventEntity)
        {
            Id = eventEntity.Id;
            Active = eventEntity.Active;
            FacilityId = eventEntity.FacilityId;
            ParentId = eventEntity.ParentId;
            EventTypeId = eventEntity.EventType?.Id;
            EventType = eventEntity.EventType;
            ActionTakenId = eventEntity.ActionTaken?.Id;
            ActionTaken = eventEntity.ActionTaken;
            StartDate = eventEntity.StartDate;
            DueDate = eventEntity.DueDate;
            CompletionDate = eventEntity.CompletionDate;
            ComplianceOfficerId = eventEntity.ComplianceOfficer?.Id;
            ComplianceOfficer = eventEntity.ComplianceOfficer;
            EventAmount = eventEntity.EventAmount;
            EventContractorId = eventEntity.EventContractor?.Id;
            EventContractor = eventEntity.EventContractor;
            Comment = eventEntity.Comment;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; } = true;

        [Required]
        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }
        public Event Parent { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public Guid? EventTypeId { get; set; }
        public EventType EventType { get; set; }

        [Required]
        [Display(Name = "Action Taken")]
        public Guid? ActionTakenId { get; set; }
        public ActionTaken ActionTaken { get; set; }

        [Display(Name = "Start Date")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "Due Date")]
        public DateOnly? DueDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [Required]
        [Display(Name = "Done By (CO)")]
        public Guid? ComplianceOfficerId { get; set; }
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [DisplayName("Event Amount")]
        [DataType(DataType.Currency)]
        public decimal? EventAmount { get; set; }

        [Display(Name = "Contractor")]
        public Guid? EventContractorId { get; set; }
        public EventContractor EventContractor { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}
