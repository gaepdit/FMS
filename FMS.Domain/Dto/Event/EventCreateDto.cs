using FMS.Domain.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class EventCreateDto
    {
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
