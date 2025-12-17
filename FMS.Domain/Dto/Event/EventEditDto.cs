using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

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
        public Guid? EventTypeId { get; set; }
        public EventType EventType { get; set; }

        [Required]
        public Guid? ActionTakenId { get; set; }
        public ActionTaken ActionTaken { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? CompletionDate { get; set; }

        [Required]
        [Display(Name = "Done By (CO)")]
        public Guid? ComplianceOfficerId { get; set; }
        public ComplianceOfficer ComplianceOfficer { get; set; }

        public decimal? EventAmount { get; set; }

        [Display(Name = "Contractor")]
        public Guid? EventContractorId { get; set; }
        public EventContractor EventContractor { get; set; }

        public string Comment { get; set; }
    }
}
