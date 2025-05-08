using System;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class EventSummaryDto
    {
        public EventSummaryDto() { }

        public EventSummaryDto(Event eventEntity)
        {
            Id = eventEntity.Id;
            Active = eventEntity.Active;
            ParentId = eventEntity.ParentId;
            EventTypeId = eventEntity.EventTypeId;
            ActionTakenId = eventEntity.ActionTakenId;
            StartDate = eventEntity.StartDate;
            DueDate = eventEntity.DueDate;
            CompletionDate = eventEntity.CompletionDate;
            ComplianceOfficerId = eventEntity.ComplianceOfficerId;
            EventAmount = eventEntity.EventAmount;
            EntityNameOrNumber = eventEntity.EntityNameOrNumber;
            Comment = eventEntity.Comment;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid? ParentId { get; set; }

        public Guid EventTypeId { get; set; }

        public Guid ActionTakenId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly DueDate { get; set; }

        public DateOnly CompletionDate { get; set; }

        public Guid ComplianceOfficerId { get; set; }

        public decimal EventAmount { get; set; }

        public string EntityNameOrNumber { get; set; }

        public string Comment { get; set; }
    }
}
