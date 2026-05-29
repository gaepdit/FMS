using FMS.Domain.Entities;
using System.ComponentModel;

namespace FMS.Domain.Dto
{
    public class UserPositionSummaryDto
    {
        public UserPositionSummaryDto(UserPosition userPosition)
        {
            Id = userPosition.Id;
            Active = userPosition.Active;
            Name = userPosition.Name;
            Description = userPosition.Description;
        }

        public Guid Id { get; }

        public bool Active { get; }

        [DisplayName("User Position")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
