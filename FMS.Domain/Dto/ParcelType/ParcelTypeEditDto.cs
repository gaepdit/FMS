using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ParcelTypeEditDto
    {
        public ParcelTypeEditDto() { }
        public ParcelTypeEditDto(ParcelType parcelType)
        {
            Id = parcelType.Id;
            Name = parcelType.Name;
            Active = parcelType.Active;
        }
        public Guid Id { get; set; }

        [Display(Name = "Parcel Type")]
        [Required(ErrorMessage = "Parcel Type Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Is Active")]
        public bool Active { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
