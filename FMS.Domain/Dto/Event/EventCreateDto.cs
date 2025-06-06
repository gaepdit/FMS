using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
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
