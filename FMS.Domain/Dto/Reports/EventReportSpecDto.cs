using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Domain.Dto
{
    public class EventReportSpecDto
    {
        public EventReportSpecDto() { }

        [Display(Name = "Start Date")]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateOnly? EndDate { get; set; } = DateOnly.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));

        [Display(Name = "Organizational Units")]
        public List<Guid> OrganizationalUnitId { get; set; }

        [Display(Name = "Compliance Officers")]
        public List<Guid> ComplianceOfficerId { get; set; }

        [Display(Name = "Event Types")]
        public List<Guid> EventTypeId { get; set; }
    }
}
