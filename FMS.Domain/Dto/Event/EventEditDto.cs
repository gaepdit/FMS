using System;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class EventEditDto
    {
        public EventEditDto() { }

        public EventEditDto(EventSummaryDto eventEntity)
        {
            Id = eventEntity.Id;
            Active = eventEntity.Active;
            FacilityId = eventEntity.FacilityId;
            ParentId = eventEntity.ParentId;
            EventTypeId = eventEntity.EventType?.Id;
            ActionTakenId = eventEntity.ActionTaken?.Id;
            StartDate = eventEntity.StartDate;
            DueDate = eventEntity.DueDate;
            CompletionDate = eventEntity.CompletionDate;
            ComplianceOfficerId = eventEntity.ComplianceOfficer?.Id;
            EventAmount = eventEntity.EventAmount;
            EntityNameOrNumber = eventEntity.EntityNameOrNumber;
            Comment = eventEntity.Comment;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; } = true;

        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? EventTypeId { get; set; }

        public Guid? ActionTakenId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? CompletionDate { get; set; }

        public Guid? ComplianceOfficerId { get; set; }

        public decimal EventAmount { get; set; }

        public string EntityNameOrNumber { get; set; }

        public string Comment { get; set; }
    }
}
