using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ComplianceOfficerCreateDto
    {
        public bool Active { get; set; }

        [Display(Name = "Compliance Officer")]
        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }
    }
}