using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Parcel : BaseActiveModel
    {
        public Parcel() { }

        public Parcel(Guid id, ParcelCreateDto parcel)
        {
            FacilityId = id;
            ParcelNumber = parcel.ParcelNumber;
            Acres = parcel.Acres;
            ParcelTypeId = parcel.ParcelTypeId;
            ListDate = parcel.ListDate;
            DeListDate = parcel.DeListDate;
            SubListParcelName = parcel.SubListParcelName;
        }
        public Guid FacilityId { get; set; }

        public string ParcelNumber { get; set; }

        public double? Acres { get; set; }

        public Guid? ParcelTypeId { get; set; }
        public ParcelType ParcelType { get; set; }

        public DateOnly? ListDate { get; set; }

        public DateOnly? DeListDate { get; set; }

        public string SubListParcelName { get; set; }
    }
}
