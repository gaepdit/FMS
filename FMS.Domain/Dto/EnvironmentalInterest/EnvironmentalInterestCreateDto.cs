using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EnvironmentalInterestCreateDto
    {
        public bool Active { get; set; }

        [Display(Name = "Environmental Interest")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
