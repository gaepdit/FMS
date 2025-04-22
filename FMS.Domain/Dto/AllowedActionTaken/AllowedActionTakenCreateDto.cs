using System;

namespace FMS.Domain.Dto
{
    public class AllowedActionTakenCreateDto
    {
        public Guid EventTypeId { get; set; }
        public Guid ActionTakenId { get; set; }
    }
}
