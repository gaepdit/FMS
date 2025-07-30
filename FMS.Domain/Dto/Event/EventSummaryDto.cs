using System;
using System.ComponentModel;
using FMS.Domain.Entities;

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
            EntityNameOrNumber = eventEntity.EntityNameOrNumber;
            Comment = eventEntity.Comment;
        }

        public Guid Id { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }

        [DisplayName("Event Type")]
        public EventType EventType { get; set; }

        [DisplayName("Action Taken")]
        public ActionTaken ActionTaken { get; set; }

        [DisplayName("Start Date")]
        public DateOnly? StartDate { get; set; }

        [DisplayName("Due Date")]
        public DateOnly? DueDate { get; set; }

        [DisplayName("Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [DisplayName("Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [DisplayName("Event Amount")]
        public decimal EventAmount { get; set; }

        [DisplayName("Entity Name or Number")]
        public string EntityNameOrNumber { get; set; }

        [DisplayName("Comment")]
        public string Comment { get; set; }
    }
}
