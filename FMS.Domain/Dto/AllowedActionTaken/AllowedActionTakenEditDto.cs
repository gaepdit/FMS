using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AllowedActionTakenEditDto
    {
        public AllowedActionTakenEditDto()
        {
            // Required for EditAllowedActionTaken page
        }

        public AllowedActionTakenEditDto(AllowedActionTaken allowedActionTaken, bool eventTypeActive, bool actionTakenActive)
        {
            Active = allowedActionTaken.Active;
            EventTypeId = allowedActionTaken.EventTypeId;
            ActionTakenId = allowedActionTaken.ActionTakenId;
            EventTypeActive = eventTypeActive;
            ActionTakenActive = actionTakenActive;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid EventTypeId { get; set; }

        public bool EventTypeActive { get; set; }

        public Guid ActionTakenId { get; set; }

        public bool ActionTakenActive { get; set; }
    }
}
