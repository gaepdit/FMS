using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
        public EventSort SortBy { get; set; } = EventSort.StartDate;

        public bool FirstPass { get; set; } = true;

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
        public decimal? EventAmount { get; set; }

        [DisplayName("Contractor")]
        public EventContractor EventContractor { get; set; }

        [DisplayName("Comment")]
        public string Comment { get; set; }

        public IDictionary<string, string> AsRouteValues =>
           new Dictionary<string, string>
           {
                {nameof(Active), Active.ToString()},
                {nameof(FacilityId), FacilityId.ToString()},
                {nameof(ParentId), ParentId?.ToString()},
                {nameof(EventType), EventType.ToString()},
                {nameof(ActionTaken), ActionTaken.ToString()},
                {nameof(StartDate), StartDate?.ToString()},
                {nameof(DueDate), DueDate?.ToString()},
                {nameof(CompletionDate), CompletionDate?.ToString()},
                {nameof(ComplianceOfficer), ComplianceOfficer.ToString()},
                {nameof(EventAmount), EventAmount?.ToString()},
                {nameof(EventContractor), EventContractor.ToString()},
                {nameof(Comment), Comment},
                {nameof(SortBy), SortBy.ToString()},
                {nameof(FirstPass), FirstPass.ToString()},
           };
    }
}

               