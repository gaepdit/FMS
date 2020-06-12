﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Models.Models
{
    public class Facility : BaseActiveModel
    {
        // Existing ID for Facility May be used by Programs - System Generated, but not a Guid
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
        public string Name { get; set; }

        // Compliance Officer assigned to this Facility
        public ComplianceOfficer ComplianceOfficer { get; set; }

        public FacilityStatus FacilityStatus { get; set; }

        // Location description distinct from mailing address
        public string Location { get; set; }

        // Facility Address
        [StringLength(50)]
        public string StreetLine1 { get; set; }

        [StringLength(50)]
        public string StreetLine2 { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; } = "GA";

        [StringLength(10)]
        public int PostalCode { get; set; }

        // site Coordinates
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // County for use to simplify queries, ease searching, etc.
        public County County { get; set; }

        // List of retention records for this Facility File
        public List<RetentionRecord> RetentionRecords { get; set; }
    }
}
