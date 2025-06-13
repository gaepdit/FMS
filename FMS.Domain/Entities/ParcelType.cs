using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class ParcelType : BaseActiveModel, INamedModel
    {
        public ParcelType() { }
        public ParcelType(ParcelTypeCreateDto ParcelType)
        {
            Name = ParcelType.Name;
        }

        public string Name { get; set; } 
    }
}
