using System;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ParcelSummaryDto
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid LocationId { get; set; }

        public string ParcelNumber { get; set; }

        public string ParcelDescription { get; set; }

        public Guid ParcelTypeId { get; set; }
        public ParcelType ParcelType { get; set; }

        public double Acres { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
