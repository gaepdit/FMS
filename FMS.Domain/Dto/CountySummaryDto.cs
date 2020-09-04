using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CountySummaryDto
    {
        public CountySummaryDto(County county)
        {
            Id = county.Id;
            Name = county.Name;
        }

        public int Id { get; set; }

        [StringLength(20)]
        [Display(Name = "County")]
        public string Name { get; set; }
    }
}
