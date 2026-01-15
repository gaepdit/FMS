using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
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

        [Display(Name = "Event Type")]
        public Guid? EventTypeId { get; set; }
        public EventType EventType { get; set; }

        [Display(Name = "Action Taken")]
        public Guid? ActionTakenId { get; set; }
        public ActionTaken ActionTaken { get; set; }

        [Display(Name = "Start Date")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "Due Date")]
        public DateOnly? DueDate { get; set; }

        [Display(Name = "Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [Display(Name = "Done By (CO)")]
        public Guid? ComplianceOfficerId { get; set; }
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [Display(Name = "Event Amount")]
        [DataType(DataType.Currency)]
        public decimal? EventAmount { get; set; }

        [Display(Name = "Contractor")]
        public Guid? EventContractorId { get; set; }
        public EventContractor EventContractor { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}
