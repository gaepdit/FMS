using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ParcelEditDto
    {
        public ParcelEditDto() { }
        public ParcelEditDto(Parcel parcel)
        {
            Id = parcel.Id;
            Active = parcel.Active;
            FacilityId = parcel.FacilityId;
            ParcelNumber = parcel.ParcelNumber;
            Acres = parcel.Acres;
            ParcelTypeId = parcel.ParcelType?.Id;
            ListDate = parcel.ListDate;
            DeListDate = parcel.DeListDate;
            SubListParcelName = parcel.SubListParcelName;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Parcel Number")]
        public string ParcelNumber { get; set; }

        [Display(Name = "Acreage")]
        public double Acres { get; set; }

        [Display(Name = "Type")]
        public Guid? ParcelTypeId { get; set; }
        public ParcelType ParcelType { get; set; }

        [Display(Name = "List Date")]
        public DateOnly? ListDate { get; set; }

        [Display(Name = "De-List Date")]
        public DateOnly? DeListDate { get; set; }

        [Display(Name = "Sub-List Parcel Name")]
        public string SubListParcelName { get; set; }
    }

}
