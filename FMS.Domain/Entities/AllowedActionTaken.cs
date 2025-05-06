using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class AllowedActionTaken : BaseActiveModel
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
