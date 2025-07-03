using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class ParcelType : BaseActiveModel, INamedModel
    {
        public ParcelType() { }

        public ParcelType(ParcelTypeCreateDto parcelType)
        {
            Name = parcelType.Name;
        }

        public string Name { get; set; } 

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
