using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class UserPositionSummaryDto
    {
        public UserPositionSummaryDto(UserPosition userPosition)
        {
            Id = userPosition.Id;
            Active = userPosition.Active;
            Name = userPosition.Name;
        }

        public Guid Id { get; }

        public bool Active { get; }

        public string Name { get; set; }
    }
}
