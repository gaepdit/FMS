using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OrganizationalUnitSpec
    {
        public bool Active { get; set; }

        [Display(Name = "Organizational Unit")]
        public string Name { get; set; }

    }
}
