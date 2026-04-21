using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ParcelSummaryDto
    {
        public ParcelSummaryDto() { }

        public ParcelSummaryDto(ParcelSummaryDto parcelSummary)
        {
            Id = parcelSummary.Id;
            Active = parcelSummary.Active;
            FacilityId = parcelSummary.FacilityId;
            ParcelNumber = parcelSummary.ParcelNumber;
            Acres = parcelSummary.Acres;
            ParcelType = parcelSummary.ParcelType;
            ListDate = parcelSummary.ListDate;
            DeListDate = parcelSummary.DeListDate;
            SubListParcelName = parcelSummary.SubListParcelName;
        }

        public ParcelSummaryDto(Parcel parcel)
        {
            Id = parcel.Id;
            Active = parcel.Active;
            FacilityId = parcel.FacilityId;
            ParcelNumber = parcel.ParcelNumber;
            Acres = parcel.Acres;
            ParcelType = parcel.ParcelType;
            ListDate = parcel.ListDate;
            DeListDate = parcel.DeListDate;
            SubListParcelName = parcel.SubListParcelName;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        public string ParcelNumber { get; set; }

        public double? Acres { get; set; }

        public ParcelType ParcelType { get; set; }

        public DateOnly? ListDate { get; set; }

        public DateOnly? DeListDate { get; set; }

        public string SubListParcelName { get; set; }
    }
}
