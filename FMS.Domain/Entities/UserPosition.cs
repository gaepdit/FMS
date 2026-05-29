using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Domain.Entities
{
    public class UserPosition : BaseActiveModel, INamedModel
    {
        public UserPosition() { }

        public UserPosition(UserPositionCreateDto userPosition)
        {
            Name = userPosition.Name;
            Description = userPosition.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
