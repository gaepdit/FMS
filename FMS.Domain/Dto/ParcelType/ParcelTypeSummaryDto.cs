using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ParcelTypeSummaryDto
    {
        public ParcelTypeSummaryDto(ParcelType parcelType)
        {
            Name = parcelType.Name;
        }

        public Guid Id { get; }

        public bool Active { get; }

        public string Name { get; set; }
    }
}
