using FMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        [Display(Name = "Facility Types")]
        public List<Guid> FacilityTypeId { get; set; }

        public IDictionary<string, string> AsRouteValues() => new Dictionary<string, string?>
        {
            { nameof(StartDate), StartDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
            { nameof(EndDate), EndDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
            { nameof(ComplianceOfficerId), ComplianceOfficerId != null ? string.Join(",", ComplianceOfficerId) : null },
            { nameof(OrganizationalUnitId), OrganizationalUnitId != null ? string.Join(",", OrganizationalUnitId) : null },
            { nameof(EventTypeId), EventTypeId != null ? string.Join(",", EventTypeId) : null },
            { nameof(FacilityTypeId), FacilityTypeId != null ? string.Join(",", FacilityTypeId) : null }
        };
    }
}
