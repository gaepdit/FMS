using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ParcelCreateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Active { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "Parcel Number")]
        public string ParcelNumber { get; set; }

        [Display(Name = "Acreage")]
        public double Acres { get; set; }

        [Display(Name = "Type")]
        public Guid ParcelTypeId { get; set; }
        public string ParcelTypeName { get; set; }

        [Display(Name = "List Date")]
        public DateOnly? ListDate { get; set; }

        [Display(Name = "De-List Date")]
        public DateOnly? DeListDate { get; set; }

        [Display(Name = "Sub-List Parcel Name")]
        public string SubListParcelName { get; set; }

        public void TrimAll()
        {
            ParcelNumber = ParcelNumber?.Trim();
            SubListParcelName = SubListParcelName?.Trim();
        }
    }
}
