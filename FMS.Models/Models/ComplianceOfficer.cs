namespace FMS.Models.Models
{
    public class ComplianceOfficer : BaseActiveModel
    {
        public string Name { get; set; }

        public OrganizationalUnit Unit { get; set; }
    }
}
