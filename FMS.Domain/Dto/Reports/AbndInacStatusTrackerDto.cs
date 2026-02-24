using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto.Reports
{
    public class AbndInacStatusTrackerDto
    {
        public AbndInacStatusTrackerDto() { }

        public string HSINumber { get; set; }

        public string FacilityName { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        [Display(Name = "Review Checklist Completed - Date")]
        public DateOnly? CompletedDate { get; set; }
    }
}
