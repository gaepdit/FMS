using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Domain.Dto
{
    public class DashboardUserEventsDto
    {
        public DashboardUserEventsDto() { }

        public DashboardUserEventsDto(Event facilityEvent)
        {
            Id = facilityEvent.Id;
            FacilityId = facilityEvent.FacilityId;
            FacilityNumber = facilityEvent.Facility.FacilityNumber;
            EventType = facilityEvent.EventType;
            ActionTaken = facilityEvent.ActionTaken;
            StartDate = facilityEvent.StartDate;
            DueDate = facilityEvent.DueDate;
            CompletionDate = facilityEvent.CompletionDate;
            ComplianceOfficer = facilityEvent.ComplianceOfficer;
            Comment = facilityEvent.Comment?.Substring(0, Math.Min(facilityEvent.Comment.Length, 100));
            Active = facilityEvent.Active;
        }

        public Guid Id { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Event Type")]
        public EventType EventType { get; }

        [Display(Name = "Action Taken")]
        public ActionTaken ActionTaken { get; set; }

        [Display(Name = "Start Date")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "Due Date")]
        public DateOnly? DueDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [Display(Name = "Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public bool Active { get; set; }
    }
}
