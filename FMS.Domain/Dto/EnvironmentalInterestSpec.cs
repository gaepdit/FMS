using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EnvironmentalInterestSpec
    {
        public bool Active { get; set; }

        [Display(Name = "Environmental Interest")]
        public string Name { get; set; }

        //public Guid BudgetCodeId { get; set; }
    }
}
