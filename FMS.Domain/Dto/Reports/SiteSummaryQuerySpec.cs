namespace FMS.Domain.Dto
{
    public class SiteSummaryQuerySpec
    {
        public SiteSummaryQuerySpec() { }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "County")]
        public int? CountyId { get; set; }

        [Display(Name = "Compliance Officer")]
        public Guid? ComplianceOfficerId { get; set; }

        [Display(Name = "Class")]
        public Guid? LocationClassId { get; set; }

        [Display(Name = "Organizational Unit")]
        public Guid? OrganizationalUnitId { get; set; }

        [Display(Name = "Add'l Organizational Unit")]
        public Guid? AdditionalOrganizationalUnitId { get; set; }

        [Display(Name = "Print Only Landfills?")]
        public bool IsLandFill { get; set; } = false;

        [Display(Name = "Print all Counties in Batches?")]
        public bool AllCounties { get; set; } = false;

        [Display(Name = "Print All COs in Batches?")]
        public bool AllCOs { get; set; } = false;

        [Display(Name = "Print All Class IV Sites?")]
        public bool AllClassIVs { get; set; } = false;

        [Display(Name = "Print All Org Units in Batches?")]
        public bool AllOrgUnits { get; set; } = false;

        [Display(Name = "Show Header at top of reports")]
        public bool ShowHeader { get; set; } = false;

        public IDictionary<string, string> AsRouteValues() => new Dictionary<string, string?>
        {
            { nameof(FacilityNumber), FacilityNumber },
            { nameof(CountyId), CountyId?.ToString() },
            { nameof(ComplianceOfficerId), ComplianceOfficerId?.ToString() },
            { nameof(LocationClassId), LocationClassId?.ToString() },
            { nameof(OrganizationalUnitId), OrganizationalUnitId?.ToString() },
            { nameof(AdditionalOrganizationalUnitId), AdditionalOrganizationalUnitId?.ToString() },
            { nameof(IsLandFill), IsLandFill.ToString() },
            { nameof(AllCounties), AllCounties.ToString() },
            { nameof(AllCOs), AllCOs.ToString() },
            { nameof(AllClassIVs), AllClassIVs.ToString() },
            { nameof(AllOrgUnits), AllOrgUnits.ToString() },
            { nameof(ShowHeader), ShowHeader.ToString() }
        };

        public void TrimAll()
        {
            FacilityNumber = FacilityNumber?.Trim();
        }
    }
}
