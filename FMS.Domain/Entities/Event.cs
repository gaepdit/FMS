using FMS.Domain.Entities.Base;
using System;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class Event : BaseActiveModel
    {
        public Event() { }

        public Event(EventCreateDto eventDto)
        {
            ParentId = eventDto.ParentId;
            EventTypeId = eventDto.EventTypeId;
            ActionTakenId = eventDto.ActionTakenId;
            StartDate = eventDto.StartDate;
            DueDate = eventDto.DueDate;
            CompletionDate = eventDto.CompletionDate;
            ComplianceOfficerId = eventDto.ComplianceOfficerId;
            EventAmount = eventDto.EventAmount;
            EntityNameOrNumber = eventDto.EntityNameOrNumber;
            Comment = eventDto.Comment;
        }

        public Guid? ParentId { get; set; }

        public Guid EventTypeId { get; set; }
        public Guid EventType { get; set; }

        public Guid ActionTakenId { get; set; }
        public Guid ActionTaken { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly DueDate { get; set; }

        public DateOnly CompletionDate { get; set; }

        public Guid ComplianceOfficerId { get; set; }
        public string ComplianceOfficer { get; set; }

        public decimal EventAmount { get; set; }

        public string EntityNameOrNumber { get; set; }

        public string Comment { get; set; }
    }
}
