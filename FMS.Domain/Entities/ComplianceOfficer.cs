using FMS.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class ComplianceOfficer : BaseActiveModel
    {
        [Display(Name = "Compliance Officer")]
        public string Name { get; set; }

        //public Guid UnitId { get; set; }
        public  OrganizationalUnit Unit { get; set; }   //virtual
    }
}
