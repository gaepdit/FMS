using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Facility : BaseActiveModel
    {
        // Existing ID for Facility May be used by Programs - System Generated, but not a Guid
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        // File label and Cabinet where this Facility is located
        public File File { get; set; }

        // Environmental Interest/Program of this Facility
        public EnvironmentalInterest EnvironmentalInterest { get; set; }

        // Type of Facility
        public FacilityType FacilityType { get; set; }

        // Unit overseeing this Facility
        public OrganizationalUnit OrganizationalUnit { get; set; }

        // Program Budget for this Facility
        public BudgetCode BudgetCode { get; set; }

        // Facility Name
        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        // Compliance Officer assigned to this Facility
        public ComplianceOfficer ComplianceOfficer { get; set; }

        public FacilityStatus FacilityStatus { get; set; }

        // Location description distinct from mailing address
        [Display(Name = "Location Description")]
        public string Location { get; set; }

        // Facility Address
        [Display(Name = "Street Address")]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; } = "GA";

        [Display(Name = "Zip Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        // site Coordinates
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        // County for use to simplify queries, ease searching, etc.
        public int CountyId { get; set; }
        public virtual County County { get; set; }

        // List of retention records for this Facility File
        public List<RetentionRecord> RetentionRecords { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
