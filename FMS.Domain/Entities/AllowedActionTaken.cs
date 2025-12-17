using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class AllowedActionTaken : BaseActiveModel
    {
        public AllowedActionTaken() { }

        public AllowedActionTaken(AllowedActionTaken allowedActionTaken)
        {
            Id = Guid.NewGuid();
            EventTypeId = allowedActionTaken.EventTypeId;
            EventType = allowedActionTaken.EventType;
            ActionTakenId = allowedActionTaken.ActionTakenId;
            ActionTaken = allowedActionTaken.ActionTaken;
            Active = allowedActionTaken.Active;
        }

        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; } 

        public Guid ActionTakenId { get; set; }
        public ActionTaken ActionTaken { get; set; }

        public bool StartDateRequired { get; set; }

        public bool DueDateRequired { get; set; }

        public bool CompletionDateRequired { get; set; }
    }
}
