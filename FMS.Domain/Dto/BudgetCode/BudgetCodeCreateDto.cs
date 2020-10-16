using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class BudgetCodeCreateDto
    {
        public bool Active { get; set; }

        [StringLength(20)]
        [Required]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Budget Code")]
        public string Name { get; set; }

        [StringLength(20)]
        public string OrganizationNumber { get; set; }

        [StringLength(20)]
        public string ProjectNumber { get; set; }

        public void TrimAll()
        {
            Code = Code?.Trim();
            Name = Name?.Trim();
            OrganizationNumber = OrganizationNumber?.Trim();
            ProjectNumber = ProjectNumber?.Trim();
        }
    }
}
