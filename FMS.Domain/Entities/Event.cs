using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Event : BaseActiveModel
    {
        public Event() { }

        public Event(EventCreateDto eventDto)
        {
            FacilityId = eventDto.FacilityId;
            ParentId = eventDto.ParentId;
            EventTypeId = eventDto.EventTypeId;
            ActionTakenId = eventDto.ActionTakenId;
            StartDate = eventDto.StartDate;
            DueDate = eventDto.DueDate;
            CompletionDate = eventDto.CompletionDate;
            ComplianceOfficerId = eventDto.ComplianceOfficerId;
            EventAmount = eventDto.EventAmount;
            EventContractorId = eventDto.EventContractorId;
            Comment = eventDto.Comment;
        }

        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? EventTypeId { get; set; }
        public EventType EventType { get; set; }
        
        public Guid? ActionTakenId { get; set; }
        public ActionTaken ActionTaken { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? CompletionDate { get; set; }

        public Guid? ComplianceOfficerId { get; set; }
        public ComplianceOfficer ComplianceOfficer { get; set; }

        public decimal? EventAmount { get; set; }

        public Guid? EventContractorId { get; set; }
        public EventContractor EventContractor { get; set; }

        public string Comment { get; set; }
    }
}
