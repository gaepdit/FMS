using System;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class AllowedActionTaken
    {
        public AllowedActionTaken() { }
        public AllowedActionTaken(AllowedActionTakenCreateDto allowedActionTaken)
        {
            EventTypeId = allowedActionTaken.EventTypeId;
            ActionTakenId = allowedActionTaken.ActionTakenId;
        }
        public Guid EventTypeId { get; set; }
        public Guid ActionTakenId { get; set; }
    }
}
