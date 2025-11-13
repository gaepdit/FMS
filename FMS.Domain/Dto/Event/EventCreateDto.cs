using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }

        public Guid? ParentId { get; set; }
        public Event Parent { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public Guid EventTypeId { get; set; }
        
        [Required]
        [Display(Name = "Action Taken")]
        public Guid ActionTakenId { get; set; }

        [Display(Name = "Start Date")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "Due Date")]
        public DateOnly? DueDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [Required]
        [Display(Name = "Done By (CO)")]
        public Guid ComplianceOfficerId { get; set; }
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [Display(Name = "Event Amount")]
        public decimal? EventAmount { get; set; }

        [Display(Name = "Contractor")]
        public Guid? EventContractorId { get; set; }
        public EventContractor EventContractor { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}
